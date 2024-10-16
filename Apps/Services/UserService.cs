using Apps.Data.Entities;
using Apps.Data.Models;
using Apps.Repositories.Interfaces;
using Apps.Services.Interfaces;
using Apps.Utilities;

namespace Apps.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<UserEntityResponse>> FindAll(UserEntityQuery queryParams)
        {
            var (lUser, _) =  await _userRepo.FindAll(queryParams);
        
            List<UserEntityResponse> list = new List<UserEntityResponse>();

            foreach (var todo in lUser)
            {
                UserEntityResponse teResponse = _Mapper.MapTo<User, UserEntityResponse>(todo);    
                list.Add(teResponse);
            }

            return list;
        }

        public async Task<UserEntityResponse?> FindById(Guid id)
        {
            User? user = await _userRepo.FindById(id);

            if (user == null)
            {
                return null;
            }

            UserEntityResponse res = _Mapper.MapTo<User, UserEntityResponse>(user);

            return res;
        }

        public async Task<UserEntityResponse?> Store(UserEntityBody body)
        {
            User item = _Mapper.MapTo<UserEntityBody, User>(body);
            
            if (item.Password != null)
            {
                item.Password = _BCrypt.Hash(item.Password);
            }

            User? user = await _userRepo.Store(item);

            if (user == null)
            {
                return null;
            }

            UserEntityResponse res = _Mapper.MapTo<User, UserEntityResponse>(user); 
            return res;
        }

        public async Task<bool?> Update(Guid id, UserEntityBodyUpdate body)
        {
            var user = await _userRepo.FindById(id);

            if (user == null)
            {
                return null;
            }

            user.Name = body.Name;
            user.Username = body.Username;
            user.Email = body.Email;

            return await _userRepo.Update(user);
        }

        public async Task<bool?> Destroy(Guid id)
        {
            var user = await _userRepo.FindById(id);

            if (user == null)
            {
                return null;
            }

            return await _userRepo.Destroy(user);
        }
    }
}