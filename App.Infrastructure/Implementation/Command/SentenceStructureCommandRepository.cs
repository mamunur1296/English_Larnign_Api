using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Domain.OthersDto;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Implementation.Command
{
    public class SentenceStructureCommandRepository : CommandRepository<SentenceStructure>, ISentenceStructureCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceStructureCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        public async Task<bool> CreateSentencesForXlsx(List<InputSentenceItem> sentenceItems)
        {
            if (sentenceItems == null || !sentenceItems.Any())
            {
                throw new ArgumentNullException(nameof(sentenceItems), "The sentence items cannot be null or empty.");
            }

            // Begin transaction
            using (var transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in sentenceItems)
                    {
                        // Initialize variables to store IDs
                        string? subCategoryId = null;
                        string? formId = null;

                        // Check and bind SubCategory ID if SubCatagoryName is provided
                        if (!string.IsNullOrEmpty(item.SubCatagoryName))
                        {
                            var subCategory = await _applicationDbContext.SubCategories
                                .FirstOrDefaultAsync(sc => sc.Name.ToLower() == item.SubCatagoryName.ToLower());

                            if (subCategory != null)
                            {
                                subCategoryId = subCategory.Id; // Ensure this is a Guid
                            }
                            // If not found, subCategoryId remains null, which is acceptable
                        }

                        // Check and bind Form ID if FormName is provided
                        if (!string.IsNullOrEmpty(item.FormName))
                        {
                            var form = await _applicationDbContext.SentenceFormss
                                .FirstOrDefaultAsync(f => f.Name.ToLower() == item.FormName.ToLower());

                            if (form != null)
                            {
                                formId = form.Id; // Ensure this is a Guid
                            }
                            // If not found, formId remains null, which is acceptable
                        }

                        // Create a new SentenceStructure entity
                        var sentenceStructure = new SentenceStructure
                        {
                            Id = Guid.NewGuid().ToString(),
                            BanglaSentence = item.BanglaSentences, // This can be null
                            EnglistSentence = item.EnglishSentences, // This can be null
                            SubCatagoryID = subCategoryId, // This can be null
                            FormsId = formId // This can be null
                        };

                        // Add the new SentenceStructure to the context
                        await _applicationDbContext.SentenceStructures.AddAsync(sentenceStructure);
                    }

                    // Save changes
                    await _applicationDbContext.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback transaction on error
                    await transaction.RollbackAsync();

                    // Log the exception (implement logging as per your requirements)
                    Console.WriteLine(ex.Message);
                    throw; // Re-throw the exception to the caller
                }
            }
        }




    }

}
