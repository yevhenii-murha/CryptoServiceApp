using CryptoService.DTOs;
using System;

namespace CryptoService.Model
{
    public readonly record struct Market
    {
        public string ExchangeId { get; }
        public string BaseId { get; }
        public string QuoteId { get; }
        public string BaseSymbol { get; }
        public string QuoteSymbol { get; }
        public decimal VolumeUsd24Hr { get; }
        public decimal PriceUsd { get; }
        public double VolumePercent { get; }

        public Market(string exchangeId, string baseId, string quoteId, string baseSymbol,
            string quoteSymbol, decimal volumeUsd24Hr, decimal priceUsd, double volumePercent)
        {
            ExchangeId = exchangeId ?? throw new ArgumentNullException(nameof(exchangeId));
            BaseId = baseId ?? throw new ArgumentNullException(nameof(baseId));
            QuoteId = quoteId ?? throw new ArgumentNullException(nameof(quoteId));
            BaseSymbol = baseSymbol ?? throw new ArgumentNullException(nameof(baseSymbol));
            QuoteSymbol = quoteSymbol ?? throw new ArgumentNullException(nameof(quoteSymbol));
            VolumeUsd24Hr = volumeUsd24Hr;
            PriceUsd = priceUsd;
            VolumePercent = volumePercent;
        }

        public static explicit operator Market(MarketDatum marketDto)
        {
            var exchangeId = marketDto.exchangeId ?? throw new ArgumentNullException(nameof(marketDto.exchangeId));
            var baseId = marketDto.baseId ?? throw new ArgumentNullException(nameof(marketDto.baseId));
            var quoteId = marketDto.quoteId ?? throw new ArgumentNullException(nameof(marketDto.quoteId));
            var baseSymbol = marketDto.baseSymbol ?? throw new ArgumentNullException(nameof(marketDto.baseSymbol));
            var quoteSymbol = marketDto.quoteSymbol ?? throw new ArgumentNullException(nameof(marketDto.quoteSymbol));

            var volumeUsd24Hr = ParseDecimal(marketDto.volumeUsd24Hr, "volumeUsd24Hr");
            var priceUsd = ParseDecimal(marketDto.priceUsd, "priceUsd");
            var volumePercent = ParseDouble(marketDto.volumePercent, "volumePercent");

            return new Market(exchangeId, baseId, quoteId, baseSymbol, quoteSymbol, volumeUsd24Hr, priceUsd, volumePercent);
        }

        private static decimal ParseDecimal(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0m;
            }

            if (!decimal.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Invalid {fieldName}: {value}");

            return result;
        }

        private static double ParseDouble(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value) || !double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Invalid {fieldName}: {value}");
            return result;
        }
    }
}