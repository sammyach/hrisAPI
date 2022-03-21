using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Services
{
    public interface IMockUpService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    }
    public class MockUpService : IMockUpService
    {
        private readonly IAppRepository _repo;
        public MockUpService(IAppRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _repo.GetAllDepartmentsAsync();
        }
    }
}
