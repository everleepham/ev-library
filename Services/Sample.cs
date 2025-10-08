namespace Library.Services;

public class Sample
{
    public static int NewNumber = 40;

    public void AddNumber(int Number)
    {
        NewNumber += Number;
    }

    public int GetNumber()
    {
        return NewNumber;
    }
    
}