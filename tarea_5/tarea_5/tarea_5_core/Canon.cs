using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace tarea_5_core
{
  [Serializable]
    public class Canon : BaseElement 
    {
       public List<Weapon> weapons { get; private set; }
       public Weapon currentWeapon { get; private set;}
       private int indexWeapon = 0;
                  public string changeWeapon { get; private set; }
                  public string notHaveWeapons { get; private set; }
       public static event Action<Canon> weaponChanged;
       public static event Action<Canon> canonDeath;
       public static event Action<Canon> canonShoot;
       public static event Action<Weapon> weaponCharged;
       public static event Action<Weapon> weaponDischarged;
public static event Action<Canon> notHaveWeapon;

       public Canon(double _width, double _height, string _deathSound, double _hp, Point _startPos, string _name, string changee, colores coloru, string sinWeapon)
       {
           name = _name;
          coloring = coloru;
          notHaveWeapons = sinWeapon;

           width = _width;
           height = _height;
           hp = _hp;
           currentHp = hp;
           currentPosition = new Point(_startPos.X,_startPos.Y);

           deathSound = _deathSound;
                      weapons = new List<Weapon>();
                      currentWeapon = null;
                      changeWeapon = changee;

       }

       public void addWeapon(Weapon newWep)
       {
           weapons.Add(newWep);
           newWep.discharge += new Action<Weapon>(newWep_discharge);
           newWep.recharge += new Action<Weapon>(newWep_recharge);
           if (currentWeapon == null)
           {
               currentWeapon = newWep;

           }
       }

       void newWep_recharge(Weapon obj)
       {
           if (currentWeapon == null)
           {
               currentWeapon = obj;
               indexWeapon = 0;
           }
           if (weaponCharged != null)
           {

               weaponCharged(obj);
           }


       }

       void newWep_discharge(Weapon obj)
       {
           if (currentWeapon == obj)
           {
               if (weaponDischarged != null)
               {
                   weaponDischarged(obj);
               }
               changeWep();

           }
           
       }
       
       public void changeWep ()
       {
           int currentInt = indexWeapon;
           indexWeapon++;

           while (true)
           {
               if (indexWeapon == currentInt)
               {
                   break;
               }
               if (indexWeapon >= weapons.Count)
               {
                   indexWeapon = 0;
               }
               if (weapons[indexWeapon].charges > 0)
               {
                   currentWeapon = weapons[indexWeapon];
                   if (weaponChanged != null)
                   {

                       weaponChanged(this);
                   }
                   break;
               }

               indexWeapon++;

           }

           if (currentWeapon!=null && currentWeapon.charges == 0)
           {
               currentWeapon=null;
               if (weaponChanged != null)
               {

                   weaponChanged(this);
               }
               if (notHaveWeapon != null)
               {

                   notHaveWeapon(this);
               }
           }
       }
               

           

       

       public override void  hiting(double damageReseived)
{
currentHp -= damageReseived;
           hitingComplete(this);

           if (currentHp <= 0)
           {
               death(this);

               if (canonDeath != null)
               {

                   canonDeath(this);
               }
               

                          }
}

       public override void move()
       {
           move(true);

       }
   public void  move(bool direccion)
{
 if (direccion == true)
 {
     if (currentPosition.X+width >= 27)
     {
         return;
     }
     else
     {
        currentPosition = new Point(currentPosition.X+1, currentPosition.Y);
         elementMoved(this);
     }
          }
 else
 {
     if (currentPosition.X <= 0)
     {
         return;
     }
     else
     {
         currentPosition = new Point(currentPosition.X - 1, currentPosition.Y);
         elementMoved(this);

     }
 }


}

       public void shoot()
       {
           if (currentWeapon == null)
           {
               if (notHaveWeapon != null)
               {
                   notHaveWeapon(this);
                   return;

               }
                          }

           if (canonShoot != null)
           {

               canonShoot(this);
           }

           currentWeapon.shoot(currentPosition);

       }

   }
}
