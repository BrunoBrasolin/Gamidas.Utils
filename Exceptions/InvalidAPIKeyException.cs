namespace Gamidas.Utils.Exceptions;

public class InvalidAPIKeyException : Exception
{
    public InvalidAPIKeyException() : base("API Key validation failed.")
    {

    }
}
