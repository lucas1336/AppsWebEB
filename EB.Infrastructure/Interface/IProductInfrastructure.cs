using EB.Infrastructure.Models;

namespace EB.Infrastructure.Interface;

public interface IProductInfrastructure
{
    public List<Product> GetAll();
    public Product GetById(int id);
    public Product Create(Product product);
    public bool ExistsById(int id);
    public bool ExistsBySerialNumber(string serialNumber);
}