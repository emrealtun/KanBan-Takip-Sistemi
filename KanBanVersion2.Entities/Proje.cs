using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{       [Table("Proje")]
        public class Proje:MyEntityBase
    {
        private int value;

        [DisplayName("Proje Adı"),Required(ErrorMessage ="{0} alanı gereklidir"),StringLength(100,ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır.")]
        public string projeAd { get; set; }
        [DisplayName("Proje Açıklaması"), Required(ErrorMessage = "{0} alanı gereklidir")]
        public string projeAciklamasi { get; set; }

        public virtual List<Todo> Todo { get; set; }
        public virtual List<ProjeKullanicisi> projeKullanicisi { get; set; }

        public Proje()
        {
           Todo = new List<Todo>();
            projeKullanicisi = new List<ProjeKullanicisi>();
        }

        public Proje(int value)
        {
            this.value = value;
        }
    }
}
