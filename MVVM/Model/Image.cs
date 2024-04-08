using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.MVVM.Model
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

        private static LogoSize logoSize;

        public static LogoSize LogoSize
        {
            get 
            { 
                if (logoSize == null)
                {
                    InitializeImgDetails();
                }

                return logoSize;
            }
            set 
            {
                logoSize = value; 
            }
        }

        private static ProfileSize profileSize;

        public static ProfileSize ProfileSize 
        {
            get 
            {
                if (profileSize == null)
                {
                    InitializeImgDetails();
                }
                return profileSize; 
            }
            set 
            {
                profileSize = value; 
            }
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

                LogoSize = new LogoSize
                {
                    W45 = responseObject["images"]["logo_sizes"][0],
                    W95 = responseObject["images"]["logo_sizes"][1],
                    W154 = responseObject["images"]["logo_sizes"][2],
                    W185 = responseObject["images"]["logo_sizes"][3],
                    W300 = responseObject["images"]["logo_sizes"][4],
                    W500 = responseObject["images"]["logo_sizes"][5],
                    Original = responseObject["images"]["logo_sizes"][6]
                };

                ProfileSize = new ProfileSize 
                {
                    W45 = responseObject["images"]["profile_sizes"][0],
                    W185 = responseObject["images"]["profile_sizes"][1],
                    H632 = responseObject["images"]["profile_sizes"][2],
                    Original = responseObject["images"]["profile_sizes"][3]
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

    internal class LogoSize
    {
        public string W45 { get; set; }
        public string W95 { get; set; }
        public string W154 { get; set; }
        public string W185 { get; set; }
        public string W300 { get; set; }
        public string W500 { get; set; }
        public string Original { get; set; }
    }

    internal class ProfileSize
    {
        public string W45 { get; set; }
        public string W185 { get; set; }
        public string H632 { get; set; }
        public string Original { get; set; }
    }
}
