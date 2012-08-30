public class FieldWrite
{
    class Inner
    {
        public int field1;
    }

    Inner inner;

    public FieldWrite()
    {
        inner.field1 = 1;
    }
}
