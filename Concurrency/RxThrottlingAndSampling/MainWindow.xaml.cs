using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace RxThrottlingAndSampling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDisposable _throttlingObservable;
        private IDisposable _samplingObservable;

        private readonly SynchronizationContext _synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();

            _synchronizationContext = SynchronizationContext.Current;
        }

        private void StartThrottlingBtn_Click(object sender, RoutedEventArgs e)
        {
            _throttlingObservable = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                    handler => (s, a) => handler(s, a),
                    handler => MouseMove += handler,
                    handler => MouseMove -= handler)
                .Select(x => x.EventArgs.GetPosition(this))
                .Throttle(TimeSpan.FromSeconds(1))
                .ObserveOn(_synchronizationContext)
                .Subscribe(x => SetCoordinates(x.X, x.Y));

            StartThrottlingBtn.IsEnabled = false;
            StopThrottlingBtn.IsEnabled = true;
            StartSamplingBtn.IsEnabled = false;
            StopSamplingBtn.IsEnabled = false;
        }

        private void StopThrottlingBtn_Click(object sender, RoutedEventArgs e)
        {
            _throttlingObservable.Dispose();

            _throttlingObservable = null;

            ClearCoordinates();

            StartThrottlingBtn.IsEnabled = true;
            StopThrottlingBtn.IsEnabled = false;
            StartSamplingBtn.IsEnabled = true;
            StopSamplingBtn.IsEnabled = false;
        }

        private void StartSamplingBtn_Click(object sender, RoutedEventArgs e)
        {
            _samplingObservable = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                    handler => (s, a) => handler(s, a),
                    handler => MouseMove += handler,
                    handler => MouseMove -= handler)
                .Select(x => x.EventArgs.GetPosition(this))
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(_synchronizationContext)
                .Subscribe(x => SetCoordinates(x.X, x.Y));

            StartThrottlingBtn.IsEnabled = false;
            StopThrottlingBtn.IsEnabled = false;
            StartSamplingBtn.IsEnabled = false;
            StopSamplingBtn.IsEnabled = true;
        }

        private void StopSamplingBtn_Click(object sender, RoutedEventArgs e)
        {
            _samplingObservable.Dispose();

            _samplingObservable = null;

            ClearCoordinates();

            StartThrottlingBtn.IsEnabled = true;
            StopThrottlingBtn.IsEnabled = false;
            StartSamplingBtn.IsEnabled = true;
            StopSamplingBtn.IsEnabled = false;
        }

        private void ClearCoordinates()
        {
            XValueTextBlock.Text = "";
            YValueTextBlock.Text = "";
        }

        private void SetCoordinates(double x, double y)
        {
            XValueTextBlock.Text = x.ToString();
            YValueTextBlock.Text = y.ToString();
        }
    }
}
