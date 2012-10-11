interface ITest
{
    event EventHandler Event;
    int Property { get; set; }
    int this[int x] { get; }
    void Method();
}