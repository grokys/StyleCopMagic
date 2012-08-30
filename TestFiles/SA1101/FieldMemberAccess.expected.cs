public class FieldWrite
{
    class Inner
    {
        public int field1;
    }

    Inner inner;

    public FieldWrite()
    {
        this.inner.field1 = 1;
    }
}
