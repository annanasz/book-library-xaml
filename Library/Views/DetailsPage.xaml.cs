using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Library.Views
{
    /// <summary>
    /// A könyv adatait megjelenítő oldal coodebehind-ja
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// Ha a szerző nevére kattintunk, akkor elnavigálunk a szerző adatait megjelenítő oldalra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Hyperlink_Click_1(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            var clickedItem = sender.AccessKey;
            ViewModel.NavigateToAuthor(clickedItem);//navigáció
        }
    }
}
