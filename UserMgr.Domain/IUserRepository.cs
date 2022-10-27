using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain;

public interface IUserRepository
{
    public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
    public Task<User?> FindOneAsync(Guid UserId);
    public Task AddNewLoginHistoryAsync(PhoneNumber number,string message);
    public Task SavePhoneNumberCodeAsync(PhoneNumber number, string code);
    public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber number);

    Task PublishEventAsync(UserAccessResultEvent _event);

}