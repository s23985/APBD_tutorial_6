namespace APBD_tutorial_6.Repositories;

public interface IProductRepository
{
    bool ProductExists(int productId);
    decimal GetProductPrice(int productId);
}