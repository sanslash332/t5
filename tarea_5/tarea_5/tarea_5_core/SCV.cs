using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;

namespace tarea_5_core
{
  [Serializable]

    public class SCV : BaseElement 
    {
       public MineralMine asociatedMine { get; private set; }
        
       public Fabrics asociatedFabrics { get; private set; }
              public string miningSound { get; private set; }
       public int miningTiming { get; private set; }
       public int miningCount { get; private set; }
       public double movementSpeed { get; private set; }
       public int mineralInHands { get; private set; }
       private bool isEnter = false;
       private bool isDeath = false;
       private bool mineralPos;
       private bool dir;
              private static object sinchroniser = new object();
              //private static object sinchroniserp2 = new object();

              private Thread executionThread;
                     public static event Action<SCV> miningNow;
                     protected virtual void onMiningNou()
                     {
                         if (miningNow != null)
                         {
                             miningNow(this);

                         }
                                              }
                     public static event Action<SCV> takeMineral;
                     protected virtual void onTakeMineral()
                     {
                         if (takeMineral != null)
                         {
                             takeMineral(this);
                         }
                     }

              public SCV(double _hp, string strsound, string dethsound, string miningssound, colores coloru, double speed, int miningtimm, int miningcounn , Point posittion, double widdth, double heggit, string _name, bool startDirAndMineralPos)
              {
                  hp = _hp;
                  currentHp = hp;
                  name = _name;
                  coloring = coloru;
                  width = widdth;
                  height = heggit;
                  startSound = strsound;
                  deathSound = dethsound;
                  miningSound = miningssound;
                  miningTiming = miningtimm;
                  miningCount = miningcounn;

                  mineralInHands = 0;
                  

                  currentPosition = new Point(posittion.X, posittion.Y);

                  movementSpeed = speed;

                                    dir = startDirAndMineralPos;
                                    mineralPos = dir;
                                    

              }

              public void startSCV()
              {
                  executionThread = new Thread(new ThreadStart(workingSCV));
                  ready(this);
                  executionThread.Start();
                  
                                                  }

              private void workingSCV()
              {
                  

                  while (true)
                  {

                      if (mineralPos == false)
                      {

                          for (int i = 0; i < movementSpeed; i++)
                          {
                              if (dir == false && currentPosition.X <= asociatedMine.mineralPos.X + asociatedMine.mineWidth)
                              {

                                  dir = true;
                                  mining();
                                  break;

                              }
                              else if (dir == true && currentPosition.X + width >= asociatedFabrics.currentPosition.X)
                              {
                                  dir = false;
                                  takeFabricsMineral();
                                  break;

                              }
                              else
                              {
                                  Thread.Sleep((int)(1000 / movementSpeed));

                                  move();

                              }
                          }
                      }
                      else
                      {

                          for (int i = 0; i < movementSpeed; i++)
                          {
                              if (dir == true && currentPosition.X + width >= asociatedMine.mineralPos.X)
                              {

                                  dir = false;
                                  mining();
                                  break;

                              }
                              else if (dir == false && currentPosition.X <= asociatedFabrics.currentPosition.X + asociatedFabrics.width)
                              {
                                  dir = true;
                                  takeFabricsMineral();
                                  break;

                              }
                              else
                              {
                                  Thread.Sleep((int)(1000 / movementSpeed));

                                  move();

                              }

                          }
                      }

                          

                                        }
                                }

              public void asociatingMineAndFabrics(Fabrics fabrica, MineralMine mine)
              {
                  asociatedFabrics = fabrica;
                  asociatedMine = mine;
                  
                                }

            private void mining()
              {
                  isEnter = true;
                Monitor.Enter(sinchroniser);
                  


                                  if (asociatedMine.currentMineral <= 0)
                  {
                      //Monitor.PulseAll(sinchroniser);

                                      Monitor.Exit(sinchroniser);
                      isEnter = false;

                      selfDestruct();

                      try
                      {
                          executionThread.Abort();


                      }
                      catch (Exception ero)
                      {
                          SendError(ero.Message);


                      }

                  }
                  else
                  {
                      for (int i = 0; i < miningTiming; i++)
                      {
                          onMiningNou();
 
                          Thread.Sleep(1000);

                          if (i + 1 == miningTiming)
                          {
                              for (int k = 0; k < miningCount; k++)
                              {
                                  if (asociatedMine.currentMineral > 0)
                                  {
                                      asociatedMine.extractingMineral(1);
                                      mineralInHands++;

                                  }
                                  else
                                  {
                                      break;

                                  }

                              }

                          }
                        }
                                        }

                                  //Monitor.PulseAll(sinchroniser);
 
                      Monitor.Exit(sinchroniser);
                      isEnter = false;

                      if (isDeath)
                      {

                          try
                          {
                              executionThread.Abort();

                                                        }
                          catch (Exception epa )
                          {
                              SendError(epa.Message);

                          }
                                                }
                                                                }

             private void takeFabricsMineral()
              {
                  asociatedFabrics.takeMineral(mineralInHands);
                  mineralInHands = 0;
                  onTakeMineral();

              }

              public void updateSpeed(double newSpeed)
              {
                  movementSpeed = newSpeed;

              }

              public void updateMiningData(int newMiningTime, int newMiningCount)
              {

                  miningCount = newMiningCount;
                  miningTiming = newMiningTime;

              }

public override void  hiting(double damageReseived)
{
 	
    currentHp -= damageReseived;

    hitingComplete(this);
    

        if (currentHp <= 0)
    {
        mineralInHands = 0;
        death(this);



        if (isEnter)
        {
            isDeath = true;

            isEnter = false;
        }
        else
        {

            try
            {


                executionThread.Abort();

            }
            catch (Exception ee)
            {
                SendError(ee.Message);


            }
        }

            }

}

private  void selfDestruct()
{
    hiting(99999);

}

public override void  move()
{
    if (dir == false)
    {
        currentPosition = new Point(currentPosition.X - 1, currentPosition.Y);

    }
    else
    {
        currentPosition = new Point(currentPosition.X + 1, currentPosition.Y);

    }
    elementMoved(this);
    
}

public SCV cloneSCV()
{
    SCV newscv = new SCV(hp, startSound, deathSound, miningSound, coloring, movementSpeed, miningTiming, miningCount, currentPosition, width, height,  name, dir );
    

    newscv.asociatingMineAndFabrics(asociatedFabrics, asociatedMine);


    return newscv;

}

}
}
