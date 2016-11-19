using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;

namespace tarea_5_core
{
   [Serializable]
    public   class Fabrics : BaseElement
    {
       public int mineralAvailable { get; private set; }
       public string notMineralSuficient { get; private set; }
       public string completedUpgrade { get; private set; }
       public string healt { get; private set; }
       public string notHaveFood { get; private set; }
      private int shieldUpgradeNumber = 0;
      public int publicShieldUpgradeNumber { get { return shieldUpgradeNumber; }}
       private Canon asociatedCanon;
                     private MineralMine asociatedMine;
       private List<SCV> AsociatedSCV;
       public SCV SCVBase { get; private set; }

       private Shield ShieldBase;
       private List<Shield> asociatedShield;

              private bool availableFabrics;
              private bool workFinished;
              private Action<int, int>  workToExecute;
              private int mineralRecharge;
             private int workCost;
              private int workTime;
              private int workTimeTranscurred;
              private int healtCount;
              private int weaponNumber;
              private int chargesNumber;
              private int scvSpeed;
              private int scvCantidad;
              private int scvTiempo;
                                          public static event Action<Fabrics> mineralReseived;
       protected virtual void onMineralReceived()
       {
           if (mineralReseived != null)
           {
               mineralReseived(this);
           }
       }
       public static event Action<Fabrics> notHaveMineralToBuy;
       protected virtual void onNotHaveMineralToBuy()
       {
           if (notHaveMineralToBuy != null)
           {
               notHaveMineralToBuy(this);
           }
       }
       public static event Action<Fabrics> upgradeCompleted;
       protected virtual void onUpgradeCompleted()
       {
           if (upgradeCompleted != null)
           {
               upgradeCompleted(this);

           }
       }
       public static event Action<Fabrics> notHaveSpaceToMoreSCV;
       protected virtual void onNotHaveMoreSpaceToMoreSVNV()
       {
           if (notHaveSpaceToMoreSCV != null)
           {
               notHaveSpaceToMoreSCV(this);
                          }
       }
       public static event Action<Fabrics> healtCompleted;
       protected virtual void onHealtCompleted()
       {
           if (healtCompleted != null)
           {
               healtCompleted(this);
           }
       }
       public static event Action<SCV> SCVCreated;
       protected virtual void onScvCreated(SCV obj )
       {
           if (SCVCreated != null)
           {
               SCVCreated(obj);

           }
       }

       public Fabrics(string _name, string _deathSound, string _startS, double _width, double _height, colores coloru, string _upgrade, string _notMinerals, string _notFood, string _healt, Point _position, double _hp, int startMineral)
       {
           name = _name;
           hp = _hp;
           currentHp = hp;
           healt = _healt;
           startSound = _startS;
           deathSound = _deathSound;
           width = _width;
           height = _height;
           SCV._death += new Action<BaseElement>(SCV__death);
           coloring = coloru;
           completedUpgrade = _upgrade;
           availableFabrics = true;
           mineralAvailable = startMineral;
           notHaveFood = _notFood;
           currentPosition = new Point(_position.X, _position.Y);
           GameTimer.ticActivated += new Action<GameTimer>(GameTimer_ticActivated);
           AsociatedSCV = new List<SCV>();
           asociatedShield = new List<Shield>();

           workTime = -1;
           workFinished = false;
           workTimeTranscurred = 0;
           workCost = 0;
           notMineralSuficient = _notMinerals;

           Shield._death += new Action<BaseElement>(Shield__death);
                  }

       void Shield__death(BaseElement obj)
       {
           try
           {
               Shield Sender = (Shield)obj;

               asociatedShield.Remove(Sender);

           }
           catch
           {

           }
                  }

       void SCV__death(BaseElement obj)
       {

           try
           {
               SCV sender = (SCV)obj;
               AsociatedSCV.Remove(sender);
               if (AsociatedSCV.Count == 0)
               {
                   onNotHaveMoreSpaceToMoreSVNV();

               }

           }
           catch (Exception erion)
           {
               SendError(erion.Message);

           }
                  }

       void GameTimer_ticActivated(GameTimer obj)
       {
           if (workTime >= 0 && workToExecute != null)
           {
               availableFabrics = false;

               if (workTimeTranscurred == workTime)
               {
                   workFinished = true;
                   availableFabrics = true;
                   workToExecute(workTime, workCost);
                   workFinished = false;
                   workTime = -1;
                   workTimeTranscurred = 0;
                   workToExecute = null;


               }
               else
               {

                   workTimeTranscurred++;
               }

           }
                  }

       public void asociateElements (Canon canion, MineralMine mina, SCV initial, Shield escudo)
       {
           asociatedCanon = canion;
           asociatedMine = mina;
           SCVBase = initial;
          ShieldBase = escudo;

                             }

       public void createSCV(int demora, int coste)
       {
           if (availableFabrics != false)
           {
               if (workFinished == false)
               {
                   if (mineralAvailable >= coste)
                   {
                       if (AsociatedSCV.Count >= 6)
                       {
                           onNotHaveMoreSpaceToMoreSVNV();
                           return;

                       }
                       takeMineral(0 - coste);
                       workTime = demora;
                       workCost = coste;
                                              workToExecute = new Action<int, int>(createSCV);

                                          }
                   else
                   {
                       onNotHaveMineralToBuy();


                   }
                                  }
               else
               {
                   SCV nuevoscv = SCVBase.cloneSCV();
                   AsociatedSCV.Add(nuevoscv);
                   onScvCreated(nuevoscv);
                   nuevoscv.startSCV();

                                  }
                                         }
       }

       public void healtCanon(int demora, int coste, int cantidad)
       {
           if (availableFabrics != false)
           {
               if (workFinished == false)
               {
                   healtCount = cantidad;
                                      procecessHealtCanon(demora, coste);
               }
               else
               {
                   asociatedCanon.hiting(0 - cantidad);
                   onHealtCompleted();
                   healtCount = 0;

                                  }
                          }
       }

       private void procecessHealtCanon(int demora, int cost)
       {
           if (workFinished == false)
           {
               if (mineralAvailable >= cost)
               {
                   takeMineral(0 - cost);

                   workCost = cost;
                   workTime = demora;
                   workToExecute = new Action<int, int>(procecessHealtCanon);
               }
               else
               {
                   onNotHaveMineralToBuy();

                   healtCount = 0;

               }
           }
           else
           {
               healtCanon(workTime, workCost, healtCount);
                          }
                  }

       public void chargeWeapon (int demora, int coste, int  indexDeArma, int cantidadDeBalas)
       {
       if (availableFabrics != false)
       {
           if (workFinished == false )
           {
               if (indexDeArma >= asociatedCanon.weapons.Count)
               {
                   return;
               }
               if (mineralAvailable < coste)
               {
                   onNotHaveMineralToBuy();

                   return;
               }
               takeMineral(0 - coste);

               weaponNumber = indexDeArma;
               chargesNumber = cantidadDeBalas;
               procecessChargeWeapon(demora, coste);
               
           }
           else
           {
               asociatedCanon.weapons[indexDeArma].charge(cantidadDeBalas);
               chargesNumber=0;
               weaponNumber=0;

                          }
                  }
       }
       
              private void procecessChargeWeapon (int demora, int coste)
       {
           if (workFinished == false)
           {
               workTime = demora;
               workCost = coste;
               workToExecute = new Action<int, int>(procecessChargeWeapon);


           }
           else
           {
               chargeWeapon(workTime, workCost, weaponNumber, chargesNumber);
                          }
           }

              public void upradeScv(int demora, int coste, int velocidad, int tiempo, int cantidad)
              {
                  if (availableFabrics)
                  {
                      if (!workFinished)
                      {
                          if (SCVBase.miningTiming >= tiempo && SCVBase.miningCount >= cantidad && SCVBase.movementSpeed >= velocidad)
                          {
                              return;

                          }
                          if (mineralAvailable < coste)
                          {
                              onNotHaveMineralToBuy();
                                                            return;
                                                        }
                          takeMineral(0 - coste);
                          scvSpeed = velocidad;
                          scvTiempo = tiempo;
                          scvCantidad = cantidad;
                          procesessUpgradeScv(demora, coste);

                                                }
                      else
                      {
                          SCVBase.updateSpeed(velocidad);
                          SCVBase.updateMiningData(tiempo, cantidad);
                          for (int i = 0; i < AsociatedSCV.Count; i++)
                          {
                              AsociatedSCV[i].updateSpeed(velocidad);
                              AsociatedSCV[i].updateMiningData(tiempo, cantidad);
                                                        }
                          scvCantidad = 0;
                          scvSpeed = 0;
                          scvTiempo = 0;

                                                    onUpgradeCompleted();

                      }
                  }
                                }

              private void procesessUpgradeScv(int demora, int coste)
              {
                  if (!workFinished)
                  {
                      workCost = coste;
                      workTime = demora;
                      workToExecute = new Action<int, int>(procesessUpgradeScv);
                                        }
                  else
                  {
                      upradeScv(demora, coste, scvSpeed, scvTiempo, scvCantidad);

                  }
                                }

              public void rechargeMineral(int demora, int coste, int cantidad)
              {
                  if (availableFabrics)
                  {
                      if (!workFinished )
                      {
                          if (asociatedMine.currentMineral == 0 || asociatedMine.currentMineral >= asociatedMine.mineralTotal)
                          {
                              return;
                          }

                          if (mineralAvailable < coste)
                          {
                              onNotHaveMineralToBuy();
                              return;
                          }
                          takeMineral(0 - coste);
                          mineralRecharge = cantidad;
                          procesessRechargeMineral(demora, coste);


                      }
                      else
                      {
                          asociatedMine.extractingMineral(0 - cantidad);
                          if (asociatedMine.currentMineral > asociatedMine.mineralTotal)
                          {
                              asociatedMine.extractingMineral((asociatedMine.currentMineral - asociatedMine.mineralTotal));


                          }
                          onHealtCompleted();


                      }
                                        }
              }

              private void procesessRechargeMineral(int demora, int costo)
              {
                  if (!workFinished)
                  {
                      workCost = costo;
                      workTime = demora;
                      workToExecute = new Action<int, int>(procesessRechargeMineral);

                  }
                  else
                  {
                      rechargeMineral(workTime, workCost, mineralRecharge);

                  }
              }

       public void takeMineral(int quantiti)
       {
           mineralAvailable += quantiti;
           onMineralReceived();

       }

       public void createShield(int cost)
       {
           if (mineralAvailable < cost)
           {
               onNotHaveMineralToBuy();
               return;
                          }

           for (int i = 0; i < asociatedShield.Count; i++)
           {
               if (asociatedCanon.currentPosition.X == asociatedShield[i].currentPosition.X)
               {
           return;    
               }
           }

           takeMineral(0 - cost);
           if (ShieldBase.dir == false)
           {
               asociatedShield.Add(ShieldBase.CloneShield(new Point(asociatedCanon.currentPosition.X, asociatedCanon.currentPosition.Y - 1)));

           }
           else
           {
               asociatedShield.Add(ShieldBase.CloneShield(new Point (asociatedCanon.currentPosition.X, asociatedCanon.currentPosition.Y+ asociatedCanon.height+1)));

           }


       }

       public void upgradeMirror(int demora, int coste)
       {
           if (availableFabrics)
           {
               if (!workFinished)
               {
                   if (ShieldBase.mirror == true && ShieldBase.currentLevel > 3)
                   {
                       return;

                   }

                   if (mineralAvailable < coste)
                   {
                       onNotHaveMineralToBuy();
                       return;

                   }

                   takeMineral(0 - coste);
                   workCost = coste;
                   workTime = demora;
                   workToExecute = new Action<int, int>(upgradeMirror);

                                  }
               else
               {
                   ShieldBase.upgradeReflection(2, true);
                   for (int i = 0; i < asociatedShield.Count; i++)
                   {
                       asociatedShield[i].upgradeReflection(2, true);
                                          }
                   onUpgradeCompleted();

               }
                                         }
                             }

       public void upgradeShields(int demora, int coste)
       {
           if (availableFabrics)
           {
               if (!workFinished)
               {
                   if (ShieldBase.currentLevel > 4)
                   {
                       return;

                   }

                                      if (mineralAvailable < coste)
                   {
                       onNotHaveMineralToBuy();
                       return;

                   }
                                      takeMineral(0 - coste);

                                      workCost = coste;
                                      workTime = demora;
                                      workToExecute = new Action<int, int>(upgradeShields);

                                  }
               else
               {
                   if (shieldUpgradeNumber == 0)
                   {
                       shieldUpgradeNumber++;
                       ShieldBase.upgradeLevel(1, 2);
                       for (int i = 0; i < asociatedShield.Count; i++)
                       {
                           asociatedShield[i].upgradeLevel(1, 2);

                       }
                                          }
                   else if (shieldUpgradeNumber == 1)
                   {
                       shieldUpgradeNumber++;
                       ShieldBase.upgradeLevel(1, 3);

                       for (int i = 0; i < asociatedShield.Count; i++)
                       {
                           asociatedShield[i].upgradeLevel(1, 3);

                       }
                                          }

                   onUpgradeCompleted();

               }
                          }
                  }

              public void selectedXD()
       {
           if (availableFabrics == true)
           {

               ready(this);
           }
                  }


       public override void hiting(double damageReseived)
        {
            currentHp -= damageReseived;
            hitingComplete(this);
            if (currentHp <= 0)
            {
                death(this);
                availableFabrics = false;
                workTime = -1;
                workToExecute = null;
                for (int i = 0; i < AsociatedSCV.Count; i++)
                {
                    AsociatedSCV[i].hiting(100);
                    i--;

                }

                            }
                   }

        public override void move()
        {
            currentPosition = new Point(currentPosition.X, currentPosition.Y);
 
        }

           }
}
