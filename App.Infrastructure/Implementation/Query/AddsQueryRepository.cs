using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Implementation.Query
{
    public class AddsQueryRepository : QueryRepository<Adds>, IAddsQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddsQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Implement additional methods specific to CategoryQueryRepository here
    }
}
