using BusinessObject.Models;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IMemberRepository
    {
        public void AddMember(MemberDTO member);
        public void UpdateMember(MemberDTO member);
        public bool DeleteMember(MemberDTO member);
        public MemberDTO? GetMemberById(int id);
        public List<MemberDTO> GetAllMembers();
        public bool Login(string username, string password);
    }
}
