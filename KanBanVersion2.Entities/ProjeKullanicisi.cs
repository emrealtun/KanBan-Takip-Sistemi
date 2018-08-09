using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{     [Table("ProjeKullanicisi")]
     public class ProjeKullanicisi
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public virtual Proje proje { get; set; }

        [DisplayName("Proje Kullanıcısı")]
        public virtual KanBanKullanici kanbankullanici { get; set; }

    }
}
