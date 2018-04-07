using Microsoft.AspNetCore.Http;
using Prettyprinter.Controllers;

namespace Prettyprinter.Controllers
{
    public class TypeSetterController
    {
        //Stub method for TypeSetter to build content into already created File object
        public void onCreate(FileBuilder fileBuilder) {
            fileBuilder.BuildContent("CONTENT");
        }

        public void onEdit(FileBuilder fileBuilder)
        {
            fileBuilder.BuildContent("CONTENT");
        }
    }
}
