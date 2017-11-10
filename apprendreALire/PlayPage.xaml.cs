using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using apprendreALire.myClass;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace apprendreALire
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class PlayPage : Page
	{
		Play newPlay = new Play();
		Turn turn;
		public PlayPage()
		{
            this.InitializeComponent();
            SetTurn();			
		}

        void SetTurn()
        {
            turn = newPlay.GetTurn();
            HintBox.Content = turn.toFind.name;
            SetImage(ImageToFind, turn.toFind);
            SetGuesses(turn.toPlay);
            Uri newuri = new Uri("ms-appx:///Assets/audio/" + turn.toFind.soundFileName);
            myPlayer.Source = newuri;
            myPlayer.Play();
        }

        void SetGuesses(List<Word> listWords)
		{
			listChoices.Children.Clear();
			foreach (Word myWord in listWords)
			{
				var newButton = new Button();
				newButton.Name = "btnCheck_" + myWord.id.ToString();
				newButton.Content = myWord.name;
				newButton.Height = 90;
				newButton.FontSize = 35;
				newButton.Width = double.NaN;
				newButton.Margin = new Thickness(10, 10, 10, 10);
				newButton.Style = (Style)App.Current.Resources["MyButtonStyle"];
				newButton.Background = new SolidColorBrush(Color.FromArgb(210, 226, 54, 100));
				newButton.FontFamily = (FontFamily)Application.Current.Resources["DyslexieFont"];
				newButton.Click += new RoutedEventHandler(CheckResult);
				listChoices.Children.Add(newButton);
			}
		}

		void CheckResult(object sender, RoutedEventArgs e)
		{
			Button b = (Button)sender;
			var btnResult = new Button();
			btnResult.Height = 150;
			btnResult.Width = 400;
			btnResult.FontSize = 40;
			Canvas.SetZIndex(b, 8);
			btnResult.Style = (Style)App.Current.Resources["MyButtonStyle"];
			btnResult.FontFamily = (FontFamily)Application.Current.Resources["DyslexieFont"];
			btnResult.Click += new RoutedEventHandler(NextTurn);
			btnResult.VerticalAlignment = VerticalAlignment.Center;
			btnResult.HorizontalAlignment = HorizontalAlignment.Center;
			FogLayer.Visibility = Visibility.Visible;
			if (b.Name.Substring(9)==turn.toFind.id.ToString())
			{
				btnResult.Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));
				btnResult.Content = "Bien joué!!";
			} else
			{
				btnResult.Background = new SolidColorBrush(Color.FromArgb(255, 150, 50, 221));
				btnResult.Content = "Éssay encore!!";
			}
			PlayGrid.Children.Add(btnResult);

		}

		private void NextTurn(object sender, RoutedEventArgs e)
		{
			FogLayer.Visibility = Visibility.Collapsed;
			Button b = (Button)sender;
			if (newPlay.IsFinish())
			{
				this.Frame.Navigate(typeof(MainPage));
			} else
			{
				PlayGrid.Children.Remove(b);
				SetTurn();
			}
		}

		void SetImage(object sender, Word word)
		{
			var img = sender as Image;
			if (img != null)
			{
				var uri = new System.Uri("ms-appx:///Assets/dessin/" + word.imageFileName);
				var bitmapImage = new BitmapImage();
				bitmapImage.UriSource = uri;
				img.Source = bitmapImage;
                img.DoubleTapped += playSound;
			}

		}

        private void playSound(object sender, DoubleTappedRoutedEventArgs e)
        {
            Uri newuri = new Uri("ms-appx:///Assets/audio/" + turn.toFind.soundFileName);
            myPlayer.Source = newuri;
            myPlayer.Play();
        }
    }
}
