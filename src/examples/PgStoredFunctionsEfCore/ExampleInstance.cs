using Concepts.Examples.PgStoredFunctionsEfCore.DataProcessing;

namespace Concepts.Examples.PgStoredFunctionsEfCore
{
    /// <summary>
    /// The class that is responsible for initializing this example.
    /// </summary>
    public class ExampleInstance : IExampleInstance
    {
        private DbDataProcessing m_dbDataProcessing { get; set; }

        /// <summary>
        /// Construstor by default.
        /// </summary>
        public ExampleInstance(
            DbDataProcessing dbDataProcessing)
        {
            m_dbDataProcessing = dbDataProcessing;
        }

        /// <summary>
        /// Responsible for launching the application.
        /// </summary>
        public void Run()
        {
            // m_dbDataProcessing.CallProcessData(1);

            var result = m_dbDataProcessing.CallProcessDataAndReturnResult(1, 1);
            System.Console.WriteLine($"result: {result}");
        }
    }
}