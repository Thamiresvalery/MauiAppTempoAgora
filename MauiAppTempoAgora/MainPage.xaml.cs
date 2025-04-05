using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using Microsoft.Maui.Networking;  // Para usar a verificação de conectividade

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verificar se há conexão com a internet
                if (!Connectivity.NetworkAccess.HasFlag(NetworkAccess.Internet))
                {
                    lbl_res.Text = "Usuário sem conexão com a internet.";
                    return;
                }

                // Verificar se o campo cidade não está vazio
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    // Tenta obter a previsão para a cidade informada
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        // Se os dados da previsão foram encontrados
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Descricao: {t.description} \n" +
                                         $"Velocidade: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility} \n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        // Se não foi possível obter a previsão para a cidade
                        lbl_res.Text = "Cidade não localizada.";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (Exception ex)
            {
                // Exceção inesperada, exibe mensagem de erro
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

