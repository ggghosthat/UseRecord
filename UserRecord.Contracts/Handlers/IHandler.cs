
namespace UserRecord.Contracts.Handlers;

public interface IHandler : IDisposable
{
    public void Create();
    public void Dispose();
}