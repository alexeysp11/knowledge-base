using System;
using System.Collections.Generic;

namespace Studying.Leetcode.StateMachines
{
    public class UsingBusinessProcessStateMachine : Studying.Leetcode.ILeetcodeProblem
    {
        public class BusinessProcessStateMachine
        {
            private Dictionary<string, List<string>> transitions;

            private string currentState;

            public BusinessProcessStateMachine()
            {
                transitions = new Dictionary<string, List<string>>();
                currentState = "Initial";
            }

            public void AddTransition(string fromState, string toState)
            {
                if (!transitions.ContainsKey(fromState))
                {
                    transitions[fromState] = new List<string>();
                }
                transitions[fromState].Add(toState);
            }

            public void ChangeState(string toState)
            {
                if (transitions.ContainsKey(currentState) && transitions[currentState].Contains(toState))
                {
                    currentState = toState;
                    Console.WriteLine($"Transitioned from {currentState} to {toState}");
                }
                else
                {
                    Console.WriteLine($"Invalid transition from {currentState} to {toState}");
                }
            }

            public string GetCurrentState()
            {
                return currentState;
            }
        }

        public void Execute()
        {
            BusinessProcessStateMachine machine = new BusinessProcessStateMachine();
            
            machine.AddTransition("Initial", "Processing");
            machine.AddTransition("Processing", "Completed");
            
            Console.WriteLine("Current state: " + machine.GetCurrentState());
            
            machine.ChangeState("Processing");
            machine.ChangeState("Completed");
            machine.ChangeState("Initial"); // Invalid transition
            
            Console.WriteLine("Final state: " + machine.GetCurrentState());
        }
    }
}
