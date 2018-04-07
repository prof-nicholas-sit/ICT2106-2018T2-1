using System.Collections.Generic;

namespace Prettyprinter.DAL
{
    public interface IDataGateway<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(string id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Save();
    }
}