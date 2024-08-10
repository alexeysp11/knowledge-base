namespace KnowledgeBase.Examples.PeopleEnumerator;

public class Program
{
    public static void Main()
    {
        Person[] peopleArray = new Person[3]
        {
            new Person("John", "Smith"),
            new Person("Jim", "Johnson"),
            new Person("Sue", "Rabon"),
        };

        People peopleList = new People(peopleArray);
        foreach (Person person in peopleList)
        {
            Console.WriteLine(person.FirstName + " " + person.LastName);
        }
    }
}