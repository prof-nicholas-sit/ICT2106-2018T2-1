using System.Collections.Generic;

namespace Prettyprinter.DAL
{
    public class DataGateway<T> : IDataGateway<T> where T : class 
    {
        protected DbObj db;
        protected GenericSet<T> data;

        public DataGateway(DbObj dbObj)
        {
            db = dbObj;
            data = new GenericSet<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return (IEnumerable<T>)db.SelectAll(typeof(T));
        }

        public T SelectById(string id)
        {
            // null is returned if not found
            return db.SelectById<T>(typeof(T), id);
        }

        public void Insert(T obj)
        {
            data.RegisterNew(obj);
        }

        public void Update(T obj)
        {
            data.RegisterDirty(obj);
        }

        public void Delete(T obj)
        {
            data.RegisterRemoved(obj);
        }

        public void Save()
        {
            // write changes to db
            db.Commit(data);
            
            // clear unit of work lists
            data = new GenericSet<T>();
        }
    }
}