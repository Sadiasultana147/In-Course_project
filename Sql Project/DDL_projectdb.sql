CREATE DATABASE projectdb
GO
USE projectdb
GO
CREATE TABLE departments
(
	deptid INT PRIMARY KEY,
	deptname VARCHAR(30) NOT NULL
)
GO
CREATE TABLE projects
(
	projectid INT PRIMARY KEY,
	title VARCHAR(30) NOT NULL,
	manager VARCHAR(30) NOT NULL,
	budget MONEY NOT NULL
)
GO
CREATE TABLE employees
(
	empid INT PRIMARY KEY,
	empname VARCHAR(30) NOT NULL,
	payrate MONEY NOT NULL,
	deptid INT REFERENCES departments(deptid),
	projectid INT NOT NULL REFERENCES projects (projectid)
)
GO
-- Index
CREATE INDEX ixEmpName 
ON employees(empid)
GO
EXEC sp_helpindex 'employees'
GO
--
-- View 
--
CREATE VIEW vEmp
AS
SELECT p.title, p.manager, p.budget, e.empname, d.deptname, e.payrate
FROM departments d
INNER JOIN employees e
ON d.deptid = e.deptid
INNER JOIN projects p
ON e.projectid = p.projectid
GO
--
-- PROC
--
CREATE PROC sInsertProject @i INT, @t VARCHAR(30), @m VARCHAR(30), @b MONEY
AS
BEGIN TRY
	INSERT INTO projects VALUES (@i, @t, @m, @b)
END TRY
BEGIN CATCH
	;
	THROW;
END CATCH
GO

SELECT * FROM projects
GO
CREATE PROC sUpdateProject @i INT, @t VARCHAR(30), @m VARCHAR(30), @b MONEY
AS
BEGIN TRY
	UPDATE projects SET title= @t, manager=@m, budget = @b
	WHERE projectid = @i
END TRY
BEGIN CATCH
	;
	THROW;
END CATCH
GO
--
-- Func to find budget of a project
--
CREATE FUNCTION fnGetBudget(@pid INT) RETURNS MONEY
AS
BEGIN
	DECLARE @b MONEY
	SELECT @b = budget FROM projects WHERE projectid = @pid
	RETURN @b
END
GO
--
-- inline table func
--
CREATE FUNCTION fnGetDetailsInline(@deptid INT) RETURNS TABLE
AS
RETURN(
	
	SELECT deptname, title, empname
	FROM departments d
	INNER JOIN employees e
	ON d.deptid = e.deptid
	LEFT OUTER JOIN projects p
	ON e.projectid = p.projectid
	WHERE d.deptid =@deptid

)
GO
--
-- Multi-statement table-valued func--
CREATE FUNCTION fnGetDetails(@deptid INT) RETURNS @tbl TABLE
(
	depname VARCHAR(30),
	title VARCHAR(30),
	empname VARCHAR(30)
)
AS
BEGIN
	INSERT INTO @tbl
	SELECT deptname, title, empname
	FROM departments d
	INNER JOIN employees e
	ON d.deptid = e.deptid
	LEFT OUTER JOIN projects p
	ON e.projectid = p.projectid
	WHERE d.deptid =@deptid
RETURN 
END
GO
--
-- TRIGGER
--
CREATE TRIGGER trEmpInsert
ON employees
FOR INSERT
AS
BEGIN 
	DECLARE @pid INT, @deptid INT
	SELECT @pid = projectid, @deptid = deptid FROM inserted
	IF EXISTS (SELECT * FROM employees WHERE deptid=@deptid AND projectid = @pid)
	BEGIN
		ROLLBACK TRAN
		RAISERROR('Already an employee of the same dep in project', 10, 1)
	END
END
GO
