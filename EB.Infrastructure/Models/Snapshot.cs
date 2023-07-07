using System.Text.Json.Serialization;

namespace EB.Infrastructure.Models;

public class Snapshot
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string SnapshotId { get; set; }
    public double Temperature { get; set; }
    public double Energy { get; set; }
    public int Leakage { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } // = null!;
}