using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace TheDojoLeague.Models
{
    public abstract class BaseEntityNinja {}
    public class Ninja : BaseEntityNinja
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Level { get; set; }

        public string AssignedDojo { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int Dojo_Id { get; set; }    // the foreign key value for the associated team (will be saved in db)

        public Dojo dojo { get; set; }      // a given player will be associated with a specific Team
        // while the Player model isn't saved with a whole team object in the db, 
        // having this object makes it much easier to interact with the Player instance in the rest of our code
    }
}