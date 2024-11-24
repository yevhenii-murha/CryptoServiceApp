using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System;
using CryptoService.Model;
using CryptoService.DTOs;

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

            var root = JsonSerializer.Deserialize<Root>(response);

            var cryptos = ToCryptos(root);

            Cryptos.Clear();
            foreach (var crypto in cryptos)
            {
                Cryptos.Add(crypto);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }

    private IEnumerable<Cryptocurrency> ToCryptos(Root? root)
    {
        if (root == null || root.data == null)
        {
            Console.WriteLine("Invalid root data.");
            return Enumerable.Empty<Cryptocurrency>();
        }

        return root.data.Select(cryptoDto => (Cryptocurrency)cryptoDto);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
