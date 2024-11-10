using App.Application.DTOs;
using App.Application.Features.AddsFeatures.CommandHandlers;
using App.Application.Features.CategoryFeatures.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IAddsServices
    {
        Task<(bool Success, string id)> CreateAsync(CreateAddsCommand entity);
        Task<IEnumerable<AddsDTO>> GetAllAsync();
        Task<AddsDTO> GetByIdAsync(string id);
        Task<(bool Success, string id)> UpdateAsync(UpdateAddsCommand entity);
        Task<(bool Success, string id)> DeleteAsync(string id);
    }
}
