using EB.Infrastructure.Context;
using EB.Infrastructure.Interface;
using EB.Infrastructure.Models;

namespace EB.Infrastructure;

public class ProductInfrastructure : IProductInfrastructure
{
    private EBContext _context;
    
    public ProductInfrastructure(EBContext context)
    {
        _context = context;
    }
    
    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product GetById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public bool ExistsById(int id)
    {
        return _context.Products.Any(p => p.Id == id);
    }

    public bool ExistsBySerialNumber(string serialNumber)
    {
        return _context.Products.Any(p => p.SerialNumber == serialNumber);
    }
}