using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
//namespace MVCAssignment
namespace MoviesDBExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source= 192.168.10.18; database:TrainingDB; user id = TrainingDB_User; password  = 'X1;xbhpUN#a5eGHt4ohF'";

            // Create a new movie
            Movie newMovie = new Movie()
            {
                Mid = 1,
                Moviename = "Titanic",
                DateofRelease = new DateTime(1992, 6, 9)
            };

            // Insert 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand insertCommand = new SqlCommand("INSERT INTO Movie (Mid, Moviename, DateofRelease) VALUES (@Mid, @Moviename, @DateofRelease)", connection);
                insertCommand.Parameters.AddWithValue("@Mid", newMovie.Mid);
                insertCommand.Parameters.AddWithValue("@Moviename", newMovie.Moviename);
                insertCommand.Parameters.AddWithValue("@DateofRelease", newMovie.DateofRelease);
                insertCommand.ExecuteNonQuery();
            }

            // Update 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand updateCommand = new SqlCommand("UPDATE Movie SET Moviename = @Moviename WHERE Mid = @Mid", connection);
                updateCommand.Parameters.AddWithValue("@Mid", newMovie.Mid);
                updateCommand.Parameters.AddWithValue("@Moviename", "Titanic: Jack & Rose");
                updateCommand.ExecuteNonQuery();
            }

            // Delete 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM Movie WHERE Mid = @Mid", connection);
                deleteCommand.Parameters.AddWithValue("@Mid", newMovie.Mid);
                deleteCommand.ExecuteNonQuery();
            }

            // Display all movies released in a given year using LINQ
            int year = 1993;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT Mid, Moviename, DateofRelease FROM Movie", connection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                List<Movie> movies = new List<Movie>();
                while (reader.Read())
                {
                    Movie movie = new Movie()
                    {
                        Mid = (int)reader["Mid"],
                        Moviename = (string)reader["Moviename"],
                        DateofRelease = (DateTime)reader["DateofRelease"]
                    };
                    if (movie.DateofRelease.Year == year)
                    {
                        movies.Add(movie);
                    }
                }
                Console.WriteLine($"Movies released in {year}:");
                foreach (Movie movie in movies)
                {
                    Console.WriteLine($"{movie.Moviename} ({movie.DateofRelease.ToShortDateString()})");
                }
            }
        }
    }

    class Movie
    {
        public int Mid { get; set; }
        public string Moviename { get; set; }
        public DateTime DateofRelease { get; set; }
    }
}

