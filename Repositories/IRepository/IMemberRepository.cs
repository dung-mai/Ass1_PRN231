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
        public void AddMember(MemberResponseDTO member);
        public void UpdateMember(MemberResponseDTO member);
        public bool DeleteMember(MemberResponseDTO member);
        public MemberResponseDTO? GetMemberById(int id);
        public List<MemberResponseDTO> GetAllMembers();
        public MemberResponseDTO? Login(string username, string password);
    }
}
