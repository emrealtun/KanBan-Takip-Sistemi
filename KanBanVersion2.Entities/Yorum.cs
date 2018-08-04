using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{    [Table("Yorum")]
    public class Yorum: MyEntityBase
    {
        [Required,StringLength(1000)]
        public string icerik { get; set; }

        public virtual Todo todo { get; set; }
        public virtual KanBanKullanici kullanici { get; set; }

    }
}
