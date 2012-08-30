public class EventCall
{
    public event EventHandler Event;

    public EventCall()
    {
        Event(null, null);
    }
}
