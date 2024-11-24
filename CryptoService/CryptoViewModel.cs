using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;

public class CryptoViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Cryptocurrency> _cryptos;

    public ObservableCollection<Cryptocurrency> Cryptos
    {
        get { return _cryptos; }
        set
        {
            _cryptos = value;
            OnPropertyChanged();
        }
    }

    public CryptoViewModel()
    {
        Cryptos = new ObservableCollection<Cryptocurrency>();
        LoadTopCryptos();
    }

    public async void LoadTopCryptos()
    {
        try
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://api.coincap.io/v2/assets");

            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("API response is empty.");
                return;
            }

            var jsonResponse = JsonSerializer.Deserialize<ApiResponse>(response);
            if (jsonResponse?.Data != null)
            {
                Console.WriteLine($"Received {jsonResponse.Data.Count} cryptocurrencies.");
                Cryptos.Clear();
                foreach (var crypto in jsonResponse.Data)
                {
                    Console.WriteLine($"Adding crypto: {crypto.Name}");
                    Console.WriteLine($"Original values: PriceUsd={crypto.PriceUsd}, MarketCapUsd={crypto.MarketCapUsd}, VolumeUsd24Hr={crypto.VolumeUsd24Hr}, ChangePercent24Hr={crypto.ChangePercent24Hr}");

                    decimal priceUsd = 0, marketCapUsd = 0, volumeUsd24Hr = 0, changePercent24Hr = 0;

                    if (!string.IsNullOrEmpty(crypto.PriceUsd))
                        decimal.TryParse(crypto.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out priceUsd);

                    if (!string.IsNullOrEmpty(crypto.MarketCapUsd))
                        decimal.TryParse(crypto.MarketCapUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out marketCapUsd);

                    if (!string.IsNullOrEmpty(crypto.VolumeUsd24Hr))
                        decimal.TryParse(crypto.VolumeUsd24Hr, NumberStyles.Any, CultureInfo.InvariantCulture, out volumeUsd24Hr);

                    if (!string.IsNullOrEmpty(crypto.ChangePercent24Hr))
                        decimal.TryParse(crypto.ChangePercent24Hr, NumberStyles.Any, CultureInfo.InvariantCulture, out changePercent24Hr);

                    Console.WriteLine($"Parsed values: PriceUsd={priceUsd:F2}, MarketCapUsd={marketCapUsd:F2}, VolumeUsd24Hr={volumeUsd24Hr:F2}, ChangePercent24Hr={changePercent24Hr:F2}");

                    Cryptos.Add(new Cryptocurrency
                    {
                        Id = crypto.Id,
                        Name = crypto.Name,
                        Symbol = crypto.Symbol,
                        PriceUsd = priceUsd.ToString("F2"),
                        MarketCapUsd = marketCapUsd.ToString("F2"),
                        VolumeUsd24Hr = volumeUsd24Hr.ToString("F2"),
                        ChangePercent24Hr = changePercent24Hr.ToString("F2"),
                    });
                }
            }
            else
            {
                Console.WriteLine("API returned empty or invalid data.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
