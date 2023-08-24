using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Dtos;
using Workshop.Models;
using Workshop.Models.Enums;

namespace Workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet] //localstorage:[port]/api/movies
        public ActionResult <List<MovieDto>> Get()
        {
            try
            {
                var moviesDb = StaticDb.Movies;
                //select all movies via MovieDto
                var allMovies = moviesDb.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }).ToList();
                
                return Ok(allMovies);
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpGet("{id}")] //localstorage:[port]/api/movies/1
        public ActionResult<MovieDto> GetMovieById (int id)
        {
            try
            {
                if( id <= 0)
                {
                    return BadRequest("The Id cannot be negative");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if( movieDb == null)
                {
                    return NotFound($"Movie with Id {id} does not exist!");
                }
                return Ok(new MovieDto
                {
                    Description = movieDb.Description,
                    Title = movieDb.Title,
                    Genre = movieDb.Genre,
                    Year = movieDb.Year,
                });
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpGet("queryString")] //localstorage:[port]/api/movies/queryString?
        public ActionResult<MovieDto> GetByIdQuerry(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("The Id cannot be negative");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound($"Movie with Id {id} does not exist!");
                }
                return Ok(new MovieDto
                {
                    Description = movieDb.Description,
                    Title = movieDb.Title,
                    Genre = movieDb.Genre,
                    Year = movieDb.Year,
                });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> FilterMoviesFromQuery(int? genre, int? year)
        {
            try
            {
                if(genre == null && year == null)
                {
                    return BadRequest("You have to enter at least one search parameter!");
                }
                if (genre.HasValue)
                {
                    var enumValues = Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>().Select(x => (int)x).ToList();
                    
                    if (!enumValues.Contains(genre.Value))
                    {
                        return BadRequest("Invalid genre value");
                    }
                }
                if (year == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }
                if(genre == null)
                {
                    List<Movie> moviesDb = StaticDb.Movies.Where(x => x.Year == year).ToList();
                    return Ok(moviesDb.Select(x => new MovieDto
                    {
                        Description = x.Description,
                        Genre = x.Genre,
                        Title = x.Title,
                        Year = x.Year
                    }));
                }
                List<Movie> movies = StaticDb.Movies.Where(x => x.Year == year && x.Genre == (GenreEnum)genre).ToList();
                return Ok(movies.Select(x => new MovieDto
                {
                    Description = x.Description,
                    Genre = x.Genre,
                    Title = x.Title,
                    Year = x.Year
                }));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
            try
            {
                Movie movieDb = StaticDb.Movies.FirstOrDefault(x=> x.Id == movie.Id); 
                if( movieDb == null)
                {
                    return NotFound("No movie with found!");
                }
                if(string.IsNullOrEmpty(movie.Title))
                {
                    return BadRequest("Title must not be empty!");
                }
                if(movie.Year <= 0)
                {
                    return BadRequest("Year must not be empty!");
                }
                if(!string.IsNullOrEmpty(movie.Description) && movie.Description.Length > 250 ) 
                {
                    return BadRequest("Description must not be longer than 250 characters!");
                }
                movieDb.Year = movie.Year;
                movieDb.Title = movie.Title;
                movieDb.Description = movie.Description;
                movieDb.Genre = movie.Genre;
                
                return NoContent();

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpPost("addMovie")]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            try
            {
                if (string.IsNullOrEmpty(movieDto.Title))
                {
                    return BadRequest("Title must not be empty!");
                }
                if(!string.IsNullOrEmpty(movieDto.Description) && movieDto.Description.Length > 250)
                {
                    return BadRequest("Description cannot be longer than 250 characters!");
                }
                if(movieDto.Year <= 0)
                {
                    return BadRequest("Year cannot be negative!");
                }
                Movie movie = new Movie()
                {
                    Title = movieDto.Title,
                    Description = movieDto.Description,
                    Genre = movieDto.Genre,
                    Year = movieDto.Year,
                    Id = ++StaticDb.MovieId,
            };
                StaticDb.Movies.Add(movie);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMovieById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("The id can not be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if(movieDb == null)
                {
                    return NotFound($"Movie with Id {id} was not found!");
                }
                
                StaticDb.Movies.Remove(movieDb);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
        [HttpDelete]
        public IActionResult DeleteMovie([FromBody] int id)
        {
            try
            {
                if (id < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The id cannot be negative!");
                }
                var movieDb = StaticDb.Movies.FirstOrDefault(x => x.Id == id);
                if (movieDb == null)
                {
                    return NotFound("Movie was not found");
                }
                StaticDb.Movies.Remove(movieDb);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }
    }
}
