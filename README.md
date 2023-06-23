# ContactManager

# Contact Manager Application

This is a simple Contact Manager application developed using Microsoft MVC in Visual Studio. It allows you to manage contacts and their associated addresses. Please follow the guidelines and requirements mentioned below to ensure proper development.

## Requirements

- Visual Studio 2022 (Pro or Community)
- Microsoft SQL Server (LocalDB, Developer, or Express editions)
- Target .NET 7
- Microsoft Entity Framework (Code First approach)
- Two separate projects within one solution:
  - MVC Web Application for the Presentation Layer
  - Class Library for Data and Business Logic

## Schema

The application uses the following database schema:

- Contact (table)
  - First Name
  - Last Name
  - Address (table, foreign key, navigation from Contact)
    - Street
    - City
    - State
    - Postal Code

## Application Features

The Contact Manager application includes the following features:

- Contact List
  - Grid view displaying all contacts
  - Search functionality allowing users to search across all fields using a "contains" approach
  - Real-time filtering of the grid as the user types in the search box
  - "New Contact" button to add a new contact
  - "Edit Contact" button to modify an existing contact
  - "Delete Contact" button to remove a contact
  - Integration with a third-party web service API to validate addresses
  - Displaying the address location on a map
  - Utilizing AJAX for seamless page updates without refreshing

## Setup Instructions

Follow these steps to set up the Contact Manager application:

1. Clone or download the repository from [GitHub].
2. Open the solution in Visual Studio 2022.
3. Make sure you have the appropriate version of Microsoft SQL Server installed.
4. Update the connection string in the `appsettings.json` file of the MVC project to point to your SQL Server database.
5. Build the solution to restore the NuGet packages.Update the initial numberOfContacts in the appsettings.json to generate seed data.
6. Update BingMaps ApiKey appsettings.json file of the MVC project to point a valid key for BingMaps
7. Build the solution to restore the NuGet packages.
8. Run the application and access it through your preferred web browser.

## Usage

Once the Contact Manager application is set up and running, you can perform the following actions:

- View the list of contacts with their associated addresses.
- Use the search box to filter contacts based on various fields.
- Add a new contact using the "New Contact" button.
- Edit an existing contact by clicking the "Edit" button.
- Delete a contact using the "Delete" button.

Please ensure that you have valid and active SQL Server credentials and connection string to interact with the database successfully.
Please ensure that you have valid and active BingMaps ApiKey to interact with the Maps

