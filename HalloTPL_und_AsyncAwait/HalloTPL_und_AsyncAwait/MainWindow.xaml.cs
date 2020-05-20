using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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
                    pb1.Dispatcher.Invoke(() => pb1.Value = i); //worker warten bis UI fertig
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

            cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100; i++)
                {

                    cts.Token.ThrowIfCancellationRequested();

                    if (cts.IsCancellationRequested)
                    {
                        //cleanup
                        break;
                    }

                    //   if (i > 30)
                    //       throw new Exception();

                    Thread.Sleep(30); //die wichtige arbeit

                    Task.Factory.StartNew(() => pb1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts); //so ist gut
                    //Task.Factory.StartNew(() => { pb1.Value = i; Thread.Sleep(1000); }, CancellationToken.None, TaskCreationOptions.None, ts); // UI extrem langsam, worker muss nicht warten
                    // Task.Factory.StartNew(() => { pb1.Value = i; throw new Exception(); }, CancellationToken.None, TaskCreationOptions.None, ts); //UI buggy, stört worker nicht
                }
            });
            //immer zum UI aufräumen
            task.ContinueWith(t => b.IsEnabled = true, CancellationToken.None, TaskContinuationOptions.None, ts);
            //bei Error oder Cancel 
            task.ContinueWith(t =>
            {
                if (t.Exception.InnerException is OperationCanceledException)
                    MessageBox.Show("Erfolgreich Abbgebrochen");
                else
                    MessageBox.Show($"Error: {t.Exception.InnerException.Message}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);
            //alles OK
            task.ContinueWith(t => MessageBox.Show($"Alles Gut!"), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, ts);
        }

        CancellationTokenSource cts = null;

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            cts = new CancellationTokenSource();
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    pb1.Value = i;
                    
                    await Task.Delay(20, cts.Token);

                    if (cts.IsCancellationRequested) //zum schleife beenden
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Abbgebrochen");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }
            ((Button)sender).IsEnabled = !false;
        }

        private async void CountEmployees(object sender, RoutedEventArgs e)
        {
            string conString = "Server=.;Database=Northwind;Trusted_Connection=true";
            conString = "Server=(localdb)\\MSSqlLocaldb;Database=Northwnd;Trusted_Connection=true";
            cts = new CancellationTokenSource();
            pb1.IsIndeterminate = true;
            ((Button)sender).IsEnabled = false;

            try
            {
                using (var con = new SqlConnection(conString))
                {
                    await con.OpenAsync(cts.Token);
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Employees;WAITFOR DELAY '00:00:10'";
                        var count = await cmd.ExecuteScalarAsync(cts.Token);
                        MessageBox.Show($"{count} Employees in DB");
                    }
                } //con.Disose(); ==> con.Close();
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Aborted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pb1.IsIndeterminate = false;
                ((Button)sender).IsEnabled = !false;
            }
        }

        public long GetValueFromAlteLangsameMethode(DateTime dt)
        {
            Thread.Sleep(4000);
            return dt.Ticks * 2;
        }

        public Task<long> GetValueFromAlteLangsameMethodeAsync(DateTime dt)
        {
            return Task.Run(() => GetValueFromAlteLangsameMethode(dt));
        }

        private async void StarteAlteMethode(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((await GetValueFromAlteLangsameMethodeAsync(DateTime.Now)).ToString());
        }

    }
}
