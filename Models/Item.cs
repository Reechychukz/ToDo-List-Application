using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_List.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Task")]
        [Required]
        public string Items { get; set; }

        [DisplayName("Details")]
        public string Info { get; set; }
    }
}
