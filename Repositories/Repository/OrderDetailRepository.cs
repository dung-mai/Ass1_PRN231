using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using BusinessObject.DTO;

namespace Repositories.IRepository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        MyDBContext _context;
        IMapper _mapper;

        public OrderDetailRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Add(OrderDetailDTO orderDetailDTO)
        {
            OrderDetailDAO orderDetailManager = new(_context);
            bool result = orderDetailManager.Add(_mapper.Map<OrderDetail>(orderDetailDTO));
            if (!result) return result;

            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
