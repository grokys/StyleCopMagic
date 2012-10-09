class TestClass
{
    void TestMethod()
    {
        using (var baz = Bar())
        {
            Foo();
        }
    }
}