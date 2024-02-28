using System.Collections.Generic;
using System;

namespace FoxTail.Serenade.Experimental.FiniteStateMachine.Construct
{
    public class PlayerStateMachine {

        public PlayerState CurrentState { get; private set; }
        private Dictionary<Type, List<Transition>> dictionaryTransitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private static List<Transition> emptyTransition = new List<Transition>();

        /*
            * View all transitions to the current A
            * If a condition is comitted then change
        */
        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null) ChangeState(transition.To);

            CurrentState.LogicUpdate();
        }

        /*
            * Commits to check whether there is an A to A transition in which null

            * End the current and switch
            * Update the current transitions list to be update to the new A
            * Enter the new state
        */
        public void ChangeState(PlayerState newState) {
            if (CurrentState == newState) return;

            CurrentState?.Exit();
            CurrentState = newState;

            dictionaryTransitions.TryGetValue(CurrentState.GetType(), out currentTransitions);
            if (currentTransitions == null) currentTransitions = emptyTransition;

            CurrentState?.Enter();

        }

        /*
            * Creates the general transition from A to B with added condition
            * If the starting A key is null then create
            * Add to the list of B destination with allocated A
        */
        public void AddTransition(PlayerState from, PlayerState to, Func<bool> condition) {
            /*
                * Checks if the from transition state exist with an attempt to fetch the transition list from it
                * Failure will create a new transition with the from type as the main key
            */
            if (dictionaryTransitions.TryGetValue(from.GetType(), out var transitions) == false) {
                /*
                    * Instantiate a new list of transitions
                    * New key under with all transitions
                */
                transitions = new List<Transition>();
                dictionaryTransitions[from.GetType()] = transitions;
            }
            // * Add the transition using the attempted transition list fetch
            transitions.Add(new Transition(to, condition));
        }

        /*
            * Class object to create the link from A to B with the condition
            * Set arguments to the variables when passed
        */
        private class Transition {
            public Func<bool> Condition { get; }
            public PlayerState To { get; }

            public Transition (PlayerState to, Func<bool> condition) {
                To = to;
                Condition = condition;
            }
        }

        /*
            * Loops through all the transitions on the current from transitions
            * If any conditons from A to B are true then return 
        */
        private Transition GetTransition() {
            foreach (var transition in currentTransitions) {
                if (transition.Condition()) return transition;
            }
            return null;
        }
    }
}
