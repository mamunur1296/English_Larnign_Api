using App.Domain.Abstractions;
using App.Domain.Abstractions.CommandRepo;
using App.Domain.Abstractions.QueryRepo;
using App.Infrastructure.DataContext;
using App.Infrastructure.Implementation.Command;
using App.Infrastructure.Implementation.Query;

namespace App.Infrastructure.Implementation
{
    public class UowRepo : IUowRepo
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IEmployeeCommandRepository employeeCommandRepository {  get; private set; }  

        public IEmployeeQueryRepository employeeQueryRepository { get; private set; }

        public ICategoryCommandRepository categoryCommandRepository { get; private set; }

        public ICategoryQueryRepository categoryQueryRepository { get; private set; }

        public ISubCategoryCommandRepository subCategoryCommandRepository { get; private set; }

        public ISubCategoryQueryRepository subCategoryQueryRepository { get; private set; }

        public ISentenceFormsCommandRepository sentenceFormsCommandRepository { get; private set; }

        public ISentenceFormsQueryRepository sentenceFormsQueryRepository { get; private set; }

        public ISentenceStructureCommandRepository sentencesStructureCommandRepository { get; private set; }

        public ISentenceStructureQueryRepository sentencesStructureQueryRepository { get; private set; }

        public IVerbCommandRepository verbCommandRepository { get; private set; }

        public IVerbQueryRepository verbQueryRepository { get; private set; }

        public UowRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            employeeCommandRepository = new EmployeeCommandRepository(applicationDbContext);
            employeeQueryRepository= new EmployeeQueryRepository(applicationDbContext);
            categoryCommandRepository = new CategoryCommandRepository(applicationDbContext);
            categoryQueryRepository = new CategoryQueryRepository(applicationDbContext);
            subCategoryCommandRepository = new SubCategoryCommandRepository(applicationDbContext);
            subCategoryQueryRepository = new SubCategoryQueryRepository(applicationDbContext);
            sentencesStructureCommandRepository = new SentenceStructureCommandRepository(applicationDbContext);
            sentencesStructureQueryRepository = new SentenceStructureQueryRepository(applicationDbContext);
            sentenceFormsCommandRepository = new SentenceFormsCommandRepository(applicationDbContext);
            sentenceFormsQueryRepository = new SentenceFormsQueryRepository(applicationDbContext);
            verbCommandRepository = new VerbCommandRepository(applicationDbContext);
            verbQueryRepository= new VerbQueryRepository(applicationDbContext);
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
