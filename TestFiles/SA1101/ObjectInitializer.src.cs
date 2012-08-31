public class ObjectInitializer
{
    public int Property1 { get; set; }
    public int Property2 { get; set; }

    public static ObjectInitializer Foo()
    {
        // Make sure 'this.' prefixes aren't added here:
        return new ObjectInitializer
        {
            Property1 = 1,
            Property1 = 2,
        };
    }
}
