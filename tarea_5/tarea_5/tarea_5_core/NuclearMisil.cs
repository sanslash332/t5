using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tarea_5_core
{
 [Serializable]

    public class NuclearMisil : Weapon
    {
      public string nuclearLaunched { get; protected set; }
     
      public double paralisation { get; protected set; }
      public static event Action<NuclearMisil> nuclearLaunch;
      protected virtual void onNukerLauncher()
      {
          if (nuclearLaunch != null)
          {
              nuclearLaunch(this);
                        }
      }
      public static event Action<NuclearMisil> nuclearDetected;
      protected virtual void onNuclearDetected()
      {
          if (nuclearDetected != null)
          {
              nuclearDetected(this);

          }
      }
      public static event Action<NuclearMisil> nuclearImpact;
      protected virtual void onNuclearImpact()
      {
          if (nuclearImpact != null)
          {
              nuclearImpact(this);

          }
      }

      public NuclearMisil(double _damage, double _width, double _height, string _name, string _fire, string _hit, string _recharge, double speed, string _launch, bool dir) : base( _damage,  _width,  _height,  _name,  _fire,  _hit,  _recharge, speed, dir)
      {
          paralisation = speed;
          nuclearLaunched = _launch;

          paralisation = 
             base.bulletHeight = _height;
            base.bulletWidht = _width;
            base.damage = _damage;
            base.name = _name;
            base.charges = 0;
            base.chargeSound = _recharge;
            base.fireSound = _fire;
            base.hitSound = _hit;
            base.speedBullet = speed;
          }

      public override void shoot(System.Windows.Point bulletPos)
      {
          if (charges <= 0)
          {
              onWeaponDischarge();
              return;
                        }

          onNukerLauncher();
          int extra1 = (int)(speedBullet * 1000) / 2;

          System.Threading.Thread.Sleep(extra1*2);
          onNuclearDetected();
          
          System.Threading.Thread.Sleep(extra1);
          onNuclearImpact();

          charges--;

          if (charges <= 0)
          {
              onWeaponDischarge();

          }

      }
      }
  }