CREATE TABLE [dbo].[Department]
(
	[DepartmentId] INT NOT NULL PRIMARY KEY IDENTITY,
	[DepartmentCode] NVARCHAR(10) NOT NULL,
	[DepartmentDesc] NVARCHAR(200) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL
)
GO

CREATE TRIGGER [dbo].[IU_Department]
    ON [dbo].[Department]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[Department] A 
				INNER JOIN [deleted] B ON A.DepartmentId = B.DepartmentId

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[Department] A 
				INNER JOIN [deleted] B ON A.DepartmentId = B.DepartmentId

		END
    END