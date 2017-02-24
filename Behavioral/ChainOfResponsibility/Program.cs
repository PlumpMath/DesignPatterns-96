using System;

namespace ChainOfResponsibility
{

    #region Participants
    //Handler   
    //ConcreteHandler
    //Client
    #endregion

    public abstract class ProductHandler
    {
        protected ProductHandler nextProduct;
        protected bool _continue = true;

        public void SetNextProduct(ProductHandler nextProduct)
        {
            this.nextProduct = nextProduct;
        }

        public void Execute()
        {
            ExecuteProcess();
            if (this.nextProduct != null && _continue)
            {
                this.nextProduct.Execute();
            }
        }

        public abstract void ExecuteProcess();
    }


    public class ErpProduct : ProductHandler
    {
        public override void ExecuteProcess()
        {
            //isexist erp product
            if (true)
            {
                Console.WriteLine("Erp isexit");
                _continue = true;
            }
        }
    }

    public class CrmProduct : ProductHandler
    {
        public override void ExecuteProcess()
        {
            //exist crm product
            if (true)
            {
                Console.WriteLine("Crm isexit");
                _continue = true;
            }
          
        }
    }

    public class SapProduct : ProductHandler
    {
        public override void ExecuteProcess()
        {
            //exist sap product
            if (true)
            {
                Console.WriteLine("Sap isexit");
                _continue = true;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            ProductHandler crmProduct = new CrmProduct();
            ProductHandler erpProduct = new ErpProduct();
            ProductHandler sapProduct = new SapProduct();

            crmProduct.SetNextProduct(erpProduct);
            erpProduct.SetNextProduct(sapProduct);

            crmProduct.Execute();

            Console.Read();
        }
    }
}
