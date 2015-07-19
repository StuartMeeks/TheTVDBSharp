using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using TheTVDBSharp.Entities;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace TheTVDBSharp.Samples.Serializer
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ITheTvDbClient Client = GlobalConfiguration.Client;
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (!Set(ref _searchText, value)) return;

                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SearchCommand { get; }

        public RelayCommand ClearCommand { get; }

        public RelayCommand SerializeCommand { get; }

        public ObservableCollection<TheTvDbSeries> SeriesCollection { get; } = new ObservableCollection<TheTvDbSeries>();

        public MainWindowViewModel()
        {
            SearchCommand = new RelayCommand(() => OnSearchExecuted(), () => !string.IsNullOrWhiteSpace(_searchText));
            ClearCommand = new RelayCommand(() => SeriesCollection.Clear(), () => SeriesCollection.Count > 0);
            SerializeCommand = new RelayCommand(OnSerializeExecuted, () => SeriesCollection.Count > 0);
            SeriesCollection.CollectionChanged += (s, e) =>
            {
                ClearCommand.RaiseCanExecuteChanged();
                SerializeCommand.RaiseCanExecuteChanged();
            };
        }

        private void OnSerializeExecuted()
        {
            var seriesCollection = SeriesCollection.ToArray();

            string seriesCollectionJson = JsonConvert.SerializeObject(seriesCollection, Formatting.Indented);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "series.json");
            File.WriteAllText(path, seriesCollectionJson);
        }

        private async Task OnSearchExecuted()
        {
            var seriesCollection = await Client.SearchSeriesAsync(_searchText, TheTvDbLanguage.English);
            foreach (var series in seriesCollection)
            {
                if (SeriesCollection.Contains(series)) continue;

                var completeSeries = await Client.GetSeriesAsync(series.SeriesId, TheTvDbLanguage.English);
                SeriesCollection.Add(completeSeries);
            }
        }
    }
}
