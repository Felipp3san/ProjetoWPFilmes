using MovieApp.Core;
using MovieApp.MVVM.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Windows;

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

        public async Task<List<Movie>> GetTheaterMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/now_playing?language=pt-PT&page=1&region=PT");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            var moviesTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<Movie> movies = new List<Movie>();

            foreach (var movie in moviesTemp["results"])
            {
                if (movie["poster_path"] != null)
                {
                    movies.Add(new Movie
                    {
                        Id = movie["id"],
                        Title = movie["title"],
                        Overview = movie["overview"],
                        ReleaseDate = movie["release_date"],
                        PosterPath = string.Join("", [Image.BaseUrl, Image.PosterSize.W352, movie["poster_path"]])
                    });
                }
            }

            return movies;
        }

        public async Task<List<Movie>> GetLatestMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=PT&page=1&primary_release_date.lte=2024-04-05&region=PT&sort_by=primary_release_date.desc");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            var moviesTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<Movie> movies = new List<Movie>();

            foreach (var movie in moviesTemp["results"])
            {
                if (movie["poster_path"] != null)
                {
                    movies.Add(new Movie
                    {
                        Id = movie["id"],
                        Title = movie["title"],
                        Overview = movie["overview"],
                        ReleaseDate = movie["release_date"],
                        PosterPath = string.Join("", [Image.BaseUrl, Image.PosterSize.W185, movie["poster_path"]])
                    });
                }
            }

            return movies; 
        }


        public async Task<List<Movie>> GetPopularMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-PT&page=1&sort_by=popularity.desc");
            var client = new RestClient(options);

            var response = await client.GetAsync(request);

            var moviesTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<Movie> movies = new List<Movie>();

            foreach (var movie in moviesTemp["results"])
            {
                if (movie["poster_path"] != null)
                {
                    movies.Add(new Movie
                    {
                        Id = movie["id"],
                        Title = movie["title"],
                        Overview = movie["overview"],
                        ReleaseDate = movie["release_date"],
                        PosterPath = string.Join("", [Image.BaseUrl, Image.PosterSize.W352, movie["poster_path"]])
                    }); ;
                }
            }

            return movies;
        }

        public async Task GetMovieById(int movieId)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/movie/movie_id?language=en-US");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId, int pageNumber)
        {
            var options = new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=pt-PT&page={pageNumber}&sort_by=popularity.desc&with_genres={genreId}");
            var client = new RestClient(options);
            var response = await client.GetAsync(request);

            var moviesTemp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            List<Movie> movies = new List<Movie>();

            foreach (var movie in moviesTemp["results"])
            {
                if (movie["poster_path"] != null)
                {
                    movies.Add(new Movie
                    {
                        Id = movie["id"],
                        Title = movie["title"],
                        Overview = movie["overview"],
                        ReleaseDate = movie["release_date"],  
                        PosterPath = string.Join("", [Image.BaseUrl, Image.PosterSize.W352, movie["poster_path"]])
                    }); ;
                }
            }

            return movies;
        }
    }
}
