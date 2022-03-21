using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IAssessmentService
    {
        Task<AppAppraisals> CreateNewAssessmentAsync(AppAppraisals model);
        Task<int> CreateNewGoalsAsync(IEnumerable<AppGoals> model);
        Task<IEnumerable<AppAppraisals>> GetAppraisalsByUsername(string username);
        Task<AppAppraisals> GetAppraisalByIdAysnc(int id);
        Task<int> UpdateAppraisalStatusAsync(int id, string status);
    }
    public class AssessmentService : IAssessmentService
    {
        private readonly IAppRepository _repo;
        public AssessmentService(IAppRepository repo)
        {
            _repo = repo;
        }
        public async Task<AppAppraisals> CreateNewAssessmentAsync(AppAppraisals model)
        {
            try
            {
                return await _repo.CreateNewAppraisal(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateNewGoalsAsync(IEnumerable<AppGoals> model)
        {
            try
            {
                return await _repo.CreateNewGoalsAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AppAppraisals> GetAppraisalByIdAysnc(int id)
        {
            try
            {
                return await _repo.GetAppraisalByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<AppAppraisals>> GetAppraisalsByUsername(string username)
        {
            try
            {
                return await _repo.GetAppraisalsByUsername(username);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> UpdateAppraisalStatusAsync(int id, string status)
        {
            if (id == 0) return 0;
            var appraisal = await _repo.GetAppraisalByIdAsync(id);
            appraisal.Status = status;
            return await _repo.UpdateAppraisalAsync(appraisal);
        }
    }
}
