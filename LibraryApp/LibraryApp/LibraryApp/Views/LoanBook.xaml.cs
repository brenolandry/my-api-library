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
    public partial class LoanBook : ContentPage
    {
        private Livro _livro;
        public LoanBook(Livro livro)
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
        }

        private async void btnSolicitarEmprestimo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    await DisplayAlert("Atenção!", "O Nome deve ser informado", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(txtTelefone.Text))
                {
                    await DisplayAlert("Atenção!", "O Telefone deve ser informado", "OK");
                    return;
                }

                SolicitacaoEmprestimo loan = new SolicitacaoEmprestimo()
                {
                    nome = txtName.Text,
                    telefone = txtTelefone.Text,
                    livro = _livro
                };

                string result = await LivroService.Loan(loan);

                if (result.Equals("true"))
                {
                    await DisplayAlert("Sucesso", "Solicitação de empréstimo realizada", "OK");
                    await Navigation.PushAsync(new Books());
                }
                else
                {
                    await DisplayAlert("Atenção!", result, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}