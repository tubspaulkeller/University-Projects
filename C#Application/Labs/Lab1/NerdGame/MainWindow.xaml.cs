using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.Media;
namespace NerdGame
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        Random random = new Random(Guid.NewGuid().GetHashCode());

        private int timeLeft;
        private byte RequestedValue { get; set; }

        private int maxValue = 255;

        private byte userInput;

        private bool won = false;

        private string winnerMessage = "Winner! :)";

        private string looserMessage = "Looser! :(";

        SoundPlayer winsound = new SoundPlayer(@"\Users\Paul Keller\source\Uni\Lab1NerdGame\NerdGame\Sounds\won.wav");
        SoundPlayer ticksound = new SoundPlayer(@"\Users\Paul Keller\source\Uni\Lab1NerdGame\NerdGame\Sounds\tick.wav");
        SoundPlayer loosersound = new SoundPlayer(@"\Users\Paul Keller\source\Uni\Lab1NerdGame\NerdGame\Sounds\lost.wav");

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += myTimer_Tick;
            
            //Setup Default Label
            GameStateLabel.Content = "Ready for new Game!";
        }

        private void startGame(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton timerButton)
            {
                timer.Start();
                timer.Interval = TimeSpan.FromSeconds(1);

                //RandomRequestedValue
                RequestedValue = createRandomNumberRequest();
                if (Hex.IsChecked == true)
                {
                    RandomNumber.Content = "0x" + RequestedValue.ToString("X2");
                }else  if(Decimal.IsChecked == true)
                {
                    RandomNumber.Content = RequestedValue.ToString();
                }

                disableOrEnableButtons(false);

                //Change GameStateLabel
                GameStateLabel.Content = "Game Running!";

                //NormalTimer 
                if (NormalTimer.IsChecked == true)
                {
                    timeLeft = 10;
                }
                else if (FastedTimer.IsChecked == true)
                {
                    int maxTimeLeftSeconds = 60;
                    timeLeft = random.Next(maxTimeLeftSeconds);
                }

            }
        }

        private void disableOrEnableButtons(bool flag)
        {
            NewGameBtn.IsEnabled = flag;

            Hex.IsEnabled = flag;
            Decimal.IsEnabled = flag;

            NormalTimer.IsEnabled = flag;
            FastedTimer.IsEnabled = flag;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (NormalTimer.IsChecked == true)
            {
                timeLeft -= 1;
            }
            else if (FastedTimer.IsChecked == true)
            {
                int randomSeconds = random.Next(5,9);
                timeLeft -= randomSeconds;
            }
            Time.Content = timeLeft.ToString() + " s";
            ticksound.Play();
          
            if (timeLeft < 0)
            {
                //event of the user control to subscribe
                UserControlBianryInput ucbi = new UserControlBianryInput();
                ucbi.changeValue += (byte x) => userInput = x;

                //Get Winner or Looser
                won = getWinnerOrLooser(RequestedValue);
                if (won)
                {
                    GameStateLabel.Content = winnerMessage;
                    winsound.Play();
                }
                else { 
                    GameStateLabel.Content = looserMessage; 
                    loosersound.Play(); 
                }

                resetGameProperties();
                disableOrEnableButtons(true);
            }
        }

        private void resetGameProperties()
        {
            //Reset Timerproperties
            timer.Stop();
            timeLeft = 10;

            if (FastedTimer.IsChecked == true)
            {
                Time.Content = "XX s";
            }
            else
            {
                Time.Content = "10 s";
            }

            RandomNumber.Content = "0x00";
            Data.HexRepresentation.Content = "0x00";

            Data.Bit0.Content = 0;
            Data.Bit0.IsChecked = false;
            Data.Bit1.Content = 0;
            Data.Bit1.IsChecked = false;
            Data.Bit2.Content = 0;
            Data.Bit2.IsChecked = false;
            Data.Bit3.Content = 0;
            Data.Bit3.IsChecked = false;
            Data.Bit4.Content = 0;
            Data.Bit4.IsChecked = false;
            Data.Bit5.Content = 0;
            Data.Bit5.IsChecked = false;
            Data.Bit6.Content = 0;
            Data.Bit6.IsChecked = false;
            Data.Bit7.Content = 0;
            Data.Bit7.IsChecked = false;
        }

        private byte createRandomNumberRequest()
        {
            byte randomNumber = 0;
            randomNumber = Convert.ToByte(random.Next(maxValue));
            return randomNumber;
        }


        private bool getWinnerOrLooser(byte requestedNumber)
        {
            return requestedNumber == userInput ? true : false;
        }

        /*
         * https://social.msdn.microsoft.com/Forums/windows/en-US/188771f3-41a1-481c-8e57-e3ede43aa20c/c-usercontrol-multiple-eventsdelegates?forum=winforms
         */
        //Listen to the event you have signed up for
        private void UserControlBianryInput_changeValue(byte newVal)
        {
            userInput = newVal;
        }

        private void showHexOrDecimal(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Name == "Decimal")
                {
                    Decimal.IsChecked = true;
                    Hex.IsChecked = false;
                    RandomNumber.Content = "00";

                }
                else if (radioButton.Name == "Hex")
                {
                    Decimal.IsChecked = false;
                    Hex.IsChecked = true;
                    RandomNumber.Content = "0x00";
                }
            }
        }

        private void fastedOrNormalTimer(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Name == "FastedTimer")
                {
                    FastedTimer.IsChecked = true;
                    NormalTimer.IsChecked = false;
                    Time.Content = "XX s";

                }
                else if (radioButton.Name == "NormalTimer")
                {
                    FastedTimer.IsChecked = false;
                    NormalTimer.IsChecked = true;
                    Time.Content = "10 s";
                }

            }
        }

    }
}


