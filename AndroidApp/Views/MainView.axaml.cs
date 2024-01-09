
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AndroidApp.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using ReactiveUI;

namespace AndroidApp.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }


    private async void Ledopen_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            using (HttpClient hc = new HttpClient())
            {
                var res = await hc.GetAsync("http://led.sttrd.xyz/led/on");

                var str = await res.Content.ReadAsStreamAsync();

                var jsoned = await JsonSerializer.DeserializeAsync<HttpResultModel>(str);
                Status.Content = jsoned?.status;
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);

        }
    }

    private async void Ledclose_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            using (HttpClient hc = new HttpClient())
            {
               var res = await hc.GetAsync("http://led.sttrd.xyz/led/off");

               var str = await res.Content.ReadAsStreamAsync();

               var jsoned = await JsonSerializer.DeserializeAsync<HttpResultModel>(str);
               Status.Content = jsoned?.status;
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception.Message);

        }
    }



    public record HttpResultModel(string status);
}
