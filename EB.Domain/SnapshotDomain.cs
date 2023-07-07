using AutoMapper;
using EB.API.Input;
using EB.Domain.Exceptions;
using EB.Domain.Interface;
using EB.Infrastructure.Interface;
using EB.Infrastructure.Models;

namespace EB.Domain;

public class SnapshotDomain : ISnapshotDomain
{
    private ISnapshotInfrastructure _snapshotInfrastructure;
    private IProductInfrastructure _productInfrastructure;
    private IMapper _mapper;

    public SnapshotDomain(ISnapshotInfrastructure snapshotInfrastructure, IMapper mapper, IProductInfrastructure productInfrastructure)
    {
        _snapshotInfrastructure = snapshotInfrastructure;
        _mapper = mapper;
        _productInfrastructure = productInfrastructure;
    }

    public Snapshot Create(SnapshotInput snapshot, int ProductId)
    {
        Snapshot snapshotModel = _mapper.Map<SnapshotInput, Snapshot>(snapshot);
        snapshotModel.Product = _productInfrastructure.GetById(ProductId);
        ValidateAdvancedProduct(snapshotModel, snapshotModel.Product.MonitoringLevel == 1);
        ValidateSnapshot(snapshotModel);
        return _snapshotInfrastructure.Create(snapshotModel);
    }

    public List<Snapshot> GetSnapshotsByProductId(int id)
    {
        ExistsProductById(id);
        return _snapshotInfrastructure.GetAllByProductId(id);
    }
    
    private void ExistsProductById(int id)
    {
        var product = _productInfrastructure.GetById(id);
        if (product == null)
        {
            throw new ResourceNotFoundException("Product not found");
        }
    }

    private void ValidateSnapshot(Snapshot snapshot)
    {
        if (snapshot.SerialNumber == null)
            throw new ValidationException("SerialNumber is required");
        if (snapshot.SnapshotId == null)
            throw new ValidationException("SnapshotId is required");
        if (snapshot.Temperature < -271)
            throw new ValidationException("Temperature must be greater than or equal to -271");
        if (snapshot.Energy < 0)
            throw new ValidationException("Energy must be greater than or equal to 0");
        if (snapshot.Leakage != 0 && snapshot.Leakage != 1)
            throw new ValidationException("Leakage must be 0 or 1");
    }
    
    private void ValidateAdvancedProduct(Snapshot snapshot, bool isAdvanced)
    {
        if (snapshot.Energy != 0 && !isAdvanced)
            throw new ValidationException("Energy must be 0");
        if (snapshot.Leakage!= 0 && !isAdvanced)
            throw new ValidationException("Leakage must be 0");
    }
}