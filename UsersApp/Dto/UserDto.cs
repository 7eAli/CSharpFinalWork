﻿using UsersApp.Db;

namespace UsersApp.Dto
{
    public class UserDto
    {        
        public string Email { get; set; }
        public string Password { get; set; }        
        public RoleType RoleId { get; set; }
    }
}
