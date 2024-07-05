# schemaobjects

Read this in other languages: [English](schemaobjects.md), [Russian/Русский](schemaobjects.ru.md). 

## Schema

A database **schema** is a collection of logical data structures, or schema objects. 
A database user owns a database schema, which has the same name as the user name.

Schema objects are user-created structures that directly refer to the data in the database. 
The database supports many types of schema objects, the most important of which are tables and indexes.

A schema object is one type of **database object**. 
Some database objects, such as profiles and roles, do not reside in schemas.

### Table 

A table describes an entity such as employees. 

A table is a **set of rows**. 
A column identifies an attribute of the entity described by the table, whereas a row identifies an instance of the entity. 
For example, attributes of the employees entity correspond to columns for employee ID and last name. 
A row identifies a specific employee.

You can optionally specify a rule, called an **integrity constraint**, for a column. 
One example is a `NOT NULL` integrity constraint. 
This constraint forces the column to contain a value in every row.

### Indexes 

An **index** is an optional data structure that you can create on one or more columns of a table. 
Indexes can increase the performance of data retrieval.

When processing a request, the database can use available indexes to locate the requested rows efficiently. 
Indexes are useful when applications often query a specific row or range of rows.

Indexes are logically and physically independent of the data. 
Thus, you can drop and create indexes with no effect on the tables or other indexes. 
All applications continue to function after you drop an index.

## Data access 

### Structured Query Language (SQL)

**Procedural languages** such as `C` describe *how things should be done*. 
**SQL is declarative** (nonprocedural) and describes *what should be done*.

SQL statements enable you to perform the following tasks:
• Query data
• Insert, update, and delete rows in a table
• Create, replace, alter, and drop objects
• Control access to the database and its objects
• Guarantee database consistency and integrity

## Transaction Management

### Transactions

A **transaction** is a logical, atomic unit of work that contains one or more SQL statements.

An RDBMS must be able to group SQL statements so that they are either all committed, which means they are applied to the database, or all rolled back, which means they are undone.
