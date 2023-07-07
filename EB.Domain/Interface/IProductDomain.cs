using EB.API.Input;
using EB.Infrastructure.Models;

namespace EB.Domain.Interface;

public interface IProductDomain
{
    public List<Product> GetAll();
    public ProductResponse GetById(int id);
    public Product Create(ProductInput product);
}