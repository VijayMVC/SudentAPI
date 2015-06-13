CREATE TABLE [dbo].[StudentCourse]
(
	[StudentCourseId] INT NOT NULL PRIMARY KEY IDENTITY,
	[StudentId] INT NOT NULL,
	[CourseId] INT NOT NULL,
	[SemesterId] INT NOT NULL, 
    [Grade] DECIMAL(3, 2) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL

    CONSTRAINT [FK_StudentCourse_To_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId])
    CONSTRAINT [FK_StudentCourse_To_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course]([CourseId])
    CONSTRAINT [FK_StudentCourse_To_Semester] FOREIGN KEY ([SemesterId]) REFERENCES [dbo].[Semester]([SemesterId])
)
GO

CREATE TRIGGER [dbo].[IU_StudentCourse]
    ON [dbo].[StudentCourse]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[StudentCourse] A 
				INNER JOIN [deleted] B ON A.[StudentCourseId] = B.[StudentCourseId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[StudentCourse] A 
				INNER JOIN [deleted] B ON A.[StudentCourseId] = B.[StudentCourseId]

		END
    END
