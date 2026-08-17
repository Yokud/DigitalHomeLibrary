using DigitalHomeLibrary.BookService.Domain.Entities;
using DigitalHomeLibrary.BookService.Domain.Repositories;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.Repositories
{
    public class EFCoreAuthorRepository(BookServiceDbContext context) : IAuthorRepository
    {
        readonly BookServiceDbContext _context = context;

        public async Task<Guid> AddAsync(Author author)
        {
            var daEntity = new AuthorDbo()
            {
                Id = author.Id,
                FirstName = author.FullName.FirstName,
                LastName = author.FullName.LastName,
                MiddleName = author.FullName.MiddleName,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                LifeStory = author.LifeStory,
                CountryName = author.Country.Name,
            };

            await _context.Authors.AddAsync(daEntity);
            await _context.SaveChangesAsync();
            return daEntity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Authors.Where(e => e.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<Author?> FindByFullNameAsync(FullName fullName)
        {
            var authorEntity = await _context.Authors.Where(e => e.FirstName == fullName.FirstName && e.LastName == fullName.LastName && e.MiddleName == fullName.MiddleName).AsNoTracking()
                .SingleOrDefaultAsync();

            return authorEntity is null ? null : new Author(authorEntity.Id, new(authorEntity.FirstName, authorEntity.LastName, authorEntity.MiddleName), authorEntity.BirthDate, new(authorEntity.CountryName), authorEntity.DeathDate, authorEntity.LifeStory);
        }

        public async Task<IReadOnlyList<Author>> GetAllAsync(PaginationParams? paginationInfo = null)
        {
            IQueryable<AuthorDbo> res = _context.Authors;

            if (paginationInfo is not null)
                res = res.Skip(paginationInfo.PageNum * paginationInfo.PageSize).Take(paginationInfo.PageSize);

            var entities = await res
                .Include(e => e.Books)
                .AsNoTracking()
                .ToListAsync();

            return [.. entities.Select(e => new Author(e.Id, new(e.FirstName, e.LastName, e.MiddleName), e.BirthDate, new(e.CountryName), e.DeathDate, e.LifeStory))];
        }

        public async Task<Author?> GetByIdAsync(Guid id)
        {
            var authorEntity = await _context.Authors.Where(e => e.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return authorEntity is null ? null : new Author(authorEntity.Id, new(authorEntity.FirstName, authorEntity.LastName, authorEntity.MiddleName), authorEntity.BirthDate, new(authorEntity.CountryName), authorEntity.DeathDate, authorEntity.LifeStory);
        }

        public async Task UpdateAsync(Author author)
        {
            await _context.Authors.Where(e => e.Id == author.Id).ExecuteUpdateAsync(s => s.
            SetProperty(e => e.FirstName, author.FullName.FirstName).
            SetProperty(e => e.LastName, author.FullName.LastName).
            SetProperty(e => e.MiddleName, author.FullName.MiddleName).
            SetProperty(e => e.BirthDate, author.BirthDate).
            SetProperty(e => e.DeathDate, author.DeathDate).
            SetProperty(e => e.CountryName, author.Country.Name).
            SetProperty(e => e.LifeStory, author.LifeStory));

            await _context.SaveChangesAsync();
        }
    }
}
