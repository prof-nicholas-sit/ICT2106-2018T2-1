namespace DBLayer.DAL
{
    public class ItemGateway : DataGateway<ItemModel>
    {
        public ItemGateway(DbObj dbObject) : base(dbObject)
        {
        }
        
    }
}