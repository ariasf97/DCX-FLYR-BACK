using Domain.Enums;
using Domain.Exception;
using Domain.Models;
using Infrastructure.helper;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Implementation
{
    public class FlightRepository : IFlightRepository
    {
        private Dictionary<string, List<Flight>> adjacencyList;
        private readonly string JSON_FILE_PATH = @"..\Infrastructure\data\markets.json";
        public FlightRepository()
        {
            this.adjacencyList = new Dictionary<string, List<Flight>>();
            
        }


        private void LoadFlightsFromJson(CurrencyType currencyType)
        {

            var jsonString = File.ReadAllText(JSON_FILE_PATH);
            var flights = JsonSerializer.Deserialize<List<Flight>>(jsonString);

            foreach (var flight in flights)
            {
                if (!adjacencyList.TryGetValue(flight.Origin, out List<Flight>? value))
                {
                    value = [];
                    adjacencyList[flight.Origin] = value;
                }
                flight.Price = CurrencyConverter.Convert(flight.Price, currencyType);
                value.Add(flight);
            }
        }
        private List<Flight> GetFlights(string origin)
        {
            return adjacencyList.TryGetValue(origin, out List<Flight>? value) ? value : [];
        }
        public Task<List<Journey>> GetFlightsByType(Filter filter,bool loadFlight)
        {
            if (loadFlight) { this.LoadFlightsFromJson(filter.CurrencyType); }
              
           
           
            string origin = filter.Origin;
            string destination = filter.Destination;

            if (!adjacencyList.ContainsKey(origin) || !adjacencyList.ContainsKey(destination))
            {
                throw new JourneysNotFoundException($"No journeys found for the specified origin: {origin} and destination: {destination}");
            }

            var journeys = new List<Journey>();
            var visited = new HashSet<string>();
            var queue = new Queue<List<Flight>>();

            // Inicializamos la cola con la lista de vuelos que parten del origen
            foreach (var flight in adjacencyList[origin])
            {
                queue.Enqueue(new List<Flight> { flight });
            }

            while (queue.Count > 0)
            {
                var currentPath = queue.Dequeue();
                var lastFlight = currentPath.Last();
                var currentLocation = lastFlight.Destination;

                // Si ya hemos visitado este destino, pasamos al siguiente camino
                if (visited.Contains(currentLocation))
                {
                    continue;
                }

                // Si llegamos al destino final, agregamos el camino a la lista de resultados
                if (currentLocation == destination)
                {
                    var totalPrice = currentPath.Sum(f => f.Price);
                    journeys.Add(new Journey(currentPath, origin, destination, totalPrice));
                }
                else
                {
                    // Si no hemos llegado al destino final, continuamos explorando desde la ubicación actual
                    visited.Add(currentLocation);

                    foreach (var nextFlight in adjacencyList[currentLocation])
                    {
                        // Verificamos si este vuelo ya está en el camino actual para evitar ciclos
                        if (!currentPath.Any(f => f.Transport.FlightNumber == nextFlight.Transport.FlightNumber))
                        {
                            var newPath = new List<Flight>(currentPath);
                            newPath.Add(nextFlight);
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }

            if (journeys.Count == 0)
            {
                throw new JourneysNotFoundException($"No journeys found for the specified origin: {origin} and destination: {destination}");
            }

            return Task.FromResult(journeys);
        }



        public async Task<List<Journey>> GetRoundtripFlights(Filter filter)
        {
            string origin = filter.Origin;
            string destination = filter.Destination;
            this.LoadFlightsFromJson(filter.CurrencyType);
            if (!adjacencyList.ContainsKey(origin) || !adjacencyList.ContainsKey(destination))
            {
                throw new JourneysNotFoundException($"No journeys found for the specified origin: {origin} and destination: {destination}");
            }

            var roundtripJourneys = new List<Journey>();

            // Obtener vuelos de ida
            var outboundJourneys = await GetFlightsByType(filter,false);

            // Obtener vuelos de vuelta
            var returnFilter = new Filter
            {
                Origin = destination,
                Destination = origin
            };
            var returnJourneys = await GetFlightsByType(returnFilter, false);

            // Combinar vuelos de ida y vuelta
            foreach (var outboundJourney in outboundJourneys)
            {
                foreach (var returnJourney in returnJourneys)
                {
                    var totalPrice = outboundJourney.Price + returnJourney.Price;
                    var journeyFlights = outboundJourney.Flights.Concat(returnJourney.Flights).ToList();

                    roundtripJourneys.Add(new Journey(journeyFlights, origin, destination, totalPrice));
                }
            }

            if (roundtripJourneys.Count == 0)
            {
                throw new JourneysNotFoundException($"No roundtrip journeys found for the specified origin: {origin} and destination: {destination}");
            }

            return roundtripJourneys;
        }




    }
}

