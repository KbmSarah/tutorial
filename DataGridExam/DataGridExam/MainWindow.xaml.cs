using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace DataGridExam
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomerList custdata = null;
        public MainWindow()
        {
            InitializeComponent();
            custdata = new CustomerList();

            custdata.CollectionChanged += CustomerDataChanged;
            CustomerGrid.DataContext = custdata;
        }
        private void CustomerDataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Collection Changed");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(custdata.ElementAt(0).FirstName);
        }
    }

    public class CustomerList : ObservableCollection<Customer>
    {
        public CustomerList() : base()
        {
            Add(new Customer() { FirstName = "Orlando", LastName = "Gee", Email = new Uri("mailto:orando@naver.com"), IsMember = true, Status = OrderStatus.Shipped });
            Add(new Customer() { FirstName = "keith", LastName = "Harris", Email = new Uri("mailto:keith@naver.com"), IsMember = false, Status = OrderStatus.Received });
            Add(new Customer() { FirstName = "Donna", LastName = "Carreras", Email = new Uri("mailto:donna@naver.com"), IsMember = true, Status = OrderStatus.Processing });
            Add(new Customer() { FirstName = "Janet", LastName = "Gates", Email = new Uri("mailto:janet@naver.com"), IsMember = true, Status = OrderStatus.New });
            Add(new Customer() { FirstName = "Lucy", LastName = "Harrington", Email = new Uri("mailto:lucy@naver.com"), IsMember = false, Status = OrderStatus.Processing });
            Add(new Customer() { FirstName = "Rosmarie", LastName = "Carroll", Email = new Uri("mailto:rosmarie@naver.com"), IsMember = true, Status = OrderStatus.None });
            Add(new Customer() { FirstName = "Dominic", LastName = "Gash", Email = new Uri("mailto:dominic@naver.com"), IsMember = true, Status = OrderStatus.Received });
            Add(new Customer() { FirstName = "Kathleen", LastName = "Garza", Email = new Uri("mailto:kathleen@naver.com"), IsMember = false, Status = OrderStatus.None });
            Add(new Customer() { FirstName = "Katherine", LastName = "Harding", Email = new Uri("mailto:katherine@naver.com"), IsMember = false, Status = OrderStatus.New });
            Add(new Customer() { FirstName = "Johnny", LastName = "Caprio", Email = new Uri("mailto:hojnny@naver.com"), IsMember = true, Status = OrderStatus.Received });
        }
    }

    //Defines the customer object
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Email { get; set; }
        public bool IsMember { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        None,
        New,
        Processing,
        Shipped,
        Received
    }

    //Converts the mailto uri to a string with just the customer alias
    public class EmailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string email = value.ToString();
                int index = email.IndexOf("@");
                string alias = email.Substring(7, index - 7);
                return alias;
            }
            else
            {
                string email = "";
                return email;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Uri email = new Uri((string)value);
            return email;
        }
    }

}
