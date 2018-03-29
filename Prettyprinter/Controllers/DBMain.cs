using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using DBLayer.DAL;


namespace DBLayer
{
    internal class DBMain
    {
        /**
         * Protoype Demo: CRUD demonstrated with AccountModel
         */
        public static void Main(string[] args){
            
            // FOR PROTOTYPE DEMO, START WITH EMPTY DB
            
            var db1 = new DbObj();
            var db2 = new DbObj();
            var accGateway = new AccountGateway(db1);
            var fileGateway = new DataGateway<ItemModel>(db2);

            // 0: nothing; 1: create; 2: read; 3: update; 4: delete; 5: select by ID, 6: get account model by email
            int demo = 6;
            string email = "ryan@email.com";

            switch (demo)
            {
                case 1:
                    // CREATE
                    ObjectId id1 = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(id1, "Ng Cai Feng", "Mr",
                        "caifeng@email.com", "password1", DateTime.Now, true, "", "Bio"));
                    ObjectId id2 = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(id2, "Chua Xiang Wei, Jerahmeel", "Mr",
                        "jerry@email.com", "password2", DateTime.Now, true, "", "Bio"));
                    ObjectId id3 = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(id3, "Ryan Chia Dong Yi", "Mr",
                        "ryan@email.com", "password3", DateTime.Now, true, "", "Bio"));
                    ObjectId id4 = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(id4, "Lim Jing Pei", "Ms",
                        "phoebe@test.com", "password4", DateTime.Now, true, "", "Bio"));
                    accGateway.Save();
                    
                    AccessControlModel asModel = new AccessControlModel(ObjectId.GenerateNewId(), id1.ToString(), true, true );
                    BsonArray permissionArray = new BsonArray();
                    permissionArray.Add(asModel.ToBsonDocument());
                    fileGateway.Insert(new ItemModel(ObjectId.GenerateNewId(), "test.txt", "", 0, DateTime.Now, "", "", permissionArray));
                    fileGateway.Save();
                    break;

                case 2:
                    // READ
                    foreach (var doc in accGateway.SelectAll())
                    {
                        Console.WriteLine(doc.ToJson());
                    }
                    
                    foreach (var doc in fileGateway.SelectAll())
                    {
                        Console.WriteLine(doc.ToJson());
                    }

                    break;

                case 3:
                    // UPDATE
                    ObjectId objId = ObjectId.Parse("5a8e6c5fdc25a0bcf64faee3"); // copy ID here
                    accGateway.Update(new AccountModel(objId, "Bobby Cai Feng Ng", "Mr", "ngcaifeng@email.com", "password1",
                        DateTime.Now, true, "", "Bio"));
                    
                    accGateway.Save();

                    break;

                case 4:
                    // DELETE
                    accGateway.Delete("5a8e6437dc25a0bc1be8f5d9"); // copy ID here
                    accGateway.Save();

                    break;

                case 5:
                    // SELECT BY ID
                    Console.WriteLine(accGateway.SelectById("5a8e6437dc25a0bc1be8f5da").ToJson());
                    
                    break;
                
                case 6:
                    // SELECT ACCOUNT MODEL BY EMAIL
                    AccountModel acc = accGateway.SelectByEmail(email);
                    if (acc != null)
                    {
                        Console.WriteLine("Name of " + email + ": " + acc.Name); 
                        Console.WriteLine("Password of " + email + ": " + acc.Password); 
                    }

                    break;
            }

        }
    }
}