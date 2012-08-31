public class AnonymousObjectInitializer
{
    public int Property1 { get; set; }
    public int Property2 { get; set; }

    public static AnonymousObjectInitializer Foo()
    {
        // Make sure 'this.' prefixes aren't added here:
        var foo = new
        {
            Property1 = 1,
            Property1 = 2,
        };
    }
}
