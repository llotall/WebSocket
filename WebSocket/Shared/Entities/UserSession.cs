using System;

namespace Shared.Entities
{
    public class UserSession
    {
        public virtual DateTime EnterDate { get; set; }

        public virtual string Token { get; set; }

        public virtual User User { get; set; }
    }
}
