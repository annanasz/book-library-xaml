using Library.Models;
using System;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;

namespace Library.Views
{
    /// <summary>
    /// a főoldal code-behindja
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            ViewModel.CheckInternetConnection();
        }
        /// <summary>
        /// ha rákattintunk egy könyvre, akkor elnavigálunk a könyv adatait tartalmaző részletes oldalra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Book_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bookHeader = (BookHeader)e.ClickedItem;
            ViewModel.NavigateToDetails(bookHeader);//navigáció
        }
    }
}
