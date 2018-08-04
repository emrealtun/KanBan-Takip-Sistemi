using KanBanVersion2.Entities.Mesajlar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KanBanVersion2.BusinessLayer.Results
{
    public class BusinessLayerResult<T> where T: class
    {
        public List<HataMesaji> Hata { get; set; }
        public T Result { get; set; }



        public BusinessLayerResult()
        {
            Hata = new List<HataMesaji>();

        }

        public void HataEkle(HataMesajiKodlari kod, string mesaj)
        {
            Hata.Add(new HataMesaji() { Kod = kod, Mesaj = mesaj });
        }
    }
}
