using System;
using System.Threading.Tasks;
using System.Windows;

namespace ThrottlingProgressUpdates
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

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (EnableThrottlingCheckBox.IsChecked == true)
            {
                ProgressValueLabel.Text = "Starting...";

                var (observable, progress) = ObservableProgress.CreateForUi<int>();

                string result = string.Empty;

                using (observable.Subscribe(value => ProgressValueLabel.Text = value.ToString()))
                {
                    result = await Task.Run(() => Solve(progress));
                }

                ProgressValueLabel.Text = $"Done! Result: {result}";
            }
            else
            {
                ProgressValueLabel.Text = "Starting...";

                var progress = new Progress<int>(value => ProgressValueLabel.Text = value.ToString());

                var result = await Task.Run(() => Solve(progress));

                ProgressValueLabel.Text = $"Done! Result: {result}";
            }
        }

        private string Solve(IProgress<int> progress)
        {
            var endTime = DateTime.UtcNow.AddSeconds(3);

            int value = 0;

            while (DateTime.UtcNow < endTime)
            {
                value++;

                progress?.Report(value);
            }

            return value.ToString();
        }
    }
}
