using AzureCards.Models;
using Microsoft.Azure.AppService.ApiApps.Service;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureCards
{
    public class DeckIsolatedStorage
    {
        private CloudIsolatedStorage _storage;

        public DeckIsolatedStorage()
        {
            _storage = Runtime.FromAppSettings().IsolatedStorage;
        }

        public async Task<string> New(Deck deck)
        {
            var deckId = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            await Save(deckId, deck);
            return deckId;
        }

        public async Task<string> Save(string deckId, Deck deck)
        {
            var filename = string.Format("{0}.json", deckId);
            var json = JsonConvert.SerializeObject(deck);
            var data = Encoding.ASCII.GetBytes(json);
            await _storage.WriteAsync(filename, data);
            return deckId;
        }

        public async Task<Deck> GetById(string deckId)
        {
            var filename = string.Format("{0}.json", deckId);
            var json = await _storage.ReadAsStringAsync(filename);
            return JsonConvert.DeserializeObject<Deck>(json);
        }
    }
}
