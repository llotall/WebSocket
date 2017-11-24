namespace Shared.Entities
{
    public class User : PersistentObject
    {
        public virtual string Name { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }
    }
}
