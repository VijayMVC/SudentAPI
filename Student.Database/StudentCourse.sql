CREATE TABLE [dbo].[StudentCourse]
(
	[StudentId] INT NOT NULL,
	[CourseId] INT NOT NULL,
	[SemesterId] INT NOT NULL,
    [Grade] DECIMAL(3, 2) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL
	
	PRIMARY KEY ([StudentId], [CourseId], [SemesterId]),
    CONSTRAINT [FK_StudentCourse_To_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student]([StudentId]),
    CONSTRAINT [FK_StudentCourse_To_CourseInstance] FOREIGN KEY ([CourseId], [SemesterId]) REFERENCES [dbo].[CourseInstance]([CourseId], [SemesterId])
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
				INNER JOIN [deleted] B ON A.[StudentId] = B.[StudentId]
					AND A.[CourseId] = B.[CourseId]
					AND A.[SemesterId] = B.[SemesterId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[StudentCourse] A 
				INNER JOIN [deleted] B ON A.[StudentId] = B.[StudentId]
					AND A.[CourseId] = B.[CourseId]
					AND A.[SemesterId] = B.[SemesterId]

		END
    END
GO

CREATE INDEX [IX_StudentCourse_Course_Semester_Student] ON [dbo].[StudentCourse] ([CourseId], [SemesterId], [StudentId]);