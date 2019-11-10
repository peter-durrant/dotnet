using System;
using System.Linq;
using Hdd.EfData;
using Microsoft.EntityFrameworkCore;

namespace Hdd.Ef.Model
{
    public sealed class DataModel : IDisposable
    {
        private readonly DatabaseContext _context;

        public DataModel(string path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));
            _context = new DatabaseContext(path);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IQueryable<Part> PartCollection()
        {
            return _context.Parts.Include(part => part.PartType).AsNoTracking();
        }
    }
}
