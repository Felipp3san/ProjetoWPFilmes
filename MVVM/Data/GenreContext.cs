using MovieApp.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.MVVM.Data
{
    internal class GenreContext
    {
        RestRequest request;

        public GenreContext()
        {
            request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1MGY4MThhZjQ5NTZkMTQ1MzM0YmUzM2JjYTA1NWRiNSIsInN1YiI6IjY1ZDcyYmExOTk3NGVlMDE3YjA2ODg5ZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FeaxWpJTIZTSuCTVk3_jPAaqaY_tg7EC011g8VCpmyA");
        }
        
        /// <summary>
        /// Faz uma requisição a API do TheMovieDB 
        /// que retorna os generos de filmes disponíveis.
        /// </summary>
        /// <returns>Lista de generos.</returns>
        public async Task<GenreList> GetGenres()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/genre/movie/list?language=pt");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);
            
            return JsonConvert.DeserializeObject<GenreList>(response.Content);
        }
    }
}
