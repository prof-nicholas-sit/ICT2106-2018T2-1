using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeSetter.Models;
using TypeSetter.DAL;

namespace TypeSetter.Controllers
{
    public class SaveController : Controller
    {
        FileManagerGateway fmGateWay;
        InterpreterJob interpreterJob;
        MarkUpIntepreterGateway muiGateway;
        ConvertModel convertM;
        HTML html;
        string htmlInStringFormat;
   

        //Convert MarkUpInterpreter's content and save it into filemanager
        //public void SaveContent(string file)
        //{
        //    //**Assuming MarkUpInterpreter has already set the object , we retrieve it now
        //    interpreterJob = muiGateway.interpreterJob;

        //    //start converting to html
        //    html = convertM.convertToHTML(interpreterJob.markUpContent);

        //    //Convert html content to string so can pass to file manager to save
        //    htmlInStringFormat = convertM.HTMLToString(html);

        //    //Save string by passing content to filemanager
        //    fmGateWay.HTMLContentForFileManager = htmlInStringFormat;


        //}

       
    }
}
