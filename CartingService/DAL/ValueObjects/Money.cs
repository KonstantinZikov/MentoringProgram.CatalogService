using DAL.Common;
using DAL.Enums;
using Domain.Exceptions;

namespace DAL.ValueObjects
{
    public class Money : ValueObject
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            if (Enum.TryParse(currency, out Currency parsedCurrency))
            {
                Currency = parsedCurrency;
            }
            else
            {
                throw new UnknowCurrencyException(currency);
            }
        }

        public decimal Amount { get; init; }

        public Currency Currency { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
