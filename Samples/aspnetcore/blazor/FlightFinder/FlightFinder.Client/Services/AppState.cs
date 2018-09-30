using FlightFinder.Shared;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorLibrary;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;

namespace FlightFinder.Client.Services
{
    public class AppState
    {
        // Actual state
        public IReadOnlyList<Itinerary> SearchResults { get; private set; }
        public bool SearchInProgress { get; private set; }

        private readonly List<Itinerary> shortlist = new List<Itinerary>();
        public IReadOnlyList<Itinerary> Shortlist => shortlist;

        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        // Receive 'http' instance from DI
        private readonly HttpClient http;
        private readonly LocalStorage localStorage;

        public AppState(HttpClient httpInstance, LocalStorage localStorageInstance)
        {
            http = httpInstance;
            localStorage = localStorageInstance;

            var savedItems = localStorage.GetItem<Itinerary[]>("shortList");
            if (savedItems != null)
            {
                shortlist.AddRange(savedItems);
            }
        }

        public async Task Search(SearchCriteria criteria)
        {
            SearchInProgress = true;
            NotifyStateChanged();

            SearchResults = await http.PostJsonAsync<Itinerary[]>("/api/flightsearch", criteria);
            SearchInProgress = false;
            NotifyStateChanged();

            ExampleJsInterop.Prompt("I am Calling My Javascript File");
        }

        public void AddToShortlist(Itinerary itinerary)
        {
            shortlist.Add(itinerary);
            NotifyStateChanged();
            localStorage.SetItem("shortList", shortlist);
            Console.WriteLine("Implement TODO SHORLIST");
        }

        public void RemoveFromShortlist(Itinerary itinerary)
        {
            shortlist.Remove(itinerary);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
