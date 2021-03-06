﻿CREATE TABLE [dbo].[CourseInstance]
(
	[CourseId] INT NOT NULL,
	[SemesterId] INT NOT NULL,
	[MaxStudentCount] INT NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[UserCreated] NVARCHAR(150) NOT NULL,
	[DateModified] DATETIME NOT NULL,
	[UserModified] NVARCHAR(150) NOT NULL,

	PRIMARY KEY ([CourseId], [SemesterId]),
    CONSTRAINT [FK_CourseInstance_To_Course] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course]([CourseId]),
    CONSTRAINT [FK_CourseInstance_To_Semester] FOREIGN KEY ([SemesterId]) REFERENCES [dbo].[Semester]([SemesterId])
)
GO

CREATE TRIGGER [dbo].[IU_CourseInstance]
    ON [dbo].[CourseInstance]
    FOR INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON

		IF UPDATE([UserModified])
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
			FROM [dbo].[CourseInstance] A 
				INNER JOIN [deleted] B ON A.[CourseId] = B.[CourseId]
					AND A.[SemesterId] = B.[SemesterId]

		END
		ELSE
		BEGIN

			UPDATE A
			SET A.DateCreated = GETDATE()
				, A.DateModified = GETDATE()
				, A.UserCreated = SUSER_SNAME()
				, A.UserModified = SUSER_SNAME()
			FROM [dbo].[CourseInstance] A 
				INNER JOIN [deleted] B ON A.[CourseId] = B.[CourseId]
					AND A.[SemesterId] = B.[SemesterId]

		END
    END
GO

CREATE INDEX [IX_CourseInstance_Semester_Course] ON [dbo].[CourseInstance] ([SemesterId], [CourseId]);