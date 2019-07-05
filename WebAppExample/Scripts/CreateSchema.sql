IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Employees] (
    [EmployeeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [DateHired] datetime2 NOT NULL,
    [SupervisorId] int NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId]),
    CONSTRAINT [FK_Employees_Employees_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Projects] (
    [ProjectId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [LeaderId] int NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId]),
    CONSTRAINT [FK_Projects_Employees_LeaderId] FOREIGN KEY ([LeaderId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProjectMembers] (
    [ProjectId] int NOT NULL,
    [MemberId] int NOT NULL,
    CONSTRAINT [PK_ProjectMembers] PRIMARY KEY ([ProjectId], [MemberId]),
    CONSTRAINT [FK_ProjectMembers_Employees_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Employees] ([EmployeeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectMembers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Employees_SupervisorId] ON [Employees] ([SupervisorId]);

GO

CREATE INDEX [IX_ProjectMembers_MemberId] ON [ProjectMembers] ([MemberId]);

GO

CREATE INDEX [IX_Projects_LeaderId] ON [Projects] ([LeaderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190705160311_InitialSchema', N'2.2.1-servicing-10028');

GO

