# SupplementsShop
> A minimal e-commerce app for nutritional supplements built with ASP.NET Core 8.

## Overview
This project implements backend and frontend functionality for an e-shopping health products website, built with ASP.NET Core MVC framework. It stores data in a local Microsoft SQL Server database.

## The purpose of the project
### The project was built as a testing ground for learning and sharpening new skills and best practices of commercial backend web development in ASP.NET Core. 
* Practice implementing architectures such as **MVC** and **Onion**
* Work with basic database persistence and **Entity Framework**
* Implement user-based authorization with **ASP.NET Core Identity**
* Practice working with external APIs
* Learn unit testing with **xUnit**
* Make the first deployment of an app

## Tech Stack

ASP.NET Core 8 MVC with Microsoft SQL Database.

## Data Flow and Architecture
The project uses Onion Architecture based on 4 layers: Domain, Infrastructure, Application and Web.
*Place for diagram*

## Features and details
* Authorization works with SignInManager and UserManager(Identity Framework).
* Authorization is role-based with User role and Admin role.
* Cart functionality is both session-based and context-based. When user is not logged in, the products are saved to cart in the session (with session extensions in infrastructure layer). When user is logged in, products are saved to both the session and the database. When user logs in, the program merges products from session-based cart and context-based cart. Logging out clears the session cart, leaving all the cart items of the given user in the database (to avoid doubling the items from subsequent login/logout actions).
* Actions that return a number of products (for example, the products associated with the given category) were reworked to return PagedLists of a given number of products (pageSize) for different pages (pageNumber) to reduce the database load (with regard to scaling and bigger amount of data).
* The edit page (accessible as and admin) allows for editing the product image which is then saved locally using the ImageService.

## TODO
* Unit testing
* Deployment