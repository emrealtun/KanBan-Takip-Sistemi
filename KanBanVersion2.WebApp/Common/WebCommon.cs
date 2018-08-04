using KanBanVersion2.Common;
using KanBanVersion2.Entities;
using KanBanVersion2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanBanVersion2.WebApp.Common
{
    public class WebCommon : ICommon
    {
        public string GetKullaniciAdi()
        {
            KanBanKullanici kullanici = CurrentSession.kullanici;
            if (kullanici != null)
                return kullanici.kullaniciadi;
            else
                return "system";
        }
    }
}