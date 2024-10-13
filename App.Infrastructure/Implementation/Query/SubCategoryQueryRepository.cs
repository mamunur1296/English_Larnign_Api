using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Implementation.Query
{
    public class SubCategoryQueryRepository : QueryRepository<SubCategory>, ISubCategoryQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubCategoryQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<SubCategory>> GetAllSubCategoryWithForms()
        {
            var subCategories = await _applicationDbContext.SubCategories
                .Include(sc => sc.SubCategoryFormMapping) 
                .ThenInclude(scfm => scfm.SentenceForm)   
                .ToListAsync();

            return subCategories;
        }



        public async Task<SubCategory> GetSubCategoryWithSentenceFormsAndStructures(string subCategoryId)
        {
            var subCategory = await _applicationDbContext.SubCategories
                .Include(sc => sc.SubCategoryFormMapping)
                    .ThenInclude(scfm => scfm.SentenceForm)
                        .ThenInclude(sf => sf.SentenceFormStructureMapping)
                            .ThenInclude(sfsm => sfsm.SentenceStructure)
                              .FirstOrDefaultAsync(ss=>ss.Id == subCategoryId);
            return subCategory;
        }



    }
}
