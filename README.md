SQL2JSON v1.0
=============

SQL2JSON is a Apache2 Licensed command line utility that captures execution of a sql query to a json file.

Requirements
------------
SQL2JSON requires .NET v4.0 framework.

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

**users.json:**

    [
    	{ "user_id": 1, "first_name": "john", "last_name": "johnson"},
    	{ "user_id": 2, "first_name": "scott", "last_name": "scottson"},
    	{ "user_id": 3, "first_name": "paul", "last_name": "paulson"}
    ]

### Example #2 - Nested Objects
This example demonstrates the use of delimiters to build a json string containing nested objects

    sql2json.exe -cs="Data Source=.;Initial Catalog=DB1;User Id=usr;Password=pwd;" -sql="select user_id, first_name as 'name::first', last_name as 'name::last' from users" -output="users.json"

**users.json:**

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

Advanced Scenarios
------------------

If you need include calculations or aggregations in your JSON objects you can either precalculate them in your sql query, or if you are more adventurous, you can write your own implementation of ITransformer to execute the required logic.