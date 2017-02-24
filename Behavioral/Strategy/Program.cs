using System;

namespace Strategy
{

    #region Participants
    //Define a family of algorithms, encapsulate each one, and make them interchangeable.
    //Strategy
    //ConcreteStrategy 
    //Context
    #endregion  

    public interface IShippingCost
    {
        decimal Calculate();
    }

    public class UpsShippingCost : IShippingCost
    {
        public decimal Calculate()
        {
            return 3.1m;
        }
    }

    public class FedexShippingCost : IShippingCost
    {
        public decimal Calculate()
        {
            return 5.1m;
        }
    }

    public class ShippingCostContext : IShippingCost
    {
        private readonly IShippingCost _shippingCost;
        public ShippingCostContext(IShippingCost shippingCost)
        {
            _shippingCost = shippingCost;
        }

        public decimal Calculate()
        {
            return _shippingCost.Calculate();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            var shippingCost = new ShippingCostContext(new FedexShippingCost());
            Console.WriteLine(shippingCost.Calculate());

            shippingCost = new ShippingCostContext(new UpsShippingCost());
            Console.WriteLine(shippingCost.Calculate());

            Console.Read();
        }
    }
}
