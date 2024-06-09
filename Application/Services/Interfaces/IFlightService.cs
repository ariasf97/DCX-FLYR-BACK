using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IFlightService
    {
        Task<List<Journey>> GetFlightsByType(Filter filter);

        Task<List<Journey>> GetRoundtripFlights(Filter filter);
    }
}
