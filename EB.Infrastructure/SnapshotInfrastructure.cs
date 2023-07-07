using EB.Infrastructure.Context;
using EB.Infrastructure.Interface;
using EB.Infrastructure.Models;

namespace EB.Infrastructure;

public class SnapshotInfrastructure : ISnapshotInfrastructure
{
    private EBContext _context;
    
    public SnapshotInfrastructure(EBContext context)
    {
        _context = context;
    }

    public List<Snapshot> GetAllByProductId(int id)
    {
        return _context.Snapshots.Where(s => s.Product.Id == id).ToList();
    }

    public Snapshot Create(Snapshot snapshot)
    {
        _context.Snapshots.Add(snapshot);
        _context.SaveChanges();
        return snapshot;
    }
}