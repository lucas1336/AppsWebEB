namespace EB.API.Input;

public class SnapshotInput
{
    public string SerialNumber { get; set; }
    public string SnapshotId { get; set; }
    public double Temperature { get; set; }
    public double Energy { get; set; }
    public int Leakage { get; set; }
}