using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KonyvtarAsztali
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Statisztika service;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Read();
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Read()
        {
            this.service = new Statisztika();
            service.Fill();
            dataGridBooks.ItemsSource = service.Konyvek;
        }
    }
}