using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Media.Imaging;

namespace ReactiveUIDemo
{
    /// <summary>
    /// Interaction logic for NugetDetailsView.xaml
    /// </summary>
    public partial class NugetDetailsView
    {
        public NugetDetailsView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                // Our 4th parameter we convert from Url into a BitmapImage. 
                // This is an easy way of doing value conversion using ReactiveUI binding.
                this.OneWayBind(ViewModel,
                    vm => vm.IconUrl,
                    view => view.IconImage.Source,
                    url => url == null ? null : new BitmapImage(url))
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    vm => vm.Title,
                    view => view.TitleRun.Text)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel,
                    vm => vm.Desctiption,
                    view => view.DescriptionRun.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    vm => vm.OpenPageCommand,
                    view => view.OpenLink)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
