# Sagera Loan App

Sagera Loan App is a loan comparer web app powered by ASP.NET Core, Entity Framework Core and Sqlite database.

Click [here](https://sagera-loans.herokuapp.com) to view site.

## Description

App was dockerized to be hosted on heroku since heroku do not have support for C# and .NET apps. A Yaml file was generated placed in .github/workflows/ folder to implement github actions for continous integration and continous deployment.

## Entity Relational Diagram

This is an image of the relationship between entities in the database. Relationship markers are shown in the image.

![ScreenShot](https://res.cloudinary.com/raphaelnagato/image/upload/v1614728460/LoanCompanyERDImg.png)

## Class Diagram

shows Models encompassing the data and logic and the relationship between them.

![ScreenShot](https://res.cloudinary.com/raphaelnagato/image/upload/v1614738219/ClassDiagram.png)

## Contributing

Do not push to master, pull requests **MUST** be made on the **develop**  branch. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to **UPDATE TESTS** as appropriate.
