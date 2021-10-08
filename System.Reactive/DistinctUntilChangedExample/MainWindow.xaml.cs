using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DistinctUntilChangedExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (SynchronizationContext.Current is not null)
            {
                Observable.FromEventPattern(SearchBox, nameof(TextBox.TextChanged))
                    .Select(_ => SearchBox.Text)
                    .Throttle(TimeSpan.FromMilliseconds(400))
                    .DistinctUntilChanged()
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(s => ResultsBox.Items.Add(s));
            }
        }
    }
}
