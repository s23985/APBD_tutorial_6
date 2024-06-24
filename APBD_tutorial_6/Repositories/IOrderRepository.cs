using APBD_tutorial_6.Models;

namespace APBD_tutorial_6.Repositories;

public interface IOrderRepository
{
    bool OrderExists(int productId, int amount, DateTime createdAt);
    bool OrderCompleted(int productId, int amount, DateTime createdAt);
    void UpdateOrderFulfilledAt(int productId, int amount, DateTime createdAt);
    int? GetOrderId(int productId, int amount, DateTime createdAt);
}