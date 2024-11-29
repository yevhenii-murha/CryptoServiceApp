using CryptoService.DTOs;
using System;
using System.Globalization;

namespace CryptoService.Models
{
    public readonly record struct Cryptocurrency
    {
        public string Id { get; }
        public int Rank { get; }
        public string Symbol { get; }
        public string Name { get; }
        public decimal Supply { get; }
        public decimal MaxSupply { get; }
        public decimal MarketCapUsd { get; }
        public decimal VolumeUsd24Hr { get; }
        public decimal PriceUsd { get; }
        public double ChangePercent24Hr { get; }
        public decimal Vwap24Hr { get; }
        public string DisplayMaxSupply => MaxSupply == 0 ? "N/A" : MaxSupply.ToString();

        public Cryptocurrency(string id, int rank, string symbol, string name, decimal supply,
            decimal maxSupply, decimal marketCapUsd, decimal volumeUsd24Hr, decimal priceUsd,
            double changePercent24Hr, decimal vwap24Hr)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Rank = rank;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Supply = supply;
            MaxSupply = maxSupply;
            MarketCapUsd = marketCapUsd;
            VolumeUsd24Hr = volumeUsd24Hr;
            PriceUsd = priceUsd;
            ChangePercent24Hr = changePercent24Hr;
            Vwap24Hr = vwap24Hr;
        }

        public static explicit operator Cryptocurrency(Datum cryptoDto)
        {
            var id = cryptoDto.id ?? throw new ArgumentNullException(nameof(cryptoDto.id));

            if (!int.TryParse(cryptoDto.rank, out var rank))
                throw new FormatException($"Invalid rank: {cryptoDto.rank}");

            var symbol = cryptoDto.symbol ?? throw new ArgumentNullException(nameof(cryptoDto.symbol));
            var name = cryptoDto.name ?? throw new ArgumentNullException(nameof(cryptoDto.name));

            var supply = ParseDecimal(cryptoDto.supply, "supply");
            var maxSupply = ParseDecimal(cryptoDto.maxSupply, "maxSupply");
            var marketCapUsd = ParseDecimal(cryptoDto.marketCapUsd, "marketCapUsd");
            var volumeUsd24Hr = ParseDecimal(cryptoDto.volumeUsd24Hr, "volumeUsd24Hr");
            var priceUsd = ParseDecimal(cryptoDto.priceUsd, "priceUsd");
            var changePercent24Hr = ParseDouble(cryptoDto.changePercent24Hr, "changePercent24Hr");
            var vwap24Hr = ParseDecimal(cryptoDto.vwap24Hr, "vwap24Hr");

            return new Cryptocurrency(id, rank, symbol, name, supply, maxSupply, marketCapUsd, volumeUsd24Hr, priceUsd, changePercent24Hr, vwap24Hr);
        }

        private static decimal ParseDecimal(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0m;
            }

            if (!decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Invalid {fieldName}: {value}");

            return result;
        }

        private static double ParseDouble(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value) || !double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Invalid {fieldName}: {value}");
            return result;
        }
    }
}