namespace UserMgr.WebApi;

[AttributeUsage(AttributeTargets.Method)]
public class UnitOfWorkAttribute:Attribute
{
    public Type[] _dbContextTypes { get; init; }

    public UnitOfWorkAttribute(params Type[] dbContextTypes)
    {
        _dbContextTypes = dbContextTypes;
    }
}