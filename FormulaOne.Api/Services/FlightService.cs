using FormulaOn.Api.Dto;
using FormulaOne.Api.Services.IServices;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RestSharp;
using System;

namespace FormulaOne.Api.Services
{
    public class FlightService : IFlightService
    {
        // Retry policy: Retry up to 4 times with Polly
        // Handle<Exception>() this will handle any exception thrown during the execution by api like timeout, server down etc
        //OrResult<RestResponse> (resp => !resp.IsSuccessful) this will handle unsuccessful responses like 5xx, 4xx etc
        private static readonly AsyncRetryPolicy<RestResponse> retryPolicy = Policy.Handle<Exception>().OrResult<RestResponse>(resp => !resp.IsSuccessful)
            .WaitAndRetryAsync(4, attempt =>
          {
              Console.WriteLine($"Attempt {attempt} - retrying...");
              return TimeSpan.FromSeconds(3 + attempt);
          });

        // Retry policy : using circuit breaker pattern
        private static readonly AsyncCircuitBreakerPolicy<RestResponse> circuitBreakerPolicy = Policy.Handle<Exception>().OrResult<RestResponse>(resp => !resp.IsSuccessful)
          .CircuitBreakerAsync(2,TimeSpan.FromSeconds(30),
              onBreak: (result, breakDelay) =>
              {
                  Console.WriteLine($"Circuit broken for {breakDelay.TotalSeconds}s");
              },
              onReset: () =>
              {
                  Console.WriteLine("Circuit reset");
              });

        //Advanced circuit breaker policy with failure threshold and sampling duration
        // 0.5 = 50% failure threshold, TimeSpan.FromMinutes(1) = sampling duration of 1 minute, 10 = minimum of 10 requests, TimeSpan.FromSeconds(30) = duration of break
        private static readonly AsyncCircuitBreakerPolicy<RestResponse> advCircuitBreakerPolicy = Policy.Handle<Exception>().OrResult<RestResponse>(resp => !resp.IsSuccessful)
          .AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromMinutes(1), 10, TimeSpan.FromSeconds(30));

        public async Task<List<FlightDto>> GetAvailableFlights()
        {
            const string calenderServiceUrl = "https://localhost:7158/api/FlightsCalender/GetFlightCalender";
            RestClient client = new();
            RestRequest request = new(calenderServiceUrl);


            //RestResponse response = await client.ExecuteAsync(request);

            // Using only Polly retry policy
            //RestResponse response = await retryPolicy.ExecuteAsync(() => client.ExecuteAsync(request));

            // Using both polly retry and circuit breaker policies
            //RestResponse response = await circuitBreakerPolicy.ExecuteAsync(() => retryPolicy.ExecuteAsync(() => client.ExecuteAsync(request)));


            // Using both polly retry and advance circuit breaker policies
            RestResponse response = await advCircuitBreakerPolicy.ExecuteAsync(() => retryPolicy.ExecuteAsync(() => client.ExecuteAsync(request)));

            if (!response.IsSuccessful)
                throw new Exception("something went wrong");
            return JsonConvert.DeserializeObject<List<FlightDto>>(response.Content ?? "[]")?? new List<FlightDto>();
        }
    }
}
