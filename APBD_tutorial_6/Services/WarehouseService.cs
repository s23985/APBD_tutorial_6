using APBD_tutorial_6.Models;
using APBD_tutorial_6.Repositories;

namespace APBD_tutorial_6.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductWarehouseRepository _productWarehouseRepository;

    public WarehouseService(IProductRepository productRepository, IWarehouseRepository warehouseRepository, IOrderRepository orderRepository, IProductWarehouseRepository productWarehouseRepository)
    {
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
        _productWarehouseRepository = productWarehouseRepository;
    }

    public int AddProductToWarehouse(ProductRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0.");
        }

        if (!_productRepository.ProductExists(request.IdProduct))
        {
            throw new InvalidOperationException("Product not found.");
        }

        if (!_warehouseRepository.WarehouseExists(request.IdWarehouse))
        {
            throw new InvalidOperationException("Warehouse not found.");
        }

        if (!_orderRepository.OrderExists(request.IdProduct, request.Amount, request.CreatedAt))
        {
            throw new InvalidOperationException("No matching order found.");
        }

        if (_orderRepository.OrderCompleted(request.IdProduct, request.Amount, request.CreatedAt))
        {
            throw new InvalidOperationException("Order already completed.");
        }

        var orderId = _orderRepository.GetOrderId(request.IdProduct, request.Amount, request.CreatedAt);

        _orderRepository.UpdateOrderFulfilledAt(request.IdProduct, request.Amount, request.CreatedAt);

        var price = _productRepository.GetProductPrice(request.IdProduct) * request.Amount;
        return _productWarehouseRepository.InsertProductWarehouse(request.IdProduct, request.IdWarehouse, orderId.Value, request.Amount, price, DateTime.Now);
    }
}