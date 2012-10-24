class Test
{
    private void PrivateMethod()
    {
    }

    public void PublicMethod()
    {
    }

#if !THESE_SHOULD_NOT_BE_MOVED
    protected void ProtectedMethod()
    {
    }

    internal void InternalMethod()
    {
    }

    protected internal void ProtectedInternalMethod()
    {
    }

    private int field;
#endif
}