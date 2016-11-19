using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.IO;

namespace tarea_5_core.NetPlay
{
  public class ServerP2P : NetManajer
    {

      public ServerP2P()
      {

      }

      public override void startNet(System.Net.IPEndPoint dirAndPort)
        {
            try
            {

                TcpListener comandosServer = new TcpListener(dirAndPort);
                                

                TcpListener vectorInformationServer = new TcpListener(dirAndPort.Address, dirAndPort.Port+1);
                
                onSendErrooor("Esperando conecciones en el puerto " + dirAndPort.Port.ToString());

                comandosServer.Start();
                comandos = comandosServer.AcceptTcpClient();
                vectorInformationServer.Start();
                vectorInformacion = vectorInformationServer.AcceptTcpClient();

                soundSocket = comandosServer.AcceptTcpClient();

                                onSendErrooor("¡Conectado ");

                vectorInformacionStream = vectorInformacion.GetStream();
                NetworkStream nsw = soundSocket.GetStream();
                soundSender = soundSocket.GetStream();
              
                                                abrecanales = new Thread(new ThreadStart(mantainOpenChanel));
                abrecanales.SetApartmentState(ApartmentState.STA);

                abrecanales.Start();

            }
            catch (Exception e)
            {
                onSendErrooor(e.Message);

            }
                            }

      public override void mantainOpenChanel()
      {
          comandoStream = comandos.GetStream();
          BinaryFormatter binareador = new BinaryFormatter();


          while (true)
          {

              try
              {
                  
                  if (comandos.Available > 0)
                  {

                      NetInformation neterds = binareador.Deserialize(comandoStream) as NetInformation;


                      onInfoReseived(neterds);
                      //onSendErrooor("mensaje resivido");

                  }


              }
              catch (Exception e)
              {
                  onSendErrooor(e.Message + ", " + e.StackTrace.ToString());
                  try
                  {

                      comandoStream = comandos.GetStream();
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
                BinaryFormatter binryu = new BinaryFormatter();
                binryu.Serialize(vectorInformacionStream, info);
                //onSendErrooor("enviado " + info.enviadoId + ", " + info.comando);

            }
            catch (Exception e)
            {
                try 

{
                                    onSendErrooor("error con el mensaje " + info._gamesStatus + "estado, " + info.comando + " Comando, " + info.enviadoId + " ID de el enviado, " + info.contentOfEnviado + "y  : " +  e.Message + ", " + e.StackTrace.ToString());
                
                

                    vectorInformacionStream = vectorInformacion.GetStream();
                }
                catch (Exception erc)
                {
                    onSendErrooor(e.Message + ", " + e.StackTrace.ToString());

                }
                                            }
                    }

        public override void sendSound(string sound)
        {

            try
            {
                BinaryFormatter binr = new BinaryFormatter();
                binr.Serialize(soundSender, sound);

                                //onSendErrooor("sonido enviado " + sound);

            }
            catch (Exception e)
            {
                onSendErrooor(e.Message + ", " + e.StackTrace.ToString());

                try
                {
                    soundSender = soundSocket.GetStream();

                }
                catch (Exception erc)
                {
                    onSendErrooor(erc.Message + ", " + e.StackTrace.ToString());

                }

            }

        }
    }
}
