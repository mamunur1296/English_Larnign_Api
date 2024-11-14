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
        private readonly DapperDbContext _dapperDbContext;

        public SubCategoryQueryRepository(ApplicationDbContext applicationDbContext, DapperDbContext dapperDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<SubCategory>> GetAllSubCategoryWithForms()
        {
            // Load subcategories with associated SentenceForms
            var subCategories = await _applicationDbContext.SubCategories
                .Include(sc => sc.SubCategoryFormMapping)
                    .ThenInclude(scfm => scfm.SentenceForm)
                .ToListAsync();

            // Load descriptions into a dictionary for quick lookup based on formatId and subCategoryId
            var descriptions = new Dictionary<(string formateId, string subCatagoryId), (string body, string bodyBangla)>();

            // Fetch all descriptions and handle duplicates
            var descriptionList = await _applicationDbContext.Descriptions
                .Where(d => d.formateId != null && d.subCatagoryId != null)
                .ToListAsync();

            // Add descriptions to the dictionary, skipping duplicates
            foreach (var description in descriptionList)
            {
                var key = (description.formateId.ToString(), description.subCatagoryId.ToString());
                descriptions.TryAdd(key, (description.body, description.bodyBangla)); // Only add if key doesn't already exist
            }

            // Create a new list to hold the subcategories with updated sentence form bodies
            var updatedSubCategories = new List<SubCategory>();

            // Map the body of each description to the corresponding SentenceForm
            foreach (var subCategory in subCategories)
            {
                // Manually create a new SubCategory and copy over the properties
                var updatedSubCategory = new SubCategory
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    SubCategoryFormMapping = new List<SubCategoryFormMapping>()
                };

                foreach (var mapping in subCategory.SubCategoryFormMapping)
                {
                    var sentenceForm = mapping.SentenceForm;
                    var updatedSentenceForm = new SentenceForms
                    {
                        Id = sentenceForm.Id,
                        Name = sentenceForm.Name,
                        body = sentenceForm.body,
                        bodyBangla = sentenceForm.bodyBangla,
                        // Copy other properties if needed
                    };

                    updatedSubCategory.SubCategoryFormMapping.Add(new SubCategoryFormMapping
                    {
                        SentenceForm = updatedSentenceForm
                    });
                }

                // Now map the descriptions to the new SentenceForm body
                foreach (var mapping in updatedSubCategory.SubCategoryFormMapping)
                {
                    var sentenceForm = mapping.SentenceForm;
                    if (sentenceForm != null)
                    {
                        var key = (sentenceForm.Id.ToString(), updatedSubCategory.Id.ToString());

                        // Assign the correct body and bodyBangla to the sentence form if a match is found in descriptions
                        if (descriptions.TryGetValue(key, out var description))
                        {
                            sentenceForm.body = description.body;
                            sentenceForm.bodyBangla = description.bodyBangla;
                        }
                    }
                }

                // Add the updated subcategory to the new list
                updatedSubCategories.Add(updatedSubCategory);
            }

            // Return the new list with updated sentence form bodies
            return updatedSubCategories;
        }

        //public async Task<IEnumerable<SubCategory>> GetAllSubCategoryWithForms()
        //{
        //    // Load subcategories with associated SentenceForms
        //    var subCategories = await _applicationDbContext.SubCategories
        //        .Include(sc => sc.SubCategoryFormMapping)
        //            .ThenInclude(scfm => scfm.SentenceForm)
        //        .ToListAsync();

        //    // Load descriptions into a dictionary for quick lookup based on formatId and subCategoryId
        //    var descriptions = await _applicationDbContext.Descriptions
        //        .Where(d => d.formateId != null && d.subCatagoryId != null)
        //        .ToDictionaryAsync(
        //            d => (d.formateId.ToString(), d.subCatagoryId.ToString()),
        //            d => (d.body, d.bodyBangla)
        //        );

        //    // Create a new list to hold the subcategories with updated sentence form bodies
        //    var updatedSubCategories = new List<SubCategory>();

        //    // Map the body of each description to the corresponding SentenceForm
        //    foreach (var subCategory in subCategories)
        //    {
        //        // Manually create a new SubCategory and copy over the properties
        //        var updatedSubCategory = new SubCategory
        //        {
        //            Id = subCategory.Id,
        //            Name = subCategory.Name,
        //            SubCategoryFormMapping = new List<SubCategoryFormMapping>()
        //        };

        //        foreach (var mapping in subCategory.SubCategoryFormMapping)
        //        {
        //            var sentenceForm = mapping.SentenceForm;
        //            var updatedSentenceForm = new SentenceForms
        //            {
        //                Id = sentenceForm.Id,
        //                Name = sentenceForm.Name,
        //                body = sentenceForm.body,
        //                bodyBangla = sentenceForm.bodyBangla,
        //                // Copy other properties if needed
        //            };

        //            updatedSubCategory.SubCategoryFormMapping.Add(new SubCategoryFormMapping
        //            {
        //                SentenceForm = updatedSentenceForm
        //            });
        //        }

        //        // Now map the descriptions to the new SentenceForm body
        //        foreach (var mapping in updatedSubCategory.SubCategoryFormMapping)
        //        {
        //            var sentenceForm = mapping.SentenceForm;
        //            if (sentenceForm != null)
        //            {
        //                var key = (sentenceForm.Id.ToString(), updatedSubCategory.Id.ToString());

        //                // Assign the correct body and bodyBangla to the sentence form if a match is found in descriptions
        //                if (descriptions.TryGetValue(key, out var description))
        //                {
        //                    sentenceForm.body = description.body;
        //                    sentenceForm.bodyBangla = description.bodyBangla;
        //                }
        //            }
        //        }

        //        // Add the updated subcategory to the new list
        //        updatedSubCategories.Add(updatedSubCategory);
        //    }

        //    // Return the new list with updated sentence form bodies
        //    return updatedSubCategories;
        //}





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
