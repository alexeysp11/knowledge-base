using System.Collections.Generic; 
using System.Linq; 
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Studying.Leetcode.Nosql
{
    /// <summary>
    /// Description: https://leetcode.com/problems/replace-employee-id-with-the-unique-identifier/
    /// </summary>
    public class EmployeeUniqueId : Studying.Leetcode.ILeetcodeProblem 
    {
        #region Private properties
        private string ConnectionString { get; } = "mongodb://127.0.0.1:27017"; 
        private string DbName { get; } = "employee_unique_id"; 
        private string EmployeeCollection { get; } = "employees"; 
        private string UniCollection { get; } = "employee_uni"; 
        #endregion  // Private properties

        #region Models
        private class Employee
        {
            public ObjectId Id { get; set; }

            [BsonElement("employee_id")]
            public int EmployeeId { get; set; }

            [BsonElement("name")]
            public string Name { get; set; }
        }

        private class EmployeeUni
        {
            public ObjectId Id { get; set; }

            [BsonElement("employee_id")]
            public int EmployeeId { get; set; }

            [BsonElement("unique_id")]
            public int UniqueId { get; set; }
        }

        private class Output
        {
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public int? UniqueId { get; set; }
        }
        #endregion  // Models

        #region Methods
        public void Execute()
        {
            System.Console.WriteLine("Replace employee id with the unique identifier\n".ToUpper()); 
            
            // InitDatabase(); 
            List<Output> list = GetUniqueIds(); 
            foreach (var emp in list) System.Console.WriteLine($"EmployeeName: {emp.EmployeeName}, UniqueId: {emp.UniqueId}"); 
        }

        private void InitDatabase()
        {
            var client = new MongoClient(ConnectionString);
            var employeeCollection = client.GetDatabase(DbName).GetCollection<Employee>(EmployeeCollection);
            var uniCollection = client.GetDatabase(DbName).GetCollection<EmployeeUni>(UniCollection);
            employeeCollection.InsertMany(new List<Employee>
            {
                new Employee { EmployeeId = 1, Name = "Alice" },
                new Employee { EmployeeId = 7, Name = "Bob" },
                new Employee { EmployeeId = 11, Name = "Meir" },
                new Employee { EmployeeId = 90, Name = "Winston" },
                new Employee { EmployeeId = 3, Name = "Jonathan" }
            });
            uniCollection.InsertMany(new List<EmployeeUni>
            {
                new EmployeeUni { EmployeeId = 3, UniqueId = 1 },
                new EmployeeUni { EmployeeId = 11, UniqueId = 2 },
                new EmployeeUni { EmployeeId = 90, UniqueId = 3 }
            });
        }

        private List<Output> GetUniqueIds()
        {
            List<Output> result = new List<Output>(); 
            var client = new MongoClient(ConnectionString);
            var employeeCollection = client.GetDatabase(DbName).GetCollection<Employee>(EmployeeCollection);
            var employeeFilter = Builders<Employee>.Filter.Empty;
            var employees = employeeCollection.Find(employeeFilter).ToList();
            foreach (var emp in employees) 
            {
                var uniCollection = client.GetDatabase(DbName).GetCollection<EmployeeUni>(UniCollection);
                var unis = uniCollection.AsQueryable().Where(u => u.EmployeeId == emp.EmployeeId).ToList();
                if (unis.Count == 0)
                    result.Add(new Output() { EmployeeId = emp.EmployeeId, EmployeeName = emp.Name });
                else 
                    foreach (var uni in unis) result.Add(new Output() { EmployeeId = emp.EmployeeId, EmployeeName = emp.Name, UniqueId = uni.UniqueId }); 
            }
            return result; 
        }
        #endregion  // Methods
    }
}