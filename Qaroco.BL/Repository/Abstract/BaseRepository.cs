using Qaroco.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Reflection;

namespace Qaroco.BL.Repository.Abstract
{
    public class BaseRepository<T> : IDisposable where T : class, new()
    {
        //protected db db;
        protected readonly QarocoDBEntities db;
        public BaseRepository()
        {
            db = new QarocoDBEntities();
        }

        public bool Add(T data)
        {
            try
            {
                //using (db=new db())
                {
                    var T = db.Set<T>().Add(data);
                    //db.Entry<T>(data).State = EntityState.Added;

                    int result = db.SaveChanges();
                    return result > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                //using (db = new db())
                {
                    var silinecekData = db.Set<T>().Find(id);
                    db.Set<T>().Remove(silinecekData);
                    //db.Entry<T>(silinecekData).State = EntityState.Deleted;
                    var result = db.SaveChanges();
                    return result > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<T> GetAll()
        {
            //using (db=new db())
            {
                return db.Set<T>().ToList();
            }
        }

        public T GetById(int id)
        {
            //using (db = new db())
            {
                return db.Set<T>().Find(id);
            }
        }

        public bool Update(T data)
        {

            //using (db = new db())
            {
                db.Entry<T>(data).State = EntityState.Modified;
                int result = db.SaveChanges();
                //ternary if
                return result > 0;
            }
        }

        public bool Update(T data, int id)
        {
            var entity = GetById(id);
            foreach (var item in typeof(T).GetProperties())
            {
                try
                {
                    item.SetValue(entity, item.GetValue(data));
                }
                catch (Exception)
                {
                }
            }
            //using (db = new db())
            {
                db.Entry<T>(entity).State = EntityState.Modified;
                int result = db.SaveChanges();
                //ternary if
                return result > 0;
            }
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> filter)//arama filtreleme
        {
            //using (db=new db())
            {
                return db.Set<T>().Where(filter).ToList();
            }
        }
        public T GetByFilterx(Expression<Func<T, bool>> filter)//arama filtreleme
        {
            //using (db=new db())
            {
                return db.Set<T>().Where(filter).FirstOrDefault();
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

