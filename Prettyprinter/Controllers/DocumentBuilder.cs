using System;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter.Controllers
{
    public abstract class DocumentBuilder
    {
        Document document;

        public abstract void BuildDocument(ApplicationDbContext context, string fileID, string userID, string creationPath,
            string parentID, string Name);

        public abstract void SaveDocument();

        public abstract Document GetDocument();
    }
}
