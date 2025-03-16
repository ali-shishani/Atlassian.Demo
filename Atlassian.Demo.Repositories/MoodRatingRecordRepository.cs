using Atlassian.Demo.Data;
using Atlassian.Demo.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Repositories
{
    public class MoodRatingRecordRepository : GenericRepository<MoodRatingRecord>, IMoodRatingRecordRepository
    {
        public MoodRatingRecordRepository(AtlassianDemoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
