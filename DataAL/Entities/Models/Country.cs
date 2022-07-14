

namespace DataAL.Entities.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<User> Users { get; set; }
        public int Active { get; set; } = 1;
    }
}
