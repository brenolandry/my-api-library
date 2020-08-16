using LibraryApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Services
{
    public class LivroService
    {
        private const string URI_BASE = "http://192.168.70.5:81";
        public static async Task<LivroRespostaLista> GetAll()
        {
            // paginação padrão... implementar no final
            Paginador pager = new Paginador()
            {
                pagina = 0,
                totalPaginas = 10,
                registroPorPagina = 10,
                totalRegistros = 100
            };

            using (HttpClient httpClient = new HttpClient())
            {
                string endpoint = URI_BASE + "/Api/Books";

                StringContent content = new StringContent(JsonConvert.SerializeObject(pager), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(endpoint, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    LivroRespostaLista respostaLista = JsonConvert.DeserializeObject<LivroRespostaLista>(await response.Content.ReadAsStringAsync());

                    return respostaLista;
                }
            }
        }

        public static async Task<Livro> Get(int id)
        {
            Livro book = new Livro();

            using (HttpClient httpClient = new HttpClient())
            {
                string endpoint = URI_BASE + "/Api/Books/" + id;

                using (var response = await httpClient.GetAsync(endpoint))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    book = JsonConvert.DeserializeObject<Livro>(apiResponse);

                    return book;
                }
            }
        }

        public static async Task<string> Loan(SolicitacaoEmprestimo loan)
        {
            using (var httpClient = new HttpClient())
            {
                string endpoint = URI_BASE + "/Api/Books/Loan";

                StringContent content = new StringContent(JsonConvert.SerializeObject(loan), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(endpoint, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    return apiResponse;
                }
            }
        }
    }
}
