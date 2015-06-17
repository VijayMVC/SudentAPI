CREATE TABLE [dbo].[Semester]
(
	[SemesterId] INT NOT NULL PRIMARY KEY IDENTITY,
	[SemesterCode] NVARCHAR(10) NOT NULL,
	[SemesterDesc] NVARCHAR(200) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL
)
GO

CREATE TRIGGER [dbo].[IU_Semester]
    ON [dbo].[Semester]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[Semester] A 
				INNER JOIN [deleted] B ON A.[SemesterId] = B.[SemesterId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[Semester] A 
				INNER JOIN [deleted] B ON A.[SemesterId] = B.[SemesterId]

		END
    END