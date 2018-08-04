using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanBanVersion2.DataAccessLayer.EntityFramework;
using KanBanVersion2.BusinessLayer.Abstract;

namespace KanBanVersion2.BusinessLayer
{
      public class ProjectManager: ManagerBase<Proje>
    {
        public override int Delete(Proje proje)
        {   
            TodoManager tm = new TodoManager();
            YorumManager ym = new YorumManager();
            ProjectUserManager pk = new ProjectUserManager();

            foreach (ProjeKullanicisi prokul in proje.projeKullanicisi.ToList())
            {
                pk.Delete(prokul);

            }
            foreach (Todo todo in proje.Todo.ToList())
            {
                //projedeki todoların silme işlemi

                foreach (Yorum yorum in todo.Yorum.ToList())
                {
                    //tododaki yorumların silme işlemi
                    ym.Delete(yorum);
                }
                tm.Delete(todo);
            }

            return base.Delete(proje);


        }

    }
}
