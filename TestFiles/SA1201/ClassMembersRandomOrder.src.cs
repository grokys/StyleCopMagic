class Test
{
    // Method...
    void Method()
    {
    }

    Test()
    {
    }

    int Property { get; set; }

    class NestedClass
    {
        int foo;
    }

    event EventHandler Event;

    delegate void Delegate();

    int field;

    /// <summary>
    /// A nested struct with comment.
    /// </summary>
    struct NestedStruct
    {
        int foo;
    }

    ~Test()
    {
    }

    int this[int x]
    {
        get { return 1; }
    }

    enum Enum
    {
        Value1
    }
}