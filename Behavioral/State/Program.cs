using System;

namespace State
{

    #region Participants
    // The State design pattern allows to change the behaviour of a method through the state of an object.
    //Context : defines the interface of interest to clie
    //State : defines an interface for encapsulating the behavior associated with a particular state of the Context
    //Concrete State : each subclass implements a behavior associated with a state of Context
    #endregion

    #region Entity
    public enum OrderStatus
    {
           New
         , Shipped
         , Cancelled
    }

    public class Order
    {

        public string Code { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


    }

    #endregion


    ///Context
    public interface IOrderState
    {
        Order State();
        bool CanDo();
       
    }

    /// <summary>
    /// Concrete State
    /// </summary>
    public class NewState : IOrderState
    {
        private Order _order;
        public NewState(Order order)
        {
            _order = order;
        }

        public bool CanDo()
        {
            return true;
        }

        public Order State()
        {

            Console.WriteLine("Order Created");
            Console.WriteLine("Sendend Created Mail");

            return _order;
        }
    }

    public class CancelledState : IOrderState
    {
        private Order _order;
        public CancelledState(Order order)
        {
            _order = order;
        }

        public bool CanDo()
        {
            if (_order.Status == OrderStatus.New)
                return true;

            return false;
        }

        public Order State()
        {
            
            Console.WriteLine("Cancelled State");

            return _order;
        }
    }

    public class ShippedState : IOrderState
    {
        private Order _order;
        public ShippedState(Order order)
        {
            _order = order;
        }

        public bool CanDo()
        {
            if (_order.Status == OrderStatus.New)
                return true;

            return false;
        }

        public Order State()
        {
            Console.WriteLine("Shipped State");

            return _order;
        }
    }
 

    public interface IOrderService
    {
        void CreateOrder(IOrderState orderState);
        Order GetOrder();
        void ChangeState(IOrderState orderState);
    
        
    }

    public class OrderServie : IOrderService
    {
     
        public void CreateOrder(IOrderState orderState)
        {
            var order = orderState.State();

            //Create order
            Console.WriteLine("Order created");
        }
        public Order GetOrder()
        {
            Order order = new Order();
            order.Code = "SP-123131";
            order.Status = OrderStatus.Shipped;

            return order;
        }

        public void ChangeState(IOrderState orderState)
        {
            if (!orderState.CanDo())
            {
                throw new NotImplementedException("Cannot");
            }

            var order = orderState.State();

            //Save order
            Console.WriteLine("Save order");
        }


    }

    class Program
    {
        static void Main(string[] args)
        {

            IOrderService _orderService = new OrderServie();
            var order = _orderService.GetOrder();

            _orderService.ChangeState(new CancelledState(order));

            Console.Read();
        }
    }
}
