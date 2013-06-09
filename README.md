SQL2JSON
========

SQL2JSON is a command line utility that captures sql query execution result as a json file.

Requirements
------------
SQL2JSON is a .NET based application and requires .NET v4 framework.

Usage
-----

Suppose you have a table called "users" with the following records:

|user_id|first_name|last_name |
|-------|----------|----------|
|1      |john      |johnson   |
|2      |scott     |scottson  |
|3      |paul      |paulson   |


### Example #1 - Simplest Case

sql2json.exe -cs="Data Source=.;Initial Catalog=DB1;User Id=usr;Password=pwd;" -sql="select * from users" -output="users.json"

**users.json**

    [
    	{ "user_id": 1, "first_name": "john", "last_name": "johnson"},
    	{ "user_id": 2, "first_name": "scott", "last_name": "scottson"},
    	{ "user_id": 3, "first_name": "paul", "last_name": "paulson"}
    ]

### Example #2 - Nested Objects
This example demonstrates the use of delimiters to build a json string containing nested objects

sql2json.exe -cs="Data Source=.;Initial Catalog=DB1;User Id=usr;Password=pwd;" -sql="select user_id, first_name as 'name::first', last_name as 'name::last' from users" -output="users.json"

**users.json**

    [
    	{
    		"user_id": 1,
    		"name": { "first": "john", "last": "johnson" }
    	},
    	{
    		"user_id": 2,
    		"name": { "first": "scott", "last": "scottson" }
    	},
    	{
    		"user_id": 3,
    		"name": { "first": "paul", "last": "paulson" }
    	}
    ]
