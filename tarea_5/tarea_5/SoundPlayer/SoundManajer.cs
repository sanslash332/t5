using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using tarea_5_core;
using tarea_5_core.NetPlay;

namespace SoundPlayer
{
   public class SoundManajer
    {
       public bool soundActive;
     static MainPlayer casetera;
       NetManajer netcm;
      
              public SoundManajer()
       {
           soundActive = true;
           casetera = new MainPlayer();
           netcm = null;

           BaseElement._death += new Action<BaseElement>(BaseElement__death);
           BaseElement._ready += new Action<BaseElement>(BaseElement__ready);
           SCV.miningNow += new Action<SCV>(SCV_miningNow);
           
           Fabrics.healtCompleted += new Action<Fabrics>(Fabrics_healtCompleted);
           Fabrics.notHaveMineralToBuy += new Action<Fabrics>(Fabrics_notHaveMineralToBuy);
           Fabrics.notHaveSpaceToMoreSCV += new Action<Fabrics>(Fabrics_notHaveSpaceToMoreSCV);
           Fabrics.upgradeCompleted += new Action<Fabrics>(Fabrics_upgradeCompleted);
           Fabrics.mineralReseived += new Action<Fabrics>(Fabrics_mineralReseived);
           Canon.weaponChanged += new Action<Canon>(Canon_weaponChanged);
           Canon.notHaveWeapon += new Action<Canon>(Canon_notHaveWeapon);
           Canon.weaponCharged += new Action<Weapon>(Canon_weaponCharged);
           Canon.canonDeath += new Action<Canon>(Canon_canonDeath);
           Weapon.shootNow += new Action<Bullet, Weapon>(Weapon_shootNow);
           Bullet.bulletContact += new Action<Bullet>(Bullet_bulletContact);
           GameTimer.levelUp+=new Action<GameTimer>(GameTimer_levelUp);
           Nave.shootNou += new Action<Nave, Bullet>(Nave_shootNou);
           NuclearMisil.nuclearLaunch += new Action<NuclearMisil>(NuclearMisil_nuclearLaunch);
           NuclearMisil.nuclearDetected += new Action<NuclearMisil>(NuclearMisil_nuclearDetected);
           NuclearMisil.nuclearImpact += new Action<NuclearMisil>(NuclearMisil_nuclearImpact);
           MineralMine.mineFinished += new Action<MineralMine>(MineralMine_mineFinished);
           Shield.reflector += new Action<Bullet, Shield>(Shield_reflector);
           
           NetManajer.soundReseived +=new Action<string>(NetManajer_soundReseived);
              }

              public static void simplePlay(string obj)
              {
                  if (casetera == null)
                  {
                      casetera = new MainPlayer();

                  }

                      casetera.Play(obj);

              }

                     void NetManajer_soundReseived(string obj)
              {
                  if (soundActive)
                      casetera.Play(obj);

              }

              public void setNet(NetManajer obj)
              {
                  netcm = obj;
                                }

void Shield_reflector(Bullet arg1, Shield arg2)
       {
           if (soundActive)
               casetera.Play(arg2.fireSound);

           if (netcm != null)
           {
//               netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, arg2.fireSound));
               netcm.sendSound(arg2.fireSound);

           }
                             }

       void MineralMine_mineFinished(MineralMine obj)
       {
           if (soundActive)
               casetera.Play(obj.deathMine);

           if (netcm != null)
           {
//               netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.deathMine));

               netcm.sendSound(obj.deathMine);

           }
                  }

       void Canon_canonDeath(Canon obj)
       {
           //if (soundActive)
               //casetera.Play(obj.deathSound);

       }

       void NuclearMisil_nuclearImpact(NuclearMisil obj)
       {
           if (soundActive)
               casetera.Play(obj.hitSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.hitSound));

               netcm.sendSound(obj.hitSound);

           }
                  }

       void NuclearMisil_nuclearDetected(NuclearMisil obj)
       {
           if (soundActive)
               casetera.Play(obj.nuclearLaunched);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.nuclearLaunched));
               netcm.sendSound(obj.nuclearLaunched);

           }
                  }

       void NuclearMisil_nuclearLaunch(NuclearMisil obj)
       {
           if (soundActive)
               casetera.Play(obj.fireSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.fireSound));
               netcm.sendSound(obj.fireSound);
                          }
                  }

       void Fabrics_mineralReseived(Fabrics obj)
       {
           if (soundActive)
               casetera.Play(obj.startSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1 , null, null, obj.startSound));

               netcm.sendSound(obj.startSound);

           }
                  }

       void Nave_shootNou(Nave arg1, Bullet arg2)
       {
           if (soundActive)
               casetera.Play(arg1.fireSound);

       }

       void GameTimer_levelUp(GameTimer obj)
       {
           if (netcm == null)
           {

               if (soundActive)
                   casetera.Play(obj.levelUpSound);
           }
                  }

              public void inactiveSound()
       {
           if (soundActive)
           {
               soundActive = false;
           }
           else
           {
               soundActive = true;
           }
                  }

       void Bullet_bulletContact(Bullet obj)
       {
           if (soundActive)
               casetera.Play(obj.hitSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.hitSound));

               netcm.sendSound(obj.hitSound);

           }
                  }

       void Weapon_shootNow(Bullet arg1, Weapon arg2)
       {
           if (soundActive)
               casetera.Play(arg2.fireSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, arg2.fireSound));
               
               netcm.sendSound(arg2.fireSound);

                          }
                  }

       
                     void Canon_weaponCharged(Weapon obj)
       {
           if (soundActive)
               casetera.Play(obj.chargeSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.chargeSound));
               netcm.sendSound(obj.chargeSound);

           }
                         }

       void Canon_notHaveWeapon(Canon obj)
       {
           if (soundActive)
               casetera.Play(obj.notHaveWeapons);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.notHaveWeapons));

               netcm.sendSound(obj.notHaveWeapons);

           }
                  }

       void Canon_weaponChanged(Canon obj)
       {
           if (soundActive)
               casetera.Play(obj.changeWeapon);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.changeWeapon));

               netcm.sendSound(obj.changeWeapon);

           }
                  }


       void Fabrics_upgradeCompleted(Fabrics obj)
       {
           if (soundActive)
               casetera.Play(obj.completedUpgrade);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.completedUpgrade));

               netcm.sendSound(obj.completedUpgrade);

           }
                  }

       void Fabrics_notHaveSpaceToMoreSCV(Fabrics obj)
       {
           if (soundActive)
               casetera.Play(obj.notHaveFood);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.notHaveFood));
               netcm.sendSound(obj.notHaveFood);

                          }
                             }

       void Fabrics_notHaveMineralToBuy(Fabrics obj)
       {
           if (soundActive)
               casetera.Play(obj.notMineralSuficient);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.notMineralSuficient));

               netcm.sendSound(obj.notMineralSuficient);

           }
                  }

       void Fabrics_healtCompleted(Fabrics obj)
       {
           if (soundActive)
               casetera.Play(obj.healt);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.healt));

               netcm.sendSound(obj.healt);

           }
       }

       
       void SCV_miningNow(SCV obj)
       {
           if (soundActive)
               casetera.Play(obj.miningSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.miningSound));

               netcm.sendSound(obj.miningSound);

           }
                  }

       void BaseElement__ready(BaseElement obj)
       {
           if (soundActive)
               casetera.Play(obj.startSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.startSound));

               netcm.sendSound(obj.startSound);

           }
                  }

       void BaseElement__death(BaseElement obj)
       {
           if (soundActive)
               casetera.Play(obj.deathSound);

           if (netcm != null)
           {
               //netcm.sendData(new NetInformation(GameStatus.ready, GameCommands.playSound, -1, null, null, obj.deathSound));

               netcm.sendSound(obj.deathSound);

           }
                  }

    }
}
