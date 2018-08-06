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
    [Table("Todo")]
    public class Todo:MyEntityBase
    {   
        public int projeId { get; set; }

        [DisplayName("Todo Adı"),Required]
        public string todoAdi{ get; set; }

        [DisplayName("Todo Açıklaması"), Required, StringLength(150)]
        public string todoAciklama { get; set; }

        [NotMapped]
        public string aciklamaOzet
        {
            get
            {
                //?? soldaki değer nullsa sağdaki değeri alıyor
                if (todoAciklama.Length > 40)
                {
                    return todoAciklama?.Substring(0, 40);
                }
                else
                    return todoAciklama;

            }
        }
        [DisplayName("Taslak ")]
        public bool taslakDurum { get; set; }

        public virtual List<Yorum> Yorum{ get; set; }
        public virtual List<TodoKullanicisi> todoKullanicisi{ get; set; }
        public virtual Proje proje { get; set; }

        [DisplayName("Durum ")]
        public todoDurum todoDurumu { get; set; }
        public Todo()
        {
            Yorum = new List<Yorum>();
            todoKullanicisi = new List<TodoKullanicisi>();
        }


    }
    public enum todoDurum
    {
        TODO,
        ANALYZE,
        DEVELOP,
        TEST,
        DONE
    };
}
