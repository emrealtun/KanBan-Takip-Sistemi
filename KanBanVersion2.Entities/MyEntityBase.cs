using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{
    public class MyEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Oluşturulma Tarihi"), Required]
        public DateTime olusturmaTarihi { get; set; }
        [DisplayName("Güncelleme Tarihi"), Required]
        public DateTime guncellemeTarihi { get; set; }
        /// <summary>
        /// /
        /// </summary>
        [DisplayName("Düzenleyen"),Required, StringLength(30)]
        public string duzenleyen { get; set; }
    }
}
