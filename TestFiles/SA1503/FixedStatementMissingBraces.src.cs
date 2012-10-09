class TestClass
{
    void TestMethod()
    {
        fixed (int* p = Bar()) 
            Foo();
    }
}