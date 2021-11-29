namespace WebAutopark.Core.Exceptions
{
    public class InvalidArgumentException : BaseException
    {
        public InvalidArgumentException(string parameterName) : base($"Invalid argument value of {parameterName}")
        {
        }
    }
}