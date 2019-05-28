using Core;

namespace Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Token { get; set; }
    }
}
