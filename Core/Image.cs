using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.Core
{
    internal class Image
    {

        private static string baseUrl = string.Empty;

        public static string BaseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(baseUrl))
                {
                    InitializeImgDetails();
                }

                return baseUrl;
            }
            set { baseUrl = value; }
        }


        private static PosterSize posterSize;

        public static PosterSize PosterSize
        {
            get
            {
                if (posterSize == null)
                {
                    InitializeImgDetails();
                }

                return posterSize;
            }

            set { posterSize = value; }
        }

        public static void InitializeImgDetails()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/configuration");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1MGY4MThhZjQ5NTZkMTQ1MzM0YmUzM2JjYTA1NWRiNSIsInN1YiI6IjY1ZDcyYmExOTk3NGVlMDE3YjA2ODg5ZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FeaxWpJTIZTSuCTVk3_jPAaqaY_tg7EC011g8VCpmyA");
            var response = client.GetAsync(request).Result;

            if (response.IsSuccessful && response.Content != null)
            {
                var responseObject = JsonConvert.DeserializeObject<dynamic>(response.Content);

                BaseUrl = responseObject["images"]["base_url"];

                PosterSize = new PosterSize
                {
                    W185 = responseObject["images"]["poster_sizes"][2],
                    W352 = responseObject["images"]["poster_sizes"][3],
                    W500 = responseObject["images"]["poster_sizes"][4],
                    W780 = responseObject["images"]["poster_sizes"][5],
                    Original = responseObject["images"]["poster_sizes"][6]
                };
            }
        }
    }

    internal class PosterSize
    {
        public string W185 { get; set; }
        public string W352 { get; set; }
        public string W500 { get; set; }
        public string W780 { get; set; }
        public string Original { get; set; }
    }
}
