using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Воозвращает автора, который написал книгу с самым длинным названием
        //в случае если таких авторов окажется несколько, возвращает автора с наименьшим Id
        public async Task<Author> GetAuthor()
        {
            return await _context.Books
                .OrderByDescending(b => b.Title.Length)
                .ThenBy(b => b.AuthorId)
                .Select(b => b.Author)
                .FirstOrDefaultAsync();
        }

        //Возвращает авторов, написавших четное количество книг, изданных после 2015 года
        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors
                    .AsNoTracking()
                    .Where(a => a.Books.Any(b => b.PublishDate.Year > 2015) &&
                            a.Books.Count(b => b.PublishDate.Year > 2015) % 2 == 0)
                    .ToListAsync();
        }
    }
}
