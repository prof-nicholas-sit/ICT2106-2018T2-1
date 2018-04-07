using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Models
{
    class InterpreterJobBuilder : IInterpreterJobBuilder
    {
        private InterpreterJob job;

        // 0 - Error (No destination found)
        // 1 - To Typesetter (Editor to Typesetter)
        // 2 - To Editor (Typesetter to Editor)
        private int destination;
        private int content;
        private string fileName;

        private string preHeaderMetaData;
        private string convertedContent;

        private bool isPreHeaderMetadataConverted;
        private bool isContentConverted;

        public InterpreterJobBuilder()
        {
            isPreHeaderMetadataConverted = false;
            isContentConverted = false;
        }

        public void BuildContent(string content)
        {
            ContentBuilder builder = new ContentBuilder();
            builder.OpenContentBuilder(destination, content);
            builder.ConvertContent();

            content = builder.GetConvertedContent().RawContent;
            convertedContent = builder.GetConvertedContent().ConvertedContent;
            job.Content = content;
            job.ConvertedContent = convertedContent;

            isContentConverted = true;
        }

        public void BuildPreHeaderMetadata(string fontFamily, double fontSize, bool emojiSupport, string lastModified)
        {
            PreHeaderMetadataBuilder builder = new PreHeaderMetadataBuilder();
            builder.OpenPreHeader(destination);
            builder.NewHeading(fontFamily, fontSize, emojiSupport, lastModified);

            preHeaderMetaData = builder.GetCompletedHeader().GetPreHeader();
            job.PreHeaderMetaData = preHeaderMetaData;

            isPreHeaderMetadataConverted = true;
        }

        public void CloseJob()
        {
            // Empty
        }

        public InterpreterJob GetCompletedJob()
        {
            if (!isPreHeaderMetadataConverted || !isContentConverted)
            {
                return null;
            }

            else
            {
                job.IsConverted = true;
                return job;
            }
        }

        public void OpenJob(int destination, string fileName)
        {
            // Initialise new job object
            job = new InterpreterJob();

            // Set default divider identifier to split between headers and content
            job.DividerIdentifier = "\n\n";

            // Set destination and set fileName
            this.destination = destination;
            this.fileName = fileName;
            job.DestinationFlag = destination;
            job.FileName = fileName;
        }
    }
}
