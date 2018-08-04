using KanBanVersion2.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanVersion2.DataAccessLayer.EntityFramework
{
    public class  RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lock = new  object();

        protected RepositoryBase()
        {
            CreateContext();
        }
        private static void CreateContext()
        {
            if(context == null)
            {
                lock (_lock)
                {
                    context = new DatabaseContext();
                }
            }
        }
    }
}
