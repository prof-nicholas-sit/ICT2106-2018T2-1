using System;
using System.Collections.Generic;

namespace Prettyprinter.DAL
{
    public class GenericSet<T> where T : class 
    {
        internal List<T> NewList { get; }
        internal List<T> DirtyList { get; }
        internal List<T> RemovedList { get; }
        
        public GenericSet()
        {
            NewList = new List<T>();
            DirtyList = new List<T>();
            RemovedList = new List<T>();
        }

        public void RegisterNew(T obj)
        {
            NewList.Add(obj);
        }

        public void RegisterRemoved(T obj)
        {
            // remove from dirty list (nothing will happen even if the obj not inside the dirty list)
            DirtyList.Remove(obj);
            
            if (NewList.Contains(obj))
            {
                // if obj is newly created, not yet saved in db, then just remove it from new list
                NewList.Remove(obj);
            }
            else if (!RemovedList.Contains(obj))
            {
                // only add obj into removed list if its not already inside
                RemovedList.Add(obj);
            }
        }

        public void RegisterDirty(T obj)
        {
            if (!NewList.Contains(obj) && !RemovedList.Contains(obj))
            {
                DirtyList.Add(obj);
            }
            // Console.WriteLine("Updating: " + obj.ToBsonDocument()[0]);\
        }

    }
}