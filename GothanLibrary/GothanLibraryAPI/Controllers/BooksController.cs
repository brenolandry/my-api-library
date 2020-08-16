using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Models;

namespace GothanLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private const string URI_BASE = "https://bibliotecagotham.azurewebsites.net";
        /// <summary>
        /// Busca todos os livros do acervo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAll(Paginador pager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object bookResult = new object();

                using (HttpClient httpClient = new HttpClient())
                {
                    string endpoint = URI_BASE + "/Acervo/obterAcervo";

                    StringContent content = new StringContent(JsonConvert.SerializeObject(pager), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        bookResult = JsonConvert.DeserializeObject(apiResponse);
                    }
                }

                return Ok(bookResult);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Busca livro do acervo pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "GetBook")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Livro book = new Livro();

                using (HttpClient httpClient = new HttpClient())
                {
                    string endpoint = URI_BASE + "/Acervo/DetalharLivro/" + id;

                    using (var response = await httpClient.GetAsync(endpoint))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        book = JsonConvert.DeserializeObject<Livro>(apiResponse);
                    }
                }

                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Ação de emprestar livro
        /// </summary>
        /// <param name="pTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Loan")]
        public async Task<IActionResult> LoanBook([FromBody] SolicitacaoEmprestimo loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string result = "";

                // remove máscara do telefone para api
                loan.telefone = loan.telefone.Replace("(", "").Replace(")", "").Replace("-", "").Trim();

                using (var httpClient = new HttpClient())
                {
                    string endpoint = URI_BASE + "/Acervo/EmprestarLivro";

                    StringContent content = new StringContent(JsonConvert.SerializeObject(loan), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(endpoint, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        result = apiResponse;
                    }
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
