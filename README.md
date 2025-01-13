Simple .NET Core 8 Web Project

Overview

This project is a simple web application built using .NET Core 8, featuring an API, Razor Pages, and a Vue.js frontend. It implements JWT authentication with tokens stored in cookies, ensuring secure user sessions. The application supports role-based authentication, allowing users to join groups, each with its own set of roles.

Features

JWT Authentication: Secure user authentication using JSON Web Tokens, stored in cookies for easy access.

Role-Based Access Control: Users can join groups, and each group has specific roles that dictate access levels.

API Integration: A RESTful API is provided for seamless communication between the frontend and backend.

Razor Pages: Server-side rendering with Razor Pages for dynamic content generation.

Vue.js Frontend: A responsive and interactive user interface built with Vue.js.

Swagger Documentation: Integrated Swagger framework for easy testing and documentation of APIs.

Getting Started

Clone the repository.

Install the required dependencies using dotnet restore.

Configure your database connection in appsettings.json.

Run the application using dotnet run.
