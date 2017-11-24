using BusinessLogic.Interfaces.Base.CRUD;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Interfaces;

namespace BusinessLogic.Services.Base.CRUD
{
    public class UserService : BaseCrudService<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }
    }
}
