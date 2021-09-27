using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
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

namespace SimpleDrawingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDisposable _drawSubscription;

        public MainWindow()
        {
            InitializeComponent();

            var mouseDowns = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseDown));
            var mouseUps = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseUp));
            var movements = Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseMove));

            Polyline? currentLine = null;

            _drawSubscription = movements
                .SkipUntil(mouseDowns.Do(_ =>
                {
                    currentLine = CreateNewLine();
                    DrawCanvas.Children.Add(currentLine);
                }))
                .TakeUntil(mouseUps)
                .Select(x => x.EventArgs.GetPosition(this))
                .Repeat()
                .Subscribe(pos => currentLine?.Points.Add(pos));
        }

        private static Polyline CreateNewLine()
        {
            Polyline line = new()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            return line;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _drawSubscription.Dispose();
        }
    }
}
