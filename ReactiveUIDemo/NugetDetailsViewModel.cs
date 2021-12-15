using NuGet.Protocol.Core.Types;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace ReactiveUIDemo
{
    public sealed class NugetDetailsViewModel
    {
        #region Fields

        private readonly IPackageSearchMetadata _metadata;
        private readonly Uri _defaultUrl;

        #endregion

        #region Properties

        public Uri IconUrl => _metadata.IconUrl ?? _defaultUrl;

        public string Desctiption => _metadata.Description;

        public Uri ProjectUrl => _metadata.ProjectUrl;

        public string Title => _metadata.Title;

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> OpenPageCommand { get; }

        #endregion

        #region Constructor

        public NugetDetailsViewModel(IPackageSearchMetadata metadata)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));

            _defaultUrl = new Uri("https://git.io/fAlfh");

            OpenPageCommand = ReactiveCommand.Create(() =>
            {
                Process.Start(new ProcessStartInfo(ProjectUrl.ToString())
                {
                    UseShellExecute = true
                });
            });
        }

        #endregion
    }
}
