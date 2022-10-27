using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain;

public class UserDomainService
{
    private readonly IUserRepository _userRepository;
    private readonly ISmsCodeSender _smsCodeSender;

    public UserDomainService(IUserRepository userRepository,ISmsCodeSender smsCodeSender)
    {
        _userRepository = userRepository;
        _smsCodeSender = smsCodeSender;
    }

    public void RestAccessFail(User user)
    {
        user._userAccessFail.Reset();
    }

    public bool IsLockOut(User user)
    {
        return user._userAccessFail.IsLockOut();
    }

    public void Fail(User user)
    {
        user._userAccessFail.Fail();
    }


    public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber number, string Password)
    {
        UserAccessResult result;
        User? user = await _userRepository.FindOneAsync(number);
        //用户不存在
        if (user==null)
        {
            result = UserAccessResult.PhoneNumberNotFound;
        }
        //用户被锁定 
        else if (IsLockOut(user))
        {
            result = UserAccessResult.LockOut;
        }
        //没设置密码
        else if (user.HasPassword())
        {
            result = UserAccessResult.NoPassword;
        }
        //密码正确
        else if (user.CheckPassword(Password))
        {
            result = UserAccessResult.Ok;
        }
        //密码错误
        else
        {
            result = UserAccessResult.PasswordError;
        }

        if (user!=null)
        {
            if (result == UserAccessResult.Ok)
            {
                this.RestAccessFail(user);
            }
            else
            {
                this.Fail(user);
            }
        }

        await _userRepository.PublishEventAsync(new UserAccessResultEvent(number, result));
        return result;
    }

    public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber number, string code)
    {
        User? user = await _userRepository.FindOneAsync(number);
        CheckCodeResult result;
        if (user==null)
        {
            return  CheckCodeResult.PhoneNumberNotFound;
        }
        if (IsLockOut(user))
        {
            return CheckCodeResult.LockOut;
        }

        string? codeInServer = await _userRepository.FindPhoneNumberCodeAsync(number);
        if (codeInServer==null)
        {
            Fail(user);
            return CheckCodeResult.CodeError;
        }

        if (code == codeInServer)
        {
            return CheckCodeResult.Ok;
        }
        else
        {
            Fail(user);
            return CheckCodeResult.CodeError;
        }
    }
}