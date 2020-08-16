using LibraryApp.Models;
using LibraryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Books : ContentPage
    {
        LivroRespostaLista respostaLivros;

        public Books()
        {
            InitializeComponent();

            LoadListView();
        }

        private async void LoadListView()
        {
            try
            {
                respostaLivros = await LivroService.GetAll();

                ListViewLivros.ItemsSource = respostaLivros.lista.OrderBy(x => x.titulo).ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = (Livro)e.Item;
            await Navigation.PushAsync(new BookDetail(details));
        }

        private void SearchBarLivros_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = SearchBarLivros.Text;

            List<Livro> listLivroFiltro =
                respostaLivros.lista.Where(x => x.titulo.ToLower().Contains(filtro.ToLower())
                || x.autor.ToLower().Contains(filtro.ToLower())).ToList();

            ListViewLivros.ItemsSource = listLivroFiltro;
        }        
    }
}