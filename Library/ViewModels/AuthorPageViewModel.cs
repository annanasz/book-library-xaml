using Library.Models;
using Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Library.ViewModels
{
    /// <summary>
    /// A szerzőket megjelenítő oldal ViewModel-je
    /// </summary>
    public class AuthorPageViewModel : ViewModelBase
    {
        /// <summary>
        /// a megjelenítendő szerző adatai
        /// </summary>
        private Author _author;
        /// <summary>
        /// publikus property a szerző lekérdezésére, ha megváltozik akkor értesülnek róla az eseményre feliratkozók, pl. a felhasználói felület
        /// </summary>
        public Author Author
        {
            get { return _author; }
            set 
            { 
                Set(ref _author, value);
                OnPropertyChanged(nameof(Author)); //feliratkozók értesítése
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
        public AuthorPageViewModel()
        {
        }
        /// <summary>
        /// Amikor az oldalra navigálunk, aszinkron módon hívődik meg ez a függvény, itt kérdezzük le api hívás segítségével a megjelenítendő szerző adatait
        /// </summary>
        /// <param name="parameter">paraméterek amiket navigáláskor átadtunk</param>
        /// <param name="mode">a navigáció módja</param>
        /// <param name="state">állapotok</param>
        /// <returns>aszinkron taszk, aminek az eredményét meg lehet várni</returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            string authorKey = (string)parameter; //a szerző azonosítója
            var service = new LibraryService(); //a hálózati kéréseket kezelő szolgáltatás
            Author = await service.GetAuthorAsync(authorKey);
            if(Author.photos == null) //ha nincs kép akkor a placeholder szerző képet beállítjuk
                Author.photo = "/Assets/person_placeholder.jpg";
            else //ellenkező esetben api hívással lekérjük a szerző képét
                Author.photo = $"https://covers.openlibrary.org/a/olid/{authorKey}-L.jpg";
            if (Author.wikipedia != null) //vannak esetek amikor a wikipedia link külön van, azt betesszük a linkek közé, hogy egyben lehessen kezelni őket
            {
                if (Author.links == null)
                    Author.links = new ObservableCollection<Link>();
                Author.links.Add(new Link { title = "Wikipedia", url = Author.wikipedia });

            }
            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
