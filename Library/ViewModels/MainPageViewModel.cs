using Template10.Mvvm;
using System.Collections.ObjectModel;
using Library.Models;
using Library.Services;
using Library.Views;
using Library.ViewModels.Helpers;
using System.Windows.Input;
using System;
using System.ComponentModel;

namespace Library.ViewModels
{
    /// <summary>
    /// Az alkalmazás főoldalának ViewModel-je
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// az oldalon megjelenítendő könyvek listája
        /// </summary>
        public ObservableCollection<BookHeader> Books { get; set; } = new ObservableCollection<BookHeader>();
        /// <summary>
        /// a keresési kritériumok listája, ami alapján böngészhet a felhasználó
        /// </summary>
        public ObservableCollection<SearchOption> SearchOptions { get; set; } = new ObservableCollection<SearchOption>();
        /// <summary>
        /// a keresési eredmény
        /// </summary>
        public SearchResult result { get; set; }
        /// <summary>
        /// a kiválasztott keresési kritérium
        /// </summary>
        public SearchOption SelectedSearchOption { get; set; }
        /// <summary>
        /// Command a könyv kereséshez
        /// </summary>
        public DelegateCommand SearchBookCommand { get; }
        /// <summary>
        /// Command a kiválasztott keresési kritérium megváltozásának kezelésére
        /// </summary>
        public ICommand SearchOptionSelectedChanged { get; }

        /// <summary>
        /// a keresés szövege
        /// </summary>
        private string _searchquery;
        /// <summary>
        /// a találatok kiirásánál a szöveg
        /// </summary>
        private string _resultstring;
        /// <summary>
        /// a töltőikon láthatósága, alapvetően nem látjuk, csak ha elindítunk egy keresést
        /// </summary>
        private string _progressringvisibility = "Collapsed";
        /// <summary>
        /// A töltőikon property-je
        /// </summary>
        public string ProgressRingVisibility
        {
            get { return _progressringvisibility; }
            set { Set(ref _progressringvisibility, value); }
        }
        /// <summary>
        /// A keresési szöveg property-je
        /// </summary>
        public string SearchQuery
        {
            get { return _searchquery; }
            set { Set(ref _searchquery, value); }
        }
        /// <summary>
        /// Az eredménynél megjelenítendő szöveg property-je
        /// </summary>
        public string ResultString
        {
            get { return _resultstring; }
            set { Set(ref _resultstring, value); }
        }

        private NetworkService networkService;

        /// <summary>
        /// konstruktor, ahol beállítjuk a commandokat valamint a 4 keresési kategóriát is hozzáadjuk
        /// </summary>

        public MainPageViewModel()
        {
            SearchBookCommand = new DelegateCommand(GetBooks);
            SearchOptionSelectedChanged = new ComboBoxSelectionChangedCommand(SelectedOptionChanged);
            SelectedSearchOption = new SearchOption("General search", "q");
            SearchOptions.Add(SelectedSearchOption);
            SearchOptions.Add(new SearchOption("Title", "title"));
            SearchOptions.Add(new SearchOption("Subject", "subject"));
            SearchOptions.Add(new SearchOption("Author", "author"));
            networkService = new NetworkService();
        }

        /// <summary>
        /// Ha megváltozik a kiválasztott keresési kategória, akkor megváltoztatjuk a SelectedSerachOption értékét, hogy naprakész legyen
        /// </summary>
        /// <param name="newSelection"></param>
        private void SelectedOptionChanged(object newSelection)
        {
            SelectedSearchOption = (SearchOption)newSelection;
        }
        /// <summary>
        /// A könyvek lekérdezése aszinkron módion, keresés esetén
        /// </summary>
        private async void GetBooks()
        {
            CheckInternetConnection();
            if (IsInternetAvailable)
            {
                ProgressRingVisibility = "Visible"; //a töltőikon megjelenik
                Books.Clear(); //kitöröljük az előző keresés eredményét
                ResultString = "";
                var service = new LibraryService();//a hálózati kommunikációt kezelő szolgáltatás
                var result = await service.GetBookHeadersAsync(SearchQuery, SelectedSearchOption.QueryOption);
                foreach (BookHeader b in result.docs) //minden könyvnek beállítjuk a borítóképének az elérési útvonalát
                {
                    if (b.cover_i == 0) //ha nincs kép az api hívás eredményében, akkor egy placeholder kép állítódik be
                        b.cover_url = "/Assets/no_cover.jpg";
                    else b.cover_url = $"https://covers.openlibrary.org/b/id/{b.cover_i}-L.jpg";
                    Books.Add(b);
                }
                ProgressRingVisibility = "Collapsed"; //a töltőikon eltűnik

                ResultString = $"{Books.Count} results found"; //kiírjuk a találatok számát
            }
            else
            {
                ResultString = "You are not connected to the internet. Connect and browse the library!";
                Books.Clear ();
            }
        }

        /// <summary>
        /// Egy könyvre kattintva elnavigálunk a önyvet részletesen megjelenítő oldalra, ha csatlakozva vagyunk az internetre
        /// </summary>
        /// <param name="bh">átadjuk a könyvfejlécet, amire rákattintottunk</param>
        public void NavigateToDetails(BookHeader bh)
        {
            CheckInternetConnection();
            if (IsInternetAvailable)
            {
                var serializedParam = Newtonsoft.Json.JsonConvert.SerializeObject(bh);
                NavigationService.Navigate(typeof(DetailsPage), serializedParam);
            }
            else
            {
                ResultString = "You are not connected to the internet. Connect and browse the library!";
                Books.Clear();
            }

        }
        /// <summary>
        /// változó, amiben eltároljuk, hogy elérhető-e az internet
        /// </summary>
        private bool isInternetAvailable;
        public bool IsInternetAvailable
        {
            get { return isInternetAvailable; }
            set
            {
                isInternetAvailable = value;
            }
        }
        /// <summary>
        /// lekérdezzük az internet elérhetőségét
        /// </summary>
        public void CheckInternetConnection()
        {
            IsInternetAvailable = networkService.IsInternetAvailable();
        }
    }
}

