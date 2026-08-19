# Cherry's Construction Portal 🏗️

[![Status](https://img.shields.io/badge/Status-Active-success.svg)]()
[![Architecture](https://img.shields.io/badge/Architecture-MVC-blue.svg)]()

> A robust Model-View-Controller (MVC) web application built to manage and showcase operations for Cherry's Construction. 

## 📖 About the Project

Cherry's Construction MVC is a full-stack web application designed to streamline the digital presence and operational data for a construction business. Built on the ASP.NET MVC framework, it strictly separates the application logic, user interface, and data models to ensure scalability, maintainability, and secure data handling.

**Core Objectives:**
* Provide a clean, intuitive interface for prospective clients to view construction services and past projects.
* Demonstrate clean architectural principles, secure data handling, and structured routing.
* Leverage relational database management for reliable business data storage.

## ✨ Features

* **Dynamic Service Showcasing:** Users can browse through various construction services with a responsive, custom-styled frontend.
* **MVC Architecture:** Clean separation of concerns using C# Controllers, Razor Views, and Data Models.
* **Interactive UI:** Enhanced frontend functionality and dynamic DOM manipulation using vanilla JavaScript.
* **Data Integration:** Seamlessly connects to a SQL Server database for dynamic content generation and data persistence.

## 🛠️ Tech Stack

* **Language:** C#
* **Framework:** ASP.NET MVC
* **Frontend:** HTML5, CSS3, JavaScript, Razor Syntax (`.cshtml`)
* **Database:** Microsoft SQL Server

## 📂 Architecture Overview

The project follows the standard ASP.NET MVC pattern:
* **Models (`/Models`):** C# classes representing the data schema and business logic, mapped to the SQL database.
* **Views (`/Views`):** Razor templates (`.cshtml`), combined with HTML/CSS/JS, that render the user interface dynamically based on Model data.
* **Controllers (`/Controllers`):** C# classes that handle incoming HTTP requests, interact with the database via Models, and return the appropriate Views.

## 🚀 Getting Started

Follow these instructions to set up the project locally. 

### Prerequisites
* Visual Studio (2019 or 2022 recommended) with the "ASP.NET and web development" workload installed.
* SQL Server installed locally (or SQL Server Express).

### Installation & Setup

1. **Clone the repository**
   Open your terminal or Git Bash and run:
   ```bash
   git clone [https://github.com/Kozarr/cherrys_construction_mvc.git](https://github.com/Kozarr/cherrys_construction_mvc.git)
