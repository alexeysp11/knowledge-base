# How to use random UID in the database 

The problem is that when you are inserting random UID into the database, balanced tree has to be recalculated each time.
The possible decisions:
- [ULID](https://github.com/oklog/ulid);
- Use caching. This approach could be implemented both on the DB level (using separate temporary table from which data will be inserted into the main table) and on code level (multithreading and thread safety is needed).

Using ULID or caching can help solve the problem of recalculating the balanced tree each time a random UID is inserted into the database. ULID (Universally Unique Lexicographically Sortable Identifier) is a type of identifier that is designed to be monotonically increasing and sortable, making it suitable for use in balanced trees. Using ULID can eliminate the need to recalculate the tree each time a new UID is added.

On the other hand, caching can also be used to improve performance when inserting UIDs into the database. This can be implemented at both the database level and the code level. On the database level, a separate temporary table can be used to store the UIDs before inserting them into the main table, reducing the need for recalculating the balanced tree. On the code level, multithreading and thread safety can be implemented to handle the insertion of UIDs in a more efficient manner, reducing the impact on the balanced tree.
