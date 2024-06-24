namespace APBD_tutorial_6.Repositories;

public interface IProductWarehouseRepository
{
    int InsertProductWarehouse(int productId, int warehouseId, int orderId, int amount, decimal price, DateTime createdAt);
}