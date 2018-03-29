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
            var fileGateway = new ItemGateway(db2);

            // 0: whatever you want to test; 1: create; 2: read; 3: update; 4: delete; 5: select by ID, 6: get account model by email
            // 7 : Create file
            int demo = 0;
            string email = "caifeng@email.com";
            ObjectId objId;
            AccountModel acc;
            
            switch (demo)
            {
                case 0:
                    // TEST ANYTHING HERE
                    objId = ObjectId.GenerateNewId();
                    AccountModel testAcc = new AccountModel(objId, "Ng Cai Feng", "Mr",
                        "caifeng@email.com", "password1", DateTime.Now, true, "", "Bio");
                    
                    accGateway.Insert(testAcc);

                    testAcc.Bio = "ABC";
                    testAcc.Name = "UPDATED";
                    accGateway.Update(testAcc);
                    // accGateway.Delete(testAcc);
                    
                    accGateway.Save();
                    break;
                case 1:
                    // CREATE
                    objId = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(objId, "Ng Cai Feng", "Mr",
                        "caifeng@email.com", "password1", DateTime.Now, true, "", "Bio"));
                    objId = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(objId, "Chua Xiang Wei, Jerahmeel", "Mr",
                        "jerry@email.com", "password2", DateTime.Now, true, "", "Bio"));
                    objId = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(objId, "Ryan Chia Dong Yi", "Mr",
                        "ryan@email.com", "password3", DateTime.Now, true, "", "Bio"));
                    objId = ObjectId.GenerateNewId();
                    accGateway.Insert(new AccountModel(objId, "Lim Jing Pei", "Ms",
                        "phoebe@test.com", "password4", DateTime.Now, true, "", "Bio"));
                    accGateway.Save();
                    
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
                    acc = accGateway.SelectByEmail(email);
                    if (acc != null)
                    {
                        acc.Name = "Bobby Cai Feng Ng";
                        acc.Title = "Mister";

                        accGateway.Update(acc);
                        accGateway.Save();
                    }

                    break;

                case 4:
                    // DELETE
                    acc = accGateway.SelectByEmail(email);
                    if (acc != null)
                    {
                        accGateway.Delete(acc);
                        accGateway.Save();
                    }

                    break;

                case 5:
                    // SELECT BY ID
                    Console.WriteLine(accGateway.SelectById("5a8e6437dc25a0bc1be8f5da").ToJson());
                    
                    break;
                
                case 6:
                    // SELECT ACCOUNT MODEL BY EMAIL
                    acc = accGateway.SelectByEmail(email);
                    if (acc != null)
                    {
                        Console.WriteLine("Name of " + email + ": " + acc.Name); 
                        Console.WriteLine("Password of " + email + ": " + acc.Password); 
                    }

                    break;
                
                case 7:
                    // CREATE FILE
                    AccessControlModel asModel = new AccessControlModel(ObjectId.GenerateNewId(), email, true, true );
                    BsonArray permissionArray = new BsonArray();
                    permissionArray.Add(asModel.ToBsonDocument());
                    fileGateway.Insert(new ItemModel(ObjectId.GenerateNewId(), "test.txt", "", 0, DateTime.Now, "", "", permissionArray));
                    fileGateway.Save();
                    
                    break;
            }

        }
    }
}