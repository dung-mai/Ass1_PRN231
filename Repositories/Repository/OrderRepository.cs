using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        MyDBContext _context;
        IMapper _mapper;

        public OrderRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddOrder(OrderDTO order)
        {
            OrderDAO orderDAO = new(_context);
            bool result = orderDAO.AddOrder(_mapper.Map<Order>(order));
            if (!result) return result;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public int GetLastInsertOrderId()
        {
            OrderDAO orderDAO = new(_context);
            return orderDAO.GetLastInsertOrderId();
        }

        public OrderDTO? GetOrderById(int orderId)
        {
            OrderDAO orderDAO = new(_context);
            Order? order = orderDAO.GetOrderById(orderId);
            return order != null ? _mapper.Map<OrderDTO>(order) : null;
        }

        public List<OrderDTO> GetOrders()
        {
            OrderDAO orderDAO = new(_context);
            return orderDAO.GetOrders().Select(o => _mapper.Map<OrderDTO>(o)).ToList();
        }
    }
}
