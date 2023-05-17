namespace NEWSHORE_AIR_API.ViewModel
{
    public class MyCustomException : Exception
    {

        public MyCustomException() : base("Lo sentimos ocurrión un error.") 
        {
        }

        public MyCustomException(string mensaje) : base(mensaje)
        {
        }

        public MyCustomException(string mensaje, Exception innerException) : base(mensaje, innerException)
        {
        }
    }
}
