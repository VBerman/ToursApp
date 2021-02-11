using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToursApp.Database;

namespace ToursApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // alt+enter + enter
        public static DatabaseContext _context = new DatabaseContext();
    }
}
