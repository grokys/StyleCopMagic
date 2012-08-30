public class UsingCrash
{
    public UsingCrash()
    {
        // Using the Roslyn June 2012 the following using statement causes a 
        // NullReferenceException in roslyn.
        using (MemoryStream s = new MemoryStream())
        {
        }
    }
}
