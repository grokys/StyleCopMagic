class Test
{
    public void PublicMethod()
    {
    }

    private void PrivateMethod()
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