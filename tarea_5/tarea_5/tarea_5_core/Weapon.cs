using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace tarea_5_core
{
  [Serializable]

    public   class Weapon
    {
     public  string name { get;protected set; }
     public  string fireSound { get; protected set; }
     public string hitSound { get; protected set; }
     public string chargeSound { get;protected set; }
            public double damage { get;  protected set;}
     public double bulletHeight { get; protected set; }
     public double bulletWidht { get; protected set; }
     public double speedBullet { get; protected set; }
     public int charges { get; protected set; }
     public bool dir { get; protected set; }
     public static event Action<Bullet, Weapon> shootNow;
     public event Action<Weapon> discharge;
     protected virtual void onWeaponDischarge()
     {
         if (discharge != null)
         {
             discharge(this);

         }
     }

     public event Action<Weapon> recharge;
     protected virtual void onRecharge()
     {
         if (recharge != null)
         {
             recharge(this);

         }
     }
    
       public Weapon(double _damage, double _width, double _height, string _name, string _fire, string _hit, string _recharge, double speed, bool direction)
     {
         dir = direction;

           bulletHeight = _height;
         bulletWidht = _width;
         damage = _damage;
         name = _name;
         charges = 0;
         chargeSound = _recharge;
         fireSound = _fire;
         hitSound = _hit;
         speedBullet = speed;

              }

     public virtual void shoot(Point bulletPos)
     {
         if (charges <= 0)
         {
             return;
         }

         charges--;
         if (shootNow != null)
         {

             shootNow(new Bullet(speedBullet, bulletPos, hitSound, damage, bulletHeight, bulletWidht, dir), this);
         }


         if (charges == 0)
         {
             if (discharge != null)
             {

                 discharge(this);
             }

                      }

         
     }

     public void charge(int cantity)
     {
         charges += cantity;
         if (recharge != null)
         {

             recharge(this);
         }

     }

    }
}
