class Test
{

    int field;

    Test()
    {
    }

    ~Test()
    {
    }

    delegate void Delegate();

    event EventHandler Event;

    enum Enum
    {
        Value1
    }

    int Property { get; set; }

    int this[int x]
    {
        get { return 1; }
    }
    // Method...
    void Method()
    {
    }

    /// <summary>
    /// A nested struct with comment.
    /// </summary>
    struct NestedStruct
    {
        int foo;
    }

    class NestedClass
    {
        int foo;
    }
}