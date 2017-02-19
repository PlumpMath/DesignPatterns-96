using System;

namespace Decorator
{

    public interface ICrmService
    {
        void Save(int id);
    }

    public class CrmService : ICrmService
    {
        public void Save(int id)
        {
            Console.WriteLine("Crm services working");
        }
    }
}
