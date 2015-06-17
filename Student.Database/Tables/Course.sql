CREATE TABLE [dbo].[Course]
(
	[CourseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MajorId] INT NOT NULL, 
	[CourseCode] NVARCHAR(10) NOT NULL,
	[CourseDesc] NVARCHAR(200) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL

    CONSTRAINT [FK_Course_To_Major] FOREIGN KEY ([MajorId]) REFERENCES [dbo].[Major]([MajorId])
)
GO

CREATE TRIGGER [dbo].[IU_Course]
    ON [dbo].[Course]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[Course] A 
				INNER JOIN [deleted] B ON A.[CourseId] = B.[CourseId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[Course] A 
				INNER JOIN [deleted] B ON A.[CourseId] = B.[CourseId]

		END
    END
