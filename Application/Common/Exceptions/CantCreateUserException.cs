
namespace Application.common.Exceptions
{
    public class CantCreateUserException : Exception
    {
        public CantCreateUserException() : base()
        {

        }

        public CantCreateUserException(
            string
            message) : base(message)
        {

        }


    }
}