using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanBanVersion2.Entities.ValueObjects;
using KanBanVersion2.Entities;
using KanBanVersion2.DataAccessLayer.EntityFramework;
using KanBanVersion2.Entities.Mesajlar;
using KanBanVersion2.Common.Helpers;
using System.Net.Mail;
using KanBanVersion2.Commouden.Helpers;
using KanBanVersion2.BusinessLayer.Results;
using KanBanVersion2.BusinessLayer.Abstract;

namespace KanBanVersion2.BusinessLayer
{
    public class KullaniciManager : ManagerBase<KanBanKullanici>
    {
        public BusinessLayerResult<KanBanKullanici> RegisterUser(RegisterViewModel data)
        {
            KanBanKullanici kullanici = Find(x => x.kullaniciadi == data.kullaniciAdi || x.email == data.eMail);
            BusinessLayerResult<KanBanKullanici> layerResult = new BusinessLayerResult<KanBanKullanici>();


            if (kullanici != null)
            {
                if (kullanici.kullaniciadi == data.kullaniciAdi)
                {
                    layerResult.HataEkle(HataMesajiKodlari.KullaniciAdiKullanildi, "Kullanıcı adı kayıtlı");
                }
                if (kullanici.email == data.eMail)
                {
                    layerResult.HataEkle(HataMesajiKodlari.EmailKullanildi, "E mail adresi kayıtlı");
                }

            }
            else
            {
                int dbresult = base.Insert(new KanBanKullanici()
                {
                    kullaniciadi = data.kullaniciAdi,
                    email = data.eMail,
                    sifre = data.sifre,
                    aktiflikGuid = Guid.NewGuid(),
                    aktifDurum = false,
                    adminDurum = false,

                });
                if (dbresult > 0)
                {
                    layerResult.Result = Find(x => x.email == data.eMail && x.kullaniciadi == data.kullaniciAdi);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string aktiveUri = $"{siteUri}/Home/KullaniciAktivasyon/{ layerResult.Result.aktiflikGuid}";
                    string body = $"Merhaba {layerResult.Result.kullaniciadi};hesabınızı aktifleştirmekk için <a href='{aktiveUri}' target ='_blank'>tıklayınız</a>.";
                    MailHelper.SendMail(body, layerResult.Result.email, "Kanban İş Takip Sistemi Hesap Aktifleştirme");
                }
            }

            return layerResult;
        }

        public BusinessLayerResult<KanBanKullanici> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<KanBanKullanici> layerResult = new BusinessLayerResult<KanBanKullanici>();
            layerResult.Result = Find(x => x.kullaniciadi == data.kullaniciAdi && x.sifre == data.sifre);

            if (layerResult.Result != null)
            {
                if (!layerResult.Result.aktifDurum)
                {
                    layerResult.HataEkle(HataMesajiKodlari.KullaniciAktifDegil, "Kullanıcı aktif değildir.");
                    layerResult.HataEkle(HataMesajiKodlari.EmailKontrol, "Lütfen e posta adresinizi kontrol ediniz");
                }
            }
            else
            {
                layerResult.HataEkle(HataMesajiKodlari.KullaniciAdiVeyaSifreYanlis, "Kullanıcı adı veya şifre uyuşmuyor");
            }

            return layerResult;
        }

        public BusinessLayerResult<KanBanKullanici> RemoveUserById(int id)
        {
            BusinessLayerResult < KanBanKullanici > res = new BusinessLayerResult<KanBanKullanici>();
            KanBanKullanici kullanici = Find(x => x.Id == id);

            if(kullanici != null)
            { 
                if(Delete(kullanici)==0)
                {
                    res.HataEkle(HataMesajiKodlari.KullaniciSilinemedi, "Kullanıcı Silinemedi");
                    return res;
                }
            }
            else
            {
                res.HataEkle(HataMesajiKodlari.KullaniciBulunamadi, "Kullanıcı Bulunamadı");
            }

            return res;
        }

        public BusinessLayerResult<KanBanKullanici> Activateuser(Guid aktiflikGuid)
        {
            BusinessLayerResult<KanBanKullanici> layerResult = new BusinessLayerResult<KanBanKullanici>();
            layerResult.Result = Find(x => x.aktiflikGuid == aktiflikGuid);

            if (layerResult.Result != null)
            {
                if (layerResult.Result.aktifDurum)
                {
                    layerResult.HataEkle(HataMesajiKodlari.KullaniciAktifEdilmisti, "Kullanıcı zaten aktif edilmiş");
                    return layerResult;
                }

                layerResult.Result.aktifDurum = true;
                Update(layerResult.Result);
            }
            else
            {
                layerResult.HataEkle(HataMesajiKodlari.AktiflestirilecekKullaniciYok, "Aktifleştirilecek kullanıcı bulunamadı");
            }
            return layerResult;

        }

        public BusinessLayerResult<KanBanKullanici> GetUserById (int id)
        {
            BusinessLayerResult<KanBanKullanici> res = new BusinessLayerResult<KanBanKullanici>();
            res.Result = Find(x => x.Id == id);

            if(res.Result == null)
            {
                res.HataEkle(HataMesajiKodlari.KullaniciBulunamadi, "Kullanıcı bulunamadı");
            }

            return res;
        }

        public BusinessLayerResult<KanBanKullanici> UpdateProfil(KanBanKullanici data)
        {
            KanBanKullanici db_kk = Find(x =>  x.Id != data.Id && ( x.kullaniciadi == data.kullaniciadi || x.email == data.email));
            BusinessLayerResult<KanBanKullanici> res = new BusinessLayerResult<KanBanKullanici>();

            if(db_kk != null && db_kk.Id != data.Id)
            { 
                if(db_kk.kullaniciadi == data.kullaniciadi)
                {
                 res.HataEkle(HataMesajiKodlari.KullaniciAdiKullanildi, "Kullanıcı adı kullanılıyor");
                }
                if (db_kk.email == data.email)
                {
                    res.HataEkle(HataMesajiKodlari.EmailKullanildi, "E mail kullanılıyor");
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.email = data.email;
            res.Result.ad = data.ad;
            res.Result.soyad = data.soyad;
            res.Result.sifre = data.sifre;
            res.Result.kullaniciadi = data.kullaniciadi;

            if(base.Update(res.Result)==0)
            {
                res.HataEkle(HataMesajiKodlari.ProfilGuncellenemedi, "Profil Güncellenemedi..");
            }

            return res;
        }


        //Metod gizleme(hiding)
        public new BusinessLayerResult<KanBanKullanici> Insert(KanBanKullanici data)
        {
            KanBanKullanici kullanici = Find(x => x.kullaniciadi == data.kullaniciadi || x.email == data.email);
            BusinessLayerResult<KanBanKullanici> layerResult = new BusinessLayerResult<KanBanKullanici>();

            layerResult.Result = data;

            if (kullanici != null)
            {
                if (kullanici.kullaniciadi == data.kullaniciadi)
                {
                    layerResult.HataEkle(HataMesajiKodlari.KullaniciAdiKullanildi, "Kullanıcı adı kayıtlı");
                }
                if (kullanici.email == data.email)
                {
                    layerResult.HataEkle(HataMesajiKodlari.EmailKullanildi, "E mail adresi kayıtlı");
                }

            }
            else
            {
                layerResult.Result.aktiflikGuid = Guid.NewGuid();

                if (base.Insert(layerResult.Result) == 0)
                {
                    layerResult.HataEkle(HataMesajiKodlari.KullaniciEklenemedi, "Kullanıcı Eklenemedi");
                }

            }

            return layerResult;

        }

        public new BusinessLayerResult<KanBanKullanici> Update(KanBanKullanici data)
        {
            KanBanKullanici db_kk = Find(x => x.Id != data.Id && (x.kullaniciadi == data.kullaniciadi || x.email == data.email));
            BusinessLayerResult<KanBanKullanici> res = new BusinessLayerResult<KanBanKullanici>();
            res.Result = data;
            if (db_kk != null && db_kk.Id != data.Id)
            {
                if (db_kk.kullaniciadi == data.kullaniciadi)
                {
                    res.HataEkle(HataMesajiKodlari.KullaniciAdiKullanildi, "Kullanıcı adı kullanılıyor");
                }
                if (db_kk.email == data.email)
                {
                    res.HataEkle(HataMesajiKodlari.EmailKullanildi, "E mail kullanılıyor");
                }
                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.email = data.email;
            res.Result.ad = data.ad;
            res.Result.soyad = data.soyad;
            res.Result.sifre = data.sifre;
            res.Result.kullaniciadi = data.kullaniciadi;
            res.Result.aktifDurum = data.aktifDurum;
            res.Result.adminDurum = data.adminDurum;


            if (base.Update(res.Result) == 0)
            {
                res.HataEkle(HataMesajiKodlari.KullaniciGuncellenemedi, "Kullanıcı Güncellenemedi..");
            }

            return res;
        }

    }
}
