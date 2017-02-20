using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    //
    public interface ILogService
    {
        void Save();
    }

    public class LogDbService : ILogService
    {
        public void Save()
        {
            Console.WriteLine("Save Db");
        }
    }

    public class LogTextService : ILogService
    {
        public void Save()
        {
            Console.WriteLine("Save Text");
        }
    }

    public class LogNullService : ILogService
    {
        public void Save()
        {
            
        }
    }


    public class MachineFactory<T> where T :class
    {
        Dictionary<string, Type> factories;

        public MachineFactory()
        {
            LoadTypesICanReturn();
        }

        public T CreateInstance(string description)
        {
            Type type = GetTypeToCreate(description);

            if (type == null)
                return new LogNullService() as T;

            return Activator.CreateInstance(type) as T;
        }

        private Type GetTypeToCreate(string machineName)
        {
            foreach (var machine in factories)
            {
                if (machine.Key.Contains(machineName))
                {
                    return factories[machine.Key];
                }
            }

            return null;
        }

        private void LoadTypesICanReturn()
        {
            factories = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(T).ToString()) != null)
                {
                    factories.Add(type.Name.ToLower(), type);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            MachineFactory<ILogService> logservice = new MachineFactory<ILogService>();
            var logDbService = logservice.CreateInstance("logdbservice");
            logDbService.Save();

            Console.Read();
        }
    }
}
