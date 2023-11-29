using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        public User Authenticate(User users);
    }
}
