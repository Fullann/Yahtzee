using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Yahtzee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreEnd : ContentPage
    {
        public ScoreEnd(string allScore)
        {
            InitializeComponent();

            score.Text = $"Bravo pour vos {allScore} points";
        }

        private void NewParty(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}