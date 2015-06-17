CREATE TABLE [dbo].[Student]
(
	[StudentId] INT NOT NULL PRIMARY KEY IDENTITY,
	[MajorId] INT NOT NULL,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL

	CONSTRAINT [FK_Student_To_Major] FOREIGN KEY ([MajorId]) REFERENCES [dbo].[Major] ([MajorId])
)
GO

CREATE TRIGGER [dbo].[IU_Student]
    ON [dbo].[Student]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[Student] A 
				INNER JOIN [deleted] B ON A.[StudentId] = B.[StudentId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[Student] A 
				INNER JOIN [deleted] B ON A.[StudentId] = B.[StudentId]

		END
    END
