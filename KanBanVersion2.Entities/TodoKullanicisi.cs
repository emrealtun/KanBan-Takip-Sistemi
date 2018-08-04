using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{   
    [Table("TodoKullanicisi")]
    public class TodoKullanicisi
    {   
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public virtual Todo todo { get; set; }
        public virtual KanBanKullanici kanbankullanici{ get; set; }


    }
}
