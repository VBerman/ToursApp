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
    /// Interaction logic for HotelList.xaml
    /// </summary>
    public partial class HotelList : Page
    {
        public static int countCurrentHotel = 15;
        public ObservableCollection<Hotel> shortList = new ObservableCollection<Hotel>(App._context.Hotels.ToList().GetRange(0, countCurrentHotel));
        public HotelList()
        {
            InitializeComponent();
            SetTextNumberPage();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Shift+F9
            NavigationService.Navigate(new EditHotel((sender as Button).DataContext as Hotel));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditHotel(null));
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            //Получаем отели для удаления
            var hotelsForRemoving = GridHotels.SelectedItems.Cast<Hotel>().ToList();
            //Спрашиваем пользователя
            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //try+tab+tab
                try
                {
                    //Удаление
                    App._context.Hotels.RemoveRange(hotelsForRemoving);
                    //ВАЖНО! Необходимо обеспечить сохранение без вылетов
                    App._context.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Page_IsVisibleChanged(null, new DependencyPropertyChangedEventArgs());
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
                
            }
            
            
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if+tab+tab

            if (Visibility == Visibility.Visible)
            {
                //Проходимся по всем сущностям и обновляем их
                App._context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                shortList = new ObservableCollection<Hotel>(App._context.Hotels.ToList().GetRange(0, countCurrentHotel));
                GridHotels.ItemsSource = shortList;

            }
        }

        private void SetRangeList()
        {
            try
            {
                var number = int.Parse(NumberPage.Text);
                
                var endCount = countCurrentHotel * number;
                var countByResult = countCurrentHotel;
                
                if (endCount + countCurrentHotel > App._context.Hotels.Count())
                {
                    countByResult = App._context.Hotels.Count() - endCount;
                }
                if (countByResult==0)
                {
                    return;
                }
                shortList = new ObservableCollection<Hotel>(App._context.Hotels.ToList().GetRange(endCount, countByResult));
                Page_IsVisibleChanged(null, new DependencyPropertyChangedEventArgs());
                //Для примера вывода переменных
                try
                {
                    SetTextNumberPage();
                }
                catch (Exception ex)
                {
                    
                 
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            
        }
        private void SetTextNumberPage()
        {
            var countPages = (App._context.Hotels.Count() / countCurrentHotel).ToString();
            Test.Text = countPages;
        }
        private void PaginationToNull_Click(object sender, RoutedEventArgs e)
        {
            NumberPage.Text = "0";
            SetRangeList();
        }

        private void PaginationMinusOne_Click(object sender, RoutedEventArgs e)
        {
            NumberPage.Text = (int.Parse(NumberPage.Text)-1).ToString();
            SetRangeList();
        }

        private void PaginationPlusOne_Click(object sender, RoutedEventArgs e)
        {
            NumberPage.Text = (int.Parse(NumberPage.Text) + 1).ToString();
            SetRangeList();
        }

        private void PaginationToMax_Click(object sender, RoutedEventArgs e)
        {
            NumberPage.Text = (App._context.Hotels.Count()/countCurrentHotel).ToString();
            SetRangeList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            countCurrentHotel = int.Parse(CountHotel.Text);
            SetRangeList();
        }

        private void NumberPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetRangeList();
        }

        private void NumberPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetRangeList();
            }
        }
    }
}
