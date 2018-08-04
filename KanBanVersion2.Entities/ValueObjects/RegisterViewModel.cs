using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KanBanVersion2.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} maksimum {1} karakter olmalıdır.")]
        public string kullaniciAdi { get; set; }

        [DisplayName("Email"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(70, ErrorMessage = "{0} maksimum {1} karakter olmalıdır."),
            EmailAddress(ErrorMessage ="{0} alanı için lütfen geçerli bir e-posta adresi giriniz.")]
        public string eMail{ get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} maksimum {1} karakter olmalıdır.")]
        public string sifre { get; set; }

        [DisplayName("Şifre Onay"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} maksimum {1} karakter olmalıdır."),
            Compare("sifre", ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string sifreTekrar { get; set; }

    }
}