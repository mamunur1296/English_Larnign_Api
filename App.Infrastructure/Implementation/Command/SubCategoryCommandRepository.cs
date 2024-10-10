using App.Application.Exceptions;
using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;

namespace App.Infrastructure.Implementation.Command
{
    public class SubCategoryCommandRepository : CommandRepository<SubCategory>, ISubCategoryCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubCategoryCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> UpdateAssainFormMapping(string SubCategoryId, List<string> SentenceFormId)
        {
            using (var transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    
                    var parsedubCategoryID = SubCategoryId;

                
                    var subCategory = await _applicationDbContext.SubCategories.FindAsync(parsedubCategoryID);
                    if (subCategory == null)
                    {
                        throw new NotFoundException("Sub Category not found.");
                    }

           
                    var existingMappings = _applicationDbContext.SubCategoryFormMappings
                                               .Where(m => m.SubCategoryId == parsedubCategoryID);
                    _applicationDbContext.SubCategoryFormMappings.RemoveRange(existingMappings);

                 
                    foreach (var FormID in SentenceFormId)
                    {
                     
                        var parsedFormID = FormID;

                        
                        var sentenceFrom = await _applicationDbContext.SentenceFormss.FindAsync(parsedFormID);
                        if (sentenceFrom == null)
                        {
                            throw new NotFoundException($"Sentence From with ID {parsedFormID} not found.");
                        }

                       
                        var mapping = new SubCategoryFormMapping
                        {
                            SubCategoryId = parsedubCategoryID,
                            SentenceFormId = parsedFormID,
                        };
                        await _applicationDbContext.SubCategoryFormMappings.AddAsync(mapping);
                    }
                    await _applicationDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error updating the mappings.", ex);
                }
            }
        }
        
    }
}
