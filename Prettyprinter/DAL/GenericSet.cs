using System;
using System.Collections.Generic;

namespace DBLayer.DAL
{
    public class GenericSet<T> where T : class 
    {
        internal List<T> NewList { get; }
        internal List<T> DirtyList { get; }
        internal List<string> RemovedList { get; }
        // internal List<T> CleanList { get; }
        
        public GenericSet()
        {
            NewList = new List<T>();
            DirtyList = new List<T>();
            RemovedList = new List<string>();
            // CleanList = new List<T>();
        }

        public void RegisterNew(T obj)
        {
            Console.WriteLine("Registering new.");
            NewList.Add(obj);
        }

        public void RegisterRemoved(string id)
        {
            RemovedList.Add(id);
        }

        public void RegisterDirty(T obj)
        {
            DirtyList.Add(obj);
        }

//        public void RegisterClean(T obj)
//        {
//            CleanList.Add(obj);
//        }
    }
}