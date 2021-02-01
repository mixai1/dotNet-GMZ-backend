using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.Models;
using Microsoft.Extensions.Logging;

namespace dotNet_GMZ_backend.DAL.Repository
{
    public class NewsRecordRepository : Repository<NewsRecord>, INewsRecordRepository
    {
        private readonly AppDbContext _appDbContext;

        public NewsRecordRepository(AppDbContext appDbContext, ILogger<Repository<NewsRecord>> logger) : base(appDbContext, logger)
        {
            _appDbContext = appDbContext;
        }
    }
}