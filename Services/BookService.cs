
using bookapi_minimal.AppContext;
using bookapi_minimal.Contracts;
using bookapi_minimal.Interfaces;
using bookapi_minimal.Models;
using Microsoft.EntityFrameworkCore;

namespace bookapi_minimal.Services
{
    public class BookService : IBookService
    {
          private readonly ApplicationContext _context; // Database context
        private readonly ILogger<BookService> _logger; // Logger for logging information and error
          // Constructor to initialize the database context and logger
        public BookService(ApplicationContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
           /// Add a new book
        /// </summary>
        /// <param name="createBookRequest">Book request to be added</param>
        /// <returns>Details of the created book</returns>
        public async Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest)
        {
            try
            {
                var book = new BookModel
                {
                    Title = createBookRequest.Title,
                    Author = createBookRequest.Author,
                    Description = createBookRequest.Description,
                    Category = createBookRequest.Category,
                    Language = createBookRequest.Language,
                    TotalPages = createBookRequest.TotalPages
                };

                // Add the book to the database
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Book added successfully.");

                // Return the details of the created book
                return new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding book: {ex.Message}");
                throw;
            }
        }

          /// <summary>
        /// Get a book by its ID
        /// </summary>
        /// <param name="id">ID of the book</param>
        /// <returns>Details of the book</returns>
        public async Task<BookResponse>  GetBookByIdAsync(Guid id)
        {
            try
            {
                // Find the book by its ID
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return null;
                }

                // Return the details of the book
                return new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving book: {ex.Message}");
                throw;
            }
        }

      
         
  /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>List of all books</returns>
        public async Task<IEnumerable<BookResponse>> GetBooksAsync()
        {
            try
            {
                // Get all books from the database
                var books = await _context.Books.ToListAsync();

                // Return the details of all books
                return books.Select(book => new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving books: {ex.Message}");
                throw;
            }
        }
      

         /// <summary>
        /// Update an existing book
        /// </summary>
        /// <param name="id">ID of the book to be updated</param>
        /// <param name="book">Updated book model</param>
        /// <returns>Details of the updated book</returns>
        public async Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest book)
        {
            try
            {
                // Find the existing book by its ID
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return null;
                }

                // Update the book details
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Description = book.Description;
                existingBook.Category = book.Category;
                existingBook.Language = book.Language;
                existingBook.TotalPages = book.TotalPages;

                // Save the changes to the database
                await _context.SaveChangesAsync();
                _logger.LogInformation("Book updated successfully.");

                // Return the details of the updated book
                return new BookResponse
                {
                    Id = existingBook.Id,
                    Title = existingBook.Title,
                    Author = existingBook.Author,
                    Description = existingBook.Description,
                    Category = existingBook.Category,
                    Language = existingBook.Language,
                    TotalPages = existingBook.TotalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book: {ex.Message}");
                throw;
            }
        }

    

        /// <summary>
        /// Delete a book by its ID
        /// </summary>
        /// <param name="id">ID of the book to be deleted</param>
        /// <returns>True if the book was deleted, false otherwise</returns>
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            try
            {
                // Find the book by its ID
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return false;
                }

                // Remove the book from the database
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Book with ID {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting book: {ex.Message}");
                throw;
            }
        }

    }
}