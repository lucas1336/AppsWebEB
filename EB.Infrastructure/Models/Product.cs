using System.Text.Json.Serialization;

namespace EB.Infrastructure.Models;

public class Product
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int MonitoringLevel { get; set; }
    [JsonIgnore]
    public List<Snapshot> Snapshots { get; set; }
}