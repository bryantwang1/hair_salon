# _Hair Salon_

#### _A project for demonstrating knowledge of basic SQL Database management with C# for Epicodus that manages basic data for a hypothetical hair salon, 12/9/2016_

#### By _**Bryant Wang**_

## Setup/Installation Requirements

_To run this web app you need the Nancy framework for C#_

1. Clone this repository
2. Open Windows PowerShell
3. Change your directory in PowerShell to the cloned project folder
4. Type `dnu restore` into Powershell and hit enter
5. Create the databases with the appropriate schema.
  * If you have Microsoft SQL Server Management Studio, select _File_ > _Open_ > _File_ and select 'hair_salon.sql', then execute it by clicking the '!Execute' button.
    * Do the same with 'hair_salon_test.sql'
  * If you don't have MS SQL Server Management Studio, do this in SQLCMD:
```
> CREATE DATABASE hair_salon;
> GO;
> USE hair_salon;
> GO
> CREATE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(255), description VARCHAR(255), stylist_id INT);
> CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255), phone VARCHAR(255), description VARCHAR(255));
> GO
```
  * Do the above for a database called hair_salon_test or just delete the test files if you don't want to bother;
* Type `dnx kestrel` into PowerShell and hit enter, the local server should now be running
* Open your preferred web browser and navigate to localhost:5004, the main page should appear
* Go wild

## Technologies Used

_C#, Nancy, Razor, SQL by way of MS SQL Server Express and MS SQL Server Management Studio, edited in Atom_

### License

*GPL*

Copyright (c) 2016 **_Bryant Wang_**
