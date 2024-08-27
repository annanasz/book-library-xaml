using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Helpers
{
    /// <summary>
    /// konverter az api válaszok inkonzisztenciájának kezelésére a szerzők esetén
    /// </summary>
    public class AuthorConverter : JsonConverter
    {
        /// <summary>
        /// ellenőrizzük, hogy a konvertálandó típus ténylge szerző típusú-e
        /// </summary>
        /// <param name="objectType">az objektum típusa</param>
        /// <returns>bool változó, ami megmondja, hogy tudjuk-e konvertálni az adott objektumot, vagy nem</returns>
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(BookAuthor);
        }
        /// <summary>
        /// Amikor olvassuk be a jsont kezeljük az inkonzisztenciát
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>a szerzők listája</returns>
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            var authors = new List<BookAuthor>(); //a szerzők listája
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                foreach (JToken item in token)
                {
                    //a két mód, ahogy megjelenik a válasz jsonben a szerző, és ennek egységesítése
                    if (item.SelectToken("author") != null)
                    {
                        authors.Add(new BookAuthor
                        {
                            author = item.SelectToken("author.key").ToString(),
                            type = item.SelectToken("type.key")?.ToString()
                        });
                    }
                    else
                    {
                        authors.Add(new BookAuthor
                        {
                            author = item.SelectToken("author.key").ToString(),
                            type = item.SelectToken("type")?.ToString()
                        });
                    }
                }
            }
            return authors;
        }
        /// <summary>
        /// erre nincsen most szükségünk, csa olvasunk, írni nem akarunk
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

