CREATE TABLE [dbo].[Major]
(
	[MajorId] INT NOT NULL PRIMARY KEY IDENTITY,
    [DepartmentId] INT NOT NULL, 
	[MajorCode] NVARCHAR(10) NOT NULL,
	[MajorDesc] NVARCHAR(200) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL

    CONSTRAINT [FK_Major_To_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department]([DepartmentId])
)
GO

CREATE TRIGGER [dbo].[IU_Major]
    ON [dbo].[Major]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[Major] A 
				INNER JOIN [deleted] B ON A.[MajorId] = B.[MajorId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[Major] A 
				INNER JOIN [deleted] B ON A.[MajorId] = B.[MajorId]

		END
    END
