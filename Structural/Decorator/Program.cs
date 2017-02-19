using System;

namespace Decorator
{
    #region Participants
    //if you want to extend the functionality of objects, you can use this pattern.
    //Component : defines the interface for objects that can have responsibilities added to them dynamically.
    //ConcreteComponent : defines an object to which additional responsibilities can be attached.
    //Decorator : maintains a reference to a Component object and defines an interface that conforms to Component's interface.
    //ConcreteDecorator : adds responsibilities to the component.
    #endregion


    /// <summary>
    /// Component
    /// </summary>
    public interface ICustomerService
    {
        bool CreateUser(string name);
    }

    /// <summary>
    /// ConcreteComponent
    /// </summary>
    public class CustomerServiceDecorator : ICustomerService
    {
        protected readonly ICustomerService _customerService;

        public CustomerServiceDecorator(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public virtual bool CreateUser(string name)
        {
            return _customerService.CreateUser(name);
        }
    }


    /// <summary>
    /// Decorator
    /// </summary>
    public class CustomerService : ICustomerService
    {
        public bool CreateUser(string name)
        {
            Console.WriteLine("Create user process");
            return true;
        }
    }


    /// <summary>
    /// ConcreteDecorator
    /// </summary>
    public class CustomerWithCRMService : CustomerServiceDecorator
    {

        private readonly ICrmService _crmServices;
        public CustomerWithCRMService(
            ICustomerService customerService,
            ICrmService crmService) : base(customerService)
        {
            //crm servis call
            _crmServices = crmService;
        }

        public override bool CreateUser(string name)
        {
            Console.WriteLine("Crm log process");
            _crmServices.Save(1);
            return base.CreateUser(name);
        }


    }


    class Program
    {
        static void Main(string[] args)
        {

            //you can use ioc (castle windsor, autofac decorator patternt)
            ICustomerService _customerService = new CustomerService();
            ICustomerService _customerWithCrm = new CustomerWithCRMService(_customerService, new CrmService());
            _customerWithCrm.CreateUser("fiti fiti");

            Console.Read();

        }
    }
}
