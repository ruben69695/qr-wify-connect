using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WifiQR.Classes;
using WifiQR.Interfaces;
using Xamarin.Forms;

namespace WifiQR.Controls
{
    public class MenuItem : Frame
    {
        private readonly ICommand _onTapItemCommand;
        private readonly ISoundService _soundService;

        private Label _labelBoxText;
        private Image _imageBox;
        private string _imgSource;
        private bool _isTapped;
        private TapGestureRecognizer _menuGestureRecognizer;

        public event EventHandler Tapped;

        public string Text 
        {
            get => _labelBoxText.Text ?? "";
            set => _labelBoxText.Text = value;
        }

        public string ImageBoxSource
        {
            get => _imgSource;
            set
            {
                _imgSource = value;
                _imageBox.Source = ImageSource.FromFile(value);
            }
        }

        public MenuItem()
        {
            _onTapItemCommand = new Command(OnItem_Tapped);
            _soundService = DependencyServicesResolver.SoundService;

            BackgroundColor = Color.FromHex("#6200EE");
            CornerRadius = 5.0f;
            Margin = new Thickness(5.0d);
            Content = CreateBoxContent();

            SetGestureRecognizers();
        }

        private void SetGestureRecognizers()
        {
            _menuGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
            };
            _menuGestureRecognizer.Command = _onTapItemCommand;

            GestureRecognizers.Add(_menuGestureRecognizer);
        }

        private async void OnItem_Tapped()
        {
            if (!_isTapped)
            {
                _isTapped = true;
                _soundService.KeyboardSoundClick();

                var currentColor = BackgroundColor;

                await Task.Run(async () =>
                {
                    BackgroundColor = Color.FromHex("#7C4DFF");
                    await Task.Delay(100);
                });

                BackgroundColor = currentColor;
                Tapped?.Invoke(this, EventArgs.Empty);

                _isTapped = false;
            }
        }

        private FlexLayout CreateBoxContent()
        {
            var flexLayout = new FlexLayout()
            {
                Direction = FlexDirection.Column,
            };

            _labelBoxText = new Label
            {
                TextColor = Color.White,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            _imageBox = new Image
            {
                Source = ImageSource.FromFile(_imgSource),
            };

            flexLayout.Children.Add(_imageBox);
            flexLayout.Children.Add(_labelBoxText);

            return flexLayout;
        }
    }
}
