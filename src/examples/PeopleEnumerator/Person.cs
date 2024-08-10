namespace KnowledgeBase.Examples.PeopleEnumerator;

public class Person
{
    public string FirstName { get; }
    public string LastName { get; }
    
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}