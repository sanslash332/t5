using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using tarea_5_core.NetPlay;

namespace tarea_5_core
{
  public class LogWriter
    {
        private StreamWriter escritor;

        public LogWriter()
        {
            FileStream fs = new FileStream("tarea_6.log", FileMode.OpenOrCreate);
                        escritor = new StreamWriter(fs, Encoding.Unicode);

                        escritor.WriteLine("program started at " + DateTime.Now.ToString());

                        BaseElement._sendError += new Action<string>(BaseElement__sendError);
                        GameCore._sendError += new Action<string>(GameCore__sendError);
                        NetManajer.sendError += new Action<string>(NetManajer_sendError);
        }

        public void escribir(string obj)
        {
            if (escritor != null)
            {
                if (obj.Contains("Unable to cast ") == false)
                {
                    
                    escritor.WriteLine(obj);
                }

            }
                    }

        void NetManajer_sendError(string obj)
        {
            if (escritor != null)
            {
                escritor.WriteLine(obj + "\n");

            }

        }

        public void terminate()
        {
            escritor.Flush();
            escritor.Dispose();
            escritor.Close();
            escritor = null;

        }

        void GameCore__sendError(string obj)
        {
            if (escritor != null)
            {
                if (obj.Contains("Unable to cast ") == false)
                {

                    escritor.WriteLine(obj + " \n");
                }
                            }
                    }

        void BaseElement__sendError(string obj)
        {
            if (escritor != null)
            {
                if (obj.Contains("Unable to cast") == false)
                {

                    escritor.WriteLine(obj + " \n ");
                }


            }
                    }


    }
}
