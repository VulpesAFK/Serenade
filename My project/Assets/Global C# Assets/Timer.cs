using System;

public class Timer
{
    public float DurationRemaining { get; private set; }
    public event Action OnTimerDone;
    private float duration;

    // Constructor method to instantiate the the timer to the passed duration arguement 
    public Timer(float duration)
    {
        // Set the timer to a private variable
        this.duration = duration;  

        // Set the remaining duration to the max duration
        TimerRewind();
    }

    // Ticks on to the time to via a deltaTime arugement to tick down
    public void Tick(float deltaTime)
    {
        // Checks if the time is equal to zero in which we return null to not be below
        if (DurationRemaining == 0f) return;

        // Reduces the duration by the deltaTime 
        DurationRemaining -= deltaTime;

        // Call the function to check whether done
        CheckTimerDone();
    }

    // Checks the conditions to make sure the LN 19 fits the conditions 
    private void CheckTimerDone()
    {
        // If the remaining time is greater than the end then return null
        if (DurationRemaining > 0f) return;

        // Set zero if the above is cancelled 
        DurationRemaining = 0f;

        // Start the actions when the conditions are set
        OnTimerDone?.Invoke();
    }

    public void TimerRewind() => DurationRemaining = duration;
}

# region Usage Manual

/*
Variables:

    // Random set duration
    private float duration

    // Set of events when the timer is done
    private UnityEvenets onTimerDone = null

    // Stores the timer as a variable
    private Timer timer

Start:

    // Instantiate the timer with the variable to store

    // Set an event to the actions variable store in the timer to a function on the runner
    timer.OnTimerDone += <function-name>

<function-name>:

    // Invoke all actions on the UnityEvents
    onTimerDone.Invoke()

    // Rewind if necessary

Update:

    // Pass the ticks on via the function

*/

# endregion