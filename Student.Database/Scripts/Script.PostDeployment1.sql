/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT dbo.Department ON
MERGE INTO [Department] AS TARGET
	USING ( VALUES 
		(1, N'ENG', N'College of Engineering')
		, (2, N'AS', N'College of Arts and Sciences')
	) AS SOURCE ([DepartmentId], [DepartmentCode], [DepartmentDesc]) 
	ON TARGET.[DepartmentId] = SOURCE.[DepartmentId] 

	WHEN MATCHED THEN 
		UPDATE SET [DepartmentCode] = SOURCE.[DepartmentCode]
			, [DepartmentDesc] = SOURCE.[DepartmentDesc]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([DepartmentId], [DepartmentCode], [DepartmentDesc], [DateCreated], [UserCreated], [DateModified], [UserModified]) 
		VALUES ([DepartmentId], [DepartmentCode], [DepartmentDesc], GETDATE(), N'SUNFLOWER\Cory Melton', GETDATE(), N'SUNFLOWER\Cory Melton') 

	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
SET IDENTITY_INSERT dbo.Department OFF


SET IDENTITY_INSERT dbo.Major ON
MERGE INTO [Major] AS TARGET
	USING ( VALUES 
		(1, 1, N'ARE-CNS', N'Architectural Engineering and Construction Science')
		, (2, 1, N'BAE', N'Biological and Agricultural Engineering')
		, (3, 1, N'CHE', N'Chemical Engineering')
		, (4, 1, N'CE', N'Civil Engineering')
		, (5, 1, N'CIS', N'Computing and Information Sciences')
		, (6, 1, N'ECE', N'Electrical and Computer Engineering')
		, (7, 1, N'IMSE', N'Industrial and Manufacturing Systems Engineering')
		, (8, 1, N'MNE', N'Mechanical and Nuclear Engineering')
		
		, (9, 2, N'MATH', N'Mathematics')
		, (10, 2, N'PHYS', N'Physics')
		, (11, 2, N'CHEM', N'Chemistry')
		, (12, 2, N'ENG', N'English')
		, (13, 2, N'ECON', N'Economics')
		, (14, 2, N'HIST', N'History')
	) AS SOURCE ([MajorId], [DepartmentId], [MajorCode], [MajorDesc]) 
	ON TARGET.[MajorId] = SOURCE.[MajorId] 

	WHEN MATCHED THEN 
		UPDATE SET [DepartmentId] = SOURCE.[DepartmentId]
			, [MajorCode] = SOURCE.[MajorCode]
			, [MajorDesc] = SOURCE.[MajorDesc]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([MajorId], [DepartmentId], [MajorCode], [MajorDesc], [DateCreated], [UserCreated], [DateModified], [UserModified]) 
		VALUES ([MajorId], [DepartmentId], [MajorCode], [MajorDesc], GETDATE(), N'SUNFLOWER\Cory Melton', GETDATE(), N'SUNFLOWER\Cory Melton') 

	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
SET IDENTITY_INSERT dbo.Major OFF


SET IDENTITY_INSERT dbo.Semester ON
MERGE INTO [Semester] AS TARGET
	USING ( VALUES 
		(1, N'F2015', N'Fall 2015')
		, (2, N'S2016', N'Spring 2016')
		, (3, N'F2016', N'Fall 2016')
		, (4, N'F2017', N'Spring 2017')
	) AS SOURCE ([SemesterId], [SemesterCode], [SemesterDesc]) 
	ON TARGET.[SemesterId] = SOURCE.[SemesterId] 

	WHEN MATCHED THEN 
		UPDATE SET [SemesterCode] = SOURCE.[SemesterCode]
			, [SemesterDesc] = SOURCE.[SemesterDesc]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([SemesterId], [SemesterCode], [SemesterDesc], [DateCreated], [UserCreated], [DateModified], [UserModified]) 
		VALUES ([SemesterId], [SemesterCode], [SemesterDesc], GETDATE(), N'SUNFLOWER\Cory Melton', GETDATE(), N'SUNFLOWER\Cory Melton') 

	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
SET IDENTITY_INSERT dbo.Semester OFF


SET IDENTITY_INSERT dbo.Course ON
MERGE INTO [Course] AS TARGET
	USING ( VALUES 
		(1, 5, N'CIS015', N'Undergraduate Seminar')
		, (2, 5, N'CIS101', N'Introduction to Computing Systems')
		, (3, 5, N'CIS111', N'Introduction to Computer Programming')
		, (4, 5, N'CIS115', N'Introduction to Computing Science')
		, (5, 5, N'CIS200', N'Programming Fundamentals')
		, (6, 5, N'CIS300', N'Algorithms & Data Structures')
		, (7, 5, N'CIS505', N'Introduction To Programming Languages')
		, (8, 5, N'CIS520', N'Operating Systems 1')

		, (9, 2, N'MATH220', N'Calculus 1')
		, (10, 2, N'MATH221', N'Calculus 3')
		, (11, 2, N'MATH222', N'Calculus 3')
		, (12, 2, N'MATH510', N'Discrete Mathematics')
	) AS SOURCE ([CourseId], [MajorId], [CourseCode], [CourseDesc]) 
	ON TARGET.[CourseId] = SOURCE.[CourseId] 

	WHEN MATCHED THEN 
		UPDATE SET [MajorId] = SOURCE.[MajorId]
			, [CourseCode] = SOURCE.[CourseCode]
			, [CourseDesc] = SOURCE.[CourseDesc]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([CourseId], [MajorId], [CourseCode], [CourseDesc], [DateCreated], [UserCreated], [DateModified], [UserModified]) 
		VALUES ([CourseId], [MajorId], [CourseCode], [CourseDesc], GETDATE(), N'SUNFLOWER\Cory Melton', GETDATE(), N'SUNFLOWER\Cory Melton') 

	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;
SET IDENTITY_INSERT dbo.Course OFF


MERGE INTO [CourseInstance] AS TARGET
	USING ( VALUES 
		(1, 1, 300)
		, (1, 2, 300)
		, (1, 3, 300)
		, (1, 4, 300)
		, (2, 1, 100)
		, (2, 2, 100)
		, (2, 3, 100)
		, (2, 4, 100)
		, (3, 1, 100)
		, (3, 2, 100)
		, (3, 3, 100)
		, (3, 4, 100)
		, (4, 1, 100)
		, (4, 2, 100)
		, (4, 3, 100)
		, (4, 4, 100)
		, (5, 1, 100)
		, (5, 2, 100)
		, (5, 3, 100)
		, (5, 4, 100)
		, (6, 1, 50)
		, (6, 2, 50)
		, (6, 3, 50)
		, (6, 4, 50)
		, (7, 1, 50)
		, (7, 2, 50)
		, (7, 3, 50)
		, (7, 4, 50)
		, (8, 1, 25)
		, (8, 2, 25)
		, (8, 3, 25)
		, (8, 4, 25)

		, (9, 1, 250)
		, (9, 2, 250)
		, (9, 3, 250)
		, (9, 4, 250)
		, (10, 1, 250)
		, (10, 2, 250)
		, (10, 3, 250)
		, (10, 4, 250)
		, (11, 1, 250)
		, (11, 2, 250)
		, (11, 3, 250)
		, (11, 4, 250)
		, (12, 1, 25)
		, (12, 2, 25)
		, (12, 3, 25)
		, (12, 4, 25)
	) AS SOURCE ([CourseId], [SemesterId], [MaxStudentCount]) 
	ON TARGET.[CourseId] = SOURCE.[CourseId] 
		AND TARGET.[SemesterId] = SOURCE.[SemesterId]

	WHEN MATCHED THEN 
		UPDATE SET [MaxStudentCount] = SOURCE.[MaxStudentCount]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([CourseId], [SemesterId], [MaxStudentCount], [DateCreated], [UserCreated], [DateModified], [UserModified]) 
		VALUES ([CourseId], [SemesterId], [MaxStudentCount], GETDATE(), N'SUNFLOWER\Cory Melton', GETDATE(), N'SUNFLOWER\Cory Melton') 

	WHEN NOT MATCHED BY SOURCE THEN 
		DELETE;