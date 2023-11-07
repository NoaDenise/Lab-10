SELECT Products.ProductName, Products.UnitPrice, Categories.CategoryName
FROM Products
INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID
ORDER BY Categories.CategoryName, Products.ProductName;
GO

SELECT Customers.CustomerID, Customers.ContactName, COUNT(Orders.OrderID) AS NumberOfOrders
FROM Customers
LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.CustomerID, Customers.ContactName
ORDER BY NumberOfOrders DESC;
GO

SELECT Employees.FirstName, Employees.LastName, Territories.TerritoryDescription
FROM Employees
INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID;
