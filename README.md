# Candidate API

This is a .NET 8 Web API for managing candidates. It include an endpoints for adding or updating candidate records based on email along with validations.  

# Prerequisites

.NET 8 SDK  
Visual Studio 2022  
SQL Server (optional)


# Getting Started

1. Clone code from develop.
2. Open solution from Visaul Studio
3. Solution contains two folders: Sln and Tests. It follow repository pattern.
4. Run application in IIS Express after project: JobBackEnd is selected as StartUp Project.
5. It contains http file which allows user to test http call. Click on Send request for test. 

# Assumptions  
1. An api is available to add or update candidate.
2. Needs validation which is performed in controller.
3. If Email does not exist add other wise update.
4. For test purpose, InMemoryDatabase has been used.

# Folder Structure

1. It contains 3 projects in Sln folder:  
   1.1 JobBackEnd: Set as StartUp Project. It contains api: AddOrUpdateCandidate.   
   1.2 JobBackEnd.BLL: It is a business logic layer that reads acceptable values sent from api and communicates to repository.  
   1.3 JobBackEnd.DAL: It is a data acess layer that communicates to database for read/write operation.  
   
2. It contains 3 projects for unit tests:  
   2.1  JobBackEnd.BLL.Services.UnitTest: Unit test to verify add and update after api call.  
   2.2  JobBackEnd.BLL.Dtos.UnitTest: Unit test to verify data annotation applied in CandidateDto.  
   2.3  JobBackEnd.BLL.Attributes.UnitTest: Unit test to verify custom attribute added from time interval.

# Note  
   Two repositories like SQL or Mango can be used as per need.
   InmemoryDatabase has been used for now. Please go to AddDependencies file to verfiy.
   For SQL connection, please update connection string in appsetting.json file.  

# To do
   Data migration is required if we go for code first approach.
   Database and tables are required if we go for database first approach.

   




