
SET IDENTITY_INSERT Employees ON 
INSERT INTO Employees (EmployeeId, Name, DateHired, SupervisorId, Username, Password, IsAdmin) VALUES (1, N'John', '2015-01-10', NULL, N'John', N'abcd', 1);
INSERT INTO Employees (EmployeeId, Name, DateHired, SupervisorId, Username, Password) VALUES (2, N'Jane', '2015-02-20', 1, N'Jane', N'abcd');
INSERT INTO Employees (EmployeeId, Name, DateHired, SupervisorId, Username, Password) VALUES (3, N'Tom', '2016-06-19', 2, N'Tom', N'abcd');
INSERT INTO Employees (EmployeeId, Name, DateHired, SupervisorId, Username, Password) VALUES (4, N'Bob', '2016-06-20', 2, N'Bob', N'abcd');
SET IDENTITY_INSERT Employees OFF

GO

SET IDENTITY_INSERT Projects ON 
INSERT INTO Projects (ProjectId, Name, LeaderId) VALUES (1, N'Firestone', 1);
INSERT INTO Projects (ProjectId, Name, LeaderId) VALUES (2, N'Blue', 2);
SET IDENTITY_INSERT Projects OFF

GO

INSERT INTO ProjectMembers VALUES (1, 1);
INSERT INTO ProjectMembers VALUES (2, 2);
INSERT INTO ProjectMembers VALUES (2, 1);

GO
