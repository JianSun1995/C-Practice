using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace LandmarkAI
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files(*.png;*.jpg)|*.png;*.jpg;*jpeg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog() != true) return;
            var fileName = dialog.FileName;
            SelectedImage.Source = new BitmapImage(new Uri(fileName));

            MakePredictionAsync(fileName);
        }

        private async void MakePredictionAsync(string fileName)
        {
            var url =
                "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/94be55ae-30bf-4c40-b4da-4c7d11f3543f/image?iterationId=21f206d8-3676-47e7-a9a7-040e5fb68ae7";
            var predictionKey = "83ea5d8404e945309952b2b760fff0dc";
            var contentType = "application/octet-stream";
            var file = File.ReadAllBytes(fileName);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, content);

                    //! We can communicate with the Http server with these methods.
                    var responseString = await response.Content.ReadAsStringAsync();

                    var predictions = (JsonConvert.DeserializeObject<CustomVision>(responseString)).Predictions;

                    predictionListView.ItemsSource = predictions;
                }
            }
        }
    }
}