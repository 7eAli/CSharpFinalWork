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

        public string AddUser(UserDto userDto)
        {
            using (_dbContext)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == userDto.Email);
                if (user == null)
                {
                    if (userDto.RoleId == RoleType.Admin)
                    {
                        var c = _dbContext.Users.Count(x => x.RoleId == userDto.RoleId);
                        if (c > 0)
                        {
                            throw new Exception("Администратор уже есть");
                        }
                    }

                    user = _mapper.Map<User>(userDto);

                    _dbContext.Add(user);
                    _dbContext.SaveChanges();

                    return user.Id.ToString();
                }
                else
                {
                    throw new Exception("Такой пользователь уже есть");
                }
            }            
        }

        public string DeleteUser(string email)
        {
            using (_dbContext)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    throw new Exception("Такого пользователя нет");
                }
                else if (user.RoleId == RoleType.Admin)
                {
                    throw new Exception("Администратора удалить нельзя");
                }

                var resId = user.Id.ToString();

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return resId;
            }
        }

        public string GetUserId(string email)
        {
            using (_dbContext)
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    throw new Exception("Такого пользователя нет");
                }
                return user.Id.ToString();
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

        public IEnumerable<UserDto> GetUsers()
        {
            using (_dbContext)
            {
                var users = _dbContext.Users.Select(_mapper.Map<UserDto>).ToList();
                return users;
            }
        }
    }
}
