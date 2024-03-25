using AutoMapper;
using UsersApp.Db;
using UsersApp.Dto;

namespace UsersApp.Repo
{
    public class UserRepository : IUserRepository
    {
        private IMapper _mapper;
        private AppDbContext _dbContext;

        public UserRepository(IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void AddUser(UserDto userDto)
        {
            using (_dbContext)
            {
                if (userDto.RoleId == RoleType.Admin)
                {
                    var c = _dbContext.Users.Count(x => x.RoleId == userDto.RoleId);
                    if (c > 0)
                    {
                        throw new Exception("Администратор уже есть");
                    }
                }
                
                var user = _mapper.Map<User>(userDto);

                _dbContext.Add(user);
                _dbContext.SaveChanges();
            }
        }

        public Guid GetUserId(string email)
        {
            using (_dbContext)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    throw new Exception("Такого пользователя нет");
                }
                return user.Id;
            }
        }

        public RoleType GetUserRole(string email, string password)
        {
            using (_dbContext)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (user == null)
                {
                    throw new Exception("Неправильный email или пароль");
                }
                return user.RoleId;
            }
        }
    }
}
