using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
            
        }
        public async Task<List<Journey>> GetFlightsByType(Filter filter)
        {
            return await _flightRepository.GetFlightsByType(filter,true);
        }

        public async Task<List<Journey>> GetRoundtripFlights(Filter filter)
        {
            return  await _flightRepository.GetRoundtripFlights(filter);
        }
    }
}
