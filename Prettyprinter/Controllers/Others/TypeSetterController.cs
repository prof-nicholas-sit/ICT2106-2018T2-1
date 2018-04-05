using Microsoft.AspNetCore.Http;
using Prettyprinter.Controllers;

namespace Prettyprinter.Controllers
{
    public class TypeSetterController
    {
        public void onCreate(FileBuilder fileBuilder) {
            fileBuilder.BuildContent("CONTENT");
        }

        public FileController editFile()
        {
            FileController data = new FileController();
            data.setContent("asd");
            data.setParentId("aPath");
            data.setNewFile(true);
            return data;
        }

    }
}
