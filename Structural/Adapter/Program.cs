using System;

namespace Adapter
{
    #region Participants
    //Main idea , you have B class but it has incompatible interface with your A class. You must to adapt your B class to A class interface.
    //Target : defines the domain-specific interface that Client uses.
    //Adaptor : adapts the interface Adaptee to the Target interface.
    //Adaptee : defines an existing interface that needs adapting.
    //Client : collaborates with objects conforming to the Target interface
    #endregion

        
    /// <summary>
    /// Target
    /// </summary>
    public interface ILogService
    {
        void Log(string message);
    }

    /// <summary>
    /// implementation
    /// </summary>
    public class TextLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine("Text log : {0}" , message);
        }
    }

    public class DbLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine("Db log : {0}", message);
        }
    }

    /// <summary>
    /// Adapter
    /// </summary>
    public class ExternalLogService : ILogService
    {
        IExternalLog _externalLogService;

        public ExternalLogService()
        {
            _externalLogService = new ExternalLog();
        }

        public ExternalLogService(IExternalLog externalLogService)
        {
            _externalLogService = externalLogService;
        }
        public void Log(string message)
        {
            _externalLogService.LogSave(message);

        }
    }


    /// <summary>
    /// Client
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ILogService logService = new DbLogService();
            logService.Log("Test 1");
            logService = new ExternalLogService();
            logService.Log("Test 2");

            Console.Read();


        }
    }
}
