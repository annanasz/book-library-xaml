using Library.Models;
using Library.Services;
using Library.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Library.ViewModels
{
    /// <summary>
    /// A könyv részleteit megjelenítő oldal ViewModel-je
    /// </summary>
    public class DetailsPageViewModel : ViewModelBase
    {
        /// <summary>
        /// az oldalon megjelenítendő könyv
        /// </summary>
        private Book _book;
        /// <summary>
        /// Az oldalon megjelenítendő könyv azonosítója
        /// </summary>
        private string _key;
        /// <summary>
        /// property a könyv adatainak lekérdezésére, ha megváltozik az értéke akkor a feliratkozók értesülnek róla
        /// </summary>
        public Book Book
        {
            get { return _book; }
            set { 
                Set(ref _book, value);
                OnPropertyChanged(nameof(Book));//feliratkozók értesítése
            }
        }

        /// <summary>
        /// esemény amelyre feliretkozni lehet és a feliratkozók értesülnek ha megváltozik a property
        /// </summary>

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// változás esetén értesítjük a feliratkozókat
        /// </summary>
        /// <param name="propertyName">a megváltozott property neve</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// property a könyv azonosítójának a lekérdezésére, ha megváltozik az értéke akkor a feliratkozók értesülnek róla
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { Set(ref _key, value); }
        }

        /// <summary>
        /// command a könyvborító nagyobb méretben való megjelenítésére
        /// </summary>

        public ICommand ShowImageCommand { get; private set; }

        /// <summary>
        /// konstruktor, a ShowImageCommandot inicializáljuk
        /// </summary>
        public DetailsPageViewModel()
        {
            this.ShowImageCommand = new DelegateCommand(ShowImage);
        }

        /// <summary>
        /// ha a felhasználó a képre kattint, megjelenik nagyobb méretben egy dialógusablakban
        /// </summary>
        private async void ShowImage()
        {
            var dialog = new ImageDialog();
            dialog.ImageUrl = Book.header.cover_url;
            await dialog.ShowAsync();
        }
        /// <summary>
        /// Amikor az oldalra navigálunk, aszinkron módon hívődik meg ez a függvény, itt kérdezzük le api hívás segítségével a megjelenítendő könyv adatait
        /// </summary>
        /// <param name="parameter">paraméterek amiket navigáláskor átadtunk</param>
        /// <param name="mode">a navigáció módja</param>
        /// <param name="state">állapotok</param>
        /// <returns>aszinkron taszk, aminek az eredményét meg lehet várni</returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            BookHeader bh = Newtonsoft.Json.JsonConvert.DeserializeObject<BookHeader>(parameter.ToString()); //a paraméterben átadtunk navigáláskor a könyv fejlécét, azt ezen az oldalon kinyerjük
           
            Key = bh.key;
            var service = new LibraryService(); //szolgáltatás a hálózati kommunikációhoz
            var result = await service.GetBookAsync(Key);
            Book = result;
            if (Book.description == null) //ha az api nem szolgáltat leírást a könyvhöz
            {
                Book.description = "-";
            }
            Book.header = bh;
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
        /// <summary>
        /// A szerző nevére kattintva elnavigálhatunk az ő oldalára, olyankor ez a függvény hívódik meg
        /// </summary>
        /// <param name="clickeditem">a szerző, amire rákattintottunk</param>
        public void NavigateToAuthor(string clickeditem)
        {
            var index = Book.header.author_name.IndexOf(clickeditem);
            var authorKey = Book.header.author_key[index];
            NavigationService.Navigate(typeof(AuthorPage), authorKey); //elnavigálunk a szerző oldalára
        }
    }
}
