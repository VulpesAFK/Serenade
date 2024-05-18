using UnityEngine;
using UnityEngine.Events;

public class ExampleTimer : MonoBehaviour
{
    // Random set duration
    [SerializeField] private float duration = 5f;
    // Random set event when done
    [SerializeField] private UnityEvent onTimerDone = null;

    // Variable to store the timer 
    private Timer timer;


    // Set all variables to be instantiated and assigned
    void Start()
    {
        // Instantiate a new timer on to the variable
        timer = new Timer(duration);

        // Set the event to be some event
        timer.OnTimerDone += HandleTimeDone;
    }

    // Function called when the time is done
    private void HandleTimeDone()
    {
        // Invoke any events tied to this
        onTimerDone.Invoke();

        // TEST
        print("Done");

        // Rewind the time to repeat itself
        timer.TimerRewind();
    }

    void Update()
    {
        // Pass the time in to the tick
        timer.Tick(Time.deltaTime);
    }
}
