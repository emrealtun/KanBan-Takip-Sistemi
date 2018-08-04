using KanBanVersion2.Common;
using KanBanVersion2.DataAccessLayer;
using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KanBanVersion2.CoreLayerr.DataAccess;

namespace KanBanVersion2.DataAccessLayer.EntityFramework
{
        public class Repository<T>: RepositoryBase, IDataAccess<T> where T: class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        } 

        public int Insert(T obj)
        {
           
            _objectSet.Add(obj);

            if(obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.olusturmaTarihi = now;
                o.guncellemeTarihi = now;
                o.duzenleyen = App.Common.GetKullaniciAdi();  //İŞLEM YAPAN KULLANICI ADI YAZILMALI
            }
            return Save();   
        }

        public int Update(T obj)
        {

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.guncellemeTarihi = DateTime.Now;
                o.duzenleyen = App.Common.GetKullaniciAdi();
                //o.duzenleyen = "system";
            }
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        } 

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);

        }
    }
}
