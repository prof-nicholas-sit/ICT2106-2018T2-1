using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Prettyprinter.DAL;
using Prettyprinter.Models;

namespace Prettyprinter
{
    public class Program
    {
        
        /**
         * Facilitate testing of data layer
         */
        public static void Main(string[] args){
            
            var accGateway = AccountGateway.GetInstance(DbObj.GetInstance());
            var metadataGateway = MetadataGateway.GetInstance(DbObj.GetInstance());
            
            string email = "ryan@email.com";
            ObjectId objId;
            AccountModel acc;

            while (true)
            {
                Console.WriteLine("\nSelect an operation:");
                Console.WriteLine("0.  Exit.");
                Console.WriteLine("1.  Create 4 new users.");
                Console.WriteLine("2.  View all users.");
                Console.WriteLine("3.  Update a user's name.");
                Console.WriteLine("4.  Delete a user's account.");
                Console.WriteLine("5.  View name & password of a user.");
                Console.WriteLine("6.  Create sample file and folders.");
                Console.WriteLine("7.  View content of user's root folder.");
                string cmd = Console.ReadLine();
                
                switch (cmd)
                {
                    case "0":
                        Console.WriteLine("\nExit ok.");
                        return;
                    case "1":
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
    
                    case "2":
                        // READ all accounts
                        var readResults = accGateway.SelectAll().GetEnumerator();
                        
                        while (readResults.MoveNext())
                        {
                            Console.WriteLine(readResults.Current?.Email + " " + readResults.Current?.Name);
                        }
                        
                        break;
    
                    case "3":
                        // UPDATE account by email
                        Console.WriteLine("Enter the email of the user whose name you want to update:");
                        email = Console.ReadLine();
                        Console.WriteLine("Enter the new name:");
                        String name = Console.ReadLine();
                        
                        acc = accGateway.SelectByEmail(email);
                        if (acc != null)
                        {
                            acc.Name = name;
    
                            accGateway.Update(acc);
                            accGateway.Save();
                        }
                        else
                        {
                            Console.WriteLine("Email not found.");    
                        }
    
                        break;
    
                    case "4":
                        // DELETE account by email
                        Console.WriteLine("Enter the email of the user to delete:");
                        email = Console.ReadLine();
                        
                        acc = accGateway.SelectByEmail(email);
                        if (acc != null)
                        {
                            accGateway.Delete(acc);
                            accGateway.Save();
                        }
                        else
                        {
                            Console.WriteLine("Email not found.");    
                        }
    
                        break;
                    
                    case "5":
                        // SELECT ACCOUNT MODEL BY EMAIL
                        Console.WriteLine("Enter the email of the user you want to view:");
                        email = Console.ReadLine();
                        
                        acc = accGateway.SelectByEmail(email);
                        if (acc != null)
                        {
                            Console.WriteLine("Name of " + email + ": " + acc.Name); 
                            Console.WriteLine("Password of " + email + ": " + acc.Password); 
                        }
                        else
                        {
                            Console.WriteLine("Email not found.");    
                        }
    
                        break;
                    
                    case "6":
                        // CREATE FILES AND FOLDERS
                        
                        AccessControlsModel acModel1 = new AccessControlsModel("ryan@email.com", true, true);
                        AccessControlsModel acModel2 = new AccessControlsModel("jerry@email.com", true, true);
                        List<AccessControlsModel> acList = new List<AccessControlsModel>();
                        acList.Add(acModel1); acList.Add(acModel2);
    
                        ObjectId userRootId = ObjectId.GenerateNewId();
                        ObjectId folderAId = ObjectId.GenerateNewId();
                        ObjectId folderBId = ObjectId.GenerateNewId();
                        ObjectId folderCId = ObjectId.GenerateNewId();
                        
                        // userRoot
                        metadataGateway.Insert(new MetadataModel(userRootId, email, email, 0, DateTime.Now, "", "", acList));
                        // folderA in root
                        metadataGateway.Insert(new MetadataModel(folderAId, email, "FolderA", 0, DateTime.Now, "", userRootId.ToString(), acList));
                        // fileA in folderA
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileA", 1, DateTime.Now, "", folderAId.ToString(), acList));
                        // fileB in folderA
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileB", 1, DateTime.Now, "", folderAId.ToString(), acList));
                        // fileC in folderA
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileC", 1, DateTime.Now, "", folderAId.ToString(), acList));
                        // folderB in folderA
                        metadataGateway.Insert(new MetadataModel(folderBId, email, "FolderB", 0, DateTime.Now, "", folderAId.ToString(), acList));
                        // fileD in folderB
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileD", 1, DateTime.Now, "", folderBId.ToString(), acList));
                        // folderC in root
                        metadataGateway.Insert(new MetadataModel(folderCId, email, "FolderC", 0, DateTime.Now, "", userRootId.ToString(), acList));
                        // fileE in folderC
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileE", 1, DateTime.Now, "", folderCId.ToString(), acList));
                        // fileF in root
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileF", 1, DateTime.Now, "", userRootId.ToString(), acList));
                        // fileG in root
                        metadataGateway.Insert(new MetadataModel(ObjectId.GenerateNewId(), email, "FileG", 1, DateTime.Now, "", userRootId.ToString(), acList));
                        
                        metadataGateway.Save();
                        
                        break;
                    
                    case "7":
                        // View content of user's root folder
                        
                        Console.WriteLine("Enter the email of the user:");
                        email = Console.ReadLine();
                        
                        var content = metadataGateway.GetChildren(email);
                        if (content != null)
                        {
                            Console.WriteLine("\nTop level files and folders:\n");
                            var contentEnumerator = content.GetEnumerator();
                            while (contentEnumerator.MoveNext())
                            {
                                Console.WriteLine(contentEnumerator.Current?.name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Email not found.");
                        }
                        
                        break;
                       
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
        
        
        
        
        /* --- ORIGINAL ---
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        */
    }
}
