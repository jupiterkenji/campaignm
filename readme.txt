Note:
-----
Operating System: Ubuntu 18.04.1 LTS
Visual Studio Code Version:
	Version: 1.29.1
	Commit: bc24f98b5f70467bc689abf41cc5550ca637088e
	Date: 2018-11-15T19:07:43.495Z
	Electron: 2.0.12
	Chrome: 61.0.3163.100
	Node.js: 8.9.3
	V8: 6.1.534.41
	Architecture: x64
SQL server Version: Server version: 8.0.13 MySQL Community Server - GPL

To run the program, go to campaignm folder and run
> dotnet run

To run test, go to test folder and run
> dotnet test

To run Javascript test, go to campaignmjs folder and run
> npm test

Toubleshoot:
> If it complains about 'The current .NET SDK does not support targeting .Net Core 2.1 ...', 
	you can update dotnet-core https://dotnet.microsoft.com/download/dotnet-core/2.1#sdk-2.1.300-preview2

I have tested the solution on both:
> Visual Studio Code 1.29.1 on Ubuntu
> Visual Studio Community Version 7.7.1 (build 15) on Mac

Question 1-4
------------
Code: campaignm > Utilities.cs
Test: test > UtilitiesTest.cs

Question 5-6
------------
SQL.txt

Question 7
----------
Code: campaignmjs > arrangeBy.js
Test: campaignmjs > test > tests.js

Question 8
----------
Code: campaignm > LineChecker.cs
Test: test > LineCheckerTest.cs


