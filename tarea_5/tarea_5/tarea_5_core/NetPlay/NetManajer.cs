using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace tarea_5_core.NetPlay
{
  public abstract class NetManajer
    {
      public TcpClient comandos { get; protected set; }
      public NetworkStream comandoStream { get; protected set;}
      public TcpClient vectorInformacion { get; protected set; }
      public NetworkStream vectorInformacionStream { get; protected set; }
      public TcpClient soundSocket { get; protected set;}
      public NetworkStream soundSender { get; protected set; }

      protected Thread abrecanales;
      protected Thread soundCapture;

      public static event Action<NetInformation> infoReseived;
      protected virtual void onInfoReseived(NetInformation obj)
      {
          if (infoReseived != null)
          {
              infoReseived(obj);

          }
                }
      public static event Action<string> sendError;
      protected virtual void onSendErrooor(string obj)
      {
          if (sendError != null)
          {
              sendError(obj);

          }
                }
      public static event Action<string> soundReseived;
      protected virtual void onSoundReseived(string obj)
      {
          if (soundReseived != null)
          {
              soundReseived(obj);

          }

      }


      public abstract void startNet(IPEndPoint dirAndPort);
            public abstract void mantainOpenChanel();
      public abstract void sendData(NetInformation info);
      public abstract void sendSound(string sound);

      public void cierraCanalDeEscucha()
      {
          try
          {
              
              abrecanales.Abort();
              soundCapture.Abort();
              comandos.Close();
              vectorInformacion.Close();
              soundSocket.Close();
              
          }
          catch (Exception e)
          {
              onSendErrooor(e.Message);

          }

      }

    }
}
