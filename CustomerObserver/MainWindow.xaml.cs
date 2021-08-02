using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CustomerObserver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {


        protected void OnpropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<NotificationData> _Data_List { get; set; }
        public ObservableCollection<NotificationData> Data_List { get { return _Data_List; } set { _Data_List = value; OnpropertyChanged(); } }

        ObservableCollection<NotificationData> new_Data_List = new ObservableCollection<NotificationData>();

     

        public NotificationData notificationData;




        public MainWindow()
        {
            InitializeComponent();


            if (!File.Exists($"../../Data.json"))
            {
                File.Create($"../../Data.json").Close();
            }

            new_Data_List = Deserialize();


            AddListBox();
        }








        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new_Data_List = Deserialize();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           

            Data_List = Deserialize();
            AddListBox();
            AddList();



        }


        public ObservableCollection<NotificationData> Deserialize()
        {


            string data = File.ReadAllText($"../../Data.json");

            Data_List = JsonConvert.DeserializeObject<ObservableCollection<NotificationData>>(data);

            if (Data_List != null)
            {
                return Data_List;
            }



            return new ObservableCollection<NotificationData>();
        }

        private void Serialize(ObservableCollection<NotificationData> notificationData)
        {
            if (!File.Exists($"../../Data.json"))
            {
                File.Create($"../../Data.json").Close();
            }

            var f = Newtonsoft.Json.Formatting.Indented;
            string data = JsonConvert.SerializeObject(notificationData, f);

            File.WriteAllText($"../../Data.json", data);
        }


        public void AddList()
        {
            var TextRangeofRichTextBox = new TextRange(AboutProductRTbox.Document.ContentStart, AboutProductRTbox.Document.ContentEnd);



            new_Data_List.Add(new NotificationData(ProductNameTbox.Text, CostumerEmailTbox.Text, TextRangeofRichTextBox.Text)
            {
                ProductName = ProductNameTbox.Text,
                CustomerEmail = CostumerEmailTbox.Text,
                AboutProduct = TextRangeofRichTextBox.Text,

            });

            Serialize(new_Data_List);

        }

        public void AddListBox()
        {

            DataListbox.ItemsSource = new_Data_List;



        }
        private void Notify_Click(object sender, RoutedEventArgs e)
        {

            foreach (var customer in new_Data_List)
            {
                customer.GetMessage();
            }

        }

        private void ProductNameTbox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ProductNameTbox.Text == "Name of product")
            {
                ProductNameTbox.Text = null;



                Color color1 = new Color();
                color1 = Color.FromArgb(255, 37, 191, 191);

                ProductNameTbox.Foreground = new SolidColorBrush(color1);


            }
        }

        private void ProductNameTbox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ProductNameTbox.Text == "")
            {

                Color color2 = new Color();
                color2 = Color.FromArgb(255, 110, 127, 128);

                ProductNameTbox.Text = "Name of product";
                ProductNameTbox.Foreground = new SolidColorBrush(color2);
            }
        }

        private void CostumerEmailTbox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (CostumerEmailTbox.Text == "Costumer's email")
            {
                CostumerEmailTbox.Text = null;



                Color color1 = new Color();
                color1 = Color.FromArgb(255, 37, 191, 191);

                CostumerEmailTbox.Foreground = new SolidColorBrush(color1);


            }
        }

        private void CostumerEmailTbox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (CostumerEmailTbox.Text == "")
            {

                Color color2 = new Color();
                color2 = Color.FromArgb(255, 110, 127, 128);

                CostumerEmailTbox.Text = "Costumer's email";
                CostumerEmailTbox.Foreground = new SolidColorBrush(color2);
            }
        }

        private void AboutProductRTbox_MouseEnter(object sender, MouseEventArgs e)
        {
            var TextRangeofRichTextBox = new TextRange(AboutProductRTbox.Document.ContentStart, AboutProductRTbox.Document.ContentEnd);

            int difference = AboutProductRTbox.Document.ContentStart.GetOffsetToPosition(AboutProductRTbox.Document.ContentEnd);


            if (difference>0)
            {



                TextRangeofRichTextBox.Text = string.Empty;
                Color color2 = new Color();
                color2 = Color.FromArgb(255, 255, 0, 0);

                AboutProductRTbox.Foreground = new SolidColorBrush(color2);


            }
        }

        private void AboutProductRTbox_MouseLeave(object sender, MouseEventArgs e)
        {
            var TextRangeofRichTextBox = new TextRange(AboutProductRTbox.Document.ContentStart, AboutProductRTbox.Document.ContentEnd);

            int difference = AboutProductRTbox.Document.ContentStart.GetOffsetToPosition(AboutProductRTbox.Document.ContentEnd);

            if (difference <= 0)
            {


                Color color1 = new Color();
                color1 = Color.FromArgb(255, 110, 127, 128);
                TextRangeofRichTextBox.Text = @"About product";
                CostumerEmailTbox.Foreground = new SolidColorBrush(color1);
            }
        }
    }




}
