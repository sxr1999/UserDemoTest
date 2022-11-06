using UserMgr.Domain.ValueObject;
using Zack.Commons;

namespace UserMgr.Domain;

public record User
{
    public Guid Id { get; init; }
    public PhoneNumber phoneNumber { get; private set; }
    private string? passwordHash;
    public UserAccessFail _userAccessFail { get; private set; }

    private User()
    {
    }

    public User(PhoneNumber phoneNumber)
    {
        this.phoneNumber = phoneNumber;
        this.Id = Guid.NewGuid();
        this._userAccessFail = new UserAccessFail(this);
    }

    public bool HasPassword()
    {
        return string.IsNullOrEmpty(passwordHash);
    }

    public void ChangePassword(string value)
    {
        if (value.Length<=3)
        {
            throw new ArgumentOutOfRangeException("密码长度必须大于3");
        }

        this.passwordHash = HashHelper.ComputeMd5Hash(value);
    }

    public bool CheckPassword(string value)
    {
        return this.passwordHash == HashHelper.ComputeMd5Hash(value);
    }

    public void ChangePhoneNumber(PhoneNumber number )
    {
        this.phoneNumber = number;
    }
    
}