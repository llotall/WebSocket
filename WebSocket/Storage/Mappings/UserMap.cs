using FluentNHibernate.Mapping;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("user");

            Id(x => x.Id, "Id");

            Map(x => x.Login, "Login");
            Map(x => x.Name, "Name");
            Map(x => x.Password, "Password");
        }
    }
}
