using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditHotel.xaml
    /// </summary>
    public partial class EditHotel : Page
    {
        public Hotel currentHotel { get; set; } = new Hotel(); 
        public EditHotel(Hotel selectedHotel)
        {
            if (selectedHotel != null)
            {
                currentHotel = selectedHotel;
            }
            InitializeComponent();
            DataContext = currentHotel;
            CountriesComboBox.ItemsSource = App._context.Countries.ToList();
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Проверка ошибок
            var errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentHotel.Name))
            {
                errors.AppendLine("Нужно заполнить наименование отеля");
            }
            if (currentHotel.CountOfStars<1 || currentHotel.CountOfStars > 5)
            {
                errors.AppendLine("Количество звезд должно быть в диапазоне от 1 до 5");
            }
            if (string.IsNullOrWhiteSpace(currentHotel.Description))
            {
                errors.AppendLine("Нужно заполнить описание отеля");
            }
            if (currentHotel.Country == null)
            {
                errors.AppendLine("Нужно выбрать страну, в которой находится отель");
            }
            //Наличие ошибок
            //!
            if (errors.Length != 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //проверка, новый ли отель
            if (currentHotel.Id == 0)
            {
                App._context.Hotels.Add(currentHotel);
            }
            //Сохранение
            try
            {
                App._context.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
