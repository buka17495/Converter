using GrpcClient;
using System.Windows;
using System.Windows.Media;

namespace Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _uri = "https://localhost:7022";

        private static readonly Client Client = new(_uri);

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await Client.ConvertAsync(Input.Text);
            if (result == null)
                return;

            if (result.IsSuccess)
            {
                Output.Text = result.Result;
            }
            else
            {
                Output.Background = Brushes.Red;
                Output.Text = result.ErrorMessage;
            }
        }
    }
}
