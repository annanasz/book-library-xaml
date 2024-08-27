using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Library.Models
{
    /// <summary>
    /// A szerzők adatainak eltárolására használt modell osztály
    /// </summary>
    public class Author : INotifyPropertyChanged
    {
        /// <summary>
        /// A szerző képére mutató uri
        /// </summary>
        private string _photo;
        /// <summary>
        /// A szerző linkjeit tároló kollekció
        /// </summary>
        private ObservableCollection<Link> _links;
        /// <summary>
        /// a szerző azonosítója, amellyel azonosítani lehet API hívásnál
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// a szerző titulusa
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// A szerző neve
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// A szerző képeinek tömbje, ha több képe van akkor mindegyik benne van itt
        /// </summary>
        public List<int> photos { get; set; }
        /// <summary>
        /// Személyes neve a szerzőnek
        /// </summary>
        public string personal_name { get; set; }
        /// <summary>
        /// A linkeket tároló publikus property, amely ha megváltozik értesül róla a felhasználói felület
        /// </summary>
        public ObservableCollection<Link> links { get { return _links; } set { _links = value; OnPropertyChanged(nameof (links)); } }
        /// <summary>
        /// A szerző alternatív nevei
        /// </summary>
        public List<string> alternate_names { get; set; }
        /// <summary>
        /// A szerző születési dátuma
        /// </summary>
        public string birth_date { get; set; }
        /// <summary>
        /// A szerző halálának dátuma
        /// </summary>
        public string death_date { get; set; }
        /// <summary>
        /// A szerző munkásságának leírása
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// A szerző wikipedia oldalára mutató link
        /// </summary>
        public string wikipedia { get; set; }
        /// <summary>
        /// a megjelenítendő kép a szerzőről, ha megváltozik, értesül róla a felhasználói felület
        /// </summary>
        public string photo { get { return _photo; } set { _photo=value; OnPropertyChanged(nameof(photo)); } }
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


    public class Link : INotifyPropertyChanged
    {
        private string _url;
        public string url
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(nameof(url)); }
        }
        public string title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
