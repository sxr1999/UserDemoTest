namespace UserMgr.Domain;

public record UserAccessFail
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; set; }
    private bool lockOut;
    public DateTime? LockoutEnd { get; private set; }
    public int AccessFailedCount { get; private set; }

    private UserAccessFail()
    {
    }

    public UserAccessFail(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }

    //账号重制取消锁定
    public void Reset()
    {
        lockOut = false;
        LockoutEnd = null;
        AccessFailedCount = 0;
    }

    //登录失败
    public void Fail()
    {
        AccessFailedCount++;
        if (AccessFailedCount>=3)
        {
            lockOut = true;
            LockoutEnd = DateTime.Now.AddMinutes(5);
        }
    }

    public bool IsLockOut()
    {
        if (lockOut)
        {
            if (LockoutEnd>=DateTime.Now)
            {
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }
        else
        {
            return false;
        }
    }
};