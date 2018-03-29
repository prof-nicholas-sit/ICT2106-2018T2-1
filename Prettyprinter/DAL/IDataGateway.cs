using System.Collections.Generic;
using MongoDB.Bson;

namespace DBLayer.DAL
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