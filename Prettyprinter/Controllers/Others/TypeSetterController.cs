using Microsoft.AspNetCore.Http;


namespace Prettyprinter.Controllers
{
    public class TypeSetterController
    {
        public FileController onCreate() {
            FileController data = new FileController();
            data.setContent(null);
            data.setParentId("apath");
            data.setNewFile(true);
            return data;
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
