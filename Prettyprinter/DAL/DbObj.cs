using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class DbObj
    {
        private static DbObj _instance;
        
        private const string DbAddress = "mongodb://18.221.150.56:27017";
        private const string DbName = "ICT2106";

        private static MongoClient _client;
        private static IMongoDatabase _database;
        
        // for create, update, delete
        private static IMongoCollection<BsonDocument> _bsonuserCollection;
        
        // for reading
        private static IMongoCollection<AccountModel> _userCollectionModel;
        
        // for create, update, delete
        private static IMongoCollection<BsonDocument> _bsonfilesCollection;
        
        // for reading
        private static IMongoCollection<MetadataModel> _filesCollectionModel;
        
        /**
         * Private constructor. nobody can use
         */
        private DbObj()
        {
        }

        public static DbObj GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbObj();
                    
                _client = new MongoClient(DbAddress);
                _database = _client.GetDatabase(DbName);
            
                if (CollectionExistsAsync("User_Account").Result == false)
                    CreateCollection("User_Account").Wait();
            
                _bsonuserCollection = _database.GetCollection<BsonDocument>("User_Account");
                _userCollectionModel = _database.GetCollection<AccountModel>("User_Account"); 
            
                if (CollectionExistsAsync("Files").Result == false)
                    CreateCollection("Files").Wait();
            
                _bsonfilesCollection = _database.GetCollection<BsonDocument>("Files");
                _filesCollectionModel = _database.GetCollection<MetadataModel>("Files"); 
            }

            return _instance;
        }
        
        /*
         * Check if collection exist
         */
        private static async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = await _client.GetDatabase("ICT2106").ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            //check for existence
            return await collections.AnyAsync();
        }
        
        /**
         * Create collection
         */
        private static async Task CreateCollection(string collectionName)
        {
            Console.WriteLine("Creating collection: " + collectionName);
            await _database.CreateCollectionAsync(collectionName);
        }


        /**
         * Select * from collection
         */
        public IEnumerable SelectAll(Type type)
        {
            if (type == typeof(AccountModel))
            {
                return _userCollectionModel.Find(new BsonDocument()).ToEnumerable();
            }

            if (type == typeof(MetadataModel))
            {
                return _filesCollectionModel.Find(new BsonDocument()).ToEnumerable();
            }
            
            // this line here is to ensure that this method returns something. For prototype only. Will be changed.
            return _userCollectionModel.Find(new BsonDocument()).ToEnumerable();
        }
        
        
        /**
         * Select * from collection where field = value
         */
        public IEnumerable SelectWhere<T>(Type type, string field, T value)
        {
            
            if (type == typeof(AccountModel))
            {
                var filter = Builders<AccountModel>.Filter.Eq(field, value);
                return _userCollectionModel.Find(filter).ToEnumerable();
            }
            
            if (type == typeof(MetadataModel))
            {
                var filter = Builders<MetadataModel>.Filter.Eq(field, value);
                return _filesCollectionModel.Find(filter).ToEnumerable();
            }
            
            // this line here is to ensure that this method returns something. For prototype only. Will be changed.
            return null;
        }
        
        
        /**
         * Select * from collection where _id = value
         */
        public T SelectById<T>(Type type, string value) where T : class
        {
            // SelectWhere will return an enumerable
            var resultInArray = SelectWhere(type, "_id", ObjectId.Parse(value)).GetEnumerator();
            
            // need to get the first element of the returned enumerable
            while (resultInArray.MoveNext())
            {
                return (T)resultInArray.Current;
            }
            
            // this line here is to ensure that this method returns something. For prototype only. Will be changed.
            return null;
        }
        
        /**
         * Save changes for genericsets to db
         */
        public async void Commit<T>(GenericSet<T> set) where T : class
        {
            
            if (typeof(T) == typeof(AccountModel))
            {
                Console.WriteLine("Writing changes to AccountModel database...");
                // CREATE
                if (set.NewList.Count > 0) 
                {
                    await _bsonuserCollection.InsertManyAsync(ListToBson(set.NewList));
                }
                
                // UPDATE
                foreach (var doc in set.DirtyList as List<AccountModel>)
                {
                    await _bsonuserCollection.ReplaceOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID), doc.ToBsonDocument());
                }
                
                // DELETE
                foreach (var doc in set.RemovedList  as List<AccountModel>)
                {
                    await _bsonuserCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID));
                }
            }
            
            if (typeof(T) == typeof(MetadataModel))
            {
                Console.WriteLine("Writing changes to Files database...");
                // CREATE
                if (set.NewList.Count > 0) 
                {
                    await _bsonfilesCollection.InsertManyAsync(ListToBson(set.NewList));
                }
                
                // UPDATE
                foreach (var doc in set.DirtyList as List<MetadataModel>)
                {
                    await _bsonfilesCollection.ReplaceOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.itemId), doc.ToBsonDocument());
                }
                
                // DELETE
                foreach (var doc in set.RemovedList as List<MetadataModel>)
                {
                    await _bsonfilesCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.itemId));
                }
            }
            
        }
        
        /**
         * Convert list to bson
         */
        private List<BsonDocument> ListToBson<T>(List<T> genericList)
        {
            var newList = new List<BsonDocument> { };
            foreach (var doc in genericList)
            {
                newList.Add(doc.ToBsonDocument());
            }

            return newList;
        }
        
    }
}