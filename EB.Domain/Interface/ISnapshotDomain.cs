using EB.API.Input;
using EB.Infrastructure.Models;

namespace EB.Domain.Interface;

public interface ISnapshotDomain
{
    public Snapshot Create(SnapshotInput snapshot, int productId);
    public List<Snapshot> GetSnapshotsByProductId(int id);
}