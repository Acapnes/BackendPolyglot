using API_MSSQL.Data;
using API_MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_MSSQL.Repositories
{
    public class EntriesRepository : IRepository<Entry>
    {
        private readonly AppDbContext _context;

        public EntriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Entry entity)
        {
            _context.Entries.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Entry entity)
        {
            _context.Entries.Remove(entity);
            _context.SaveChanges();
        }

        public Entry Get(int id)
        {
            return _context.Entries.Find(id);
        }

        public IEnumerable<Entry> GetAll()
        {
            return _context.Entries.ToList();
        }

        public void Update(Entry entity)
        {
            _context.Entries.Update(entity);
            _context.SaveChanges();
        }
    }
}
