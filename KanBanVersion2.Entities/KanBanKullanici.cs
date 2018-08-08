using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.Entities
{       [Table("KanBanKullanici")]

    public class  KanBanKullanici:MyEntityBase
    {
        [DisplayName("Ad"), StringLength(30, ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır")]
        public string ad { get; set; }

        [DisplayName("Soyad"),StringLength(30, ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır")]
        public string soyad { get; set; }

        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı gereklidir") ,StringLength(30,ErrorMessage ="{0} alanı maksimum {1} karakter olmalıdır")]
        public string kullaniciadi { get; set; }

        [DisplayName("E-mail"), Required(ErrorMessage = "{0} alanı gereklidir"), StringLength(50, ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır")]
        public string email { get; set; }

        [DisplayName("Şifre") ,Required(ErrorMessage = "{0} alanı gereklidir"), StringLength(30, ErrorMessage = "{0} alanı maksimum {1} karakter olmalıdır")]
        public string sifre { get; set; }

        [DisplayName("Aktiflik Durumu")]
        public bool aktifDurum { get; set; }

        [Required,ScaffoldColumn(false)]
        public Guid aktiflikGuid { get; set; }

        [DisplayName("Adminlik Durumu")]
        public bool adminDurum{ get; set; }

        public Proje secilenProje { get; set; }

        public virtual List<TodoKullanicisi> todoKullanicisi { get; set; }
        public virtual List<Yorum> Yorum { get; set; }
        public virtual List<ProjeKullanicisi> projeKullanicisi  { get; set; }


    }
}
