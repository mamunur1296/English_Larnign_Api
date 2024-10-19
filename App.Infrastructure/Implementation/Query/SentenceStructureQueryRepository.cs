using App.Application.DTOs;
using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Implementation.Query
{
    public class SentenceStructureQueryRepository : QueryRepository<SentenceStructure>, ISentenceStructureQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceStructureQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // Method implementation with pagination and filtering
        public async Task<IEnumerable<SentenceStructure>> GetAllFilterBySubCatagoryIdAndFormsIdAsync(string subCatagoryID, string formsId, int pageSize, int pageNumber)
        {
            // Ensure the input values are validated
            if (string.IsNullOrWhiteSpace(subCatagoryID) || string.IsNullOrWhiteSpace(formsId))
                throw new ArgumentException("subCatagoryID and formsId must be provided.");

            // Set default values for pagination if not provided or invalid
            pageSize = pageSize <= 0 ? 10 : pageSize; // Default page size to 10 if it's <= 0
            pageNumber = pageNumber <= 0 ? 1 : pageNumber; // Default page number to 1 if it's <= 0

            // Query the database with filters, and apply pagination
            var query = _applicationDbContext.SentenceStructures
                .AsNoTracking() // Improve performance by not tracking entities
                .Where(s => s.SubCatagoryID == subCatagoryID && s.FormsId == formsId)
                .OrderBy(s => s.CreationDate) // Customize the ordering if necessary

                // Select only the necessary fields to improve performance
                .Select(s => new SentenceStructure
                {
                    BanglaSentence = s.BanglaSentence,
                    EnglistSentence = s.EnglistSentence,
                    SubCatagoryID = s.SubCatagoryID,
                    FormsId = s.FormsId,
                    Id = s.Id,
                });

            // Apply pagination if valid values are provided
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Execute the query and return the result
            return await query.ToListAsync();
        }



    }

}
