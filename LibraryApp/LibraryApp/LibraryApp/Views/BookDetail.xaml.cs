using LibraryApp.Models;
using LibraryApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetail : ContentPage
    {
        private Livro _livro;
        public BookDetail(Livro livro)
        {
            InitializeComponent();

            _livro = livro;

            if (livro.imagemCapa != null)
            {
                imgCapa.Source = new UriImageSource()
                {
                    Uri = new Uri(livro.imagemCapa)
                };
            }

            lblTitulo.Text = livro.titulo;
            lblAutor.Text = livro.autor;
            lblDescricao.Text = livro.descricao;
            lblDisponivel.Text = livro.disponivel ? "Disponível" : "Indisponível";
            btnEmprestar.IsEnabled = livro.disponivel;
        }

        private async void btnEmprestar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoanBook(_livro));
        }
    }
}