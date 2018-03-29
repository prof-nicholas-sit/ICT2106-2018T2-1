using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    interface IDataGateway<T> where T:class
    {
        void CreateFile(T obj);
        void UpdateFile(T obj);
        void RenameFile(string fileId, string fileName);
        void MoveFile(string fileId, string parentId);
        void DeleteFile(string fileId);
        IEnumerable<T> SelectAll(string folderId);
        T SelectById(string fileId);
    }
}
