using MovieApp.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.MVVM.Data
{
    internal class CastContext
    {

        RestRequest request;

        public CastContext()
        {
            request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1MGY4MThhZjQ5NTZkMTQ1MzM0YmUzM2JjYTA1NWRiNSIsInN1YiI6IjY1ZDcyYmExOTk3NGVlMDE3YjA2ODg5ZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FeaxWpJTIZTSuCTVk3_jPAaqaY_tg7EC011g8VCpmyA");
        }

        public async Task<List<Actor>> GetCastByMovieId(int movieId)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{movieId}/credits?language=pt-PT");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            List<Actor> cast = new List<Actor>();

            var tempCast = JsonConvert.DeserializeObject<dynamic>(response.Content);

            foreach (var actor in tempCast["cast"]) 
            {
                if (cast.Count() < 11)
                {
                    cast.Add(new Actor
                    {
                        Id = actor["id"],
                        Name = actor["name"],
                        Character = actor["character"],
                        ProfilePath = string.Join("", [Image.BaseUrl, Image.ProfileSize.H632, actor["profile_path"]])
                    });
                }
            }

            return cast;
        }
    }
}
