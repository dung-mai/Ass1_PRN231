using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private MyDBContext _context;

        public MemberDAO(MyDBContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            if (member != null)
            {
                _context.Members.Add(new Member
                {
                    City = member.City,
                    Country = member.Country,
                    CompanyName = member.CompanyName,
                    Email = member.Email,
                    Password = member.Password
                });
            }
        }

        public void UpdateMember(Member member)
        {
            if (member != null)
            {
                Member? m = GetMemberById(member.MemberId);
                if (m != null)
                {
                    m.City = member.City;
                    m.Country = member.Country;
                    m.CompanyName = member.CompanyName;
                    m.Email = member.Email;
                    m.Password = member.Password;
                }
            }
        }

        public void DeleteMember(Member member)
        {
            if (member != null)
            {
                Member? m = GetMemberById(member.MemberId);
                if (m != null)
                {
                    _context.Members.Remove(m);
                }
            }
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Member? GetMemberById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.MemberId == id);
        }

        public Member? GetMemberByEmail(string? email)
        {
            return _context.Members.FirstOrDefault(m => m.Email.Equals(email));
        }
    }
}
