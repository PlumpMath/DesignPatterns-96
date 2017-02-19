using System;
using System.Collections.Generic;

namespace Bridge
{
    #region Participants
    //if a class implements an interface or derives from an abstract superclass then the abstraction and the implementation are tightly coupled . If you change the structure of the interface, such as add a new method to it, then all implementations will need to change as well. The bridge pattern aims to decouple them so that we get rid of this coupling.

    //Abstraction : defines the abstraction's interface. Maintains a reference to an object of type Implementor.
    //RefinedAbstraction : extends the interface defined by Abstraction.
    //Implementor : defines the interface for implementation classes.This interface doesn't have to correspond exactly to Abstraction's interface; in fact the two interfaces can be quite different.Typically the Implementation interface provides only primitive operations, and Abstraction defines higher-level operations based on these primitives.
    //ConcreteImplementor : implements the Implementor interface and defines its concrete implementation.
    #endregion



    /// <summary>
    /// Abstraction
    /// </summary>
    public interface IFormatter
    {
        string Format(string value);
    }


    /// <summary>
    /// RefinedAbstraction
    /// </summary>
    public class HtmlFormatter : IFormatter
    {
        public string Format(string value)
        {
            return string.Format("<b>{0}</b>", value);
        }
    }

    public class XmlFormatter : IFormatter
    {
        public string Format(string value)
        {
            return string.Format("<name>{0}</name>", value);
        }
    }
    /// <summary>
    /// Implementor
    /// </summary>
    public interface IReport
    {
        string Get(IFormatter formatter);
    }


    /// <summary>
    /// ConcreteImplementor
    /// </summary>
    public class Report : IReport
    {
        public string Name { get; set; }

        public string Get(IFormatter formatter)
        {
            return formatter.Format(Name);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            IFormatter htmlFormatter = new HtmlFormatter();
            IFormatter xmlFormatter = new XmlFormatter();
            List<Report> reports = new List<Report>();
            reports.Add(new Report
            {
                Name = "Fatih"
            });

            reports.Add(new Report
            {
                Name = "Fiti"
            });

            foreach (var item in reports)
            {
                var result = item.Get(xmlFormatter);
                Console.WriteLine(result);
            }

            Console.Read();
        }
    }
}
