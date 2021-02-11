using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToursApp.Database;

namespace ToursApp.Pages
{
    /// <summary>
    /// Interaction logic for TourList.xaml
    /// </summary>
    public partial class TourList : Page
    {
        //alt+enter+enter
        public ObservableCollection<Tour> Tours;
        public ObservableCollection<string> Types = new ObservableCollection<string>();
        public TourList()
        {
            
            InitializeComponent();
            Tours = new ObservableCollection<Tour>(App._context.Tours.ToList());
            Types.Add("Все типы");
            foreach (var item in App._context.Types.ToList())
            {
                Types.Add(item.Name);
            }
            SelectedType.ItemsSource = Types;
            Refresh("", new RoutedEventArgs());
            
        }

        public void Refresh(object sender, RoutedEventArgs e)
        {
            var list = App._context.Tours.ToList();
            var copyList = new List<Tour>(list);
            foreach (var tour in copyList)
            {
                //Поиск
                //Есть ли в наименовании тура строка поиска
                if (!tour.Name.ToLower().Contains(SearchString.Text.ToLower()) && !string.IsNullOrEmpty(SearchString.Text))
                {
                    list.Remove(tour);
                }
                //Тип
                //Лечебно-озд и так далее
                //Если равно 0, то наш тур не относится к выбранному типу
                //SelectedItem
                if (SelectedType.SelectedItem.ToString() != "Все типы" && tour.Types.Where( t => t.Name == SelectedType.SelectedItem.ToString()).Count() == 0)
                {
                    list.Remove(tour);
                }
                //Актуальный
                //true false null
                if (ActualTour.IsChecked.Value && tour.IsActual == false)
                {
                    list.Remove(tour);
                }
            }

            Tours = new ObservableCollection<Tour>(list);
            ListViewTour.ItemsSource = Tours;
        }

        private void SelectedType_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
