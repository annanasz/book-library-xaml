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
    /// konverter az api válaszok inkonzisztenciájának kezelésére a könyvek leírása esetén
    /// </summary>
    public class DescriptionConverter:JsonConverter
    {
        /// <summary>
        /// ellenőrizzük, hogy a konvertálandó típus ténylge string típusú-e
        /// </summary>
        /// <param name="objectType">az objektum típusa</param>
        /// <returns>bool változó, ami megmondja, hogy tudjuk-e konvertálni az adott objektumot, vagy nem</returns>
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(string);
        }

        /// <summary>
        /// Amikor olvassuk be a jsont kezeljük az inkonzisztenciát
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>a leírás</returns>
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                return token.Value<string>();
            }
            else if (token.Type == JTokenType.Object)
            {
                //a két mód, ahogy megjelenik a válasz jsonben a szerző, és ennek egységesítése
                var description = (JObject)token;
                if (description["value"] != null)
                {
                    return description["value"].Value<string>();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new JsonSerializationException("Unexpected token type: " + token.Type);
            }
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
