using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain;

//此实体与User为两个独立的聚合，聚合之间引用应避免直接引用实体，应当引用标志符（主键）
public record UserLoginHistory : IAggregateRoot
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public PhoneNumber PhoneNumber { get; init; }
    public DateTime CreateDateTime { get; init; }
    public string Message { get; init; }

    private UserLoginHistory()
    {
        
    }

    public UserLoginHistory(Guid? userId,PhoneNumber phoneNumber,string message)
    {
        Id = Guid.NewGuid();
        this.UserId = userId;
        this.PhoneNumber = phoneNumber;
        this.CreateDateTime = DateTime.Now;
        this.Message = message;
    }
}