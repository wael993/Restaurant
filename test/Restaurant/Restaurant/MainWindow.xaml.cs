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

            if (ctx.Rechnung_element.All(x => x.Speise.Produkt_ID != s.Produkt_ID)) //check whether the product has already been invoiced
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
                if (decimal.TryParse(AddPreis.Text, out decimal result)) //Check if price is decimal
                {
                    Speise NeueSpeise = new Speise();
                    NeueSpeise.Beschreibung = AddBeschreibung.Text;
                    NeueSpeise.Preis = Convert.ToDecimal(result);
                    NeueSpeise.Produkt_Name = AddName.Text;
                    ctx.Speise.Add(NeueSpeise);
                    AddBeschreibung.Text = "";
                    AddName.Text = "";
                    AddPreis.Text = "";
                }
                else
                {
                    MessageBox.Show("The entered value must be a number");
                }
            }
            ctx.SaveChanges();
            MainGrid_SpeisekarteVerwalten.DataContext = null;
            MainGrid_SpeisekarteVerwalten.DataContext = ctx.Speise.ToList();
        }
    

        private void RechnungAufnehmen(object sender, MouseButtonEventArgs e)
        {
            Rechnung_element re = new Rechnung_element();
            Speise s = (Speise)SpeiseAufnehmen.SelectedItem;
            if (BestellungAufnehmen_Rechnungsposten != null && BestellungAufnehmen_Rechnungsposten.Any(x => x.Speise.Produkt_ID == s.Produkt_ID))
            {
                re = BestellungAufnehmen_Rechnungsposten.FirstOrDefault(x => x.Speise.Produkt_ID == s.Produkt_ID);
                re.Anzahl++;
            }
            else
            {
                re.Speise = s;
                re.Anzahl = 1;
                BestellungAufnehmen_Rechnungsposten.Add(re);
            }
            Gesamt();
            Bestellung.DataContext = null;
            Bestellung.DataContext = BestellungAufnehmen_Rechnungsposten;
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            Rechnung_element re = (Rechnung_element)Bestellung.SelectedItem;
            if (re.Anzahl == 1)
            {
                BestellungAufnehmen_Rechnungsposten.Remove(re);
            }
            re.Anzahl--;
            Gesamt();
            Bestellung.DataContext = null;
            Bestellung.DataContext = BestellungAufnehmen_Rechnungsposten;
        }
        private void Gesamt()
        {
            decimal total = 0;
            foreach (Rechnung_element pos in BestellungAufnehmen_Rechnungsposten)
            {
                total += pos.Speise.Preis * pos.Anzahl;
            }
            Rechnungskosten.Text = Convert.ToString(total);
        }
        private void Rechnung_Speichern(object sender, RoutedEventArgs e)
        {
            if (ID_Mitarbeiter.Text == "")
            {
                MessageBox.Show("MiTarbeiter ID!");
            }
            else if (Tischnummer.Text == "")
            {
                MessageBox.Show("Tischnummer!");
            }
            else
            {
                Rechnung neueRechnung = new Rechnung();
                neueRechnung.Mitarbeiter_id = Convert.ToInt32(ID_Mitarbeiter.Text);
                neueRechnung.Rechnung_status = "bezahlt";
                neueRechnung.Tisch_ID = Convert.ToInt32(Tischnummer.Text);
                neueRechnung.Rechnung_datum = DateTime.Now;
                ctx.Rechnung.Add(neueRechnung);
                ctx.SaveChanges();
                int id = neueRechnung.Rechnung_id;
                foreach (var v in BestellungAufnehmen_Rechnungsposten)
                {
                    v.Rechnung_id = id;
                    ctx.Rechnung_element.Add(v);
                }
                ctx.SaveChanges();

                ID_Mitarbeiter.Text = "";
                Tischnummer.Text = "";
                BestellungAufnehmen_Rechnungsposten.Clear();
                Bestellung.DataContext = null;
                Bestellung.DataContext = BestellungAufnehmen_Rechnungsposten;
                Rechnungskosten.Text = "";
            }
        }

        private void Grid_Rechnungen_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Status_ändern_Rechnungen(object sender, RoutedEventArgs e)
        {

        }
    }
}
