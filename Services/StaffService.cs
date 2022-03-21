using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Services
{
    public interface IStaffService
    {
        Task<Staff> FindStaffByUsernameAsync(string username);
    }
    public class StaffService : IStaffService
    {
        private readonly IAppRepository _repo;
        public StaffService(IAppRepository repo) {
            _repo = repo;
        }
        public async Task<Staff> FindStaffByUsernameAsync(string username)
        {
            return await _repo.FindStaffByUsername(username);
        }
    }
}
