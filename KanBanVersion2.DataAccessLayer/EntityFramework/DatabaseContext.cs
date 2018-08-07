using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.DataAccessLayer.EntityFramework
{
     public class DatabaseContext: DbContext
    {
        public DbSet<KanBanKullanici> KanBanKullanici { get; set; }
        public DbSet<Proje> Proje { get; set; }
        public DbSet<ProjeKullanicisi> ProjeKullanicisi { get; set; }
        public DbSet<Todo> Todo{ get; set; }
        public DbSet<TodoKullanicisi> TodoKullanicisi { get; set; }
        public DbSet<Yorum> Yorum { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext,KanBanVersion2.DataAccessLayer.Migrations.Configuration>());
        }


    }
}
