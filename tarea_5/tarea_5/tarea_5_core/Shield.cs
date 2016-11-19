using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace tarea_5_core
{
  [Serializable]

    public class Shield : BaseElement
    {
      public string fireSound { get; private set; }
      public string hitSound { get; private set; }


      public int currentLevel { get; private set; }
      public bool mirror { get; private set; }
      public double mirrorWidth { get; private set; }
      public double mirrorHeight { get; private set; }
      public double mirrorSpeed { get; private set; }
      public bool dir { get; private set; }

      public static event Action<Bullet, Shield> reflector;
      private void onReflector(Bullet objs)
      {
          if (reflector != null)
          {
              reflector(objs, this);
                        }
                }

      public Shield(string nombre, double _hp, double _width, double _height,string _startSound, string _deathSound, string _fireSound, string _HitSound , double _speed, double bulletw, double bullethg, colores coloru, int _Level, bool _reflect, bool _dir, Point _pos)
      {

          name =  nombre;
        hp =   _hp;
          currentHp = hp;
                   width=  _width;
         height=  _height;
        startSound =   _startSound;
                  deathSound = _deathSound;
         fireSound = _fireSound;
         hitSound =   _HitSound ;
        mirrorSpeed = _speed;
        mirrorWidth = bulletw;
         mirrorHeight = bullethg;
        coloring =   coloru;
         currentLevel = _Level;
         mirror = _reflect;
          dir = _dir;
          currentPosition = new Point(_pos.X, _pos.Y);

                }

      public void upgradeLevel(int aumento, int newHp)
      {
          hp = newHp;
          currentHp = hp;
          currentLevel += aumento;

      }

      public void upgradeReflection(int aumento, bool reflectionStatus)
      {
          mirror = reflectionStatus;
          currentLevel += aumento;

      }

        public override void hiting(double damageReseived)
        {
            currentHp -= damageReseived;

            if (currentHp <= 0)
            {
                death(this);

            }

            if (mirror == true)
            {
                onReflector(new Bullet(mirrorSpeed , currentPosition, hitSound, damageReseived, mirrorHeight ,mirrorWidth , dir));

                            }

            
            
        }

        public override void move()
        {
                    }

        public Shield CloneShield(Point pos)
        {
            Shield aux = new Shield(name , hp,width ,height  , startSound , deathSound , fireSound , hitSound , mirrorSpeed , mirrorWidth , mirrorHeight , coloring , currentLevel , mirror , dir , pos );
            ready(aux);
            return aux;

        }

    }
}
