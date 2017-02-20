using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    #region Participants
    //Ensure a class has only one instance and provide a global point of access to it.
    //Singleton : defines an Instance operation that lets clients access its unique instance.Instance is a class operation. responsible for creating and maintaining its own unique instance. 
    #endregion 

    public class LogService
    {

        private static LogService _instance;

        protected LogService()
        {
            Console.WriteLine("LogService");
        }


        #region Singleton Method
        public static LogService Instance
        {
            get
            {
                // Note: this is not thread safe.
                if (_instance == null)
                {
                    _instance = new LogService();
                }

                return _instance;
            }


        }


        public static LogService InstanceTreadeSave
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly LogService instance = new LogService();
        }

        public void Save()
        {
            Console.WriteLine("Save");
        }
        #endregion
    }







    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                LogService _logService = LogService.Instance;
                LogService _logServiceThread = LogService.InstanceTreadeSave;
                _logService.Save();
                _logServiceThread.Save();

            }

            Console.Read();
        }
    }
}
