An ASP.NET Core Web API project for managing a book collection stored in an XML file.
Includes full CRUD support (Create, Read, Update, Delete).

## Technologies Used

- ASP.NET Core Web API
- C# (.NET 8)
- HTML, CSS, JavaScript
- XML (for book storage)
- Git + GitHub

## Features

-  Load books from an XML file
-  Display them in an HTML table
-  Add new books
-  Edit existing books
-  Delete books

## Project Architecture

The project is built in a modular and separated way to allow for future expansion.
There is a separation of data access logic into a Repository,
which makes it easy to swap data sources in the future.

BookService:
Handles the business logic related to books,
such as adding, updating, and retrieving book details.
It interacts with the repository to access the data but does not manage the actual data storage itself.

BookRepository:
Manages the data access layer,
responsible for retrieving and saving book data to a data source (such as an XML file or a database).
It abstracts the underlying storage mechanism and provides data to the BookService.

Due to the use of XML, 
a locking mechanism (lock) has been implemented to prevent race conditions 
in case of concurrent access to the same file. 
This ensures that there is no simultaneous access to the XML file during read or write operations.

##  How to Run

1. dotnet run 
2. Swagger UI: https://localhost:7260/swagger/index.html
3. HTML Page: https://localhost:7260/index.html


   
