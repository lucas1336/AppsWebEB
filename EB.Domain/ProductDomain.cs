using AutoMapper;
using EB.API.Input;
using EB.Domain.Exceptions;
using EB.Domain.Interface;
using EB.Infrastructure.Interface;
using EB.Infrastructure.Models;

namespace EB.Domain;

public class ProductDomain : IProductDomain
{
    private IProductInfrastructure _productInfrastructure;
    private IMapper _mapper;

    public ProductDomain(IProductInfrastructure productInfrastructure, IMapper mapper)
    {
        _productInfrastructure = productInfrastructure;
        _mapper = mapper;
    }

    public List<Product> GetAll()
    {
        return _productInfrastructure.GetAll();
    }

    public ProductResponse GetById(int id)
    {
        ExistsById(id);
        Product product = _productInfrastructure.GetById(id);
        return _mapper.Map<Product, ProductResponse>(product);
    }

    public Product Create(ProductInput product)
    {
        ExistsBySerialNumber(product.SerialNumber);
        ValidateProduct(product);
        Product newProduct = _mapper.Map<ProductInput, Product>(product);
        return _productInfrastructure.Create(newProduct);
    }
    
    private void ExistsById(int id)
    {
        if (!_productInfrastructure.ExistsById(id))
        {
            throw new ResourceNotFoundException("Product does not exist");
        }
    }
    
    private void ExistsBySerialNumber(string serialNumber)
    {
        if (_productInfrastructure.ExistsBySerialNumber(serialNumber))
        {
            throw new Exception("Product already exists");
        }
    }

    private void ValidateProduct(ProductInput product)
    {
        if (product.SerialNumber == null)
            throw new ValidationException("El nombre del producto es obligatorio");
        if (product.Brand == null)
            throw new ValidationException("La marca del producto es obligatoria");
        if (product.Model == null)
            throw new ValidationException("El modelo del producto es obligatorio");
    }
}