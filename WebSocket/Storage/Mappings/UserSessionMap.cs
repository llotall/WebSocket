using FluentNHibernate.Mapping;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Mappings
{
    public class UserSessionMap : ClassMap<UserSession>
    {
        public UserSessionMap()
        {
            Table("usersession");

            Id(x => x.Id, "Id");

            Map(x => x.Token, "Token");
            Map(x => x.EnterDate, "EnterDate");

            References(x => x.User, "IdUser").Cascade.All();
        }
    }
}
