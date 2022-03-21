using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<string> AuthenticateAsync(AuthenticateRequest model);
        Task<AppUsers> GetByIdAsync(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test1", Password = "test1" }
        };

        private readonly AppSettings _appSettings;
        private readonly IAppRepository _repo;

        public UserService(IOptions<AppSettings> appSettings, IAppRepository repo)
        {
            _appSettings = appSettings.Value;
            _repo = repo;
        }

        public async Task<string> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _repo.FindUserByUsernameAndPasswordAsync(model.Username, model.Password); //_users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return await token;
        }

        //public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        //{
        //    var user = await _repo.FindUserByUsernameAndPasswordAsync(model.Username, model.Password); //_users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        //    // return null if user not found
        //    if (user == null) return null;

        //    // authentication successful so generate jwt token
        //    var token = generateJwtToken(user);

        //    return new AuthenticateResponse(user, token);
        //}

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> generateJwtToken(AppUsers user)
        {
            // generate token that is valid for 1 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var authClaims = new List<Claim> {
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                };
            var userRoles = await _repo.GetRolesByUserIdAsync(user.UserId);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Rolename));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AppUsers> GetByIdAsync(int id)
        {
            var db = new AppDbContext();
            return await db.AppUsers.Where(x => x.UserId == id).SingleOrDefaultAsync();
        }
    }
}