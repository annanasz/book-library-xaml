using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    /// <summary>
    /// a keresési eredmények tárolására létrehozott osztály, keresés esetén az api ilyen formában adja vissza az adatokat
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// a találatok száma
        /// </summary>
        public int numFound;
        /// <summary>
        /// hol kezdődnek a találatok
        /// </summary>
        public int start;
        /// <summary>
        /// pontosan hány találat van
        /// </summary>
        public bool numFoundExact;
        /// <summary>
        /// a talált könyvek listája
        /// </summary>
        public List<BookHeader> docs;
    }
}
