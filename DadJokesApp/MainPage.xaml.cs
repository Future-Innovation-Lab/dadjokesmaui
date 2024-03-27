using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using DadJokesApp.Models;


namespace DadJokesApp
{
    public partial class MainPage : ContentPage
    {
        private string _latestJoke;

        public string LatestJoke
        {
            get { return _latestJoke; }
            set { _latestJoke = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewJokeCommand { get; set; }

        private HttpClient _client;


        public MainPage()
        {
            InitializeComponent();

            NewJokeCommand = new Command(GetLatestJoke);

            BindingContext = this;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async void GetLatestJoke(object parameters)
        {
           string response = await _client.GetStringAsync(new Uri("https://icanhazdadjoke.com/"));

            DadJoke responseJoke = JsonConvert.DeserializeObject<DadJoke>(response);
            
            if (responseJoke != null)
            {
                LatestJoke = responseJoke.joke;
            }

        }
       
    }

}
