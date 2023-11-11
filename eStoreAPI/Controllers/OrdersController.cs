using BusinessObject.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get()
        {
            return _orderRepository.GetOrders();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<OrderDTO?> Get(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO o)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.AddOrder(o);

                return NoContent();
            }
            return BadRequest();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDTO o)
        {
            var tempOrder = _orderRepository.GetOrderById(id);
            if (tempOrder == null)
            {
                return NotFound();
            }
            //_orderRepository.UpdateOrder(o);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tempOrder = _orderRepository.GetOrderById(id);
            if (tempOrder == null)
            {
                return NotFound();
            }
            //bool result = _orderRepository.DeleteOrder(tempOrder);
            //if (result)
            //{
            //    return NoContent();
            //}
            //else
            //{
            //    return BadRequest();
            //}
            return NoContent();
        }
    }
}
