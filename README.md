# Library Management System - Updated By Haixing Zhang @ 2024 Feb 25th

## Introduction

This document provides the necessary instructions to set up and run the Library Management application. This application utilizes Entity Framework Core with SQLite to manage and interact with the library database.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- You have installed the latest version of [.NET Core SDK](https://dotnet.microsoft.com/download).
- You have a Windows/Linux/Mac machine to run the application.
- You have basic knowledge of Entity Framework Core and SQLite.

## Setting Up the Database & Seeding Data

The application uses Entity Framework Core to manage migrations and update the database schema. To set up your database, follow these steps:

1. Clone the repository to your local machine using `git clone <repository-url>`.
2. Navigate to the root directory of the project via the command line.
3. Run the following command to apply the initial database migration and create the database with the required schema, run:
shell
```shell
dotnet ef database update
```
This command will create an SQLite database according to the migration scripts included in the project.

## Running the Application
After setting up the database, you can run the application with the following command:

```shell
dotnet run
```
Navigate to http://localhost:5088 or https://localhost:5088 (as applicable) in your web browser to view and interact with the application.

