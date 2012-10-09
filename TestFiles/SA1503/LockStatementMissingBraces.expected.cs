class TestClass
{
    void TestMethod()
    {
        lock (Bar())
        {
            Foo();
        }
    }
}