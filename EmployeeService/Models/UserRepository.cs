using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.Models
{
    public class UserRepository
    {
        private static List<User> _userList = new List<User>
 {
  new User{ Id = 1, Username = "admin", Password = "123"},
  new User{ Id = 2, Username = "quangle", Password = "123"}
 };

        public bool Validate(string username, string password)
        {
            var user = _userList.FirstOrDefault(t => t.Username == username);
            if (user == null)
                return false;
            if (user.Password == password) return true;
            return false;
        }

    }
}