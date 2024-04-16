using MovieApp.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Windows.Input;

namespace MovieApp.MVVM.Data
{
    internal class MovieContext
    {
        RestRequest request;

        public MovieContext()
        {
            request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1MGY4MThhZjQ5NTZkMTQ1MzM0YmUzM2JjYTA1NWRiNSIsInN1YiI6IjY1ZDcyYmExOTk3NGVlMDE3YjA2ODg5ZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FeaxWpJTIZTSuCTVk3_jPAaqaY_tg7EC011g8VCpmyA");
        }

        public async Task<List<Movie>?> GetMovies(string uri, string imgSize)
        {
            var options = new RestClientOptions(uri);
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            List<Movie>? movies = null;

            if (response.Content != null)
            {
                var deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);

                if (deserializedJson != null)
                {
                    movies = new List<Movie>();

                    foreach (var movie in deserializedJson["results"])
                    {
                        if (movie["poster_path"] != null)
                        {
                            movies.Add(new Movie
                            {
                                Id = movie["id"],
                                Title = movie["title"],
                                Overview = movie["overview"],
                                ReleaseDate = movie["release_date"],
                                PosterPath = string.Join("", [Image.BaseUrl, imgSize, movie["poster_path"]])
                            });
                        }
                    }
                }
            }

            return movies;
        }

        public async Task<Movie?> GetMovie(string uri, string imgSize)
        {
            var options = new RestClientOptions(uri);
            var client = new RestClient(options);

            var response = await client.GetAsync(request);

            Movie? movie = null;

            if (response.Content != null)
            {
                var deserializedJson = JsonConvert.DeserializeObject<dynamic>(response.Content);

                if (deserializedJson != null)
                {
                    movie = new Movie
                    {
                        Id = deserializedJson["id"],
                        Title = deserializedJson["title"],
                        Overview = deserializedJson["overview"],
                        ReleaseDate = deserializedJson["release_date"],
                        PosterPath = string.Join("", [Image.BaseUrl, imgSize, deserializedJson["poster_path"]])
                    };

                    movie.ProductionCompanies = GetProductionCompanies(deserializedJson);
                }
            }

            return movie;
        }

        public List<ProductionCompany>? GetProductionCompanies(dynamic deserializedJson)
        {
            List<ProductionCompany>? productionCompanies = null;

            if (deserializedJson != null)
            {
                productionCompanies = new List<ProductionCompany>();
                
                foreach (var company in deserializedJson["production_companies"])
                {
                    if (company["logo_path"] != null)
                    {
                        productionCompanies.Add(new ProductionCompany
                        {
                            Id = company["id"],
                            Name = company["name"],
                            LogoPath = string.Join("", [Image.BaseUrl, Image.LogoSize.W154, company["logo_path"]])
                        });
                    }
                }
            }

            return productionCompanies;
        }

        public async Task<int> GetMovieId(string movieName)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/search/movie?query={movieName}&include_adult=false&language=pt-PT&page=1");
            var client = new RestClient(options);

            var response = await client.GetAsync(request);

            if (response.Content != null)
            {
                var movieTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

                if (movieTemp != null)
                {
                    return movieTemp["results"][0]["id"];
                }
            }

            return 0;
        }

        public async Task<List<Movie>?> GetTheaterMovies()
        {
            var uri = "https://api.themoviedb.org/3/movie/now_playing?language=pt-PT&page=1&region=PT";
            var posterSize = Image.PosterSize.W352;

            return await GetMovies(uri, posterSize);
        }

        public async Task<List<Movie>?> GetLatestMovies()
        {
            var uri = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=PT&page=1&primary_release_date.lte=2024-04-05&region=PT&sort_by=primary_release_date.desc";
            var posterSize = Image.PosterSize.W185;

            return await GetMovies(uri, posterSize);
        }


        public async Task<List<Movie>?> GetPopularMovies()
        {
            var uri = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-PT&page=1&sort_by=popularity.desc";
            var posterSize = Image.PosterSize.W352;

            return await GetMovies(uri, posterSize);
        }

        public async Task<List<Movie>?> GetMoviesByGenre(int genreId, int pageNumber)
        {
            var uri = $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-PT&page={pageNumber}&sort_by=popularity.desc&with_genres={genreId}";
            var posterSize = Image.PosterSize.W352;

            return await GetMovies(uri, posterSize);
        }

        public async Task<Movie?> GetMovieById(int movieId)
        {
            var uri = $"https://api.themoviedb.org/3/movie/{movieId}?language=pt-PT";
            var posterSize = Image.PosterSize.W780;

            return await GetMovie(uri, posterSize);
        }
    }
}
