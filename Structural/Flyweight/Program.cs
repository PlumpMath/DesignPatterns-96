using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{

    #region Participants
    //Flyweight : declares an interface through which flyweights can receive and act on extrinsic state.
    //FlyweightFactory : creates and manages flyweight objects 
    #endregion

    /// <summary>
    /// Flyweight
    /// </summary>
    public interface IProductService
    {
        string GetName();
    }

    public class NewProductService : IProductService
    {
        public string GetName()
        {
            return "new product";
        }
    }


    public class BestSallersProductService : IProductService
    {
        public string GetName()
        {
            return "Best Sallers";
        }
    }


    /// <summary>
    /// FlyweightFactory
    /// </summary>

    public enum ProductType
    {
        New,
        BestSaller
    }

    public class ProductFactory
    {
        static Dictionary<ProductType, IProductService> _product = new Dictionary<ProductType, IProductService>();

        public IProductService GetProduct(ProductType productType)
        {
            if (_product.ContainsKey(productType))
                return _product[productType];

            switch (productType)
            {
                case ProductType.New:
                    _product[productType] = new NewProductService();
                    break;
                case ProductType.BestSaller:
                    _product[productType] = new BestSallersProductService();
                    break;
                default:
                    break;
            }


            return _product[productType];
        }

    }

    class Program
    {
        static void Main(string[] args)
        {


            ProductFactory factory = new ProductFactory();
            var bestSeller = factory.GetProduct(ProductType.BestSaller);
            bestSeller.GetName();
            var bestSeller1 = factory.GetProduct(ProductType.BestSaller);
            bestSeller.GetName();

            Console.Read();

        }
    }
}
