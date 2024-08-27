using Library.Models;
using Library.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Services
{
    /// <summary>
    /// Service osztály a http kérések lebonyolítására
    /// </summary>
    public class LibraryService
    {
        /// <summary>
        /// a könyvtár api elérési útvonala
        /// </summary>
        private readonly Uri serverUrl = new Uri("https://openlibrary.org");

        /// <summary>
        /// A könyvtár API-tól aszinkron módon lekérdezzük a megadott uri-n lévő adatot
        /// </summary>
        /// <typeparam name="T">általános típussal dolgozunk</typeparam>
        /// <param name="uri">a keresett adat uri-ja</param>
        /// <returns></returns>
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new AuthorConverter(), new DescriptionConverter() }
                };//az api inkonzisztencia miatt szükséges konverterek
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync(); //beolvassuk a válasz json-t, ami jön az api-tól
                try {
                    T result = JsonConvert.DeserializeObject<T>(json,settings); //deszerializáljuk az eredményt a megadott konverterek segítségével
                    return result;
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine("Deserialization failed: " + ex.Message);
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine("JSON parsing failed: " + ex.Message);
                }
                return default(T);
            }
        }

        /// <summary>
        /// könyvek keresése kulcsszó alapján aszinkron módon
        /// </summary>
        /// <param name="search">a kulcsszó amire keresünk</param>
        /// <param name="queryoption">a keresési kritérium</param>
        /// <returns></returns>
        public async Task<SearchResult> GetBookHeadersAsync(string search, string queryoption)
        {
            return await GetAsync<SearchResult>(new Uri(serverUrl, $"/search.json?{queryoption}={search}"));
        }
        /// <summary>
        /// egy darab könyv adatainak lekérdezése aszinkron módon
        /// </summary>
        /// <param name="key">a könyv azonosítója</param>
        /// <returns></returns>
        public async Task<Book> GetBookAsync(string key)
        {
            return await GetAsync<Book>(new Uri(serverUrl, $"{key}.json"));
        }
        /// <summary>
        /// szerző adatainak lekérdezése aszinkron módon
        /// </summary>
        /// <param name="key">a szerző azonosítója</param>
        /// <returns></returns>
        
        public async Task<Author> GetAuthorAsync(string key)
        {
            return await GetAsync<Author>(new Uri(serverUrl, $"/authors/{key}.json"));
        }

    }
}
