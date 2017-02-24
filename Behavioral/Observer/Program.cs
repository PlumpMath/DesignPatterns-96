using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{

    #region Event, Delegates

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class PersonChangeEventArg : EventArgs
    {
        public Person Person { get; set; }

        public PersonChangeEventArg(Person person)
        {
            Person = person;
        }
    }

    public class PersonMonitor
    {
        private Person _person;
        public event EventHandler<PersonChangeEventArg> personChange;

        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                this.OnPersonChange(new PersonChangeEventArg(_person));
            }
        }

        protected virtual void OnPersonChange(PersonChangeEventArg e)
        {
            personChange?.Invoke(this, e);
        }
    }

    public class AdultObserver
    {
        public AdultObserver(PersonMonitor monitor)
        {
            monitor.personChange += Monitor_personChange;
        }

        private void Monitor_personChange(object sender, PersonChangeEventArg e)
        {
            CheckFilter(e.Person);
        }

        private void CheckFilter(Person person)
        {
            if (person.Age >= 18)
            {
                Console.WriteLine("{0} is Adult", person.Name);
            }
        }
    }

    public class TeenObserver
    {
        public TeenObserver(PersonMonitor monitor)
        {
            monitor.personChange += Monitor_personChange;
        }

        private void Monitor_personChange(object sender, PersonChangeEventArg e)
        {
            CheckFilter(e.Person);
        }

        private void CheckFilter(Person person)
        {
            if (person.Age < 18)
            {
                Console.WriteLine("{0} is Teen", person.Name);
            }
        }
    }

    #endregion

    #region Base Solution
    public class AdultObserverBase : IObserver<Person>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Err: {0}", error.Message);
        }

        public void OnNext(Person person)
        {
            if (person.Age >= 18)
            {
                Console.WriteLine("{0} is Adult", person.Name);

            }
        }
    }


    public class TeenObserverBase : IObserver<Person>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Err: {0}", error.Message);
        }

        public void OnNext(Person person)
        {
            if (person.Age < 18)
            {
                Console.WriteLine("{0} is Teen", person.Name);

            }
        }
    }

    public class ObservableCommodity : IObservable<Person>
    {
        private List<IObserver<Person>> _observers = new List<IObserver<Person>>();
        private Person _person;
        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                this.Notify(_person);
            }
        }

        public IDisposable Subscribe(IObserver<Person> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return null;
        }

        private void Notify(Person person)
        {
            foreach (IObserver<Person> observer in _observers)
            {

                observer.OnNext(person);
            }
        }

    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person
            {
                Name="Fatih",
                Age = 25
            });
            personList.Add(new Person
            {
                Name = "Ali",
                Age = 15
            });
            personList.Add(new Person
            {
                Name = "Veli",
                Age = 18
            });

            PersonMonitor personMonitor = new PersonMonitor();
            new AdultObserver(personMonitor);
            new TeenObserver(personMonitor);

            foreach (var person in personList)
            {
                personMonitor.Person = person;
            }

            Console.WriteLine("-------------");


            ObservableCommodity _observer = new ObservableCommodity();
            _observer.Subscribe(new AdultObserverBase());
            _observer.Subscribe(new TeenObserverBase());

            foreach (var person in personList)
            {
                _observer.Person = person;
            }

            Console.Read();

        }
    }
}
