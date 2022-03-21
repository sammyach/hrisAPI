using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Data
{    
    public interface IAppRepository
    {
        Task<AppUsers> FindUserByUsernameAndPasswordAsync(string username, string password);
        Task<IEnumerable<AppUserRoles>> GetRolesByUserIdAsync(int id);
        Task<int> UpdateUser(AppUsers model);

        Task<Staff> FindStaffByUsername(string username);

        Task<IEnumerable<Department>> GetAllDepartmentsAsync();

        Task<AppAppraisals> CreateNewAppraisal(AppAppraisals model);
        Task<int> CreateNewGoalsAsync(IEnumerable<AppGoals> model);
        Task<IEnumerable<AppAppraisals>> GetAppraisalsByUsername(string username);
        Task<AppAppraisals> GetAppraisalByIdAsync(int id);
        Task<int> UpdateAppraisalAsync(AppAppraisals model);
    }
    public class AppRepository : IAppRepository
    {
        public async Task<AppAppraisals> CreateNewAppraisal(AppAppraisals model)
        {
            var db = new AppDbContext();
            db.AppAppraisals.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public async Task<int> CreateNewGoalsAsync(IEnumerable<AppGoals> model)
        {
            var db = new AppDbContext();
            db.AppGoals.AddRange(model);
            return await db.SaveChangesAsync();
        }

        public async Task<Staff> FindStaffByUsername(string username)
        {
            var db = new AppDbContext();
            return await db.Staff.Where(x => x.Username == username).SingleOrDefaultAsync();
        }

        public async Task<AppUsers> FindUserByUsernameAndPasswordAsync(string username, string password)
        {
            var db = new AppDbContext();
            return await db.AppUsers.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var db = new AppDbContext();
            return await db.Department.ToListAsync();
        }

        public async Task<AppAppraisals> GetAppraisalByIdAsync(int id)
        {
            var db = new AppDbContext();
            return await db.AppAppraisals.Where(x => x.AppraisalId == id).Include(g => g.AppGoals).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AppAppraisals>> GetAppraisalsByUsername(string username)
        {
            var db = new AppDbContext();
            return await db.AppAppraisals.Where(x => x.CreatedBy == username).ToListAsync();
        }

        public async Task<IEnumerable<AppUserRoles>> GetRolesByUserIdAsync(int id)
        {
            var db = new AppDbContext();
            return await db.AppUserRoles.Where(x => x.Userid == id).Include(r => r.Role).ToListAsync();
        }

        public async Task<int> UpdateAppraisalAsync(AppAppraisals model)
        {
            var db = new AppDbContext();
            db.AppAppraisals.Update(model);
            return await db.SaveChangesAsync();
        }

        public async Task<int> UpdateUser(AppUsers model)
        {
            var db = new AppDbContext();
            db.AppUsers.Update(model);
            return await db.SaveChangesAsync();
        }
    }
}
