using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace tarea_5_core
{
  public class GameTimer
    {
      public int CurrentTime { get; private set; }
      public int levelUpTime { get; private set; }
      public string levelUpSound { get; private set; }
                  private DispatcherTimer temporizador;
            public static event Action<GameTimer> ticActivated;
      protected virtual void onTicActivated()
      {
          if (ticActivated != null)
          {
              ticActivated(this);

          }
      }
      public static event Action<GameTimer> levelUp;
      protected virtual void onLevelUp ()
      {
          if (levelUp != null)
          {
              levelUp(this);

          }
      }

      public GameTimer(TimeSpan Intervalo, int _levelUpTime, string _levelUpSound)
      {
          temporizador = new DispatcherTimer();
          temporizador.Interval = Intervalo;
          levelUpSound = _levelUpSound;

          CurrentTime = 0;
          levelUpTime = _levelUpTime;
          temporizador.Tick += new EventHandler(temporizador_Tick);
      }

      void temporizador_Tick(object sender, EventArgs e)
      {
          CurrentTime++;
          onTicActivated();

          if (CurrentTime % levelUpTime == 0)
          {
              onLevelUp();

          }

      }

      public void startTimer()
      {
          temporizador.Start();

      }

      public void stopTimer()
      {
          temporizador.Stop();

      }
    }
}
