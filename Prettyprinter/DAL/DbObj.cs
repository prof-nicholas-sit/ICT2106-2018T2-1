using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBLayer.DAL
{
    public class DbObj
    {
        // private const string DbAddress = "mongodb://localhost:27017";
        private const string DbAddress = "mongodb://52.91.110.127:27017";
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
        private static IMongoCollection<ItemModel> _filesCollectionModel;
        
        /**
         * Constructor that makes sure required db and collections are created
         */
        public DbObj()
        {
            _client = new MongoClient(DbAddress);
            _database = _client.GetDatabase(DbName);
            
            if (CollectionExistsAsync("User_Account").Result == false)
                CreateCollection("User_Account").Wait();
            
            _bsonuserCollection = _database.GetCollection<BsonDocument>("User_Account");
            _userCollectionModel = _database.GetCollection<AccountModel>("User_Account"); 
            
            if (CollectionExistsAsync("Files").Result == false)
                CreateCollection("Files").Wait();
            
            _bsonfilesCollection = _database.GetCollection<BsonDocument>("Files");
            _filesCollectionModel = _database.GetCollection<ItemModel>("Files"); 
            /*
             (if(CollectionExistsAsync("Files").Result == false)
                CreateFiles().Wait();

            MainAsync().Wait();*/
        }
        
        /*
         * Main ASync 
         */
        private static async Task MainAsync()
        {
            /*var itemList = new List<ItemModel>();
            var acctList = new List<AccountModel>();
            
            // Method 1
            using (IAsyncCursor<AccountModel> cursor = await userCollectionModel.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<AccountModel> batch = cursor.Current;
                    foreach (AccountModel document2 in batch)
                    {
                        //acctList.Add(document2);
                        acctList.Add(document2);
                        Console.WriteLine(document2.ToJson());
                        Console.WriteLine();
                    }
                }
            }

            using (IAsyncCursor<ItemModel> cursor = await fileMgrCollectionModel.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<ItemModel> batch = cursor.Current;
                    foreach (ItemModel document in batch)
                    {
                        //Console.WriteLine(document);
                        itemList.Add(document);
                        Console.WriteLine(document.ToJson());
                        Console.WriteLine();
                    }
                }
            }
            
            _userCollectionModel = _database.GetCollection<AccountModel>("User_Account");
            
            if (CollectionExistsAsync("User_Account").Result == false)
                CreateCollection("User_Account").Wait();
                */
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
                Console.WriteLine("SelectAll collection AccountModel");
                return _userCollectionModel.Find(new BsonDocument()).ToEnumerable();
            }

            if (type == typeof(ItemModel))
            {
                Console.WriteLine("SelectAll collection ItemModel");
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
                Console.WriteLine("Selectwhere collection AccountModel");
                var filter = Builders<AccountModel>.Filter.Eq(field, value);
                return _userCollectionModel.Find(filter).ToEnumerable();
            }
            
            if (type == typeof(ItemModel))
            {
                Console.WriteLine("Selectwhere collection ItemModel");
                var filter = Builders<ItemModel>.Filter.Eq(field, value);
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
                Console.WriteLine("Writing changes to AccountModel database");
                // CREATE
                if (set.NewList.Count > 0) 
                {
                    Console.WriteLine("Writing Create to AccountModel database");
                    await _bsonuserCollection.InsertManyAsync(ListToBson(set.NewList));
                }
                
                // UPDATE
                foreach (var doc in set.DirtyList as List<AccountModel>)
                {
                    Console.WriteLine("Writing Update to AccountModel database");
                    await _bsonuserCollection.ReplaceOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID), doc.ToBsonDocument());
                }
                
                // DELETE
                foreach (var doc in set.RemovedList  as List<AccountModel>)
                {
                    Console.WriteLine("Writing Delete to AccountModel database");
                    await _bsonuserCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID));
                }
            }
            
            if (typeof(T) == typeof(ItemModel))
            {
                Console.WriteLine("Writing changes to Files database");
                // CREATE
                if (set.NewList.Count > 0) 
                {
                    Console.WriteLine("Writing Create to Files database");
                    await _bsonfilesCollection.InsertManyAsync(ListToBson(set.NewList));
                }
                
                // UPDATE
                foreach (var doc in set.DirtyList as List<ItemModel>)
                {
                    Console.WriteLine("Writing Update to Files database");
                    await _bsonfilesCollection.ReplaceOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID), doc.ToBsonDocument());
                }
                
                // DELETE
                foreach (var doc in set.RemovedList as List<ItemModel>)
                {
                    Console.WriteLine("Writing Delete to Files database");
                    await _bsonfilesCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", doc.ID));
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