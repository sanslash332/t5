using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.IO;
using System.Threading;
using System.Media;
using IrrKlang;

namespace SoundPlayer
{
    public class MainPlayer
    {
        private List<IWavePlayer> output;
        private ISoundEngine controlador = new ISoundEngine();
        
        private List<WaveStream> mainStreams;
        private List<WaveChannel32> volumeStreams;
        private Dictionary<string, int> direccion;
        private List<long> lengths;
        private List<WaveMixerStream32> mixeador;
        
        private Dictionary<IWavePlayer, WaveStream> diccionarioLoco;
        private Dictionary<IWavePlayer, WaveMixerStream32> diccionarioReLocoMixer;
                public event Action<string> error;
                private int aguante = 0;

                public MainPlayer()
                {
                    output = new List<IWavePlayer>();
                    direccion = new Dictionary<string, int>();
                                        
                    diccionarioLoco = new Dictionary<IWavePlayer, WaveStream>();
                    diccionarioReLocoMixer = new Dictionary<IWavePlayer, WaveMixerStream32>();

                    mainStreams = new List<WaveStream>();
                    volumeStreams = new List<WaveChannel32>();
                    lengths = new List<long>();

                    mixeador = new List<WaveMixerStream32>();

                }
            


        public void addSound(string sampleName)
        {
            

            try
            {
               Play(sampleName);

                /*
                 * 
                CreateInputStream(sampleName);
                direccion.Add(sampleName, mainStreams.Count-1);
                lengths.Add(mainStreams[mainStreams.Count - 1].Length);

                mainStreams[mainStreams.Count - 1].Position = lengths[lengths.Count - 1];

                output.Add(new DirectSoundOut());
                mixeador.Add(new WaveMixerStream32());
                mixeador[mixeador.Count - 1].AutoStop = true;

                mixeador[mixeador.Count - 1].AddInputStream(mainStreams[mainStreams.Count - 1]);
                output[output.Count - 1].Init(mixeador[mixeador.Count - 1]);

                

                output[output.Count - 1].PlaybackStopped += new EventHandler(MainPlayer_PlaybackStopped);
                */
            }
            catch (Exception e)
            {
                if (error != null)
                {
                    error(e.Message + " error en la creación del wav");

                }
                            }
                    }

        void MainPlayer_PlaybackStopped(object sender, EventArgs e)
        {
            try
            {

                error("output parado");

            }
            catch (Exception err)
            {

            }

            IWavePlayer axit = (IWavePlayer)sender;
            axit.Dispose();
                        
            
        }

        private WaveStream CreateInputStreamReturnable (string fileName)
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


        private void CreateInputStream(string fileName)
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
            volumeStreams.Add(inputStream);
            mainStreams.Add(volumeStreams[volumeStreams.Count - 1]);

        }

        public void Play(string soundName)
        {
            try
            {
                if (aguante >= 30)
                {
                    return;
                }

                //aguante++;
                //if (output[direccion[soundName]].PlaybackState == PlaybackState.Playing)   
               // {
                if (soundName.EndsWith(".wav") == true)
                {
                    //playWithMedia(soundName);
                    //return;

                }
              ISound sfx = controlador.Play2D(soundName);
              sfx.Dispose();

                return;

                    
                    WaveStream auxoxo = CreateInputStreamReturnable(soundName);
                    WaveMixerStream32 extrr = new WaveMixerStream32();
                    extrr.AutoStop = true;
                    extrr.AddInputStream(auxoxo);

                    auxoxo.Position = 0;


                    DirectSoundOut auxili = new DirectSoundOut();
                
                    
                                        diccionarioLoco.Add(auxili, auxoxo);
                    diccionarioReLocoMixer.Add(auxili, extrr);

                    auxili.PlaybackStopped += new EventHandler(auxili_PlaybackStopped);

                
                    auxili.Init(extrr);
                    auxili.Play();

                   
                /*auxili.Dispose();
                    auxili.Stop();
  

                    //error("dentro del if");

                //}
                else
                {
                    mainStreams[direccion[soundName]].Position = 0;
                    mixeador[direccion[soundName]].AddInputStream(mainStreams[direccion[soundName]]);

                    output[direccion[soundName]].Init(mixeador[direccion[soundName]]);

                    output[direccion[soundName]].Play();
 

                    output[direccion[soundName]].Init(mainStreams[direccion[soundName]]);
                    output[direccion[soundName]].Play();
                    
                    output[direccion[soundName]].Dispose();
                    

                    error("no estoy en el if ");

                }
                
                output[direccion[soundName]].PlaybackStopped += new EventHandler(MainPlayer_PlaybackStopped);
    
            */
            }
            catch (Exception e)
            {
                if (error != null)
                {
                    error(e.Message + " error en play " + direccion[soundName]);

                }
                            }

        }

        void auxili_PlaybackStopped(object sender, EventArgs e)
        {
           
            IWavePlayer auxxx = (IWavePlayer)sender;
            //error("auxxx detenido");

            aguante--;


            try
            {
               
diccionarioReLocoMixer[auxxx].RemoveInputStream(diccionarioLoco[auxxx]);
diccionarioReLocoMixer[auxxx].Close();
diccionarioLoco.Remove(auxxx);
diccionarioReLocoMixer.Remove(auxxx);

auxxx.Dispose();

auxxx = null;

            }
            catch (Exception err)
            {
                if (error != null)
                {

                    error(err.Message);
                }


            }

            
        }

        

        public void Play(int soundCount)
        {
            try
            {
                mainStreams[soundCount].Position = 0;

                output[soundCount].Init(mainStreams[soundCount]);
                output[soundCount].Play();


            }
            catch (Exception e)
            {
                if (error != null)
                {
                    error(e.Message + " error en play " + soundCount);

                }
            }

        }

        private void playWithMedia(string obj)
        {
            try
            {
                System.Media.SoundPlayer songr = new System.Media.SoundPlayer(obj);
                songr.Play();
                songr.Disposed += new EventHandler(songr_Disposed);
                    
            }
            catch (Exception erio)
            {
                
            }
                    }

        void songr_Disposed(object sender, EventArgs e)
        {
            sender = null;

        }





            }
}
