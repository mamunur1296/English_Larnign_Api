using App.Application.Exceptions;
using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;

namespace App.Infrastructure.Implementation.Command
{
    public class SentenceFormsCommandRepository : CommandRepository<SentenceForms>, ISentenceFormsCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SentenceFormsCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // Implement additional methods specific to SentenceFormsCommandRepository here
        public async Task<bool> CreateSentenceStructureAndFormateMapping(string formateID, List<string> structureIDs)
        {
            using (var transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Parse the FormateID from the input string
                    var parsedFormateID = formateID;

                    // Fetch the SentenceForm based on the provided FormateID
                    var sentenceForm = await _applicationDbContext.SentenceFormss.FindAsync(parsedFormateID);
                    if (sentenceForm == null)
                    {
                        throw new NotFoundException("Sentence Form not found.");
                    }

                    // Loop through each StructureID and create mappings
                    foreach (var structureID in structureIDs)
                    {
                        // Parse the StructureID from the input string
                        var parsedStructureID = structureID;

                        // Fetch the SentenceStructure based on the StructureID
                        var sentenceStructure = await _applicationDbContext.SentenceStructures.FindAsync(parsedStructureID);
                        if (sentenceStructure == null)
                        {
                            throw new NotFoundException($"Sentence Structure with ID {structureID} not found.");
                        }

                        // Create the mapping between the form and the structure
                        var mapping = new SentenceFormStructureMapping
                        {
                            FormateID = sentenceForm.Id,
                            StructureID = sentenceStructure.Id,
                        };

                        // Add the mapping to the context
                        await _applicationDbContext.FormStructureMappings.AddAsync(mapping);
                    }

                    // Save all mappings to the database
                    await _applicationDbContext.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    // Log or rethrow the exception
                    throw new Exception("Error creating the mappings.", ex);
                }
            }
        }

        public async Task<bool> UpdateSentenceStructureAndFormateMapping(string formateID, List<string> structureIDs)
        {
            using (var transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Parse the FormateID from the input string
                    var parsedFormateID = formateID;

                    // Fetch the SentenceForm based on the provided FormateID
                    var sentenceForm = await _applicationDbContext.SentenceFormss.FindAsync(parsedFormateID);
                    if (sentenceForm == null)
                    {
                        throw new Exception("Sentence Form not found.");
                    }

                    // Remove existing mappings for the given FormateID
                    var existingMappings = _applicationDbContext.FormStructureMappings
                                               .Where(m => m.FormateID == parsedFormateID);
                    _applicationDbContext.FormStructureMappings.RemoveRange(existingMappings);

                    // Loop through each StructureID and create new mappings
                    foreach (var structureID in structureIDs)
                    {
                        // Parse the StructureID from the input string
                        var parsedStructureID = structureID;

                        // Fetch the SentenceStructure based on the StructureID
                        var sentenceStructure = await _applicationDbContext.SentenceStructures.FindAsync(parsedStructureID);
                        if (sentenceStructure == null)
                        {
                            throw new Exception($"Sentence Structure with ID {structureID} not found.");
                        }

                        // Create the mapping between the form and the structure
                        var mapping = new SentenceFormStructureMapping
                        {
                            FormateID = parsedFormateID,
                            StructureID = parsedStructureID,
                        };

                        // Add the new mapping to the context
                        await _applicationDbContext.FormStructureMappings.AddAsync(mapping);
                    }

                    // Save changes to the database
                    await _applicationDbContext.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    // Log or rethrow the exception
                    throw new Exception("Error updating the mappings.", ex);
                }
            }
        }
    }
}
