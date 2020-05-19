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

namespace EfModelFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Model1Container context = new Model1Container();

        private void Laden(object sender, RoutedEventArgs e)
        {
            myGrid.ItemsSource = context.PersonSet.OfType<Kunde>()
                                                  .Where(x => x.Name.StartsWith("F"))
                                                  .OrderBy(x => x.GebDatum.Second)
                                                  .ToList();
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            var k = new Kunde() { Name = "Fred", GebDatum = DateTime.Now.AddDays(-78391), KdNummer = "000" };
            k.Mitarbeiter = new Mitarbeiter() { Name = "Wilma", GebDatum = DateTime.Now.AddDays(-731), Beruf = "lala" };
            context.PersonSet.Add(k);
            context.SaveChanges();
        }
    }
}
