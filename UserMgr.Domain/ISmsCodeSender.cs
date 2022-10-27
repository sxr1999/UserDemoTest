using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain;

public interface ISmsCodeSender
{
    Task SendAsync(PhoneNumber number, string code);
}