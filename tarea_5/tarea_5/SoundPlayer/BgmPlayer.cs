using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio;
using System.Threading;
using IrrKlang;
using tarea_5_core;

namespace SoundPlayer
{
   public class BgmPlayer
    {
       public static string BgmAdress { get; private set; }
       private static ISoundEngine mediaPlayerControl = new ISoundEngine();
       private static ISound musica;

       private static IWavePlayer mainOutput;
       private static  bool manualStop = false;
       private static WaveMixerStream32 mixerin;
       private static float volumen=1.0f;
                     public static event Action<string> error;
      private static void onError(String sender)
       {
           if (error != null)
           {
               //error(sender);
           }
       }
      public static event Action loop;
      private static void onLoop()
      {
          if (loop != null)
          {
              loop();

          }

      }


       public static void playBgm(string soundName, float volumenn)
       {

           try
           {
               volumen = volumenn;
               mediaPlayerControl.SoundVolume = volumen;


               musica = mediaPlayerControl.Play2D(soundName, true);


               

                              //mainOutput = new DirectSoundOut();

               //mainOutput = new DirectSoundOut();

               
                              BgmAdress = soundName;
//               WaveMixerStream32 mixerer = new WaveMixerStream32();
               //mixerer.AutoStop = true;
                             //mixerer.AddInputStream(CreateInputStreamReturnable(BgmAdress));
                             //mainOutput.Init(mixerer);
                             //mixerin = mixerer;
                             //mainOutput.Volume = volumen;

                              //mainOutput.Play();


               //mainOutput.PlaybackStopped += new EventHandler(mainOutput_PlaybackStopped);

           }
           catch (Exception e)
           {
               onError(e.Message);

           }
                  }
       
     private static void mainOutput_PlaybackStopped(object sender, EventArgs e)
       {
           try
           {
               mainOutput.Stop();

               mainOutput.Dispose();
                        mixerin.Dispose();
                mixerin.Close();
                mainOutput = null;
               mixerin = null;


               Thread.Sleep(400);

               onLoop();
                                           
           }
                          catch (Exception erri)
           {
               onError(erri.Message);

           }
                         }

     public static void stopBgm()
     {
         if (musica != null)
         {
             musica.Stop();
             musica.Dispose();
             musica = null;

         }


         if (mainOutput != null)
         {

             //mainOutput.Stop();
         }


              }

     public static void rePlay()
     {
         if (BgmAdress != null)
         {
             playBgm(BgmAdress, volumen);

         }
     }

       private static  WaveStream CreateInputStreamReturnable(string fileName)
       {

WaveChannel32 inputStream;
           if (fileName.EndsWith(".mp3"))
           {

               WaveStream mp3Reader = new Mp3FileReader(fileName);
               inputStream = new WaveChannel32(mp3Reader);
           }
           else if (fileName.EndsWith(".wav"))
           {

               WaveStream readerStream = new WaveFileReader(fileName);

               if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
               {
                   readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                   readerStream = new BlockAlignReductionStream(readerStream);
               }
               if (readerStream.WaveFormat.BitsPerSample != 16)
               {
                   var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                    16, readerStream.WaveFormat.Channels);
                   readerStream = new WaveFormatConversionStream(format, readerStream);
               }
               inputStream = new WaveChannel32(readerStream);
           }

           else
           {
               throw new InvalidOperationException("Unsupported extension");
           }
           return inputStream;
       }


        



   }
}
