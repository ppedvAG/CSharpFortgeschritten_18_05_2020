using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HalloTPL_und_AsyncAwait
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

        private void StartOhneTask(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)(sender)).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(30);
                }
                Dispatcher.Invoke(() => ((Button)(sender)).IsEnabled = true);
            });
        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext(); // muss im UI Thread gemacht werden

            var b = (Button)sender;
            b.IsEnabled = false;

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(30); //die wichtige 
                    Task.Factory.StartNew(() => pb1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts); //so ist gut
                    //Task.Factory.StartNew(() => { pb1.Value = i; Thread.Sleep(1000); }, CancellationToken.None, TaskCreationOptions.None, ts); // UI extrem langsam, worker muss nicht warten
                   // Task.Factory.StartNew(() => { pb1.Value = i; throw new Exception(); }, CancellationToken.None, TaskCreationOptions.None, ts); //UI buggy, stört worker nicht
                }
            }).ContinueWith(t => b.IsEnabled = true, CancellationToken.None, TaskContinuationOptions.None, ts);
        }
    }
}
