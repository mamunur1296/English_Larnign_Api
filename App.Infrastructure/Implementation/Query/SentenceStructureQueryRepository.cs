
using App.Domain.Abstractions.QueryRepo;
using App.Domain.Entities;
using Dapper;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Query.Base;


namespace App.Infrastructure.Implementation.Query
{
    public class SentenceStructureQueryRepository : QueryRepository<SentenceStructure>, ISentenceStructureQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DapperDbContext _dapperDbContext;

        public SentenceStructureQueryRepository(ApplicationDbContext applicationDbContext, DapperDbContext dapperDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dapperDbContext = dapperDbContext;
        }

        // Method implementation with pagination and filtering
        public async Task<(IEnumerable<SentenceStructure>, int PageCount)> GetAllFilterBySubCatagoryIdAndFormsIdAsync(
     string subCatagoryID, string formsId, int pageSize, int pageNumber)
        {
            // Validate input values
            if (string.IsNullOrWhiteSpace(subCatagoryID) || string.IsNullOrWhiteSpace(formsId))
                throw new ArgumentException("Both subCatagoryID and formsId must be provided.");

            // Set default values for pagination if not provided or invalid
            pageSize = pageSize <= 0 ? 10 : pageSize; // Default page size to 10 if <= 0
            pageNumber = pageNumber <= 0 ? 1 : pageNumber; // Default page number to 1 if <= 0

            // Calculate offset for pagination
            var offset = (pageNumber - 1) * pageSize;

            // SQL query for counting total records matching the filter
            const string countSql = @"
            SELECT 
                COUNT(*) 
            FROM 
                SentenceStructures
            WHERE 
                SubCatagoryID = @SubCatagoryID 
                AND FormsId = @FormsId;";

            // SQL query for efficient data retrieval with filtering and pagination
            const string dataSql = @"
            SELECT 
                BanglaSentence,
                EnglistSentence
            FROM 
                SentenceStructures
            WHERE 
                SubCatagoryID = @SubCatagoryID 
                AND FormsId = @FormsId
            ORDER BY 
                Id ASC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            using (var connection = _dapperDbContext.CreateConnection())
            {
                // Increase timeout for large queries
                var commandTimeout = 180; // Timeout in seconds
                connection.Open();

                // Execute the queries
                // 1. Get the total count of records
                var totalCount = await connection.ExecuteScalarAsync<int>(
                    countSql,
                    new { SubCatagoryID = subCatagoryID, FormsId = formsId },
                    commandTimeout: commandTimeout);

                // 2. Fetch paginated data
                var sentenceStructures = await connection.QueryAsync<SentenceStructure>(
                    dataSql,
                    new { SubCatagoryID = subCatagoryID, FormsId = formsId, Offset = offset, PageSize = pageSize },
                    commandTimeout: commandTimeout);

                // Calculate total page count
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);


                return (sentenceStructures, pageCount);
            }
        }



        public async Task<IEnumerable<SentenceStructure>> GetAllSentenceStructureAsync()
        {

            const string sql = @"
                    SELECT 
                        BanglaSentence,
                        EnglistSentence
                    FROM 
                        SentenceStructures";

            using (var connection = _dapperDbContext.CreateConnection())
            {
                connection.Open();
                // Ensure that you are using asynchronous query methods to handle large datasets.
                var sentenceStructures = await connection.QueryAsync<SentenceStructure>(sql);

                // Return the result as an IEnumerable<SentenceStructure>
                return sentenceStructures;
            }
        }

        //public async Task<IEnumerable<SentenceStructure>> GetAllSentenceStructureAsync()
        //{
        //    const string sql = @"
        //        SELECT 
        //            BanglaSentence,
        //            EnglistSentence
        //        FROM 
        //            SentenceStructures
        //        ORDER BY 
        //            Id ASC
        //        OFFSET @Offset ROWS FETCH NEXT @BatchSize ROWS ONLY;";
        //    using (var connection = _dapperDbContext.CreateConnection())
        //    {
        //         connection.Open();
        //        var sentenceStructures = await connection.QueryAsync<SentenceStructure>(sql, new
        //        {
        //            Offset = 200,
        //            BatchSize = 0
        //        });

        //        return sentenceStructures;
        //    }
        //}


    }

}
