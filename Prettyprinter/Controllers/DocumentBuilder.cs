using System;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public abstract class DocumentBuilder
    {
        public abstract void BuildDocument(ApplicationDbContext context, string fileID, string userID, string creationPath,
            string parentID, string Name,Boolean permission);

     

        public abstract void SaveDocument();

        public abstract Document GetDocument();
    }
}
