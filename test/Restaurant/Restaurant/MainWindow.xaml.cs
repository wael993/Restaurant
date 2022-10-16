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

namespace Restaurant
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_ZumHauptmenü(object sender, RoutedEventArgs e)
        {
            
            MainGrid_Menü.Visibility = Visibility.Visible;
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Hidden;
        }

        private void Button_SpeisekarteVerwalten(object sender, RoutedEventArgs e)
        {
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Visible;
            MainGrid_Menü.Visibility = Visibility.Hidden;

        }

        private void Button_RechnungErstellen(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Rechnungsübersicht(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Github(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_DeleteCurrentItem(object sender, RoutedEventArgs e)
        {

        }

        private void Button_SaveChanges(object sender, RoutedEventArgs e)
        {

        }
    }
}
