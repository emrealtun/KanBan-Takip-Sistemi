using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KanBanVersion2.Entities;

namespace KanBanVersion2.DataAccessLayer.EntityFramework
{
     public class MyInitiliazer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            KanBanKullanici admin = new KanBanKullanici()
            {
                ad = "Emre",
                soyad="Altun",
                email="altunemre6@gmail.com",
                aktiflikGuid = Guid.NewGuid(),
                aktifDurum = true,
                adminDurum=true,
                kullaniciadi="emrealtun",
                sifre="emre1234",
                olusturmaTarihi=DateTime.Now,
                guncellemeTarihi=DateTime.Now.AddMinutes(5),
                duzenleyen="emrealtun"
            };

            KanBanKullanici standartKullanici = new KanBanKullanici()
            {
                ad = "Çağrı",
                soyad = "Akça",
                email = "cagriakca@gmail.com",
                aktiflikGuid = Guid.NewGuid(),
                aktifDurum = true,
                adminDurum = false,
                kullaniciadi = "cagriakca",
                sifre = "cagri1234",
                olusturmaTarihi = DateTime.Now.AddHours(1),
                guncellemeTarihi = DateTime.Now.AddMinutes(65),
                duzenleyen = "emrealtun"
            };

            context.KanBanKullanici.Add(admin);
            context.KanBanKullanici.Add(standartKullanici);

            for (int i = 0; i < 8; i++)
            {
                KanBanKullanici kullanici = new KanBanKullanici()
                {
                    ad = FakeData.NameData.GetFirstName(),
                    soyad =FakeData.NameData.GetSurname(),
                    email = FakeData.NetworkData.GetEmail(),
                    aktiflikGuid = Guid.NewGuid(),
                    aktifDurum = true,
                    adminDurum = false,
                    kullaniciadi = $"user{i}",
                    sifre = "123",
                    olusturmaTarihi = DateTime.Now.AddHours(1),
                    guncellemeTarihi = DateTime.Now.AddMinutes(65),
                    duzenleyen = $"user{i}"
                };
                context.KanBanKullanici.Add(kullanici);

            }

            context.SaveChanges();
            //FAKE DATA İLE PROJE EKLEME
            for (int i = 0; i < 10; i++)
            {
                Proje pro = new Proje()
                {
                    projeAd = FakeData.PlaceData.GetStreetName(),
                    projeAciklamasi= FakeData.PlaceData.GetAddress(),
                    olusturmaTarihi=DateTime.Now,
                    guncellemeTarihi=DateTime.Now.AddHours(1),
                    duzenleyen=admin.kullaniciadi

                };


                //PORJE KULLANİCİSİ EKLEME
                ProjeKullanicisi projekullanicisi = new ProjeKullanicisi();
                projekullanicisi.kanbankullanici = (i % 2 == 0) ? admin : standartKullanici; 
                pro.projeKullanicisi.Add(projekullanicisi);
                context.Proje.Add(pro);

                //FAKE DATA İLE TODO EKLEME
                for (int k = 0; k < FakeData.NumberData.GetNumber(3,5); k++)
                {
                    Todo todo = new Todo()
                    {
                        todoAdi = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(10, 30)),
                        todoAciklama = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(50, 150)),
                        taslakDurum=false,
                        duzenleyen=admin.kullaniciadi,
                        olusturmaTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-2),DateTime.Now.AddYears(-1)),
                        guncellemeTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                    };
                    //ARA TABLODAKİ TODO KULLANICISINDAN NESNE OLUŞTURULUP EKLENDİ
                    //TODO KULLANICISINA ULAŞMAK İÇİLEN YAZILACAK KOD
                    //   todo.todoKullanicisi.Where(en => en.kanbankullanici.Id == 1);
                    TodoKullanicisi todoKullanicisi = new TodoKullanicisi();
                    todoKullanicisi.kanbankullanici = (k % 2 == 0) ? admin : standartKullanici;
                    todo.todoKullanicisi.Add(todoKullanicisi);
                    pro.Todo.Add(todo);


                    //FAKE DATA İLE YORUM EKLEME
                    for (int j= 0; j < FakeData.NumberData.GetNumber(1,3); j++)
                    {
                        Yorum yor = new Yorum()
                        {
                            icerik = FakeData.TextData.GetSentence(),
                            olusturmaTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                            guncellemeTarihi= FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                            duzenleyen=( j % 2 ==0) ? admin.kullaniciadi: standartKullanici.kullaniciadi,
                            kullanici = (j%2==0) ? admin : standartKullanici
                            
                        };

                        todo.Yorum.Add(yor);


                    }

               
                }
            }
            context.SaveChanges();
        }
    }
}
