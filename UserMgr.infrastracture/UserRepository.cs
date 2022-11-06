using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using UserMgr.Domain;
using UserMgr.Domain.ValueObject;

namespace UserMgr.infrastracture;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;
    private readonly IDistributedCache _distributedCache;
    private readonly IMediator _mediator;

    public UserRepository(UserDbContext dbContext,IDistributedCache distributedCache,IMediator mediator)
    {
        _dbContext = dbContext;
        _distributedCache = distributedCache;
        _mediator = mediator;
    }
    public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
    {
        return await  _dbContext.Users.Include(x=>x._userAccessFail).SingleOrDefaultAsync(x =>
            x.phoneNumber.Number == phoneNumber.Number 
            && x.phoneNumber.RegionNumber == phoneNumber.RegionNumber);
        //return Task.FromResult(result);
    }

    public async Task<User?> FindOneAsync(Guid UserId)
    {
        return await  _dbContext.Users.Include(x=>x._userAccessFail).SingleOrDefaultAsync(x=>x.Id==UserId);
    }

    public async Task AddNewLoginHistoryAsync(PhoneNumber number, string message)
    {
        User? user =await FindOneAsync(number);
        Guid? userId = null;
        if (user!=null)
        {
            userId = user.Id;
        }

        _dbContext.UserLoginHistories.Add(new UserLoginHistory(userId, number, message));
    }

    public Task SavePhoneNumberCodeAsync(PhoneNumber number, string code)
    {
        string key = $"PhoneNumberCode_{number.RegionNumber}";
        
        //将数据保存到分布式缓存中,有效时间为5分钟
        return _distributedCache.SetStringAsync(key,code,new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
    }

    public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber number)
    {
        var code = await _distributedCache.GetStringAsync($"PhoneNumberCode_{number.RegionNumber}");
        _distributedCache.Remove($"PhoneNumberCode_{number.RegionNumber}");
        return code;
    }

    public Task PublishEventAsync(UserAccessResultEvent _event)
    {
       return _mediator.Publish(_event);
    }

    public async  Task AddNew(LoginByPhone model)
    {
        User user = new User(model.PhoneNumber);
        user.ChangePassword(model.PassWord);
        await _dbContext.Users.AddAsync(user);
        
    }
}