using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
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

namespace RxSendingNotificationsToContext
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var uiContext = SynchronizationContext.Current;

            Trace.WriteLine($"UI Thread is {Environment.CurrentManagedThreadId}");

            Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                    handler => (s, a) => handler(s, a),
                    handler => MouseMove += handler,
                    handler => MouseMove -= handler)
                .Select(evt => evt.EventArgs.GetPosition(this))
                .ObserveOn(Scheduler.Default)
                .Select(position =>
                {
                    Thread.Sleep(100); // complex calculations

                    var result = position.X + position.Y;

                    var thread = Environment.CurrentManagedThreadId;

                    Trace.WriteLine($"Calculated result {result} on thread {thread}");

                    return result;
                })
                .ObserveOn(uiContext)
                .Subscribe(x => Trace.WriteLine($"Result {x} on thread {Environment.CurrentManagedThreadId}"));
        }
    }
}
