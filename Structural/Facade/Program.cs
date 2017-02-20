using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    #region Participants
    //intent in short, you put a facade in front of the complex code.
    //Facade :knows which subsystem classes are responsible for a request. delegates client requests to appropriate subsystem objects.
    //Subsystem classes  
    #endregion  

    /// <summary>
    /// Subsystem Class
    /// </summary>
    
    public class CrmUserService
    {
        public string GetSurname()
        {
            //connect crm , get user data
            return "CrmUser Name";
        }
    } 

    public class ErpUserService
    {
        public string GetEmail()
        {
            return "ErpUser Name";
        }
    }

    /// <summary>
    /// Facade
    /// </summary>
    public interface IUserService
    {
        string GetName();
    }

    public class UserService : IUserService
    {
        private readonly CrmUserService _crmUserService;
        private readonly ErpUserService _erpUserService;

        public UserService() 
            : this(new CrmUserService(), new ErpUserService())
        {

        }

        public UserService(
            CrmUserService crmUserService,
            ErpUserService erpUserService)
        {
            _crmUserService = crmUserService;
            _erpUserService = erpUserService;

        }

        public string GetName()
        {

            _crmUserService.GetSurname();
            _erpUserService.GetEmail();

           return "User Name";
           
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            IUserService _userService = new UserService();
            _userService.GetName();
        }
    }
}
