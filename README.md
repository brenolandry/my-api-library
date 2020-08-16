# my-api-library

O projeto GothanLibrary refere-se a api em dotnet core 3.1

E o projeto LibraryApp refere-se ao aplicativo Xamarin

Particularidades para compilar:
- Publicar a API GothanLibrary em um servidor web acessível
- Na classe \Services\LivroService.cs, na linha de código: private const string URI_BASE = "http://192.168.70.5:81";, informar o endereço IP da API publicada.
