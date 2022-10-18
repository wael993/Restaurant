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
using System.Data.Entity;
using System.Diagnostics;

namespace Restaurant
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rechnung_element> Rechnungspositionen;
        List<Rechnung_element> BestellungAufnehmen_Rechnungsposten = new List<Rechnung_element>();
        private La_Trattoria_del_PostillioneEntities ctx = new La_Trattoria_del_PostillioneEntities();
        public MainWindow()
        {
            InitializeComponent();
            MainGrid_Menü.Visibility = Visibility.Visible;
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Hidden;
            MainGrid_Rechungen_erstellen.Visibility = Visibility.Hidden;
            MainGrid_Rechnungsübersicht.Visibility = Visibility.Hidden;
        }
        private void Button_ZumHauptmenü(object sender, RoutedEventArgs e)
        {
            
            MainGrid_Menü.Visibility = Visibility.Visible;
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Hidden;
            MainGrid_Rechungen_erstellen.Visibility = Visibility.Hidden;
            MainGrid_Rechnungsübersicht.Visibility = Visibility.Hidden;

        }

        private void Button_SpeisekarteVerwalten(object sender, RoutedEventArgs e)
        {
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Visible;
            MainGrid_Menü.Visibility = Visibility.Hidden;
            MainGrid_Rechungen_erstellen.Visibility = Visibility.Hidden;
            MainGrid_Rechnungsübersicht.Visibility = Visibility.Hidden;
            ctx.Speise.Load();
            MainGrid_SpeisekarteVerwalten.DataContext = ctx.Speise.ToList();

            
        }

        private void Button_RechnungErstellen(object sender, RoutedEventArgs e)
        {
            MainGrid_Rechungen_erstellen.Visibility = Visibility.Visible;
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Hidden;
            MainGrid_Menü.Visibility = Visibility.Hidden;
            MainGrid_Rechnungsübersicht.Visibility = Visibility.Hidden;
            ctx.Speise.Load();
            SpeiseAufnehmen.DataContext = ctx.Speise.ToList();
            Bestellung.DataContext = BestellungAufnehmen_Rechnungsposten;
        }

        private void Button_Rechnungsübersicht(object sender, RoutedEventArgs e)
        {
            MainGrid_Rechnungsübersicht.Visibility = Visibility.Visible;
            MainGrid_Rechungen_erstellen.Visibility = Visibility.Hidden;
            MainGrid_SpeisekarteVerwalten.Visibility = Visibility.Hidden;
            MainGrid_Menü.Visibility = Visibility.Hidden;
            ctx.Rechnung.Load();
            Grid_Rechnungen.DataContext = ctx.Rechnung.ToList();
        }

        private void Button_Github(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/wael993/Restaurant");

        }

        private void Button_DeleteCurrentItem(object sender, RoutedEventArgs e)
        {
            int id = (int)AusgewähltesProdukt_ID.Content; //Label (hidden)
            Speise s = ctx.Speise.Where(x => x.Produkt_ID == id).FirstOrDefault();

            if (ctx.Rechnung_element.All(x => x.Speise.Produkt_ID != s.Produkt_ID)) //überprüfen,ob Produkt schon in Rechnung vorhanden
            {
                ctx.Speise.Remove(s);
                ctx.SaveChanges();
                MainGrid_SpeisekarteVerwalten.DataContext = null;
                MainGrid_SpeisekarteVerwalten.DataContext = ctx.Speise.ToList();
            }
            else
            {
                MessageBox.Show("Produkt schon in Rechnung vorhanden.");
            }

        }

        private void Button_SaveChanges(object sender, RoutedEventArgs e)
        {
            if (AddBeschreibung.Text != "" && AddName.Text != "" && AddPreis.Text != "")
            {
                if (decimal.TryParse(AddPreis.Text, out decimal result)) //Prüfen, ob Preis deicmal
                {
                    Speise NeueSpeise = new Speise();
                    NeueSpeise.Beschreibung = AddBeschreibung.Text;
                    NeueSpeise.Preis = Convert.ToDecimal(result);
                    NeueSpeise.Produkt_Name = AddName.Text;
                    //ctx.Speise.Count();
                    ctx.Speise.Add(NeueSpeise);
                    AddBeschreibung.Text = "";
                    AddName.Text = "";
                    AddPreis.Text = "";
                }
                else
                {
                    MessageBox.Show("Der eingegebe Wert muss eine Zahl sein");
                }
            }
            ctx.SaveChanges();
            MainGrid_SpeisekarteVerwalten.DataContext = null;
            MainGrid_SpeisekarteVerwalten.DataContext = ctx.Speise.ToList();
        }
    }

        private void RechnungAufnehmen(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rechnung_Speichern(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Rechnungen_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Status_ändern_Rechnungen(object sender, RoutedEventArgs e)
        {

        }
    }
}
