USE projectdb
GO
--
-- Test data
--
INSERT INTO departments 
VALUES (1, 'IT'), (2, 'Database'), (3, 'Implementation')
GO
INSERT INTO projects
VALUES
(1, 'HR Management', 'Manager 1', 780000.00),
(2, 'Pay roll', 'Manager 2', 880000.00),
(3, 'Fund management', 'Manager 3', 560000.00),
(4, 'Attendance', 'Manager 4', 180000.00)
GO
INSERT INTO employees
VALUES
(1, 'Emp 1', 1700.00, 1, 1),
(2, 'Emp 2', 1800.00, 2, 1),
(3, 'Emp 3', 1700.00, 1, 2),
(4, 'Emp 4', 1700.00, 3, 3),
(5, 'Emp 5', 1700.00, 1, 3),
(6, 'Emp 6', 1700.00, 1, 4),
(7, 'Emp 7', 1700.00, 3, 4)
GO
--
-- Fetch data
--
SELECT p.title, p.manager, p.budget, e.empname, d.deptname, e.payrate
FROM departments d
INNER JOIN employees e
ON d.deptid = e.deptid
INNER JOIN projects p
ON e.projectid = p.projectid
GO
--
-- Project of highest budget
--
--sub query
SELECT * FROM projects
WHERE budget = (SELECT MAX(budget) FROM projects) 
GO
--
-- Test view
--
SELECT * FROM vEmp
GO
--
--Test proc
--
EXEC sInsertProject 5, 'Cash flow', 'Manager 5', 900000.00 
--EXEC sInsertProject 5, 'Test', 'Manager 5', 900000.00 --IF Throws Error, then ok
GO
--
-- Test scalar func
--
SELECT  dbo.fnGetBudget(1)
GO
--
-- test inline table func
--
SELECT * FROM fnGetDetailsInline(1)
GO
--
-- test multi-statement
--
SELECT * FROM fnGetDetails(1)
GO
--
-- test trigger
--
INSERT INTO employees
VALUES (9, 'Emp 8',900.00, 1, 1)
GO