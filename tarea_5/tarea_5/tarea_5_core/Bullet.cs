using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace tarea_5_core
{
  [Serializable]

    public   class Bullet
    {
       public string hitSound { get; private set; }
       public Point startPosition { get; private set; }
       public double movementSpeed { get; private set; }
       public double bulletDamage { get; private set; }
       public double height { get; private set; }
       public double width { get; private set; }
       public bool direccion { get; private set; }


       public double netIdentifier { get; protected set; }
       public void setId(double id)
       {
           netIdentifier = id;

       }
              
       public static event Action<Bullet> bulletMoved;
       public static event Action<Bullet> bulletDisapears;
              public static event Action<Bullet> bulletContact;
       public static event Action<Bullet> appears;

       public Bullet(double speed, Point startPos, string hitss, double damagee, double bulletHeight, double bulletWidht, bool dir)
       {
           movementSpeed = speed;
           startPosition = startPos;
           hitSound = hitss;
           bulletDamage = damagee;
           height = bulletHeight;
           width = bulletWidht;
           direccion = dir;

       }

       public void moveBullet()
       {
           if (direccion == true)
           {
               startPosition = new Point(startPosition.X, startPosition.Y + 1);

           }
           else
           {
               startPosition = new Point(startPosition.X, startPosition.Y - 1);

           }

           if ((startPosition.Y < 0  && direccion == false)|| (startPosition.Y+height > 27 && direccion==true))
           {
               if (bulletDisapears != null)
               {
                   bulletDisapears(this);

               }
           }
           else
           {

               if (bulletMoved != null)
               {
                   bulletMoved(this);
               }
           }
       }
      
       public void bulletImpact()
       {
           if (bulletContact != null)
           {
               bulletContact(this);

           }

           if (bulletDisapears != null)
           {
               bulletDisapears(this);

           }
       }

   }
}
