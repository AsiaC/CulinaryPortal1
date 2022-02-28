using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Persistence.Repositories
{
    public class MeasureRepository :BaseRepository<Measure>, IMeasureRepository
    {
        public MeasureRepository(CulinaryPortalDbContext dbContext) : base(dbContext)
        {

        }
    }
}
