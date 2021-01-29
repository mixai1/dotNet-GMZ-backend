using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.Models;
using Microsoft.Extensions.Logging;

namespace dotNet_GMZ_backend.DAL.Repository
{
    public class NewsRecordRepository : Repository<NewsRecord>, INewsRecordRepository
    {
        public NewsRecordRepository(AppDbContext appDbContext, ILogger logger) : base(appDbContext, logger)
        {
        }
    }
}