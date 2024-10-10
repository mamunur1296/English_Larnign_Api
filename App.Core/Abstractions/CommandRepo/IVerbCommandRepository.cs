using App.Domain.Abstractions.CommandRepo.Base;
using App.Domain.Entities;


namespace App.Domain.Abstractions.CommandRepo
{
    public interface IVerbCommandRepository : ICommandRepository<Verb>
    {
        // Add specific command methods here if needed
    }
}
