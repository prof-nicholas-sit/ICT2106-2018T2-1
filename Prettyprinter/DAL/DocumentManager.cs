﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prettyprinter.DAL
{
    public abstract class DocumentManager
    {
        public abstract String readFile(String fileID);
        public abstract String getParentOfFile(String fileID);
        public abstract Boolean createDocument(String path, String fileID);
        public abstract Boolean deleteDocument(String path, String fileID);
    }
}
