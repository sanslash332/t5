using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace tarea_5_core
{
   [Serializable]

    public class Nave : BaseElement
    {
        public double speed { get; private set; }
        public double damage { get; private set; }
        public string fireSound { get; private set; }
                public double provavilityFire {get; private set;}
                public double provavilityAppears { get; private set; }
                public double bulletHeight { get; private set; }
                public double bulletWidth { get; private set; }
                        public string hitSound { get; private set; }
        public double speedBullet       {get; private set;}
        public static event Action<Nave ,Bullet > shootNou;

                public Nave(string nombre, double _hp, double _width, double _height,string _startSound, string _deathSound, string _fireSound, string _HitSound , double _speed, double _damage, double _fireProvavility, double bulletSpeed, double appearsProvavility, double bulletw, double bullethg, colores coloru)
                {
                   name = nombre;
                    hp = _hp;
                    currentHp = hp;
                    speed = _speed;
                    
                    speedBullet = bulletSpeed;
                    coloring = coloru;
                    currentPosition = new Point(0, 0);

                    height=_height;
                    width = _width;
                    startSound = _startSound;
                    deathSound = _deathSound;
                    damage = _damage;
                    provavilityFire = _fireProvavility;
                    hitSound = _HitSound;
                    provavilityAppears = appearsProvavility;
                    bulletHeight = bullethg;
                    bulletWidth = bulletw;
                    fireSound = _fireSound;

                }

                public override void hiting(double damageReseived)
                {
                    currentHp -= damageReseived;
                    hitingComplete(this);
                    if (currentHp <= 0)
                    {
                            death(this);
                        }
    
                                    }

        public void updateProvavilityFire (double updateed)
        {
            provavilityFire = updateed;

        }

        public void updateProvavilityAppears(double apearss)
        {
            provavilityAppears = apearss;

        }

        public override void move()
        {
if ( currentPosition.X + width >= 27)
{
    currentPosition = new Point(0, currentPosition.Y + 1);
    
    }
    else
{
    currentPosition = new Point(currentPosition.X + 1, currentPosition.Y);
}
    elementMoved(this);


        }

        public void shoot()
        {
            if (shootNou != null)
            {
                shootNou(this, new Bullet(speedBullet, currentPosition, hitSound, damage, bulletHeight, bulletWidth, true));
            }

            
        }


        public void cloneSheep()
        {
            Nave retornable = new Nave(name, hp, width, height, startSound, deathSound, fireSound, hitSound , speed, damage, provavilityFire, speedBullet, provavilityAppears, bulletWidth, bulletHeight, coloring);   
            ready(retornable);
            

            
        }

    }
}
