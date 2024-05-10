namespace Domain.Exceptions;

public class UnknowCurrencyException : Exception
{
    public UnknowCurrencyException(string code)
        : base($"Currency \"{code}\" is unknown.")
    {
    }
}
