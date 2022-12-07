# WebTimeSheetManagement

<b>Basic Timesheet Application ASP.NET MVC 5</b>

<b>Process</b>

This project focuses completely on the timesheet process in which there are three roles:

1. User
2. Admin
3. SuperAdmin.

Let’s start with “Super Admin” Role. “Super Admin” has access to create a New “Admin” and “User”. First, “Super Admin” creates “Admin” and after creating the “Admin”, “Super Admin” creates a “User” and then assigns User to a particular “Admin”.

After assigning a user to particular “Admin”, the “Admin” has right to view User Profile and has rights to “Approve” or “Reject” the Timesheet and Expenses which the User has submitted. “Admin” can export the Timesheet according to the Date and Users and in a similar way, the expense can be exported according to the date.

Next, let’s have a look at “User” Role. User has a right to fill in and submit Timesheet and Expenses. After submitting it can be viewed by Admin who can  then “Approve” or “Reject” Timesheet and Expense.


<b>About Platform Used </b>

I had developed entire Application using Microsoft visual studio 2015 with SQL Server 2008 R2.
Frame worked used is ASP.NET MVC 5 and language is C# and Dapper, Entity Framework as ORM and Repository Pattern.
Microsoft visual studio 2015 with Update 3

