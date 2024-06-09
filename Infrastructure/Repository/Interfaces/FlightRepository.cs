using Domain.Models;
using System;

namespace Infrastructure.Repository.Interfaces
{
    public interface IFlightRepository
    {
        Task<List<Journey>> GetFlightsByType(Filter filter);
        
    }

}
