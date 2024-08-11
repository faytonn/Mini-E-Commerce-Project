namespace Mini_E_Commerce_Project.Exceptions
{
    public class OrderAlreadyCanceledException : Exception
    {
        public OrderAlreadyCanceledException(string message) : base(message) 
        {

        }
    }
}
