using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public ActionResult<IEnumerable<MemberDTO>> Get()
        {
            return _memberRepository.GetAllMembers();
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public ActionResult<MemberDTO?> Get(int id)
        {
            return _memberRepository.GetMemberById(id);
        }

        // POST api/<MembersController>
        [HttpPost]
        public IActionResult Post([FromBody] MemberDTO m)
        {
            if (ModelState.IsValid)
            {
                _memberRepository.AddMember(m);

                return NoContent();
            }
            return BadRequest();
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MemberDTO m)
        {
            var tempMember = _memberRepository.GetMemberById(id);
            if (tempMember == null)
            {
                return NotFound();
            }
            _memberRepository.UpdateMember(m);
            return NoContent();
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tempMember = _memberRepository.GetMemberById(id);
            if (tempMember == null)
            {
                return NotFound();
            }
            bool result = _memberRepository.DeleteMember(tempMember);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
