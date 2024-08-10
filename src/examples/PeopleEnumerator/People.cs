using System.Collections;

namespace KnowledgeBase.Examples.PeopleEnumerator;

public class People : IEnumerable
{
    private Person[] m_people;

    public People(Person[] personArray)
    {
        m_people = new Person[personArray.Length];

        for (int i = 0; i < personArray.Length; i++)
        {
            m_people[i] = personArray[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
       return (IEnumerator) GetEnumerator();
    }

    public PeopleEnum GetEnumerator()
    {
        return new PeopleEnum(m_people);
    }
}