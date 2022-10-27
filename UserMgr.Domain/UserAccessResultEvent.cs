using MediatR;
using UserMgr.Domain.ValueObject;

namespace UserMgr.Domain;

public record UserAccessResultEvent(PhoneNumber Number, UserAccessResult Result) : INotification; 