using UserMgr.Domain;
using UserMgr.Domain.ValueObject;

namespace UserMgr.infrastracture;

public class MockSmsCodeSender : ISmsCodeSender
{
    public Task SendAsync(PhoneNumber number, string code)
    {
        Console.WriteLine($"向{number.Number}-{number.RegionNumber}发送验证码:{code}");
        return Task.CompletedTask;
    }
}