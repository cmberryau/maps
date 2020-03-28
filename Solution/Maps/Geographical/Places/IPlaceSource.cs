using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps.Geographical.Places
{
    /// <summary>
    /// Interface for a source of places
    /// </summary>
    public interface IPlaceSource : IDisposable
    {
        /// <summary>
        /// Does the Place source support box queries?
        /// </summary>
        bool SupportsBoxQueries
        {
            get;
        }

        /// <summary>
        /// Does the Place source support range queries?
        /// </summary>
        bool SupportsRangeQueries
        {
            get;
        }

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places at the given coordinate
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<PlaceCategory> GetCategories(Geodetic2d coordinate, double range);

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places for the given coordinate box
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<PlaceCategory> GetCategories(GeodeticBox2d box);

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places at the given coordinate asyncronously
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<PlaceCategory>> GetCategoriesAsync(Geodetic2d coordinate, double range);

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places for the given coordinate box asyncronously
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<PlaceCategory>> GetCategoriesAsync(GeodeticBox2d box);

        /// <summary>
        /// Returns a list of place results for the given coordinate and query string
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(Geodetic2d coordinate, string query, double range);

        /// <summary>
        /// Returns a list of place results for the given coordinate box and query string
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(GeodeticBox2d box, string query);

        /// <summary>
        /// Asyncronously returns a list of place results for the given 
        /// coordinate and query string
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(Geodetic2d coordinate, string query, double range);

        /// <summary>
        /// Asyncronously returns a list of place results for the given 
        /// coordinate box and query string
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(GeodeticBox2d box, string query);

        /// <summary>
        /// Returns a list of place results for the given coordinate and category
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(Geodetic2d coordinate, PlaceCategory category, double range);

        /// <summary>
        /// Returns a list of place results for the given coordinate box and category
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(GeodeticBox2d box, PlaceCategory category);

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate and category
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(Geodetic2d coordinate, PlaceCategory category, double range);

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate box and category
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(GeodeticBox2d box, PlaceCategory category);

        /// <summary>
        /// Returns a list of place results for the given coordinate and categories
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(Geodetic2d coordinate, IList<PlaceCategory> categories, double range);

        /// <summary>
        /// Returns a list of place results for the given coordinate box and categories
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Place> Get(GeodeticBox2d box, IList<PlaceCategory> categories);

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate and categories
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <param name="range">The range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(Geodetic2d coordinate, IList<PlaceCategory> categories,
            double range);

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate box and categories
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Place>> GetAsync(GeodeticBox2d box, IList<PlaceCategory> categories);
    }
}