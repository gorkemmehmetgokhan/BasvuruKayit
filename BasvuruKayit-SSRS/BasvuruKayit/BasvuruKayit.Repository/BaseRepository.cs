using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasvuruKayit.Veritabani;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BasvuruKayit.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        //Singleton Patterrn:Uygulamanın tek connection üzerinden işlen yapmasını sağlayan tasarım desenidir.
        private static DB_UniversiteEntities context;
        public static DB_UniversiteEntities Context
        {
            get
            {
                context = context ?? new DB_UniversiteEntities();
                return context;
            }
            set { context = value; }
        }

        public IEnumerable<T> SelectAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Insert(T obj)
        {
            Context.Set<T>().Add(obj);
        }
        public void Update(T obj)
        {
            Context.Set<T>().Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }
        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public void DeleteById(object id, T obj)
        {
            Context.Set<T>();
        }

        public void Delete(T obj)
        {
            Context.Set<T>().Attach(obj);
            Context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
        }

        public tbl_Ogrenci GetByOgrenciID(int id)
        {
            tbl_Ogrenci ogr = Context.tbl_Ogrenci.SingleOrDefault(t => t.OgrenciID == id);
            return ogr;
        }

        public tbl_OgrenciDers GetByOgrenciDersID(int id)
        {
            tbl_OgrenciDers ogrDers = Context.tbl_OgrenciDers.SingleOrDefault(t => t.OgrenciDersID == id);
            return ogrDers;
        }

        public tbl_Sinav GetByOgrenciDersIDSinavTipID(int OgrenciDersID, int SinavTipID)
        {
            tbl_Sinav sinav = Context.tbl_Sinav.SingleOrDefault(t => t.OgrenciDersID == OgrenciDersID && t.SinavTipID == SinavTipID);
            return sinav;
        }

        public T GetById(int bid)
        {
            return Context.Set<T>().Find(bid);
        }
    }
}
