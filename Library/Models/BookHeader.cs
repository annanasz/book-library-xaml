using Library.Models.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Library.Models
{
    /// <summary>
    /// A könyvek előnézetét, fejlécét reprezentálja, az APi hívás során érkezett adatok tárolására szolgál. keresés során a könyvekről ezkeóóeket az információkat kapjuk vissza
    /// </summary>
    public class BookHeader
    {
        /// <summary>
        /// a könyv borítójának a képének az id-ja, ennek segítségével kérhetjük le API híváson keresztül a képet
        /// </summary>
        public int cover_i { get; set; }
        /// <summary>
        /// hány kiadása volt már a könyvnek
        /// </summary>
        public int edition_count { get; set; }
        /// <summary>
        /// a könyv oldalainak száma
        /// </summary>
        public int number_of_pages_median { get; set; }
        /// <summary>
        /// az elérhető nyelvek
        /// </summary>
        public string[] language { get; set; }
        /// <summary>
        /// hány db ebook van ebbpl a könyvből
        /// </summary>
        public int ebook_count_i { get; set; }
        /// <summary>
        /// a könyv címe
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// a szerző neve
        /// </summary>
        public List<string> author_name { get; set; }
        /// <summary>
        /// első publikáció éve
        /// </summary>
        public int first_publish_year { get; set; }
        /// <summary>
        /// a könyv azonosítója, ami segítségével lekérdezhetjük az api-tól a könyv adatait
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// a szerző(k) azonosítója
        /// </summary>
        public List<string> author_key { get; set; }
        /// <summary>
        /// a borítókép url-je
        /// </summary>
        public string cover_url { get; set; }
    }

    /// <summary>
    /// A teljes könyv objektum, amikor egy konkrét könyvet kérdezünk le, akkor ezeket az adatokat kapjuk vissza, valamint tartalmazza a fejlécet is 
    /// </summary>

    public class Book : INotifyPropertyChanged
    {
        /// <summary>
        /// a könyv fejléce
        /// </summary>
        private BookHeader _header;
        /// <summary>
        /// property a könyv fejlécére, ha változik akkor értesülnek a feliratkozók
        /// </summary>
        public BookHeader header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(nameof(header)); //értesítjük a feliratkozókat
                }
            }
        }
        /// <summary>
        /// a könyv címe
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// a könyv azonosítója, ami segítségével lekérdezhetjük az api-tól a könyv adatait
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// a könyv szerzőinek listája. az api visszatérésében vannak inkonzisztenciák, ezt hivatott kezelni a konverter
        /// </summary>
        [JsonConverter(typeof(AuthorConverter))]
        public List<BookAuthor> authors { get; set; }
        /// <summary>
        /// a leírása. az api visszatérésében vannak inkonzisztenciák, ezt hivatott kezelni a konverter
        /// </summary>
        [JsonConverter(typeof(DescriptionConverter))]
        public string description { get; set; }
        /// <summary>
        /// a könyvben említett helyek
        /// </summary>
        public string[] subject_places { get; set; }
        /// <summary>
        /// a könyv témái
        /// </summary>
        public string[] subjects { get; set; }
        /// <summary>
        /// a kömnyvben említett emberek
        /// </summary>
        public string[] subject_people { get; set; }
        /// <summary>
        /// a könyvben megjelenő történelmi idők
        /// </summary>
        public string[] subject_times { get; set; }
        /// <summary>
        /// Az IProperyChanged interfész megvalósításához az event, amire feliratkozhatunk
        /// </summary>

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// változás esetén értesítjük a feliratkozókat
        /// </summary>
        /// <param name="propertyName">a megváltozott property neve</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// a könyv leírásának tárolására létrehozott osztály
    /// </summary>
    public class Description
    {
        /// <summary>
        /// típus, általában mindig szöveg
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// a leírás maga
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// a könyv szerzőének tárolására létrehozott osztály
    /// </summary>
    public class BookAuthor
    {
        /// <summary>
        /// a szerző neve
        /// </summary>
        [JsonProperty("author")]
        public string author { get; set; }
        /// <summary>
        /// a szerző típusa
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }
    }

}
