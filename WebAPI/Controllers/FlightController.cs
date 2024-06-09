﻿using Domain.Models;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.helper;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightService;

        public FlightController(IFlightRepository flightService)
        {
            _flightService = flightService;
        }

        [HttpPost("GetFlightsByType-airports")]
        public async Task<IActionResult> GetFlightsByType(Filter filter            )
        {
            try
            {
                var destinationAirports = await _flightService.GetFlightsByType(filter);
                return Ok(new ApiResponse<List<Journey>>(destinationAirports, "Destination airports retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>(null, $"An error occurred: {ex.Message}"));
            }
        }

    }
}