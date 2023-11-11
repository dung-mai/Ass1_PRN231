using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        MyDBContext _context;

        public OrderDAO(MyDBContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders(int StartIndex, int Size)
        {
            return _context.Orders
                .Include(order => order.Member)
                .Skip(StartIndex - 1)
                .Take(Size)
                .ToList();
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(order => order.Member)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.Include(order => order.Member).ToList();
        }

        public int DeleteOrder(int OrderId)
        {
            Order? order = _context.Orders
                .Include(order => order.Member)
                .FirstOrDefault(o => o.OrderId == OrderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                return 1;
            }
            return 0;
        }

        public bool AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetLastInsertOrderId()
        {
            Order? order = _context.Orders.OrderBy(o => o.OrderId).LastOrDefault();
            return order != null ? order.OrderId : 0;
        }
    }
}
