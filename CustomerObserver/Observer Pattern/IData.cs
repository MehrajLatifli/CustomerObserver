namespace CustomerObserver
{
    public interface IData
    {

         string ProductName { get; set; }

         string CustomerEmail { get; set; }
        string AboutProduct { get; set; }

        string FullData { get; }

        void GetMessage();
    }




}
