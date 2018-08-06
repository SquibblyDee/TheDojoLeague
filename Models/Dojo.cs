using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TheDojoLeague.Models
{
    public abstract class BaseEntityDojo {}
    public class Dojo : BaseEntityDojo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [MinLength(10)]
        public string Information { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Ninja> ninjas { get; set; }    // a given team may have many players

    	public Dojo() {
            ninjas = new List<Ninja>();    
        }
    }
}