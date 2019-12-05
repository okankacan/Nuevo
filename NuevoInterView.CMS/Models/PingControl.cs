using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Models
{
    [Table("PingControls", Schema = "dbo")]

    public class PingControl: PersistentEntity
    {
       

        public string Name { get; set; }

        public string Url { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
