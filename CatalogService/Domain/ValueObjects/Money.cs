using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public Money(decimal amount, string currency)
        {
            this.Amount = amount;
            if (Enum.TryParse<Currency>(currency, out Currency parsedCurrency))
            {
                this.Currency = parsedCurrency;
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
