using System.Collections.Generic; 
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Studying.Leetcode.Nosql
{
    public class BookstoreTest : Studying.Leetcode.ILeetcodeProblem 
    {
        private class Book
        {
            public ObjectId Id { get; set; }

            [BsonElement("title")]
            public string Title { get; set; }

            [BsonElement("author")]
            public string Author { get; set; }

            [BsonElement("pages")]
            public int Pages { get; set; }

            [BsonElement("genres")]
            public List<string> Genres { get; set; }

            [BsonElement("rating")]
            public int Rating { get; set; }
        }

        public void Execute()
        {
            System.Console.WriteLine("Bookstore (test)\n".ToUpper()); 

            string connectionString = "mongodb://127.0.0.1:27017"; 
            string dbName = "bookstore"; 
            string collectionName = "books"; 

            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase(dbName).GetCollection<Book>(collectionName);
            // var filter = Builders<Book>.Filter.Empty;
            var filter = Builders<Book>.Filter.Eq(b => b.Title, "Name of the Wind");
            var list = collection.Find(filter).ToList();
            foreach (var doc in list) System.Console.WriteLine($"Id: {doc.Id}, Title: {doc.Title}, Author: {doc.Author}, Pages: {doc.Pages}");

            // Insert new book
            // collection.InsertOne(new Book {Title = "Some new book", Author = "Anonymous", Pages = 325, Genres = new List<string>() {"comedy"}, Rating = 9}); 
            // collection.InsertOne(new Book {Title = "Some new book 2342", Author = "Anonymous", Pages = 325, Genres = new List<string>() {"comedy"}, Rating = 9}); 

            // Update existing book 
            // const string oldValue = "Some new book 2342";
            // const string newValue = "Book to delete";
            // filter = Builders<Book>.Filter.Eq(b => b.Title, oldValue);
            // var update = Builders<Book>.Update.Set(b => b.Title, newValue);
            // collection.UpdateOne(filter, update);

            // Delete a book 
            filter = Builders<Book>.Filter.Eq(b => b.Title, "Book to delete");
            collection.DeleteOne(filter); 
        }

        private void GetByFieldName()
        {
            string connectionString = "mongodb://127.0.0.1:27017"; 
            string dbName = "bookstore"; 
            string collectionName = "books"; 

            var client = new MongoClient(connectionString);
            var collection = client.GetDatabase(dbName).GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("title", "Name of the Wind");
            var document = collection.Find(filter).First();
            System.Console.WriteLine(document);
        }
    }
}