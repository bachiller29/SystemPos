namespace PosSystem.Views
{
    public partial class MainPage : ContentPage
    {  
        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

        private int count = 0;

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterBtn.Text = $"Clics: {count}";
        }
    }

}
