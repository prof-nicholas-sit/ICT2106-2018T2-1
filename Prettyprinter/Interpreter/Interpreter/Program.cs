using Interpreter.Controllers;
using Interpreter.Interfaces;
using Interpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Testing
            InterpreterJob job1 = new InterpreterJob();
            job1.DestinationFlag = 1;
            //job1.FileName = "test1.txt";
            job1.FileName = "Text.txt";
            string[] tempStore;
            tempStore = System.IO.File.ReadAllLines(@"..\..\Data\" + job1.FileName);
            foreach (string s in tempStore)
            {
                job1.Content += s;
                job1.Content += "\n";
            }

            job1.FontFamily = "sans-serif";
            job1.FontSize = 12.0;
            job1.LastModified = "12-02-2017 16:43:26";

            InterpreterJob job2 = new InterpreterJob();
            job2.DestinationFlag = 2;
            //job2.Content = "TestContent";
            job2.FileName = "test2.txt";
            string[] tempStore2;
            tempStore2 = System.IO.File.ReadAllLines(@"..\..\Data\" + job2.FileName);
            foreach (string s in tempStore2)
            {
                job2.Content += s;
                job2.Content += "\n";
            }
            job2.FontFamily = "sans-serif";
            job2.FontSize = 12.0;
            job2.LastModified = "12-02-2017 16:43:26";

            // For Editor Usage
            IEditorToTypesetter eot = new JobController();
            InterpreterJob resultingJobForTypesetter = eot.ConvertToTypesetter(job1);
            Console.WriteLine(resultingJobForTypesetter.PreHeaderMetaData);

            Console.WriteLine("\n\n");

            // For Typesetter Usage
            ITypesetterToEditor toe = new JobController();
            InterpreterJob resultingJobForEditor = toe.ConvertToEditor(job2);
            Console.WriteLine(resultingJobForEditor.PreHeaderMetaData);

            Console.ReadLine();
        }
    }
}
