using System.Collections;

namespace RawDeal;

public class PlaysList : IEnumerable<Play>
{
    private List<Play> plays;

    public PlaysList()
    {
        plays = new List<Play>();
    }

    public void Add(Play play)
    {
        plays.Add(play);
    }

    public IEnumerator<Play> GetEnumerator()
    {
        return plays.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public Play this[int index]
    {
        get
        {
            if (index >= 0 && index < plays.Count)
                return plays[index];
            throw new IndexOutOfRangeException("Index is out of range.");
        }
        set
        {
            if (index >= 0 && index < plays.Count)
                plays[index] = value;
            else
                throw new IndexOutOfRangeException("Index is out of range.");
        }
    }
}
