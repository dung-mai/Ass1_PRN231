using DataAccess.DAO;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IOrderRepository
    {
        List<OrderDTO> GetOrders();
        OrderDTO? GetOrderById(int orderId);
        bool AddOrder(OrderDTO order);
        int GetLastInsertOrderId();
    }
}
