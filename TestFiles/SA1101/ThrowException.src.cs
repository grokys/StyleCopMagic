public class ThrowException
{
    public ThrowException()
    {
        var x = new TestException();
    }
}

public class TestException : Exception
{
    public static TestException()
    {
    }
}