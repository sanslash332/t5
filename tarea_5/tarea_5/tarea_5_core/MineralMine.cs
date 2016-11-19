using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace tarea_5_core
{
    [Serializable]
    public class MineralMine
    {
        public int mineralTotal { get; private set; }
       public int currentMineral {get; private set;}
        public string deathMine {get; private set;}

        public double netIdentifier { get; protected set; }
        public void setId(double id)
        {
            netIdentifier = id;

        }
            
        public Point mineralPos { get; private set; }
        public double mineHeight { get; private set; }
        public double mineWidth { get; private set; }
        public colores mineColor { get; private set; }
        public string name { get; private set; }
        public static event Action<MineralMine> extractMineral;
        public static event Action<MineralMine> mineFinished;


        public MineralMine(string _name,Point  _posi, double _height, double _wiodht, int mineCounting, colores minecoco,string deth )
        {
            name = _name;
            deathMine = deth;

            mineHeight   = _height;
            mineWidth = _wiodht;
            mineralPos = _posi;
            mineColor = minecoco;
            mineralTotal = mineCounting;
            currentMineral = mineralTotal;



        }

        public void extractingMineral(int mineralTaked)
        {
            currentMineral -= mineralTaked;
            if (extractMineral != null)
            {
                extractMineral(this);
            }

            if (currentMineral <= 0)
            {
                if (mineFinished != null)
                {
                    mineFinished(this);

                }
            }

        }

    }
}
