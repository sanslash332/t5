using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace tarea_5_core
{
    [Serializable]

public abstract class BaseElement
    {
        public double hp { get; protected set; }
        public double currentHp { get; protected  set; }
        public Point currentPosition{ get; protected set; }
        public colores coloring { get; protected set; }     
        public double width { get; protected set; }
        public double height { get; protected set; }
            public string name { get; protected set; }
            public string image { get; protected set; }
        public string startSound { get; protected set; }
        public string deathSound { get; protected set; }
        public double netIdentifier { get; protected set; }
        public void setId(double id)
        {
            netIdentifier = id;

        }


        public static event Action<BaseElement> _elementMoved;

    protected virtual void elementMoved(BaseElement obj)
    {
        if (_elementMoved != null)
        {
            _elementMoved(obj);
        }
            }

        public static event Action<BaseElement> _hitingComplete;

        protected virtual void hitingComplete(BaseElement obj)
        {
            if (_hitingComplete != null)
            {
                _hitingComplete(obj);
            }
                    }

        public static event Action<BaseElement> _death;

        protected virtual void death(BaseElement obj)
        {
            if (_death != null)
            {
                _death(obj);
            }

        }
    public static event Action<BaseElement> _ready;

    protected virtual void
   ready(BaseElement obj)
    {
        if (_ready != null)
        {
            _ready(obj);
        }
    }
    public static event Action<string> _sendError;
    protected virtual void SendError(string obj)
    {
        if (_sendError != null)
        {
            _sendError(obj);

        }
            }

       public abstract void hiting(double damageReseived);
            public abstract void move();


    }

public enum colores
{
    black, red, blue, yellow, green, darkRed, darkGreen, brown, lightBlue, orange, cyan, fuxia, gray, gold, silver, pink, white
}
}
