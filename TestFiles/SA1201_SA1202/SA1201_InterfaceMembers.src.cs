interface ITest
{
    void Method();
    int Property { get; set; }
    event EventHandler Event;
    int this[int x] { get; }
}