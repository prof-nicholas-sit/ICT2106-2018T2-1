﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prettyprinter.Models;
using Microsoft.EntityFrameworkCore;

namespace Prettyprinter.DAL
{
    public class DataGateway<T> : IDataGateway<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> data = null;
        public DataGateway(ApplicationDbContext context)
        {
            this.db = context;
            this.data = db.Set<T>();
        }
        public void CreateFile(T obj)
        {
            db.Add(obj);
            db.SaveChanges();
        }

        public void UpdateFile(T obj)
        {
            db.Update(obj);
            db.SaveChanges();
        }

        public void DeleteFile(string fileId)
        {
            T obj = data.Find(fileId);
            db.Remove(obj);
            db.SaveChanges();
        }
        
        public virtual IEnumerable<T> SelectAll(string _id, string col)
        {
            string[] tableName = typeof(T).ToString().Split(".");

            return data.FromSql("SELECT * FROM ["+ tableName[2] + "] WHERE "+col+" = "+_id);
        }

        public T SelectById(string fileId)
        {
            return data.Find(fileId);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
