using Apps.Data.Entities;
using Apps.Data.Models;
using Apps.Repositories.Interfaces;
using Apps.Services.Interfaces;
using Apps.Utilities._BCrypt;
using Apps.Utilities._Mapper;

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
                UserEntityResponse teResponse = _Mapper.Map<User, UserEntityResponse>(todo);    
                list.Add(teResponse);
            }

            return list;
        }

        public async Task<UserEntityResponse?> FindById(Ulid id)
        {
            User? user = await _userRepo.FindById(id);

            if (user == null)
            {
                return null;
            }

            UserEntityResponse res = _Mapper.Map<User, UserEntityResponse>(user);

            return res;
        }

        public async Task<UserEntityResponse?> Store(UserEntityBody body)
        {
            User item = _Mapper.Map<UserEntityBody, User>(body);
            
            if (item.Password != null)
            {
                item.Password = _BCrypt.Hash(item.Password);
            }

            User? user = await _userRepo.Store(item);

            if (user == null)
            {
                return null;
            }

            UserEntityResponse res = _Mapper.Map<User, UserEntityResponse>(user); 
            return res;
        }

        public async Task<bool?> Update(Ulid id, UserEntityBodyUpdate body)
        {
            var user = await _userRepo.FindById(id);

            if (user == null)
            {
                return null;
            }

            _Mapper.MapTo<UserEntityBodyUpdate, User>(body, ref user);

            // user.Name = body.Name ?? user.Name;
            // user.Username = body.Username ?? user.Username;
            // user.Email = body.Email ?? user.Email;

            return await _userRepo.Update(user);
        }

        public async Task<bool?> Destroy(Ulid id)
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