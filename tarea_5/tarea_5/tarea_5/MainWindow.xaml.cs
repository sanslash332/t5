using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Ogg;
using SoundPlayer;
using System.IO;
using System.Threading;
using tarea_5_core;
using tarea_5_core.NetPlay;

namespace tarea_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SoundManajer _soundManajer { get; private set; }
              public bool gameStarted;
       
        GameCore Core;
                Dictionary<BaseElement, Label> mapleador;
                Dictionary<double, Label> mapleadorNetClient;

                Dictionary<Bullet, Rectangle> balasMaples;
                Dictionary<double, Rectangle> balasMaplesNetClient;

                Dictionary<MineralMine, Label> matlabeador;
                Dictionary<double, Label> matlabeadorNetClient;

                private bool bgmActive;
                bool netClient;
                        NetManajer gameNet;
                        private LogWriter logk;

        public MainWindow()
        {
            logk = new LogWriter();
            this.Closed+=new EventHandler(MainWindow_Closed);
            gameStarted = false;
            _soundManajer = new SoundManajer();

            StartWindow muestra = new StartWindow(this);
            
            bgmActive = true;
            reguleBgm();
            this.Title = "¡Space invaders!";



            GameCore.looseGame += new Action<GameCore, string>(GameCore_looseGame);
            GameCore.winsGame += new Action<GameCore>(GameCore_winsGame);

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.Closed += new EventHandler(MainWindow_Closed);
            BgmPlayer.loop += new Action(BgmPlayer_loop);
            mapleador = new Dictionary<BaseElement, Label>();
                                    balasMaples = new Dictionary<Bullet, Rectangle>();
            matlabeador = new Dictionary<MineralMine, Label>();
            
            
            GameTimer.ticActivated += new Action<GameTimer>(GameTimer_ticActivated);
            Nave.shootNou += new Action<Nave, Bullet>(Nave_shootNou);

            muestra.ShowDialog();

        }

        
        void Shield_reflector(Bullet arg1, Shield arg2)
        {

            Rectangle bala = new Rectangle();
            Grid.SetColumn(bala, (int)arg1.startPosition.X);
            Grid.SetColumnSpan(bala, (int)arg1.width);
            Grid.SetRow(bala, (int)arg1.startPosition.Y);
            Grid.SetRowSpan(bala, (int)arg1.height);
            bala.Fill = Brushes.LightCoral;
            balasMaples.Add(arg1, bala);
            gridd1.Children.Add(bala);

            if (gameNet != null && netClient == false)
            {
                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.AddBullet, arg1.netIdentifier, null, arg1.startPosition.X, arg1.startPosition.Y, arg1.width, arg1.height, colores.gold));

            }
                                }

             public  void startSinglePlayer(bool fasterMode)
        {
            this.InitializeComponent();
                        Core = new GameCore(fasterMode);
                        BaseElement._death += new Action<BaseElement>(BaseElement__death);
                        BaseElement._hitingComplete += new Action<BaseElement>(Canon__hitingComplete);
                        BaseElement._ready += new Action<BaseElement>(BaseElement__ready);
                        BaseElement._elementMoved += new Action<BaseElement>(BaseElement__elementMoved);
                        MineralMine.extractMineral += new Action<MineralMine>(MineralMine_extractMineral);
                        MineralMine.mineFinished += new Action<MineralMine>(MineralMine_mineFinished);
                        Fabrics.mineralReseived += new Action<Fabrics>(Fabrics_mineralReseived);
                        Weapon.shootNow += new Action<Bullet, Weapon>(Weapon_shootNow);


                        Shield.reflector += new Action<Bullet, Shield>(Shield_reflector);
                        Bullet.bulletMoved += new Action<Bullet>(Bullet_bulletMoved);
                        Bullet.bulletDisapears += new Action<Bullet>(Bullet_bulletDisapears);


                        Bullet.bulletContact += new Action<Bullet>(Bullet_bulletContact);

                        GameTimer.levelUp += new Action<GameTimer>(GameTimer_levelUp);
                        gameStarted = true;

                        reguleBgm();
            initialStart();
            if (fasterMode == true)
            {
                this.Title += " ¡Faster mode!";

            }
                                     }

             public void startMultiPlayer(int playLife, int initialMine, int totalMineral, int netPort)
             {
                 gameStarted = true;


                 

                 BgmPlayer.stopBgm();
                 BgmPlayer.playBgm(@"BGM\bane.mp3", 0.3f);

                 gameNet = new ServerP2P();
                                  
                                                   IPEndPoint ipaux = new IPEndPoint(IPAddress.Any, netPort);
                                  gameNet.startNet(ipaux);
                                  _soundManajer.setNet(gameNet);

                netClient=false;
                                 NetManajer.infoReseived += new Action<NetInformation>(NetManajer_infoReseived);
                                  this.InitializeComponent();

                                  textBox1.Text = "Multi play Mode: p1";

                                  Core = new GameCore(playLife, initialMine, totalMineral);
                                  BaseElement._death += new Action<BaseElement>(BaseElement__death);
                                  BaseElement._hitingComplete += new Action<BaseElement>(Canon__hitingComplete);
                                  BaseElement._ready += new Action<BaseElement>(BaseElement__ready);
                                  BaseElement._elementMoved += new Action<BaseElement>(BaseElement__elementMoved);
                                  MineralMine.extractMineral += new Action<MineralMine>(MineralMine_extractMineral);
                                  MineralMine.mineFinished += new Action<MineralMine>(MineralMine_mineFinished);
                                  Fabrics.mineralReseived += new Action<Fabrics>(Fabrics_mineralReseived);
                                  Weapon.shootNow += new Action<Bullet, Weapon>(Weapon_shootNow);


                                  Shield.reflector += new Action<Bullet, Shield>(Shield_reflector);
                                  Bullet.bulletMoved += new Action<Bullet>(Bullet_bulletMoved);
                                  Bullet.bulletDisapears += new Action<Bullet>(Bullet_bulletDisapears);


                                  Bullet.bulletContact += new Action<Bullet>(Bullet_bulletContact);


                 initialStart();
                 reguleBgm();

                 this.Title = "Space invaders craft! MultiPlay";

                              }

             public void startClientPlay(IPAddress ipa, int port)
             {
                 
                 BgmPlayer.stopBgm();
                 BgmPlayer.playBgm(@"BGM\bane.mp3", 1.0f);

                 netClient = true;


                 NetManajer.infoReseived+=new Action<NetInformation>(NetManajer_infoReseived);


                 mapleadorNetClient = new Dictionary<double, Label>();

                 matlabeadorNetClient = new Dictionary<double, Label>();
                 balasMaplesNetClient = new Dictionary<double, Rectangle>();

                 
                 IPEndPoint ipus = new IPEndPoint(ipa, port);
                 gameNet = new ClientP2P();
                 ClientP2P.errorConect += new Action<string>(ClientP2P_errorConect);
                 gameNet.startNet(ipus);
                 this.InitializeComponent();
                 this.Title = "Space Invaders multiPlay... Player 2";
                 textBox1.Text = "MultiPlay Mode... p2";
                 
                                  reguleBgm();

             }

             void ClientP2P_errorConect(string obj)
             {
              
                 MessageBox.Show(obj);
                 Environment.Exit(1);

             }

             void NetManajer_infoReseived(NetInformation obj)
             {
                 Dispatcher.Invoke(new Action<NetInformation>(executionCommand), obj);

                              }

             void executionCommand(NetInformation obj)
             {

                 if (netClient == false)
                 {
                     switch (obj.comando)
                     {
                         case GameCommands.shoot:
                             Core.ShootCanonp2();
                             break;
                         case GameCommands.changeWeapon:
                             Core.changeCanonWeaponP2();
                             break;
                         case GameCommands.createNuke:
                             Core.comprarNukeP2();
                             break;
                         case GameCommands.createScv:
                             Core.createScvp2();
                             break;
                         case GameCommands.createShield:
                             Core.createShieldP2();
                             break;
                         case GameCommands.killFirstMember:
                             if (Core.secondGoodForcesElements.Count >= 2)
                             {
                                 Core.secondGoodForcesElements[2].hiting(999999999);

                             }
                                                          break;
                         case GameCommands.moveLeft:
                                                          Core.moveCanonLeftP2();
                                                          break;
                         case GameCommands.moveRight:
                                                          Core.moveCanonRightP2();
                                                          break;
                         case GameCommands.rechargeWeapon1:
                                                          Core.comprarLacerp2();
                                                          break;
                         case GameCommands.rechargeWeapon2:
                                                          Core.comprarRocketp2();
                                                          break;
                         case GameCommands.rechhargeWeapon3:
                                                          Core.comprarPlasmap2();
                                                          break;
                         case GameCommands.restoreCanonHp:
                                                          Core.healCanonp2();
                                                          break;
                         case GameCommands.restoreMineralMine:
                                                          Core.rechargeMineralP2();
                                                          break;
                         case GameCommands.suicide:
                                                          Core.SecondCanon.hiting(999999999999);
                                                          break;
                         case GameCommands.upgradeShields:
                                                          Core.upgradeShieldsP2();
                                                          break;
                         case GameCommands.upgradeShieldsMirror:
                                                          Core.upgradeShieldMirrorP2();
                                                          break;
                         case GameCommands.upgradeSuperScv:
                                                          Core.upgradeSuperScvP2();
                                                          break;
                                                  }
                 }
                 else
                 {
                     if (obj._gamesStatus == GameStatus.end_loose)
                     {
                         gameNet.cierraCanalDeEscucha();
                         logk.escribir("Canales cerrados ");

                         GameCore_looseGame(null, "Juego finalizado, el ganador es el jugador 1");

                     }
                     else if (obj._gamesStatus == GameStatus.end_win)
                     {
                         gameNet.cierraCanalDeEscucha();
                         logk.escribir("Canales cerrados ");

                         GameCore_looseGame(null, "Juego Finalizado, el ganador es el jugador 2");
                                              }
                     else if (obj._gamesStatus == GameStatus.ready)
                     {
                        try
                        {

                         switch (obj.comando)
                         {
                             case GameCommands.addElement:
                                 Label neko = new Label();
                                 neko.Content = obj.contentOfEnviado;
                                 Grid.SetColumn(neko, (int)obj.vectorX);
                                 Grid.SetColumnSpan(neko, (int)obj.width);
                                 Grid.SetRow(neko, (int)obj.vectorY);
                                 Grid.SetRowSpan(neko, (int)obj.height);
                                 gridd1.Children.Add(neko);
                                 neko.Background = prepaColor(obj.colok);

                                 mapleadorNetClient.Add(obj.enviadoId, neko);

                                 break;
                             case GameCommands.AddBullet:
                                 Rectangle reco = new Rectangle();
                                 reco.Fill = prepaColor(obj.colok);
                                 Grid.SetColumn(reco, (int)obj.vectorX);
                                 Grid.SetColumnSpan(reco, (int)obj.width);
                                 Grid.SetRow(reco, (int)obj.vectorY);
                                 Grid.SetRowSpan(reco, (int)obj.height);
                                 gridd1.Children.Add(reco);
                                                                  balasMaplesNetClient.Add(obj.enviadoId, reco);
                                 

                                 break;
                             case GameCommands.updateElementInformation:
                                 mapleadorNetClient[obj.enviadoId].Content = obj.contentOfEnviado;
                                                                  break;
                             case GameCommands.impactBullet:
                                                                 balasMaplesNetClient[obj.enviadoId].Fill = Brushes.Black;

                                                                  break;

                             case GameCommands.moveElement:
                                                                  Grid.SetColumn(mapleadorNetClient[obj.enviadoId], (int)obj.vectorX);
                                                                  Grid.SetRow(mapleadorNetClient[obj.enviadoId], (int)obj.vectorY);

                                 break;
                             case GameCommands.moveBullet:
                                 Grid.SetColumn(balasMaplesNetClient[obj.enviadoId], (int)obj.vectorX);
                                 Grid.SetRow(balasMaplesNetClient[obj.enviadoId], (int)obj.vectorY);
                                                                  break;
                             case GameCommands.removeElement:
                                                                  gridd1.Children.Remove(mapleadorNetClient[obj.enviadoId]);
                                                                  mapleadorNetClient.Remove(obj.enviadoId);
                                                                                                   break;
                             case GameCommands.RemoveBullet:
                                                                                                   gridd1.Children.Remove(balasMaplesNetClient[obj.enviadoId]);
                                                                                                   balasMaplesNetClient.Remove(obj.enviadoId);

                                 break;
                                                          }
                     }
                                                     catch (Exception pp)
                         {
                             if (logk != null)
                             {
                                                                  logk.escribir(pp.Message + " Con: " + obj.enviadoId + ", ");
                                                                  if (obj.contentOfEnviado != null)
                                                                  {
                                                                      logk.escribir(obj.contentOfEnviado);

                                                                  }
                                                              }

                         }

                     }

                 }
                              }

                void BgmPlayer_loop()
        {
            if (bgmActive)
            {
                if (BgmPlayer.BgmAdress != null)
                {
                    BgmPlayer.rePlay();

                }
                            }
                    }

        void GameCore_winsGame(GameCore obj)
        {
            bgmActive = false;
            Thread stopthread = new Thread(new ThreadStart( BgmPlayer.stopBgm));
            stopthread.Start();
            stopthread.Join();

                        BgmPlayer.playBgm(Core.bgmSound[0], 1.0f);
            bgmActive = !bgmActive;

            if (MessageBox.Show("¡Has ganado! ¡felicidades!!!!!!. Tu puntaje fue de" + Core.points + " ¡Congratulations!!!!") == MessageBoxResult.OK)
            {
                Thread.Sleep(3000);

                Environment.Exit(1);

            }
            else
            {
                Thread.Sleep(5000);

                Environment.Exit(1);

            }
                    }

        void reguleBgm()
        {
            if (bgmActive == false)
            {
                BgmPlayer.stopBgm();

            }
            else
            {
                BgmPlayer.stopBgm();
                if (Core == null && netClient == false)
                {
                    BgmPlayer.playBgm(@"BGM\Menu.ogg", 0.5f);
                    return;

                }
                
                if ((Core != null  &&  Core.multiplay == true) || netClient == true)
                {
                    BgmPlayer.playBgm(@"BGM\wolf.mp3", 1.0f);

                }
                else
                {


                    if (Core.currentLevel < 2)
                    {
                        BgmPlayer.playBgm(Core.bgmSound[1], 1.0f);

                    }
                    else if (Core.currentLevel < 4 && Core.currentLevel >= 2)
                    {
                        BgmPlayer.playBgm(Core.bgmSound[2], 1.0f);

                    }
                    else if (Core.currentLevel < 6 && Core.currentLevel >= 4)
                    {
                        BgmPlayer.playBgm(Core.bgmSound[3], 1.0f);

                    }
                    else if (Core.currentLevel < 8 && Core.currentLevel >= 6)
                    {
                        BgmPlayer.playBgm(Core.bgmSound[4], 1.0f);

                    }
                    else
                    {
                        BgmPlayer.playBgm(Core.bgmSound[5], 1.0f);

                    }
                                    }
            }
        }
       
        void GameTimer_ticActivated(GameTimer obj)
        {
            if (Core.multiplay == false)
            {

                textBox1.Text = "nivel: " + Core.currentLevel + Core.temporizador.CurrentTime + " Puntaje actual " + Core.points;
            }
                                }

        void GameCore_looseGame(GameCore arg1, string arg2)
                {
                    
                    

            bgmActive = false;
            
                Thread stopthread = new Thread(new ThreadStart(BgmPlayer.stopBgm));
            stopthread.Start();
            stopthread.Join();


                if (netClient == true && arg1 == null )
                {
                BgmPlayer.playBgm(@"BGM\wins.mp3", 1.0f);
    if (MessageBox.Show(arg2) == MessageBoxResult.OK)
    {
        Thread.Sleep(3000);
        Environment.Exit(1);

    }
                    else
    {
        Thread.Sleep(3000);
        Environment.Exit(1);


    }
                                                    }

            
            bgmActive = !bgmActive;
            if ((Core.multiplay == false && Core != null ))
            {
                                    

                BgmPlayer.playBgm(Core.bgmSound[Core.bgmSound.Count - 1], 1.0f);

                if (MessageBox.Show(arg2) == MessageBoxResult.OK)
                {
                    Thread.Sleep(5000);

                    Environment.Exit(1);

                }
                else
                {
                    Thread.Sleep(5000);

                    Environment.Exit(1);

                }
                Environment.Exit(1);
            }
            else
            {
                BgmPlayer.playBgm(Core.bgmSound[0], 1.0f);
                if (gameNet != null)
                {

                if (Core.MainCanon.currentHp <= 0)
                {
                    gameNet.sendData(new NetInformation(GameStatus.end_win, GameCommands.onlyChangeStatus));
                    gameNet.cierraCanalDeEscucha();
                    logk.escribir("Canales cerrados");

                }
                else
                {
                    gameNet.sendData(new NetInformation(GameStatus.end_loose, GameCommands.onlyChangeStatus));
                    gameNet.cierraCanalDeEscucha();
                    logk.escribir("Canales cerrados");

                }
            }

                if (MessageBox.Show(arg2) == MessageBoxResult.OK)
                {
                    Thread.Sleep(3000);
                    Environment.Exit(1);

                }
                else
                {
                    Thread.Sleep(3000);
                    Environment.Exit(1);

                }
            }
        }

        void Bullet_bulletContact(Bullet obj)
        {
            balasMaples[obj].Fill = Brushes.Black;

            if (netClient == false && gameNet != null)
            {
                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.impactBullet, obj.netIdentifier, null));

            }

        }

        void Bullet_bulletDisapears(Bullet obj)
        {
            gridd1.Children.Remove(balasMaples[obj]);
            balasMaples.Remove(obj);

            if (gameNet != null && netClient == false)
            {
                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.RemoveBullet, obj.netIdentifier, null));

            }
                    }

        void Bullet_bulletMoved(Bullet obj)
        {
            try
            {

                Grid.SetColumn(balasMaples[obj], (int)obj.startPosition.X);
                Grid.SetRow(balasMaples[obj], (int)obj.startPosition.Y);
                if (gameNet != null && netClient == false)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.moveBullet, obj.netIdentifier, obj.startPosition.X, obj.startPosition.Y));

                                    }
                            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
        }

        void Nave_shootNou(Nave arg1, Bullet arg2)
                    {


            Rectangle bala = new Rectangle();
            Grid.SetColumn(bala, (int)arg2.startPosition.X);
            Grid.SetColumnSpan(bala, (int)arg2.width);
            Grid.SetRow(bala, (int)arg2.startPosition.Y);
            Grid.SetRowSpan(bala, (int)arg2.height);
            bala.Fill = Brushes.LightSteelBlue;
            balasMaples.Add(arg2, bala);
            gridd1.Children.Add(bala);

                    }

        void Weapon_shootNow(Bullet arg1, Weapon arg2)
        {
            Rectangle bala = new Rectangle();
            Grid.SetColumn(bala, (int)arg1.startPosition.X);
            Grid.SetColumnSpan(bala, (int)arg1.width);
            Grid.SetRow(bala, (int)arg1.startPosition.Y);
            Grid.SetRowSpan(bala, (int)arg1.height);
            bala.Fill = Brushes.Gold;
            balasMaples.Add(arg1, bala);
            gridd1.Children.Add(bala);

            if (gameNet != null && netClient == false)
            {
                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.AddBullet, arg1.netIdentifier, null, arg1.startPosition.X, arg1.startPosition.Y, arg1.width, arg1.height, colores.gold));

            }
                    }

        void Fabrics_mineralReseived(Fabrics obj)
        {
            try
            {


                Dispatcher.Invoke(new Action<BaseElement>(adjustCanonLabel), obj);
            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
                    }

        void MineralMine_mineFinished(MineralMine obj)
        {
            try
            {

                Dispatcher.Invoke(new Action<MineralMine>(updateMineral), obj);
            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
        }

        void MineralMine_extractMineral(MineralMine obj)
        {
            try
            {

                Dispatcher.Invoke(new Action<MineralMine>(updateMineral), obj);
            }
            catch (Exception e )
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
                                }

        void updateMineral(MineralMine obj)
        {
            try
            {

                matlabeador[Core.MainMineralMine].Content = Core.MainMineralMine.name + ", " + Core.MainMineralMine.currentMineral.ToString();

                if (gameNet != null && netClient == false)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.updateElementInformation, Core.secondMineralMine.netIdentifier, Core.secondMineralMine.name + ", " + Core.secondMineralMine.currentMineral.ToString()));

                }


                if (obj.currentMineral <= 0)
                {
                    gridd1.Children.Remove(matlabeador[obj]);
                    matlabeador.Remove(obj);
                    if (gameNet != null && netClient == false)
                    {
                        gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.removeElement, obj.netIdentifier, null));

                    }
                                                        }

                
            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
                                }

        void GameTimer_levelUp(GameTimer obj)
        {
textBox1.Text = "nivel: " + (Core.currentLevel+1) + Core.temporizador.CurrentTime + " Puntaje actual " + Core.points;

if (Core.currentLevel % 2 == 1 && Core.currentLevel < 11)
{
    reguleBgm();

   
}
        }

        void BaseElement__elementMoved(BaseElement obj)
        {
            Dispatcher.Invoke(new Action<BaseElement>(moveLavel), obj);

        }

        void moveLavel(BaseElement obj)
        {
            try
            {

                Grid.SetColumn(mapleador[obj], (int)obj.currentPosition.X);
                Grid.SetRow(mapleador[obj], (int)obj.currentPosition.Y);

                if (gameNet != null && netClient == false)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.moveElement, obj.netIdentifier, obj.currentPosition.X, obj.currentPosition.Y));

                }

            }
            catch (Exception er)
            {
                if (logk != null)
                    logk.escribir(er.Message);

            }
                    }

        void BaseElement__ready(BaseElement obj)
        {
            Label nuevolabel = new Label();
            nuevolabel.Content = obj.name + ", " + obj.currentHp;
            nuevolabel.Background = prepaColor(obj.coloring);
            Grid.SetColumn(nuevolabel, (int)obj.currentPosition.X);
            Grid.SetColumnSpan(nuevolabel, (int)obj.width);
            Grid.SetRow(nuevolabel, (int)obj.currentPosition.Y);
            Grid.SetRowSpan(nuevolabel, (int)obj.height);
            mapleador.Add(obj, nuevolabel);
            gridd1.Children.Add(nuevolabel);

            if (gameNet != null && netClient== false)
            {
                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, obj.netIdentifier, obj.name + ", " + obj.currentHp, obj.currentPosition.X, obj.currentPosition.Y, obj.width, obj.height, obj.coloring));


            }
                                }

        void BaseElement__death(BaseElement obj)
        {
            try
            {

                gridd1.Children.Remove(mapleador[obj]);
                mapleador.Remove(obj);

                textBox1.Text = "nivel: " + Core.currentLevel + " " + Core.temporizador.CurrentTime + " Puntaje actual " + Core.points;
                if (gameNet != null && netClient == false)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.removeElement, obj.netIdentifier, null));

                }
                                            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
                    }

        void Canon__hitingComplete(BaseElement obj)
        {
            Action<BaseElement> auxiliot = new Action<BaseElement>(adjustCanonLabel);

            Dispatcher.Invoke( auxiliot, obj);


        }
    
                void adjustCanonLabel( BaseElement obj)
        {
            try
            {

                if (obj != null)
                {
                    if (obj.currentHp > 0)
                    {

                        mapleador[obj].Content = obj.name + " hp " + obj.currentHp;

                        if (gameNet != null && netClient == false)
                        {
                            gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.updateElementInformation, obj.netIdentifier, obj.name + " hp " + obj.currentHp));

                        }
                    }
                }

                if (Core.MainCanon.currentHp > 0)
                {

                    mapleador[Core.MainCanon].Content = Core.MainCanon.name + ", " + Core.MainCanon.currentHp + "hp, current weapon: " + Core.MainCanon.currentWeapon.name + ", cargas " + Core.MainCanon.currentWeapon.charges;
                }


                if (Core.MainFabrics.currentHp > 0)
                {

                    mapleador[Core.MainFabrics].Content = Core.MainFabrics.name + ", " + Core.MainFabrics.currentHp + " hp, " + Core.MainFabrics.mineralAvailable + " minerales";
                }

                if (gameNet != null && netClient == false)
                {
                    if (Core.SecondCanon.currentHp > 0)
                    {

                        gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.updateElementInformation, Core.SecondCanon.netIdentifier, Core.SecondCanon.name + ", " + Core.SecondCanon.currentHp + "hp, current weapon: " + Core.SecondCanon.currentWeapon.name + ", cargas " + Core.SecondCanon.currentWeapon.charges));
                    }

                    if (Core.secondFabrics.currentHp > 0)
                    {

                        gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.updateElementInformation, Core.secondFabrics.netIdentifier, Core.secondFabrics.name + ", " + Core.secondFabrics.currentHp + " hp, " + Core.secondFabrics.mineralAvailable + " minerales"));
                    }

                                    }
                            }
            catch (Exception e)
            {
                if (logk != null)
                    logk.escribir(e.Message);

            }
                            }

        void initialStart()
        {
            Label minerales = new Label();
            minerales.Content = Core.MainMineralMine.name + ", " + Core.MainMineralMine.currentMineral.ToString();
            Grid.SetColumn(minerales, (int)Core.MainMineralMine.mineralPos.X);
            Grid.SetRow(minerales, (int)Core.MainMineralMine.mineralPos.Y);
            Grid.SetRowSpan(minerales, (int)Core.MainMineralMine.mineHeight);
            Grid.SetColumnSpan(minerales, (int)Core.MainMineralMine.mineWidth);
            minerales.Background = prepaColor(Core.MainMineralMine.mineColor);
            matlabeador = new Dictionary<MineralMine, Label>();
           
            matlabeador.Add(Core.MainMineralMine, minerales);
                        gridd1.Children.Add(minerales);
                        Label canion = new Label();
                        canion.Content = Core.MainCanon.name + ", " + Core.MainCanon.currentHp + "hp, current weapon: " + Core.MainCanon.currentWeapon.name + ", cargas " + Core.MainCanon.currentWeapon.charges;
                        canion.Background = prepaColor(Core.MainCanon.coloring);
                        Grid.SetColumn(canion, (int)Core.MainCanon.currentPosition.X);
                        Grid.SetColumnSpan(canion, (int)Core.MainCanon.width);
                        Grid.SetRow(canion, (int)Core.MainCanon.currentPosition.Y);
                        Grid.SetRowSpan(canion, (int)Core.MainCanon.height);
                        mapleador.Add(Core.MainCanon, canion);
                       gridd1.Children.Add(canion);
                       Label fabroca = new Label();
                       fabroca.Content = Core.MainFabrics.name + ", " + Core.MainFabrics.currentHp + " hp, " + Core.MainFabrics.mineralAvailable + " minerales";
                       fabroca.Background = prepaColor(Core.MainFabrics.coloring);
                       Grid.SetColumn(fabroca, (int)Core.MainFabrics.currentPosition.X);
                       Grid.SetColumnSpan(fabroca, (int)Core.MainFabrics.width);
                       Grid.SetRow(fabroca, (int)Core.MainFabrics.currentPosition.Y);
                       Grid.SetRowSpan(fabroca, (int)Core.MainFabrics.height);
                       mapleador.Add(Core.MainFabrics, fabroca);
                       gridd1.Children.Add(fabroca);

                       if (Core.multiplay == true)
                       {
                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.MainCanon.netIdentifier, Core.MainCanon.name + ", " + Core.MainCanon.currentHp, Core.MainCanon.currentPosition.X, Core.MainCanon.currentPosition.Y, Core.MainCanon.width, Core.MainCanon.height, Core.MainCanon.coloring));

                           
                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.MainFabrics.netIdentifier, Core.MainFabrics.name + ", " + Core.MainFabrics.currentHp, Core.MainFabrics.currentPosition.X, Core.MainFabrics.currentPosition.Y, Core.MainFabrics.width, Core.MainFabrics.height, Core.MainFabrics.coloring));
                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.MainMineralMine.netIdentifier, Core.MainMineralMine.name, Core.MainMineralMine.mineralPos.X, Core.MainMineralMine.mineralPos.Y, Core.MainMineralMine.mineWidth, Core.MainMineralMine.mineHeight, Core.MainMineralMine.mineColor));

                                                      Label minerales2 = new Label();
                           minerales2.Content = Core.secondMineralMine.name;
                           Grid.SetColumn(minerales2, (int)Core.secondMineralMine.mineralPos.X);
                           Grid.SetRow(minerales2, (int)Core.secondMineralMine.mineralPos.Y);
                           Grid.SetRowSpan(minerales2, (int)Core.secondMineralMine.mineHeight);
                           Grid.SetColumnSpan(minerales2, (int)Core.secondMineralMine.mineWidth);
                           minerales2.Background = prepaColor(Core.secondMineralMine.mineColor);
                           
                                                      matlabeador.Add(Core.secondMineralMine, minerales2);
                           gridd1.Children.Add(minerales2);
                           Label canion2 = new Label();
                           canion2.Content = Core.SecondCanon.name;
                           canion2.Background = prepaColor(Core.SecondCanon.coloring);
                           Grid.SetColumn(canion2, (int)Core.SecondCanon.currentPosition.X);
                           Grid.SetColumnSpan(canion2, (int)Core.SecondCanon.width);
                           Grid.SetRow(canion2, (int)Core.SecondCanon.currentPosition.Y);
                           Grid.SetRowSpan(canion2, (int)Core.SecondCanon.height);
                           mapleador.Add(Core.SecondCanon, canion2);
                           gridd1.Children.Add(canion2);
                           Label fabroca2 = new Label();
                           fabroca2.Content = Core.secondFabrics.name;
                           fabroca2.Background = prepaColor(Core.secondFabrics.coloring);
                           Grid.SetColumn(fabroca2, (int)Core.secondFabrics.currentPosition.X);
                           Grid.SetColumnSpan(fabroca2, (int)Core.secondFabrics.width);
                           Grid.SetRow(fabroca2, (int)Core.secondFabrics.currentPosition.Y);
                           Grid.SetRowSpan(fabroca2, (int)Core.secondFabrics.height);
                           mapleador.Add(Core.secondFabrics, fabroca2);
                           gridd1.Children.Add(fabroca2);
                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.SecondCanon.netIdentifier, Core.SecondCanon.name + ", " + Core.SecondCanon.currentHp + " hp, " + Core.SecondCanon.currentWeapon.name + ", cargas " + Core.SecondCanon.currentWeapon.charges, Core.SecondCanon.currentPosition.X, Core.SecondCanon.currentPosition.Y, Core.SecondCanon.width, Core.SecondCanon.height, Core.SecondCanon.coloring));
                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.secondFabrics.netIdentifier, Core.secondFabrics.name + ", " + Core.secondFabrics.currentHp + "hp, " + Core.secondFabrics.mineralAvailable + " mineral", Core.secondFabrics.currentPosition.X, Core.secondFabrics.currentPosition.Y, Core.secondFabrics.width, Core.secondFabrics.height, Core.secondFabrics.coloring));

                           gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.addElement, Core.secondMineralMine.netIdentifier, Core.secondMineralMine.currentMineral + " Mineral restante", Core.secondMineralMine.mineralPos.X, Core.secondMineralMine.mineralPos.Y, Core.secondMineralMine.mineWidth, Core.secondMineralMine.mineHeight, Core.secondMineralMine.mineColor));


                                                                             }
                                            }

                void MainWindow_Closed(object sender, EventArgs e)
        {
            logk.terminate();

                    Environment.Exit(1);

        }

                void MainWindow_KeyDown(object sender, KeyEventArgs e)
                {
                    if (Keyboard.IsKeyDown(Key.Space)  == true || Keyboard.IsKeyDown(Key.Z) == true)
            {
                if (!netClient)
                {

                    Core.ShootCanon();
                }
                else
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.shoot));

                }

                    }
               
                    
                    if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                if (netClient)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.changeWeapon));

                }
                else
                {

                    Core.ChangeCanonWeapon();
                }


            }
                            
            else if (e.Key == Key.S)
            {
                if (netClient)
                {
                    gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.createScv));

                }
                else
                {

                    Core.createScv();
                }

    
            }
                        else if (e.Key == Key.D1)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.rechargeWeapon1));

                            }

                            else
                            {

                                Core.comprarLacer();
                            }


                                                    }
                                                    else if (e.Key == Key.D2)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.rechargeWeapon2));

                            }
                            else
                            {

                                Core.comprarRocket();
                            }

                        }
                        else if (e.Key == Key.D3)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready , GameCommands.rechhargeWeapon3));

                            }
                            else
                            {
                                
                                Core.comprarPlasma();
                            }
                                                                                }
                        else if (e.Key == Key.D4)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.createNuke));

                            }
                            else
                            {
                                Core.ComprarNuke();
                            }

                        }
                        else if (e.Key == Key.Left)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.moveLeft));

                            }
                            else
                            {

                                Core.moveCanonLeft();
                            }

                        }
                        else if (e.Key == Key.Right )
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.moveRight));

                            }
                            else
                            {

                                Core.moveCanonRight();
                            }
                                                    }
                        else if (e.Key == Key.M)
                        {

                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.restoreMineralMine));

                            }
                            else
                            {
                                Core.rechargeMineral();
                            }


                        }

                        else if (e.Key == Key.U)
                        {

                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.upgradeSuperScv));

                            }
                            else
                            {

                                Core.upgradeSuperScv();

                            }

                        }
                        
                        else if (e.Key == Key.D0)
                        {
                            _soundManajer.inactiveSound();

                        }
                        else if (e.Key == Key.D9)
                        {
                            bgmActive = !bgmActive;
                            reguleBgm();

                        }
                        else if (e.Key == Key.Delete)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.suicide));

                            }
                            else
                            {
                                
                                Core.MainCanon.hiting(100000000);
                            }

                        }
                        else if (e.Key == Key.H)
                        {
                            if (netClient)
                            {
                                gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.restoreCanonHp));

                            }
                            else
                            {

                                Core.healCanon();
                            }

                        }
                    else if (e.Key == Key.R)
                    {
                        if (netClient)
                        {
                            gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.upgradeShieldsMirror));

                        }
                        else
                        {

                            Core.upgradeShieldMirror();
                        }

                    }
                    else if (e.Key == Key.A)
                    {
                        if (netClient)
                        {
                            gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.upgradeShields));

                        }
                        else
                        {

                            Core.upgradeShields();
                        }

                    }
                    else if (e.Key == Key.Up)
                    {
                        if (netClient)
                        {
                            gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.createShield));

                        }
                        else
                        {

                            Core.createShield();
                        }

                    }
                    else if (e.Key == Key.Back)
                    {
                        //Core.ShootCanonp2();

                    }

                    else if (e.Key == Key.RightShift)
                    {
                        if (netClient)
                        {
                            gameNet.sendData(new NetInformation(GameStatus.ready, GameCommands.killFirstMember));

                        }
                        else
                        {

                            if (Core.goodForcesElements.Count >= 3)
                            {

                                Core.goodForcesElements[2].hiting(100);
                            }
                        }
                    }

                    else
                    {


                    }
                    }

        Brush prepaColor(colores colorc)
        {
            switch (colorc)
            {
                case colores.black:
                    return Brushes.Black;
                    
                case colores.blue:
                    return Brushes.Blue;
                    
                case colores.brown:
                    return Brushes.Brown;
                    
                case colores.cyan:
                    return Brushes.Cyan;
                    
                case colores.darkGreen:
                    return Brushes.DarkGreen;
                    
                case colores.darkRed:
                    return Brushes.DarkRed;
                    
                case colores.fuxia:
                    return Brushes.Fuchsia;
                    
                case colores.gold:
                    return Brushes.Gold;
                    
                case colores.gray:
                    return Brushes.Gray;
                    
                case colores.green:
                    return Brushes.Green;
                    
                case colores.lightBlue:
                    return Brushes.LightBlue;
                    
                case colores.orange:
                    return Brushes.Orange;
                    
                case colores.pink:
                    return Brushes.Pink;
                    
                case colores.red:
                    return Brushes.Red;
                    
                case colores.silver:
                    return Brushes.Silver;
                    
                case colores.white:
                    return Brushes.GhostWhite;
                    
                case colores.yellow:
                    return Brushes.Yellow;
                    
                                    default:
                    return Brushes.Black;
                    
            }

        }

            }
}
