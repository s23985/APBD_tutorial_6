using APBD_tutorial_6.Models;

namespace APBD_tutorial_6.Services;

public interface IWarehouseService
{
    int AddProductToWarehouse(ProductRequest request);
}