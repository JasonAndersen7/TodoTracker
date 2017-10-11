# TodoTracker
An application to Track and Modify Todos


The approach that I took (especially with iterative development) is to implement a simple solution and then refactor as necessary later.  
This keeps the code as simple as possible, and avoids over-engineering.

Overall Architecture -
Since this is a simple application, I used ASP.NET MVC for the web application with an N-Tier Architecture.
The main components are :
                TodoTrackerData - Handles the Todo Repo to connect to the Repository
                TodoTrackerBiz/Service - Handles Business Logic and future Validation
                TodoTracker - The UI layer
                TodoTrackerModels - POCO class to contain data transfer objects
               
TodoTrackerData
 
SQLite as the back end database
                Currently one table to hold tasks, this contains the following columns
                Requester - The Person making the request of the Todo
                Assignee - The Person assigned to do the Todo
                Due Date - The date the Todo is due to be completed
                Description - A description of the Todo
                IsCompleted - Is the task completed?
               
Dapper - ORM
                Since you mentioned that you use Dapper as part of your ORM, I decided to try that and use that
                to connect to the database and to run SQL commands against it.
 
TodoTrackerBiz/Service
               
Dependency Injection
                                Unity is used as the DI Module, at this point, it only targets my Service/Biz layer
                               
 
TodoTracker
                                                Angular 1.0 used for my Javascript framework
                                                Bootstrap used for CSS styling
 
TodoTrackerTests
                The first iteration of tests that I used are MSTest Integration tests, not pure Unit tests. This was to test the
                functionality of the SQL Statements. I agree that these are not Unit Tests, but I feel that they are still useful to
                test the functionality of the database integration            
                
				There are some MOQ test implementations as well that use NUnit as their testing framework
 
Logging
                NLog is providing the basic logging for the application. It logs to a simple text file with basic settings (INFO, DEBUG, ERROR, etc)
               
Future Features
                Add administration section to create users to assign to tasks
                Change MVC to Web Api to allow for greater routing flexibility
                Upgrade Angular 1 to Angular 4 TypeScript
                Add more unit testing around business logic
                Create a section to see archived Todo
               
