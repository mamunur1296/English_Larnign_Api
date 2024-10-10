using App.Domain.Abstractions.CommandRepo;
using App.Domain.Abstractions.QueryRepo;


namespace App.Domain.Abstractions
{
    public interface IUowRepo
    {
        IEmployeeCommandRepository employeeCommandRepository { get; }
        IEmployeeQueryRepository employeeQueryRepository { get; }
        ICategoryCommandRepository categoryCommandRepository { get; }
        ICategoryQueryRepository categoryQueryRepository { get; }
        ISubCategoryCommandRepository subCategoryCommandRepository { get; }
        ISubCategoryQueryRepository subCategoryQueryRepository { get; }
        ISentenceFormsCommandRepository sentenceFormsCommandRepository { get; }
        ISentenceFormsQueryRepository sentenceFormsQueryRepository { get; }
        ISentenceStructureCommandRepository sentencesStructureCommandRepository { get; }
        ISentenceStructureQueryRepository sentencesStructureQueryRepository { get; }
        IVerbCommandRepository verbCommandRepository { get; }
        IVerbQueryRepository verbQueryRepository { get; }   
        Task SaveAsync();
    }
}
