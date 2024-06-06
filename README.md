# ZBUS - Online Bus Reservation Web Application

Welcome to **ZBUS**, an open-source online bus reservation web application designed to make booking bus travel simple, convenient, and accessible for everyone. This repository contains the codebase for ZBUS, which includes features for both users and administrators.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Code Practices](#code-practices)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

### User Functionality
- **Search Trips**: Users can search for bus trips by date, departure, and arrival station.
- **Book Tickets**: Users can book and pay for bus tickets directly through the application.
- **Manage Trips**: Users can view and manage their upcoming trips, with options to modify or cancel reservations.
- **User Dashboard**: Access a dashboard to view trip history and payment details.

### Admin Functionality
- **Manage Stations**: Admins can add, edit, and delete station information.
- **Manage Schedules**: Create and update bus trip schedules, including routes, departure/arrival times, and pricing.
- **Driver and Vehicle Management**: Maintain driver and bus vehicle information.
- **User Bookings and Payments**: Monitor and manipulate user bookings and payments.

## Technologies Used
- **Backend**: ASP.NET MVC
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework
- **Dependency Injection**: Built-in ASP.NET DI
- **Validation**: Data Annotations
- **Version Control**: Git

## Installation

### Prerequisites
- .NET SDK
- Microsoft SQL Server
- Git

### Steps

1. **Clone the repository:**

    ```sh
    git clone https://github.com/ELglaly/ZBUS-Website.git
    cd ZBUS-Website
    ```

2. **Setup the Backend:**

    Navigate to the project directory:
    
    ```sh
    cd ZBUS-Website
    ```

    Restore the .NET dependencies:
    
    ```sh
    dotnet restore
    ```

    Update the `appsettings.json` with your SQL Server connection string.

    Apply migrations to set up the database:
    
    ```sh
    dotnet ef database update
    ```

    Run the application:
    
    ```sh
    dotnet run
    ```

## Usage

- **User Access**: Users can sign up, search for bus trips, book tickets, and manage their bookings through the web interface.
- **Admin Access**: Admins can log in to access administrative functionalities like managing stations, schedules, drivers, and user bookings.

## Project Structure

```plaintext
ZBUS-Website/
├── Controllers/            # MVC Controllers
├── Models/                 # Data models
├── Views/                  # View templates
├── wwwroot/                # Static files (CSS, JavaScript, images)
├── Data/                   # Database context and migrations
├── Services/               # Business logic services
├── ViewModels/             # View models for strongly-typed views
├── appsettings.json        # Configuration settings
├── Program.cs              # Application entry point
├── Startup.cs              # Configuration and middleware
└── README.md               # Project documentation
