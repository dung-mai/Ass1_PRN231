using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        MyDBContext _context;
        public OrderDetailDAO(MyDBContext context)
        {
            _context = context;
        }

        public bool Add(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
