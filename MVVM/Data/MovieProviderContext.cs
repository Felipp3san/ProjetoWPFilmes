using MovieApp.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Dynamic;

namespace MovieApp.MVVM.Data
{
    internal class MovieProviderContext
    {
        RestRequest request;

        public MovieProviderContext()
        {
            request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1MGY4MThhZjQ5NTZkMTQ1MzM0YmUzM2JjYTA1NWRiNSIsInN1YiI6IjY1ZDcyYmExOTk3NGVlMDE3YjA2ODg5ZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FeaxWpJTIZTSuCTVk3_jPAaqaY_tg7EC011g8VCpmyA");
        }

        public async Task<List<MovieProvider>?> GetProvidersByMovieId(int movieId)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{movieId}/watch/providers");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            var providersTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<MovieProvider>? providers = null; 

            try
            {
                providers = new List<MovieProvider>();

                foreach (var provider in providersTemp["results"]["PT"]["flatrate"])
                {
                    providers.Add(new MovieProvider
                    {
                        Id = provider["provider_id"],
                        Name = provider["provider_name"],
                        LogoPath = string.Join("", [Image.BaseUrl, Image.LogoSize.W154, provider["logo_path"]])
                    });
                }
            }
            catch {}

            return providers;
        }
    }

}
