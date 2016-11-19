        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Automation;
using System.Net;


namespace tarea_5
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        MainWindow playerScreen;
        bool startedGame;

                public StartWindow()
        {
            InitializeComponent();
            StartSinglePlayerButton.Click += new RoutedEventHandler(StartSinglePlayerButton_Click);
        }

        public StartWindow(MainWindow _screen)
        {

            InitializeComponent();
            StartSinglePlayerButton.Click += new RoutedEventHandler(StartSinglePlayerButton_Click);
            buttonCreate.Click += new RoutedEventHandler(buttonCreate_Click);
            buttonJoin.Click += new RoutedEventHandler(buttonJoin_Click);
            playerScreen = _screen;
            AutomationProperties.SetName(IpTextBox, "Ip to conect");
            AutomationProperties.SetName(PortConectTextBox, "Port to use in conection");
            AutomationProperties.SetName(playlifetextbox, "Life to habe te canons");
            AutomationProperties.SetName(InitialMineralTextBox, "Mineral to start");
            AutomationProperties.SetName(TotalMineralTextBox, "Max mineral of mine");
            AutomationProperties.SetName(PortTextBox, "Port use for server");
            AutomationProperties.SetName(iptextBoxdisplay, "ips availables");
            iptextBoxdisplay.Text = "Your ip directions: ";
            adjustip();

            Closed += new EventHandler(StartWindow_Closed);
        }

        void adjustip()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < ips.Length ; i++)
            {
                iptextBoxdisplay.Text += ips[i].ToString() + "; ";

            }
                    }

        void buttonJoin_Click(object sender, RoutedEventArgs e)
        {
           SoundPlayer.SoundManajer.simplePlay(@"bgm\start.wav");

            buttonJoin.Content = "conecting ... "; 
            this.Title = "conectin ... ";



            try
            {
                IPAddress ipCo = IPAddress.Parse(IpTextBox.Text);
                int puerto = int.Parse(PortConectTextBox.Text);
                playerScreen.startClientPlay(ipCo, puerto);
                startedGame = true;

                this.Close();

            
            }
            catch (Exception epep)
                            {
                MessageBox.Show("error en el ingreso de datos: " + epep.Message);
                this.Title = "startWindow";
                buttonJoin.Content = "JoinGame";

            }
                    }

        void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.SoundManajer .simplePlay(@"bgm\start.wav");


            try
            {
                buttonCreate.Content = "waitingGamers... ";
                this.Title = "Waitings gamers to start...";
                ToolTip tutul = new System.Windows.Controls.ToolTip();
                tutul.Content = "Waiting other players... to cancel, cloce the window";
                tutul.Background = Brushes.Red;
                this.ToolTip = tutul;
                tutul.IsOpen = true;

                int playLife = int.Parse(playlifetextbox.Text);
                int initialMineral = int.Parse(InitialMineralTextBox.Text);
                int totalMineral = int.Parse(TotalMineralTextBox.Text);
                int netPort =int.Parse(PortTextBox.Text);
                playerScreen.gameStarted = true;

                

                playerScreen.startMultiPlayer(playLife, initialMineral, totalMineral, netPort);

                startedGame = true;

                this.Close();
                
            }
            catch (Exception eppa)
            {
                MessageBox.Show("Uno de los datos ingresados es erróneo " + eppa.Message + ", " + eppa.Source.ToString() + ", " + eppa.StackTrace.ToString() );
                this.Title = "StartWindow";
                buttonCreate.Content = "Create Game";

            }
                    }

        void StartWindow_Closed(object sender, EventArgs e)
        {
            if (startedGame == false)
            {
                Environment.Exit(1);

            }
                    }

        void StartSinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
           SoundPlayer.SoundManajer.simplePlay(@"bgm\start.wav");

                        startedGame = true;
                if (fasterModeCheckBox.IsChecked == false)
            {
                playerScreen.startSinglePlayer(false);

                                this.Close();

            }
            else
            {
                playerScreen.startSinglePlayer(true);

                this.Close();

            }
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SoundPlayer.SoundManajer.simplePlay(@"bgm\click.wav");
 
        }

        private void fasterModeCheckBox_Click(object sender, RoutedEventArgs e)
        {
           SoundPlayer.SoundManajer.simplePlay(@"bgm\click.wav");

        }
    }
}
