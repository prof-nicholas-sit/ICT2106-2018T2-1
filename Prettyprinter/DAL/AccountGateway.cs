using System;
using Prettyprinter.Models;

namespace Prettyprinter.DAL
{
    public class AccountGateway : DataGateway<AccountModel>
    {
        
        public AccountGateway(DbObj dbObject) : base(dbObject)
        {
        }

        public AccountModel SelectByEmail(string email)
        {
            var resultInArray = db.SelectWhere(typeof(AccountModel), "Email", email).GetEnumerator();
                
            // need to get the first element of the returned enumerable
            while (resultInArray.MoveNext())
            {
                return (AccountModel)resultInArray.Current;
            }

            return null;
        }

        public bool ValidateLogin(string email, string pw)
        {
            var acc = SelectByEmail(email);
            return acc != null && pw.Equals(acc.Password);
        }

        public bool SetUserPassword(string email, string pw)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Password = pw;
            Update(acc);
            
            return true;
        }
        
        public bool SetUserBio(string email, string bio)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Bio = bio;
            Update(acc);
            
            return true;
        }
        
        public bool SetUserDisplayPicUrl(string email, string url)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.DisplayPicURL = url;
            Update(acc);
            
            return true;
        }
        
        public bool SetUserTitle(string email, string title)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Title = title;
            Update(acc);
            
            return true;
        }
        
        public bool SetUserName(string email, string name)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Name = name;
            Update(acc);
            
            return true;
        }
        
        public bool SetUserBirthDate(string email, int year, int month, int day)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            acc.Birthday = new DateTime(year, month, day);
            Update(acc);
            
            return true;
        }

        public bool DeleteAccount(string email)
        {
            var acc = SelectByEmail(email);
            if (acc == null) return false;
            
            // TODO: delete all files belonging to this account
            // TODO: delete all access controls??
            Delete(acc);

            return true;
        }
    }
}