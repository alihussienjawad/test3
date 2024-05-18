
using System.Collections.Generic;

namespace SiliconAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

       public List<Contact> contacts { get; set; }

    }
}
