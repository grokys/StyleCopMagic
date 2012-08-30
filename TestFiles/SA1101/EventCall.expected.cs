public class EventCall
{
    public event EventHandler Event;

    public EventCall()
    {
        this.Event(null, null);
    }
}
