using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows;

namespace tarea_5_core
{
   public   class GameCore
    {
       public Canon MainCanon { get; private set; }
       public Canon SecondCanon { get; private set; }
       public bool multiplay { get; private set; } 
       SCV MainScvBase;
       SCV secondScvBase;
              public Fabrics MainFabrics { get; private set; }
       public Fabrics secondFabrics { get; private set;}
              public MineralMine MainMineralMine { get; private set; }
       public MineralMine secondMineralMine { get; private set; }

       public Shield mainShieldBase { get; private set;}
       public Shield secondShieldBase { get; private set; }

     List<Nave> MainBaseNave;
     public List<Nave> activeNaves { get; private set; }
     public List<Bullet> activeBullets { get; private set; }
     public List<BaseElement> goodForcesElements { get; private set; }
     public List<BaseElement> secondGoodForcesElements { get; private set; }
     public double netIdentifierCounter { get; private set; }

     public GameTimer temporizador { get; private set; }
     public int currentLevel { get; private set; }
     public double points { get; private set; }
      
   public Queue<Nave> colaNaves { get; private set; }
   Random aleatorio;
   

   public List<string> bgmSound;
   public static event Action<GameCore, string> looseGame;
   protected virtual void onLooseGame(string obj)
   {
       if (looseGame != null)
       {
           looseGame(this, obj);
                  }
   }
   public static event Action<GameCore> winsGame;
   protected virtual void onWinsGame()
   {
       if (winsGame != null)
       {
           winsGame(this);

       }
   }
   public static event Action<string> _sendError;
   protected virtual void onSendError(string obj)
   {
       if (_sendError != null)
       {
           _sendError(obj);

       }
          }

                public GameCore(bool fasterMode)
   {
       

       multiplay = false;

       MainCanon = new Canon(3, 2, @"CanonSounds\death.wav", 500, new Point(0, 23), "cañón", @"CanonSounds\change.wav", colores.blue, @"CanonSounds\clip.wav");
       MainCanon.addWeapon(new Weapon(1, 2, 2, "Lácer", @"CanonSounds\atk1.wav", @"CanonSounds\hit1.wav", @"CanonSounds\charge.wav", 10, false));
                                           MainCanon.addWeapon(new Weapon(2,3, 10, "Rocket", @"CanonSounds\atk2.wav", @"CanonSounds\hit2.wav", @"CanonSounds\charge.wav", 12, false));
                                           MainCanon.addWeapon(new Weapon(3, 6, 6, "PlasmaPot", @"CanonSounds\atk3.wav", @"CanonSounds\hit3.wav", @"CanonSounds\charge.wav", 14, false));
                                           MainCanon.addWeapon(new NuclearMisil(100, 27, 27, "NuclearMisil", @"CanonSounds\nukel.wav", @"CanonSounds\nukex.wav", @"CanonSounds\nuke1.wav", 3, @"CanonSounds\nuke2.wav", false));
                                           MainBaseNave = new List<Nave>();

                                           if (fasterMode == false)
                                           {
                                               temporizador = new GameTimer(new TimeSpan(0, 0, 1), 70, @"FabricsSounds\level.mp3");
                                               MainCanon.weapons[0].charge(100);
                                               MainCanon.weapons[1].charge(80);
                                               MainCanon.weapons[2].charge(50);

                                               MainBaseNave.Add(new Nave("interceptor", 1, 2, 2, @"SheepsSounds\1\ready.wav", @"SheepsSounds\1\death.wav", @"SheepsSounds\1\fire.wav", @"SheepsSounds\1\hit.wav", 1, 5, 0.025, 5, 0.5, 1, 1, colores.red));
                                               MainBaseNave.Add(new Nave("Mutalisc", 2, 3, 2, @"SheepsSounds\2\ready.wav", @"SheepsSounds\2\death.wav", @"SheepsSounds\2\fire.wav", @"SheepsSounds\2\hit.wav", 3, 8, 0.1, 10, -1, 2, 3, colores.brown));
                                               MainBaseNave.Add(new Nave("wright", 3, 2, 1, @"SheepsSounds\3\ready.wav", @"SheepsSounds\3\death.wav", @"SheepsSounds\3\fire.wav", @"SheepsSounds\3\hit.wav", 9, 5, 0.15, 20, -1, 2, 10, colores.darkRed));
                                               MainBaseNave.Add(new Nave("RyuSheep", 4, 2, 2, @"SheepsSounds\4\ready.wav", @"SheepsSounds\4\death.wav", @"SheepsSounds\4\fire.wav", @"SheepsSounds\4\hit.wav", 3, 13, 0.2, 10, -1, 5, 1, colores.red));
                                               MainBaseNave.Add(new Nave("Dragoon", 5, 5, 5, @"SheepsSounds\5\ready.wav", @"SheepsSounds\5\death.wav", @"SheepsSounds\5\fire.wav", @"SheepsSounds\5\hit.wav", 2, 15, 0.25, 3, -1, 20, 2, colores.fuxia));
                                               MainBaseNave.Add(new Nave("FlyingLurker", 6, 4, 4, @"SheepsSounds\6\ready.wav", @"SheepsSounds\6\death.wav", @"SheepsSounds\6\fire.wav", @"SheepsSounds\6\hit.wav", 1, 25, 0.3, 15, -1, 3, 10, colores.gray));
                                               MainBaseNave.Add(new Nave("Pajarín", 7, 1, 1, @"SheepsSounds\7\ready.wav", @"SheepsSounds\7\death.wav", @"SheepsSounds\7\fire.wav", @"SheepsSounds\7\hit.wav", 15, 25, 0.35, 20, -1, 1, 1, colores.green));
                                               MainBaseNave.Add(new Nave("BattleCruicer", 8, 10, 10, @"SheepsSounds\8\ready.wav", @"SheepsSounds\8\death.wav", @"SheepsSounds\8\fire.wav", @"SheepsSounds\8\hit.wav", 2, 30, 0.4, 25, -1, 20, 20, colores.darkGreen));
                                               MainBaseNave.Add(new Nave("guardian", 9, 5, 5, @"SheepsSounds\9\ready.wav", @"SheepsSounds\9\death.wav", @"SheepsSounds\9\fire.wav", @"SheepsSounds\9\hit.wav", 1, 30, 0.45, 20, -1, 27, 1, colores.cyan));
                                               MainBaseNave.Add(new Nave("Galactus", 10, 20, 10, @"SheepsSounds\10\ready.wav", @"SheepsSounds\10\death.wav", @"SheepsSounds\10\fire.wav", @"SheepsSounds\10\hit.wav", 1, 90, 0.5, 5, -1, 5, 6, colores.white));
                                                      
                                                                                          }
                                           else
                                           {

                                               temporizador = new GameTimer(new TimeSpan(0, 0, 1), 15, @"FabricsSounds\level.mp3");
                                              
                                               MainCanon.weapons[0].charge(300);
                                               MainCanon.weapons[1].charge(200);
                                               MainCanon.weapons[2].charge(100);
                                               MainCanon.weapons[3].charge(2);

                                               MainBaseNave.Add(new Nave("interceptor", 2, 2, 2, @"SheepsSounds\1\ready.wav", @"SheepsSounds\1\death.wav", @"SheepsSounds\1\fire.wav", @"SheepsSounds\1\hit.wav", 1, 5, 0.2, 5, 0.5, 1, 1, colores.red));
                                               MainBaseNave.Add(new Nave("Mutalisc", 4, 3, 2, @"SheepsSounds\2\ready.wav", @"SheepsSounds\2\death.wav", @"SheepsSounds\2\fire.wav", @"SheepsSounds\2\hit.wav", 3, 8, 0.3, 10, -1, 2, 3, colores.brown));
                                               MainBaseNave.Add(new Nave("wright", 6, 2, 1, @"SheepsSounds\3\ready.wav", @"SheepsSounds\3\death.wav", @"SheepsSounds\3\fire.wav", @"SheepsSounds\3\hit.wav", 9, 5, 0.35, 20, -1, 2, 10, colores.darkRed));
                                               MainBaseNave.Add(new Nave("RyuSheep", 7, 2, 2, @"SheepsSounds\4\ready.wav", @"SheepsSounds\4\death.wav", @"SheepsSounds\4\fire.wav", @"SheepsSounds\4\hit.wav", 3, 13, 0.4, 10, -1, 5, 1, colores.red));
                                               MainBaseNave.Add(new Nave("Dragoon", 12, 5, 5, @"SheepsSounds\5\ready.wav", @"SheepsSounds\5\death.wav", @"SheepsSounds\5\fire.wav", @"SheepsSounds\5\hit.wav", 2, 15, 0.6, 3, -1, 20, 2, colores.fuxia));
                                               MainBaseNave.Add(new Nave("FlyingLurker", 9, 4, 4, @"SheepsSounds\6\ready.wav", @"SheepsSounds\6\death.wav", @"SheepsSounds\6\fire.wav", @"SheepsSounds\6\hit.wav", 1, 25, 0.7, 15, -1, 3, 10, colores.gray));
                                               MainBaseNave.Add(new Nave("Pajarín", 7, 1, 1, @"SheepsSounds\7\ready.wav", @"SheepsSounds\7\death.wav", @"SheepsSounds\7\fire.wav", @"SheepsSounds\7\hit.wav", 15, 25, 0.75, 20, -1, 1, 1, colores.green));
                                               MainBaseNave.Add(new Nave("BattleCruicer", 15, 10, 10, @"SheepsSounds\8\ready.wav", @"SheepsSounds\8\death.wav", @"SheepsSounds\8\fire.wav", @"SheepsSounds\8\hit.wav", 2, 30, 0.8, 25, -1, 20, 20, colores.darkGreen));
                                               MainBaseNave.Add(new Nave("guardian", 15, 5, 5, @"SheepsSounds\9\ready.wav", @"SheepsSounds\9\death.wav", @"SheepsSounds\9\fire.wav", @"SheepsSounds\9\hit.wav", 1, 30, 0.85, 20, -1, 27, 1, colores.cyan));
                                               MainBaseNave.Add(new Nave("Galactus", 20, 20, 10, @"SheepsSounds\10\ready.wav", @"SheepsSounds\10\death.wav", @"SheepsSounds\10\fire.wav", @"SheepsSounds\10\hit.wav", 1, 90, 0.9, 5, -1, 5, 6, colores.white));
                                                      

                                           }

                                           bgmSound = new List<string>();

                          bgmSound.Add("BGM\\Wins.mp3");
                                           bgmSound.Add("BGM\\fire.mp3");
                                           bgmSound.Add("BGM\\Sand.mp3");
             bgmSound.Add("BGM\\Bane.mp3");                              
                                           bgmSound.Add("BGM\\Wolf.mp3");
             bgmSound.Add("BGM\\ML.mp3");
             bgmSound.Add("BGM\\Loose.mp3");

                  points = 0;
       aleatorio = new Random();
       currentLevel = 1;
       

       MainMineralMine = new MineralMine("Minerales", new Point(0, 25), 3, 1, 1000, colores.gold, @"FabricsSounds\MineralDefeat.mp3");
       
       activeBullets = new List<Bullet>();
       activeNaves = new List<Nave>();
       colaNaves = new Queue<Nave>();
       MainFabrics = new Fabrics("Barraca", @"FabricsSounds\death.wav", @"FabricsSounds\select.wav", 3, 3, colores.silver, @"FabricsSounds\upgrade.wav", @"FabricsSounds\mineral.wav", @"FabricsSounds\pilon.wav", @"FabricsSounds\heal.wav", new Point(25, 25), 700, 50);
                                  MainScvBase = new SCV(5, @"ScvSounds\ready.wav", @"ScvSounds\death.wav", @"ScvSounds\mining.wav", colores.yellow, 6, 3, 5, new Point(MainFabrics.currentPosition.X, 26), 2, 2, "SCV", false);
                                  MainScvBase.asociatingMineAndFabrics(MainFabrics, MainMineralMine);
       mainShieldBase = new Shield("Escudo", 1, 1, 1,@"ShieldSounds\ready.wav", @"ShieldSounds\Death.wav", @"ShieldSounds\fire.wav", @"ShieldSounds\hit.wav", 20, 3, 3, colores.cyan, 1, false, false,new Point(0, 0));

                    MainFabrics.asociateElements(MainCanon, MainMineralMine, MainScvBase, mainShieldBase);
       goodForcesElements = new List<BaseElement>();
       goodForcesElements.Add(MainCanon);
       goodForcesElements.Add(MainFabrics);
       GameTimer.levelUp += new Action<GameTimer>(GameTimer_levelUp);
       GameTimer.ticActivated += new Action<GameTimer>(GameTimer_ticActivated);
       Fabrics.SCVCreated += new Action<SCV>(Fabrics_SCVCreated);
       temporizador.startTimer();
       MainFabrics.createSCV(0, 0);
       


       Weapon.shootNow += new Action<Bullet, Weapon>(Weapon_shootNow);
       Shield.reflector += new Action<Bullet, Shield>(Shield_reflector);
       Nave.shootNou += new Action<Nave, Bullet>(Nave_shootNou);
       Bullet.bulletDisapears += new Action<Bullet>(Bullet_bulletDisapears);
       NuclearMisil.nuclearImpact += new Action<NuclearMisil>(NuclearMisil_nuclearImpact);
       Fabrics._death += new Action<BaseElement>(Fabrics__death);
       SCV._death += new Action<BaseElement>(SCV__death);
       Canon._death += new Action<BaseElement>(Canon__death);
           Canon.canonDeath += new Action<Canon>(Canon_canonDeath);
           Nave._death += new Action<BaseElement>(Nave__death);
           Nave._ready += new Action<BaseElement>(Nave__ready);
           Shield._ready += new Action<BaseElement>(Shield__ready);
           Shield._death += new Action<BaseElement>(Shield__death);
   }

       public GameCore(int playLife, int initialMineral, int maxMineral)
                  {
           

       MainCanon = new Canon(3, 2, @"CanonSounds\death.wav", playLife, new Point(0, 23), "cañón p1", @"CanonSounds\change.wav", colores.blue, @"CanonSounds\clip.wav");
       MainCanon.setId(netIdentifierCounter);
       //onSendError("Cañón creado: #" + netIdentifierCounter);

           netIdentifierCounter++;

           MainCanon.addWeapon(new Weapon(1, 2, 2, "Lácer", @"CanonSounds\atk1.wav", @"CanonSounds\hit1.wav", @"CanonSounds\charge.wav", 10, false));
                                           MainCanon.addWeapon(new Weapon(2,3, 10, "Rocket", @"CanonSounds\atk2.wav", @"CanonSounds\hit2.wav", @"CanonSounds\charge.wav", 12, false));
                                           MainCanon.addWeapon(new Weapon(3, 6, 6, "PlasmaPot", @"CanonSounds\atk3.wav", @"CanonSounds\hit3.wav", @"CanonSounds\charge.wav", 14, false));
                                           MainCanon.addWeapon(new NuclearMisil(100, 27, 27, "NuclearMisil", @"CanonSounds\nukel.wav", @"CanonSounds\nukex.wav", @"CanonSounds\nuke1.wav", 3, @"CanonSounds\nuke2.wav", false));


                                          SecondCanon = new Canon(3, 2, @"CanonSounds\death.wav", playLife, new Point(24, 3), "cañón p2", @"CanonSounds\change.wav", colores.red , @"CanonSounds\clip.wav");
                                          SecondCanon.setId(netIdentifierCounter);
                                          //onSendError("Cañón creado: #" + netIdentifierCounter);

                                          netIdentifierCounter++;

                                           SecondCanon.addWeapon(new Weapon(1, 2, 2, "Lácer", @"CanonSounds\atk1.wav", @"CanonSounds\hit1.wav", @"CanonSounds\charge.wav", 10, true));
                                           SecondCanon.addWeapon(new Weapon(2, 3, 10, "Rocket", @"CanonSounds\atk2.wav", @"CanonSounds\hit2.wav", @"CanonSounds\charge.wav", 12, true));
                                           SecondCanon.addWeapon(new Weapon(3, 6, 6, "PlasmaPot", @"CanonSounds\atk3.wav", @"CanonSounds\hit3.wav", @"CanonSounds\charge.wav", 14, true ));
                                           SecondCanon.addWeapon(new NuclearMisil(100, 27, 27, "NuclearMisil", @"CanonSounds\nukel.wav", @"CanonSounds\nukex.wav", @"CanonSounds\nuke1.wav", 3, @"CanonSounds\nuke2.wav", true));
           
                                               MainCanon.weapons[0].charge(100);
                                               MainCanon.weapons[1].charge(80);
                                               MainCanon.weapons[2].charge(50);
                                                          SecondCanon.weapons[0].charge(100);
                                               SecondCanon.weapons[1].charge(80);
                                               SecondCanon.weapons[2].charge(50);
                                                         
                                           bgmSound = new List<string>();
                                     bgmSound.Add("BGM\\Wins.mp3");
                                           bgmSound.Add("BGM\\fire.mp3");
                                           bgmSound.Add("BGM\\Sand.mp3");
             bgmSound.Add("BGM\\Bane.mp3");                              
                                           bgmSound.Add("BGM\\Wolf.mp3");
             bgmSound.Add("BGM\\ML.mp3");
             bgmSound.Add("BGM\\Loose.mp3");
                                    aleatorio = new Random();
             
                  MainMineralMine = new MineralMine("Minerales", new Point(0, 25), 3, 1, maxMineral, colores.gold, @"FabricsSounds\MineralDefeat.mp3");
                  MainMineralMine.setId(netIdentifierCounter);
                  //onSendError("Mineral creado: #" + netIdentifierCounter);
                             netIdentifierCounter++;

           secondMineralMine = new MineralMine("Minerales", new Point(26 , 0), 3, 1, maxMineral, colores.gold, @"FabricsSounds\MineralDefeat.mp3");
           secondMineralMine.setId(netIdentifierCounter);
           //onSendError("Mineral creado: #" + netIdentifierCounter);
                      netIdentifierCounter++;

       activeBullets = new List<Bullet>();
       
       MainFabrics = new Fabrics("Barraca", @"FabricsSounds\death.wav", @"FabricsSounds\select.wav", 3, 3, colores.silver, @"FabricsSounds\upgrade.wav", @"FabricsSounds\mineral.wav", @"FabricsSounds\pilon.wav", @"FabricsSounds\heal.wav", new Point(25, 25), (playLife+200) , initialMineral);
       MainFabrics.setId(netIdentifierCounter);
       //onSendError("Fábrica creada: #" + netIdentifierCounter);
                      netIdentifierCounter++;

           secondFabrics = new Fabrics("Barraca", @"FabricsSounds\death.wav", @"FabricsSounds\select.wav", 3, 3, colores.silver, @"FabricsSounds\upgrade.wav", @"FabricsSounds\mineral.wav", @"FabricsSounds\pilon.wav", @"FabricsSounds\heal.wav", new Point(0, 0), (playLife + 200), initialMineral);
           secondFabrics.setId(netIdentifierCounter);
           //onSendError("Fabrica creada: #" + netIdentifierCounter);

           netIdentifierCounter++;

           MainScvBase = new SCV(5, @"ScvSounds\ready.wav", @"ScvSounds\death.wav", @"ScvSounds\mining.wav", colores.yellow, 6, 3, 5, new Point(MainFabrics.currentPosition.X, 26), 2, 2, "SCV", false);
           MainScvBase.asociatingMineAndFabrics(MainFabrics, MainMineralMine);

           secondScvBase = new SCV(5, @"ScvSounds\ready.wav", @"ScvSounds\death.wav", @"ScvSounds\mining.wav", colores.yellow, 6, 3, 5, new Point(secondFabrics.currentPosition.X+secondFabrics.width , 0), 2, 2, "SCV", true);
           secondScvBase.asociatingMineAndFabrics(secondFabrics, secondMineralMine);

           mainShieldBase = new Shield("Escudo", 1, 1, 1,@"ShieldSounds\ready.wav", @"ShieldSounds\Death.wav", @"ShieldSounds\fire.wav", @"ShieldSounds\hit.wav", 20, 3, 3, colores.cyan, 1, false, false,new Point(0, 0));
          
           secondShieldBase  = new Shield("Escudo", 1, 1, 1, @"ShieldSounds\ready.wav", @"ShieldSounds\Death.wav", @"ShieldSounds\fire.wav", @"ShieldSounds\hit.wav", 20, 3, 3, colores.cyan, 1, false, true, new Point(0, 0));        
           MainFabrics.asociateElements(MainCanon, MainMineralMine, MainScvBase, mainShieldBase);
           secondFabrics.asociateElements(SecondCanon,secondMineralMine, secondScvBase,secondShieldBase);
                  goodForcesElements = new List<BaseElement>();
       goodForcesElements.Add(MainCanon);
       goodForcesElements.Add(MainFabrics);
       secondGoodForcesElements = new List<BaseElement>();
       secondGoodForcesElements.Add(SecondCanon);
       secondGoodForcesElements.Add(secondFabrics);
       temporizador = new GameTimer(new TimeSpan(0, 0, 1), 70, @"FabricsSounds\level.mp3");


           GameTimer.ticActivated+=new Action<GameTimer>(GameTimer_ticActivated);
              
       Fabrics.SCVCreated += new Action<SCV>(Fabrics_SCVCreated);
       temporizador.startTimer();
       MainFabrics.createSCV(0, 0);
       secondFabrics.createSCV(0, 0);



       Weapon.shootNow += new Action<Bullet, Weapon>(Weapon_shootNow);
       Shield.reflector += new Action<Bullet, Shield>(Shield_reflector);
       
       Bullet.bulletDisapears += new Action<Bullet>(Bullet_bulletDisapears);
       NuclearMisil.nuclearImpact += new Action<NuclearMisil>(NuclearMisil_nuclearImpact);
       Fabrics._death += new Action<BaseElement>(Fabrics__death);
       SCV._death += new Action<BaseElement>(SCV__death);
       Canon._death += new Action<BaseElement>(Canon__death);
           Canon.canonDeath += new Action<Canon>(Canon_canonDeath);
           

           Shield._ready += new Action<BaseElement>(Shield__ready);
           Shield._death += new Action<BaseElement>(Shield__death);
           multiplay = true;

   }

                       void Shield__death(BaseElement obj)
                {
                   try
                   {
                       Shield sender = (Shield)obj;

                       if (sender.dir == false)
                       {

                           goodForcesElements.Remove(sender);
                       }
                       else
                       {
                           secondGoodForcesElements.Remove(sender);

                       }
                       //onSendError("escudo muerto: " + sender.netIdentifier);

                   }
                    catch (Exception ep)
                   {
                        onSendError(ep.Message);

                    }

                }

                void Shield__ready(BaseElement obj)
                {
                    try
                    {
                        Shield sender = (Shield)obj;

                        if (sender.dir == false)
                        {

                            goodForcesElements.Add(sender);
                        }
                        else
                        {
                            secondGoodForcesElements.Add(sender);

                        }
                       
                        
                        sender.setId(netIdentifierCounter);
                        //onSendError("Escudo creado: #" + netIdentifierCounter);

                        netIdentifierCounter++;

                    }
                    catch (Exception e)
                    {
                        onSendError(e.Message);

                    }
                                    }

                void Shield_reflector(Bullet arg1, Shield arg2)
                {
                    activeBullets.Add(arg1);
                    arg1.setId(netIdentifierCounter);
                    //onSendError("Bala: #" + netIdentifierCounter);

                    netIdentifierCounter++;

                }

         void Nave__ready(BaseElement obj)
         {
             try
             {

                 activeNaves.Add((Nave)obj);
             }
             catch (Exception e)
             {
                 onSendError(e.Message);

             }
                      }

         void Nave__death(BaseElement obj)
         {
             try
             {

                 activeNaves.Remove((Nave)obj);
             }
             catch (Exception e)
             {
                 onSendError(e.Message);

             }

                      }

       void Canon__death(BaseElement obj)
       {
           try
           {
               Canon sender = (Canon)obj;
               if (sender == SecondCanon)
               {
                   secondGoodForcesElements.Remove(sender);

               }
               else
               {

                                      goodForcesElements.Remove(obj);
               }
               //onSendError("muerto cañón: #" + sender.netIdentifier);

           }
           catch (Exception e)
           {

               onSendError(e.Message);

           }
                  }

       void Canon_canonDeath(Canon obj)
       {
           
           temporizador.stopTimer();
           string msg = "";


           if (MainCanon.currentHp <= 0)
           {
               msg = "¡juego finalizado! El ganador fue el jugador 2";

               while (goodForcesElements.Count != 0)
               {

                   goodForcesElements[0].hiting(999999999);


               }
           }

           if (!multiplay)
           {
               msg = "¡has perdido! Tu puntaje fue de " + points.ToString() + " suerte que no se te pegó el juego";

               while (activeNaves.Count != 0)
               {

                   activeNaves[0].hiting(100);
               }
           }
           else
           {
               if (SecondCanon.currentHp <= 0)
               {
                   msg = "¡juego finalizado! El ganador ha sido el jugador 1";

                   while (secondGoodForcesElements.Count != 0)
                   {
                       secondGoodForcesElements[0].hiting(9999999999);

                   }
               }

           }

           onSendError(msg);

           
           onLooseGame(msg);
                      
                  }

       void SCV__death(BaseElement obj)
       {
           try
           {
               SCV sender = (SCV)obj;
               if (sender.asociatedFabrics == secondFabrics)
               {
                   secondGoodForcesElements.Remove(sender);
               }
               else
               {

                   goodForcesElements.Remove(obj);
               }
               //onSendError("scv muerto: #" + sender.netIdentifier);

           }
           catch (Exception e)
           {
               onSendError(e.Message);

           }

                  }

       void Fabrics__death(BaseElement obj)
       {
           try
           {

               Fabrics sender = (Fabrics)obj;
               if (sender.SCVBase == secondScvBase)
               {
                   secondGoodForcesElements.Remove(sender);

               }
               else
               {

                   goodForcesElements.Remove(obj);
               }
               //onSendError("fábrica muerta: #" + sender.netIdentifier);

           }
           catch (Exception e)
           {
               onSendError(e.Message);

           }
                  }

       void NuclearMisil_nuclearImpact(NuclearMisil obj)
       {
           if (!multiplay)
           {

               while (activeNaves.Count != 0)
               {
                   points += activeNaves[0].currentHp + 20;
                   activeNaves[0].hiting(100);

               }
           }
           else
           {
               if (obj.dir == false)
               {
                   for (int i = 0; i < secondGoodForcesElements.Count; i++)
                   {
                       if (obj.damage >= secondGoodForcesElements[i].currentHp)
                       {
                           secondGoodForcesElements[i].hiting(obj.damage);
                           i--;

                       }
                       else
                       {
                           secondGoodForcesElements[i].hiting(obj.damage);
                       }
                                          }
                                  }
               else
               {


                   for (int i = 0; i < goodForcesElements.Count ; i++)
                   {
                       if (obj.damage >= goodForcesElements[i].currentHp)
                       {
                           goodForcesElements[i].hiting(obj.damage);
                           i--;

                       }
                       else
                       {
                           goodForcesElements[i].hiting(obj.damage);
                       }
                                          }
               }
                          }
                             }

       void Bullet_bulletDisapears(Bullet obj)
       {
           activeBullets.Remove(obj);
           //onSendError("bala desapareció: #" + obj.netIdentifier);

       }

       void Nave_shootNou(Nave arg1, Bullet arg2)
       {
           activeBullets.Add(arg2);

       }

       void Weapon_shootNow(Bullet arg1, Weapon arg2)
       {
           if (activeBullets.Count < 150)
           {
               activeBullets.Add(arg1);
               arg1.setId(netIdentifierCounter);
               //onSendError("bala creada: #" + netIdentifierCounter);

               netIdentifierCounter++;

           }
       }

            void GameTimer_ticActivated(GameTimer obj)
     {
         if (obj.CurrentTime == 1)
         {
             MainFabrics.createSCV(0, 0);
             if (multiplay)
             {
                 secondFabrics.createSCV(0, 0);
             }
                                   }
         if (!multiplay)
         {

             sheepsAppears();
             sheepsFires();
             moveBullets();
             moveSheeps();
         }
         else
         {

             moveBullets();
         }
                                     }

            void moveSheeps()
            {
                for (int i = 0; i < activeNaves.Count; i++)
                {
                    for (int k = 0; k < activeNaves[i].speed; k++)
                    {
                                                activeNaves[i].move();

                                                if (activeNaves[i]          .currentPosition.Y == MainCanon.currentPosition.Y || activeNaves[i].currentPosition.Y+activeNaves[i].height >= 27)
                                                {
                                                    MainCanon.hiting(500);

                                                }

                    }
                    
                    
                }
                            }

            void moveBullets()
            {
                int i = 0;
                try
                {

                    while (i < activeBullets.Count)
                    {
                        for (int k = 0; k < activeBullets[i].movementSpeed; k++)
                        {
                            bulletImpact(activeBullets[i]);

                            if (activeBullets[i].startPosition.Y == 0 && activeBullets[i].direccion == false)
                            {
                                activeBullets[i].moveBullet();
                                i--;
                                break;

                            }
                            else if (activeBullets[i].startPosition.Y == 27 && activeBullets[i].direccion == true)
                            {
                                activeBullets[i].moveBullet();
                                i--;
                                break;

                            }
                            else
                            {

                                activeBullets[i].moveBullet();
                            }

                        }
                        i++;
                    }
                }
                catch (Exception e)
                {
                    onSendError(e.Message);

                }
                                            }

            void bulletImpact(Bullet obj)
            {
                try
                {

                    bool impact = false;

                    if (obj.direccion == true)
                    {
                        for (int i = 0; i < goodForcesElements.Count; i++)
                        {
                            for (double h = obj.startPosition.X; h <= obj.startPosition.X + obj.width; h++)
                            {
                                bool contact = false;
                                for (double k = obj.startPosition.Y; k <= obj.startPosition.Y + obj.height; k++)
                                {
                                    if (goodForcesElements[i].currentPosition.X <= h && h <= goodForcesElements[i].currentPosition.X + goodForcesElements[i].width && goodForcesElements[i].currentPosition.Y <= k && k <= goodForcesElements[i].currentPosition.Y + goodForcesElements[i].height)
                                    {
                                        contact = true;
                                        impact = true;

                                        goodForcesElements[i].hiting(obj.bulletDamage);


                                        break;

                                    }
                                }


                                if (contact == true)
                                {
                                    break;

                                }
                            }
                        }


                    }
                    else
                    {

                        if (!multiplay)
                        {

                            for (int i = 0; i < activeNaves.Count; i++)
                            {
                                for (double h = obj.startPosition.X; h <= obj.startPosition.X + obj.width; h++)
                                {
                                    bool contact = false;
                                    for (double k = obj.startPosition.Y; k <= obj.startPosition.Y + obj.height; k++)
                                    {
                                        if (activeNaves[i].currentPosition.X <= h && h <= activeNaves[i].currentPosition.X + activeNaves[i].width && activeNaves[i].currentPosition.Y <= k && k <= activeNaves[i].currentPosition.Y + activeNaves[i].height)
                                        {
                                            contact = true;
                                            impact = true;

                                            activeNaves[i].hiting(obj.bulletDamage);
                                            points += obj.bulletDamage;
                                            break;

                                        }
                                    }


                                    if (contact == true)
                                    {
                                        break;

                                    }
                                }
                            }
                        }
                        else
                        {


                            for (int i = 0; i < secondGoodForcesElements.Count ; i++)
                            {
                                for (double h = obj.startPosition.X; h <= obj.startPosition.X + obj.width; h++)
                                {
                                    bool contact = false;
                                    for (double k = obj.startPosition.Y; k <= obj.startPosition.Y + obj.height; k++)
                                    {
                                        if (secondGoodForcesElements[i].currentPosition.X <= h && h <= secondGoodForcesElements[i].currentPosition.X + secondGoodForcesElements[i].width && secondGoodForcesElements[i].currentPosition.Y <= k && k <= secondGoodForcesElements[i].currentPosition.Y + secondGoodForcesElements[i].height)
                                        {
                                            contact = true;
                                            impact = true;

                                            secondGoodForcesElements[i].hiting(obj.bulletDamage);
                                            
                                            break;

                                        }
                                    }


                                    if (contact == true)
                                    {
                                        break;

                                    }
                                }
                            }
                                                                           }
                                            }

                    if (impact == true)
                    {
                        obj.bulletImpact();

                    }
                }
                catch (Exception e)
                {
                    obj.bulletImpact();
                    onSendError(e.Message);

                }
            }

            void sheepsFires()
            {
                for (int k = 0; k < activeNaves.Count;k++)
                {
                    if (aleatorio.NextDouble() <= activeNaves[k].provavilityFire)
                    {
                        if (activeBullets.Count < 100)
                        {

                            activeNaves[k].shoot();
                        }



                    }

   

                    
                                    }
                            }

            void sheepsAppears()
            {
                for (int i = 0; i < MainBaseNave.Count; i++)
                {
                    if (aleatorio.NextDouble() <= MainBaseNave[i].provavilityAppears)
                    {
                        if (colaNaves.Count <= 10)
                        {

                            colaNaves.Enqueue(MainBaseNave[i]);
                        }

                        addSheeps();

                                            }
                                    }
                                            }

            void addSheeps()
            {
                Nave auxol = colaNaves.Dequeue();
                if (auxol != null)
                {
                    if (activeNaves.Count <= 20)
                    {

                        auxol.cloneSheep();

                         
                    }
                    else
                    {
                        colaNaves.Enqueue(auxol);

                    }

                                    }
            }

            void GameTimer_levelUp(GameTimer obj)
            {
                if (!multiplay)
                {

                    currentLevel++;
                    if (currentLevel == 13)
                    {

                        while (activeNaves.Count != 0)
                        {
                            points += activeNaves[0].currentHp;
                            activeNaves[0].hiting(100);

                        }
                        points = points * 10;
                        onSendError("Juego ganado, puntaje: " + points);

                        


                        onWinsGame();

                    }

                    switch (currentLevel)
                    {
                        case 2:
                            MainBaseNave[0].updateProvavilityAppears(0.3);
                            MainBaseNave[1].updateProvavilityAppears(0.3);
                            break;
                        case 3:
                            MainBaseNave[0].updateProvavilityAppears(0.2);
                            MainBaseNave[1].updateProvavilityAppears(0.2);
                            MainBaseNave[2].updateProvavilityAppears(0.1);
                            break;
                        case 4:
                            MainFabrics.createSCV(0, 0);
                            MainBaseNave[0].updateProvavilityAppears(0.1);
                            MainBaseNave[2].updateProvavilityAppears(0.2);
                            MainBaseNave[3].updateProvavilityAppears(0.1);
                            break;
                        case 5:
                            MainCanon.weapons[0].charge(200);
                            MainCanon.weapons[1].charge(150);
                            MainCanon.weapons[2].charge(100);
                            MainCanon.weapons[3].charge(10);
                            MainFabrics.hiting(0 - 100);
                            MainCanon.hiting(0 - 100);
                            MainMineralMine.extractingMineral(0 - 500);
                            MainFabrics.takeMineral(200);

                            MainBaseNave[0].updateProvavilityAppears(-1);
                            MainBaseNave[1].updateProvavilityAppears(0.1);
                            MainBaseNave[3].updateProvavilityAppears(0.2);
                            MainBaseNave[4].updateProvavilityAppears(0.1);
                            break;
                        case 6:
                            MainFabrics.createSCV(0, 0);
                            MainBaseNave[5].updateProvavilityAppears(0.1);
                            MainBaseNave[2].updateProvavilityAppears(0.1);
                            break;
                        case 7:
                            MainFabrics.createSCV(0, 0);

                            MainBaseNave[0].updateProvavilityAppears(-1);
                            MainBaseNave[1].updateProvavilityAppears(-1);
                            MainBaseNave[2].updateProvavilityAppears(0.1);
                            MainBaseNave[3].updateProvavilityAppears(0.2);
                            MainBaseNave[4].updateProvavilityAppears(0.2);
                            MainBaseNave[5].updateProvavilityAppears(0.2);
                            MainBaseNave[6].updateProvavilityAppears(0.1);

                            break;
                        case 8:
                            MainFabrics.createSCV(0, 0);
                            MainBaseNave[3].updateProvavilityAppears(0.1);
                            MainBaseNave[4].updateProvavilityAppears(0.1);
                            MainBaseNave[6].updateProvavilityAppears(0.2);
                            MainBaseNave[7].updateProvavilityAppears(0.1);

                            break;
                        case 9:
                            MainFabrics.createSCV(0, 0);
                            MainBaseNave[8].updateProvavilityAppears(0.1);
                            MainBaseNave[7].updateProvavilityAppears(0.2);
                            MainBaseNave[2].updateProvavilityAppears(-1);

                            break;
                        case 10:
                            MainFabrics.createSCV(0, 0);

                            MainBaseNave[3].updateProvavilityAppears(-1);

                            MainBaseNave[4].updateProvavilityAppears(0.1);
                            MainBaseNave[5].updateProvavilityAppears(0.1);
                            MainBaseNave[6].updateProvavilityAppears(0.2);
                            MainBaseNave[7].updateProvavilityAppears(0.2);
                            MainBaseNave[8].updateProvavilityAppears(0.2);
                            MainBaseNave[9].updateProvavilityAppears(0.1);

                            break;
                        case 11:
                            MainBaseNave[9].updateProvavilityAppears(0.2);
                            MainBaseNave[5].updateProvavilityAppears(0.1);

                            break;
                        default:

                            MainBaseNave[9].updateProvavilityAppears(0.9);
                            break;

                    }
                }
            }

     public void createScv()
     {
         MainFabrics.createSCV(3, 10);
     }

     public void createScvp2()
     {
         secondFabrics.createSCV(3, 10);

     }

     void Fabrics_SCVCreated(SCV obj)
     {
         try
         {
             if (obj.asociatedFabrics == secondFabrics)
             {
                 secondGoodForcesElements.Add(obj);

             }
             else
             {


                 goodForcesElements.Add(obj);
             }
             obj.setId(netIdentifierCounter);
             //onSendError("scv: #" + netIdentifierCounter);
            
             netIdentifierCounter++;

         }
         catch (Exception e)
         {
             onSendError(e.Message);

         }
              }

   public void ShootCanon()
     {
         MainCanon.shoot();

     }

   public void ShootCanonp2()
   {
       SecondCanon.shoot();

          }

          public void ChangeCanonWeapon()
   {
       MainCanon.changeWep();

   }

          public void changeCanonWeaponP2()
          {
              SecondCanon.changeWep();

                        }

   public void comprarLacer()
   {
       MainFabrics.chargeWeapon(0, 10, 0, 10);

   }

          public void comprarLacerp2()
   {
       secondFabrics.chargeWeapon(0, 10, 0, 10);

          }

       
      public void comprarRocket()
   {
       MainFabrics.chargeWeapon(2, 10, 1, 5);

   }

      public void comprarRocketp2()
      {
          secondFabrics.chargeWeapon(2, 10, 1, 5);

      }

   public void comprarPlasma()
   {
       MainFabrics.chargeWeapon(3, 10, 2, 3);

   }

   public void comprarPlasmap2()
   {

       secondFabrics.chargeWeapon(3, 10, 2, 3);

   }

   public void ComprarNuke()
   {
       MainFabrics.chargeWeapon(8, 40, 3, 1);

   }

   public void comprarNukeP2()
   {
       secondFabrics.chargeWeapon(8, 40, 3, 1);

   }

   public void moveCanonLeft()
   {
       MainCanon.move(false);
          }

   public void moveCanonRight()
   {
       MainCanon.move(true);

   }

   public void moveCanonLeftP2()
   {
       SecondCanon.move(false);

   }

   public void moveCanonRightP2()
   {
       SecondCanon.move(true);

   }

   public void healCanon()
   {
       MainFabrics.healtCanon(0, 20, 10);

   }

   public void healCanonp2()
   {
       secondFabrics.healtCanon(0, 20, 10);

   }

   public void upgradeSuperScv()
   {
       MainFabrics.upradeScv(10, 50, 10, 1, 10);
   }

   public void upgradeSuperScvP2()
   {
       secondFabrics.upradeScv(10, 50, 10, 1, 10);

   }

   public void rechargeMineral()
   {
       MainFabrics.rechargeMineral(0, 30, 100);
   }

   public void rechargeMineralP2()
   {
       secondFabrics.rechargeMineral(0, 30, 100);

   }

   public void createShield()
   {
       int coste = 1;
       if (mainShieldBase.mirror == true)
       {
           coste += 4;

       }

              MainFabrics.createShield(coste);

   }

   public void createShieldP2()
   {

       int coste = 1;
       if (mainShieldBase.mirror == true)
       {
           coste += 4;

              }
              
       secondFabrics.createShield(coste);
          }

   public void upgradeShieldMirror()
   {
       MainFabrics.upgradeMirror(15, 35);

   }

   public void upgradeShieldMirrorP2()
   {
       secondFabrics.upgradeMirror(15, 35);

   }

   public void upgradeShields()
   {
       if (MainFabrics.publicShieldUpgradeNumber == 0)
       {
           MainFabrics.upgradeShields(3, 10);

       }
       else
       {
           MainFabrics.upgradeShields(6, 20);

       }


   }

   public void upgradeShieldsP2()
   {
       if (secondFabrics.publicShieldUpgradeNumber == 0)
       {
           secondFabrics.upgradeShields(3, 10);

       }
       else
       {
           
          secondFabrics.upgradeShields(6, 20);

       }

          }
       
             }
}
