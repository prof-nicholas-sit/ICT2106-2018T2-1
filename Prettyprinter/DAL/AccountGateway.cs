namespace DBLayer.DAL
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
    }
}