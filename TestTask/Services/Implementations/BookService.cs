using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        
        private static readonly DateTime CarolusRexReleaseDate = new DateTime(2012, 5, 25);

        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Возвращает книгу с наибольшей стоимостью опубликованного тиража
        public async Task<Book> GetBook()
        {
            return await _context.Books
                .AsNoTracking()
                .OrderByDescending(b=>b.Price*b.QuantityPublished)
                .FirstOrDefaultAsync();
        }

        //Возвращает книги, в названии которой содержится "Red" и которые опубликованы после выхода альбома "Carolus Rex" группы Sabaton
        public async Task<List<Book>> GetBooks()
        {
            // Дата выхода альбома "Carolus Rex"
            var carolusRexReleaseDate = new DateTime(2012, 5, 25);

            return await _context.Books
                .AsNoTracking()
                .Where(b=>b.Title.Contains("Red") && b.PublishDate > CarolusRexReleaseDate)
                .ToListAsync();
        }
    }
}
