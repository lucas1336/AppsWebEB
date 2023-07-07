using EB.Infrastructure.Models;

namespace EB.Infrastructure.Interface;

public interface ISnapshotInfrastructure
{
    List<Snapshot> GetAllByProductId(int id);
    public Snapshot Create(Snapshot snapshot);
}