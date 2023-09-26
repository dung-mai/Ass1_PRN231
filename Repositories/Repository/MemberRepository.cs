using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using BusinessObject.DTO;
using Repositories.IRepository;

namespace Repositories.Repository
{
    public class MemberRepository : IMemberRepository
    {
        MyDBContext _context;
        private readonly IMapper _mapper;

        public MemberRepository(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMember(MemberDTO memberBO)
        {
            MemberDAO memberDAO = new(_context);
            var member = _mapper.Map<Member>(memberBO);
            memberDAO.AddMember(member);
            _context.SaveChanges();
        }

        public bool DeleteMember(MemberDTO memberBO)
        {
            MemberDAO memberDAO = new(_context);
            try
            {
                var member = _mapper.Map<Member>(memberBO);
                memberDAO.DeleteMember(member);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MemberDTO> GetAllMembers()
        {
            MemberDAO memberDAO = new(_context);
            return memberDAO.GetAllMembers().Select(p => _mapper.Map<MemberDTO>(p)).ToList();
        }

        public MemberDTO? GetMemberById(int id)
        {
            MemberDAO memberDAO = new(_context);
            return _mapper.Map<MemberDTO>(memberDAO.GetMemberById(id));
        }

        public void UpdateMember(MemberDTO memberBO)
        {
            MemberDAO memberDAO = new(_context);
            var member = _mapper.Map<Member>(memberBO);
            memberDAO.UpdateMember(member);
            _context.SaveChanges();
        }

        public bool Login(string email, string password)
        {
            MemberDAO memberDAO = new(_context);
            Member? member = memberDAO.GetMemberByEmail(email);
            if (member == null)
            {
                return false;
            }
            else
            {
                if (!member.Password.Equals(password))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
