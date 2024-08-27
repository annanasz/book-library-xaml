
namespace Library.ViewModels.Helpers { 

    /// <summary>
    /// segédosztály a keresési kategóriák kezelésére
    /// </summary>
    public class SearchOption
    {
        /// <summary>
        /// a kategória neve
        /// </summary>
        public string Name {get; set;}
        /// <summary>
        /// a kategória esetén az api híváshoz szükséges string
        /// </summary>
        public string QueryOption;
        public SearchOption(string name, string queryOption)
        {
           Name = name;
           QueryOption = queryOption;
        }
    }
}
