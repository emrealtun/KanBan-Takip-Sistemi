using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanBanVersion2.Entities.ValueObjects
{ 
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage="{0} alanı boş geçilemez."),StringLength(25,ErrorMessage = "{0} maksimum {1} karakter olmalıdır.")]
        public string kullaniciAdi { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."),DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} maksimum {1} karakter olmalıdır.")]
        public string sifre { get; set; }

    }
}