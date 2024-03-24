using System;

namespace Concepts.Models
{
    /// <summary>
    /// Represents information about a programming concept 
    /// </summary>
    public class Concept
    {
        /// <summary>
        /// Name of a concept
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Family of a concept (SOLID, GRASP, Design pattern etc)
        /// </summary>
        public string Family { get; set; }
        
        /// <summary>
        /// Explanation of a concept 
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Status of execution 
        /// </summary>
        public bool IsExecuted { get; set; }

        /// <summary>
        /// Result of execution 
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Additional information about an execution 
        /// </summary>
        public string ExecSummary { get; set; }
    }
}
