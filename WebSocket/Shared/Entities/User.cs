using Shared.Entities.JsonModels;

namespace Shared.Entities
{
    public class User : PersistentObject
    {
        public virtual string Name { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public static implicit operator User(UserRegJsonModel user)
        {
            return new User
            {
                Name = user.Name,
                Login = user.Login,
                Password = user.Password
            };
        }
    }
}
