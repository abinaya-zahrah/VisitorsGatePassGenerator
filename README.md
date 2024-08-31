Info'  &  Prerequisites 

Operating System: Windows 10/11
Development Environment:

Visual Studio 2022: Ensure Visual Studio 2022 is installed on your machine with the following components:
    .NET Desktop Development
    ASP.NET and web development (if applicable)
    SQL Server Management Studio (SSMS): Required for managing the database used in the project.
    .NET Framework:
                        Database Creation: The CREATE DATABASE gatePass command initializes the database. The USE gatePass command ensures that all subsequent commands are executed in this database context.
            
            Tables Overview:
            
            appUser: Manages application users with roles and enabled status.
            employee: Stores employee details and links each employee to a user in appUser.
            visitors: Manages basic visitor information.
            Visitors: A more detailed visitor table that includes additional fields like email, photo, and visit details.
            Pass: Manages visitor passes and links to the Visitors table using the UniqueID foreign key.
            Data Types: The script uses appropriate data types, such as VARCHAR for text, DATETIME for date-time fields, and VARBINARY for storing images.
            
            Constraints: Primary keys and foreign keys are defined to maintain data integrity.
            
            Consistency: The script consistently applies naming conventions and constraints, ensuring that the database structure is clear and maintainable.

.NET Framework Version [Specify Version, e.g., 4.7.2 or later]: Ensure that the required version of the .NET Framework is installed.
  Database:

SQL Server: Install SQL Server 2019 (or the version used in your project) for database management.
Database Setup: Restore the provided database backup file or run the SQL scripts in the Database folder to set up the database schema and initial data.
Git:

Git: Ensure Git is installed for version control and repository management.

******Clone the Repository: Use the following command to clone the repository:******
          bash
          git clone https://github.com/username/repository.git
Other Dependencies:

NuGet Packages: Restore NuGet packages used in the project. Visual Studio should automatically restore them on build.
******Open Visual Studio 2022:*****

Open your project or solution in Visual Studio 2022.
Install the NuGet Package:

Right-click on your project in the Solution Explorer.
Select Manage NuGet Packages.
In the Browse tab, search for the desired PDF package PdfSharp
Select the package from the list and click Install.
Verify Installation:

After installation, check the Installed tab to verify that the package was successfully installed.

*****Steps to Install a NuGet Package Using the Command Line in Visual Studio 2022*****
Open Visual Studio 2022:

Launch Visual Studio 2022 and open your solution or project.
Open the Package Manager Console:

In Visual Studio, go to the top menu and select Tools > NuGet Package Manager > Package Manager Console.
This will open the Package Manager Console at the bottom of the IDE.
Install the NuGet Package:

In the Package Manager Console, type the following command to install the desired NuGet package:
    powershell
        Install-Package <PackageName>
       --- For example, to install PdfSharp---
        powershell
           Install-Package PdfSharp

Additional Notes:
Update-Package: If you want to update an existing package to the latest version, you can use the Update-Package command.

powershell
Update-Package PdfSharp
Uninstall-Package: To uninstall a package, you can use the Uninstall-Package command.

powershell
Uninstall-Package PdfSharp
       
Web Browser:
Modern Web Browser: If the project is a web application, ensure you have a modern web browser like Google Chrome, Mozilla Firefox, or Microsoft Edge.
