using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    interface IDataGateway<T> where T:class
    {
        //CRUD to be implemented in DataGateway
        void CreateFile(T obj);
        IEnumerable<T> SelectAll(string _id, string col);
        T SelectById(string _id);
        void UpdateFile(T obj);
        void DeleteFile(string fileId);
    }
}
