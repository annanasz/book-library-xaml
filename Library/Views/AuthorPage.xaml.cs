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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Library.Views
{
    /// <summary>
    /// A szerző oldalának a code-behindja
    /// </summary>
    public sealed partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// ha a szerző egy linkjére kattintunk, akkor elnavigálunk a megfelelő weboldalra 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Uri uri = new Uri(sender.NavigateUri.ToString());
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
