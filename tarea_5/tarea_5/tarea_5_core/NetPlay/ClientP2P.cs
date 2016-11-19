using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace tarea_5_core.NetPlay
{
  public class ClientP2P : NetManajer
    {
      public static event Action<string> errorConect;
      private int abc;

      public ClientP2P()
      {
          abc = 0;

      }

      public override void startNet(System.Net.IPEndPoint dirAndPort)
        {
            

            try
            {
                comandos = new TcpClient();
                vectorInformacion = new TcpClient();
                soundSocket = new TcpClient();

                                onSendErrooor("conectando... con el servidor " + dirAndPort.Address.ToString() + ": " + dirAndPort.Port.ToString());

                comandos.Connect(dirAndPort);
                vectorInformacion.Connect(dirAndPort.Address ,dirAndPort.Port+1);
                soundSocket.Connect(dirAndPort);

                onSendErrooor("conectado! ... ");

                comandoStream = comandos.GetStream();

                abrecanales = new Thread(new ThreadStart(mantainOpenChanel));
                abrecanales.SetApartmentState(ApartmentState.STA);

                abrecanales.Start();

                soundCapture = new Thread(new ThreadStart(captureSounds));
                soundCapture.SetApartmentState(ApartmentState.STA);
                soundCapture.Start();

                            }
            catch (Exception e)
            {
                onSendErrooor(e.Message);
                abc++;
                onSendErrooor("intento fallido, provando número " + abc);

                if (abc >= 2)
                {
                    errorConect("no se ha podido conectar al servidor, por fabor intente nuevamente"); 

                }

                startNet(dirAndPort);

            }
                  }

      public override void mantainOpenChanel()
      {
          vectorInformacionStream = vectorInformacion.GetStream();
          BinaryFormatter binewuprpoieqwrtckckjrkjkr = new BinaryFormatter();

          while (true)
          {
              try
              {
                  

                  if (vectorInformacion.Available > 0)
                  {

                      NetInformation infi = binewuprpoieqwrtckckjrkjkr.Deserialize(vectorInformacionStream) as NetInformation;
                      onInfoReseived(infi);
                      //onSendErrooor("resivido: " + infi.enviadoId + ", " + infi.comando);

                  }


              }
              catch (Exception e)
              {
                  onSendErrooor(e.Message + ", " + e.StackTrace.ToString());
                  try
                  {

                      vectorInformacionStream = vectorInformacion.GetStream();
                  }
                  catch (Exception erc)
                  {
                      onSendErrooor(erc.Message + ", " + erc.StackTrace.ToString());

                  }


              }
                        }
      }

        public override void sendData(NetInformation info)
        {

            try
            {
                BinaryFormatter binwfkt = new BinaryFormatter();
                binwfkt.Serialize(comandoStream, info);
                //onSendErrooor("Mensaje enviado");

            }
            catch (Exception e)
            {
                 try
                {

                    onSendErrooor("error con el mensaje " + info._gamesStatus + "estado, " + info.comando + " Comando, " + info.enviadoId + " ID de el enviado, " + info.contentOfEnviado + " y : " + e.Message + ", " + e.StackTrace.ToString());
               

                    comandoStream = comandos.GetStream();
                }
                catch (Exception erc)
                {
                    onSendErrooor(erc.Message + ", " + erc.StackTrace.ToString());

                }
                            }
        }

        public override void sendSound(string sound)
        {
            
        }

        public void captureSounds()
        {
            soundSender = soundSocket.GetStream();
            BinaryFormatter binryu = new BinaryFormatter();
            
                        while (true)
            {
                try
                {
                    if (soundSocket.Available > 0)
                    {

                      string nits = binryu.Deserialize(soundSender) as string;

                        onSoundReseived(nits);
                        //onSendErrooor("sonido resivido " + nits.sonablo);
                    }
                                    }
                catch (Exception e)
                {
                    onSendErrooor("error en sonido " + e.Message + ", " + e.StackTrace.ToString());
                    try
                    {
                        soundSender = soundSocket.GetStream();


                    }
                    catch (Exception ep)
                    {
                        onSendErrooor(ep.Message + ", " + ep.StackTrace.ToString());

                    }
                                    }
                                            }
                    }

    }
}
