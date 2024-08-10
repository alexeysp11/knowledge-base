using System.Collections;

namespace KnowledgeBase.Examples.PeopleEnumerator;

public class PeopleEnum : IEnumerator
{
    private Person[] m_people;
    private int m_position = -1;

    public PeopleEnum(Person[] peopleList)
    {
        m_people = peopleList;
    }

    public bool MoveNext()
    {
        m_position++;
        return (m_position < m_people.Length);
    }

    public void Reset()
    {
        m_position = -1;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public Person Current
    {
        get
        {
            try
            {
                return m_people[m_position];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new System.InvalidOperationException();
            }
        }
    }
}