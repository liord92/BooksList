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


##  How to Run

1. Make sure the XML file path is correctly set in `appsettings.Development.json`:

   ```json
   {
     "XmlFilePath": "wwwroot/books.xml"
   }

2. dotnet run 
3. Swagger UI: https://localhost:7260/swagger/index.html
5. HTML Page: https://localhost:7260/index.html


   
