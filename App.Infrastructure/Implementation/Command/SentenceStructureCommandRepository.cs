using App.Domain.Abstractions.CommandRepo;
using App.Domain.Entities;
using App.Domain.OthersDto;
using Dapper;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command.Base;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace App.Infrastructure.Implementation.Command
{
    public class SentenceStructureCommandRepository : CommandRepository<SentenceStructure>, ISentenceStructureCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DapperDbContext _dapperDbContext;

        public SentenceStructureCommandRepository(ApplicationDbContext applicationDbContext, DapperDbContext dapperDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dapperDbContext = dapperDbContext;
        }



        public async Task<bool> CreateSentencesForXlsx(List<InputSentenceItem> sentenceItems)
        {
            if (sentenceItems == null || !sentenceItems.Any())
            {
                throw new ArgumentNullException(nameof(sentenceItems), "The sentence items cannot be null or empty.");
            }

            // SQL queries for fetching IDs and inserting data
            const string fetchSubCategoryIdSql = "SELECT Id FROM SubCategories WHERE LOWER(Name) = @Name";
            const string fetchFormIdSql = "SELECT Id FROM SentenceFormss WHERE LOWER(Name) = @Name";
            const string insertSentenceStructureSql = @"
    INSERT INTO SentenceStructures (Id, BanglaSentence, EnglistSentence, SubCatagoryID, FormsId, SrNumber)
    VALUES (@Id, @BanglaSentence, @EnglistSentence, @SubCatagoryID, @FormsId, @SrNumber)";

            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                // Check if connection is open
                if (connection.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("The database connection could not be opened.");
                }

                // Begin a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int index = 1; // Start from 1 or any number you want for the serial number

                        foreach (var item in sentenceItems)
                        {
                            // Initialize parameters for IDs
                            string? subCategoryId = null;
                            string? formId = null;

                            // Fetch SubCategory ID if the SubCatagoryName is provided
                            if (!string.IsNullOrEmpty(item.SubCatagoryName))
                            {
                                subCategoryId = await connection.QueryFirstOrDefaultAsync<string>(
                                    fetchSubCategoryIdSql,
                                    new { Name = item.SubCatagoryName.ToLower() },
                                    transaction);
                            }

                            // Fetch Form ID if FormName is provided
                            if (!string.IsNullOrEmpty(item.FormName))
                            {
                                formId = await connection.QueryFirstOrDefaultAsync<string>(
                                    fetchFormIdSql,
                                    new { Name = item.FormName.ToLower() },
                                    transaction);
                            }

                            //// Check if the English sentence already exists
                            //var sentenceExists = await connection.ExecuteScalarAsync<int>(
                            //    checkSentenceExistsSql,
                            //    new { EnglistSentence = item.EnglishSentences.ToLower() }, // Case-insensitive check
                            //    transaction);

                            //// If sentence exists, skip this item
                            //if (sentenceExists > 0)
                            //{
                            //    continue; // Skip the rest of the loop and move to the next item
                            //}

                            // Create and insert the SentenceStructure item
                            var sentenceStructure = new
                            {
                                Id = index.ToString(),
                                BanglaSentence = item.BanglaSentences,
                                EnglistSentence = item.EnglishSentences,
                                SubCatagoryID = subCategoryId,
                                FormsId = formId,
                                SrNumber = index, // Use the index for the serial number
                            };

                            // Insert the SentenceStructure record
                            await connection.ExecuteAsync(
                                insertSentenceStructureSql,
                                sentenceStructure,
                                transaction);

                            index++; // Increment the index for the next serial number
                        }

                        // Commit transaction
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if any error occurs
                        transaction.Rollback();
                        connection.Close();

                        // Log the error (ensure proper logging is implemented in production)
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                // Define the SQL query to delete all records from the SentenceStructures table
                const string deleteAllSql = "DELETE FROM SentenceStructures";

                // Create the connection
                using (var connection = _dapperDbContext.CreateConnection())
                {
                    connection.Open();

                    // Execute the delete operation
                    var affectedRows = await connection.ExecuteAsync(deleteAllSql);

                    // If any rows were affected, return true indicating success
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the error (ensure proper logging in production)
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }

}
