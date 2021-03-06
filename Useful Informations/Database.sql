USE [EduBIN]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[code] [varchar](60) NULL,
	[descriptions] [varchar](300) NULL,
	[program_id] [int] NOT NULL,
	[entryBy_id] [int] NOT NULL,
	[entryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[descriptions] [varchar](300) NULL,
	[institution_id] [int] NOT NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Designations]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[institution_id] [int] NOT NULL,
	[entryBy_id] [int] NOT NULL,
	[entrydate] [datetime] NOT NULL,
 CONSTRAINT [PK_Designations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](300) NOT NULL,
	[nidNumber] [varchar](20) NULL,
	[designation_id] [int] NOT NULL,
	[department_id] [int] NOT NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institutions]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institutions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[contact] [varchar](14) NOT NULL,
	[email] [varchar](200) NULL,
	[address] [varchar](300) NULL,
	[logo] [varchar](300) NULL,
	[registrationDate] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Institutions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[username] [varchar](300) NOT NULL,
	[password] [varchar](300) NOT NULL,
	[role] [varchar](20) NOT NULL,
	[isActive] [bit] NOT NULL,
	[lastLoginDate] [datetime] NULL,
	[institution_id] [int] NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalInformations]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalInformations](
	[username] [varchar](300) NOT NULL,
	[fullName] [varchar](60) NOT NULL,
	[fatherName] [varchar](60) NOT NULL,
	[motherName] [varchar](60) NOT NULL,
	[gender] [varchar](6) NOT NULL,
	[dateofBirth] [date] NULL,
	[nationality] [varchar](50) NOT NULL,
	[image] [varchar](200) NULL,
	[contact] [varchar](14) NOT NULL,
	[email] [varchar](300) NOT NULL,
	[address] [varchar](300) NULL,
 CONSTRAINT [UniqueContact] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UniqueEmail] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Programs]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Programs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[descriptions] [varchar](300) NULL,
	[department_id] [int] NOT NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Programs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Routines]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[section] [varchar](20) NOT NULL,
	[day] [varchar](10) NOT NULL,
	[timeStart] [time](7) NOT NULL,
	[timeEnd] [time](7) NOT NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Routine] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[routine_id] [int] NOT NULL,
	[faculty_id] [int] NOT NULL,
	[student_id] [int] NOT NULL,
	[timeStart] [time](7) NOT NULL,
	[timeEnd] [time](7) NOT NULL,
	[roomNumber] [varchar](60) NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](300) NOT NULL,
	[program_id] [int] NOT NULL,
	[guardianName] [varchar](60) NOT NULL,
	[guardianContact] [varchar](14) NOT NULL,
	[guardianAddress] [varchar](300) NOT NULL,
	[entryby_id] [int] NULL,
	[entryDate] [datetime] NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tracer]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[actor_id] [int] NOT NULL,
	[actionName] [varchar](100) NOT NULL,
	[tableName] [varchar](100) NOT NULL,
	[actionApplied_id] [int] NOT NULL,
	[actionTime] [datetime] NOT NULL,
	[institution_id] [int] NOT NULL,
 CONSTRAINT [PK_Tracer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_Courses_Programs] FOREIGN KEY([program_id])
REFERENCES [dbo].[Programs] ([id])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_Courses_Programs]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_Institutions] FOREIGN KEY([institution_id])
REFERENCES [dbo].[Institutions] ([id])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_Institutions]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_Departments] FOREIGN KEY([department_id])
REFERENCES [dbo].[Departments] ([id])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_Departments]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_Designations] FOREIGN KEY([designation_id])
REFERENCES [dbo].[Designations] ([id])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_Designations]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_Login] FOREIGN KEY([username])
REFERENCES [dbo].[Login] ([username])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_Login]
GO
ALTER TABLE [dbo].[PersonalInformations]  WITH CHECK ADD  CONSTRAINT [FK_PersonalInformations_Login] FOREIGN KEY([username])
REFERENCES [dbo].[Login] ([username])
GO
ALTER TABLE [dbo].[PersonalInformations] CHECK CONSTRAINT [FK_PersonalInformations_Login]
GO
ALTER TABLE [dbo].[Programs]  WITH CHECK ADD  CONSTRAINT [FK_Programs_Departments] FOREIGN KEY([department_id])
REFERENCES [dbo].[Departments] ([id])
GO
ALTER TABLE [dbo].[Programs] CHECK CONSTRAINT [FK_Programs_Departments]
GO
ALTER TABLE [dbo].[Routines]  WITH CHECK ADD  CONSTRAINT [FK_Routine_Courses] FOREIGN KEY([course_id])
REFERENCES [dbo].[Courses] ([id])
GO
ALTER TABLE [dbo].[Routines] CHECK CONSTRAINT [FK_Routine_Courses]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Faculty] FOREIGN KEY([faculty_id])
REFERENCES [dbo].[Faculty] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Faculty]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Students] FOREIGN KEY([student_id])
REFERENCES [dbo].[Students] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Students]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Routines] FOREIGN KEY([routine_id])
REFERENCES [dbo].[Routines] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Table_1_Routines]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Programs] FOREIGN KEY([program_id])
REFERENCES [dbo].[Programs] ([id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Programs]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteDepartments]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteDepartments] 
	@Id INT
AS
BEGIN
	DELETE FROM Departments
	WHERE Departments.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletePrograms]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeletePrograms] 
	@Id INT
AS
BEGIN
	DELETE FROM Programs
	WHERE Programs.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllDepartments]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllDepartments] 

AS
BEGIN
	SELECT * FROM Departments;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllDesignations]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllDesignations]
AS
BEGIN
	SELECT * FROM Designations;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllFaculty]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllFaculty]
AS
BEGIN
	SELECT * FROM 
	Faculty, Designations,Departments, PersonalInformations, Login
	WHERE 
	Faculty.designation_id = Designations.id and 
	Faculty.department_id  = Departments.id and 
	Faculty.username = PersonalInformations.username and
	Faculty.username = Login.username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllInstitution]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllInstitution]
AS
BEGIN
	SELECT * FROM Institutions;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllLogin]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAllLogin]

AS
BEGIN
	SELECT * FROM Login;	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllPrograms]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllPrograms] 

AS
BEGIN
	SELECT * FROM Programs;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllStudent]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllStudent]
AS
BEGIN 
	SELECT * FROM Students, Programs, PersonalInformations, Login
	WHERE 
	Students.program_id = Programs.id and
	Students.username = PersonalInformations.username and
	Students.username = Login.username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllTracer]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllTracer] 

AS
BEGIN
	SELECT * FROM Tracer;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDepartmentsById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetDepartmentsById] 
	@Id INT
AS
BEGIN
	SELECT * FROM Departments
	WHERE Departments.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDesignationsById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetDesignationsById]
	@Id INT
AS
BEGIN
	SELECT * FROM Designations
	WHERE Designations.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetFacultyById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetFacultyById]
	@Id INT
AS
BEGIN
	SELECT * FROM 
	Faculty, Designations,Departments, PersonalInformations, Login
	WHERE 
	Faculty.designation_id = Designations.id and 
	Faculty.department_id  = Departments.id and 
	Faculty.username = PersonalInformations.username and
	Faculty.username = Login.username and 
	Faculty.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetInstitutionById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetInstitutionById]
	@Id INT
AS
BEGIN
	SELECT * FROM Institutions
	WHERE Institutions.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoginByUsername]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLoginByUsername]
	@Username VARCHAR(300)
AS
BEGIN
	SELECT * FROM Login
	WHERE Login.username = @Username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProgramsById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetProgramsById] 
	@Id INT
AS
BEGIN
	SELECT * FROM Programs
	WHERE Programs.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStudentById]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetStudentById]
	@Id INT
AS
BEGIN 
	SELECT * FROM Students, Programs, PersonalInformations, Login
	WHERE 
	Students.program_id = Programs.id and
	Students.username	= PersonalInformations.username and
	Students.username	= Login.username and
	Students.id			= @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveDepartments]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveDepartments] 
	@Name VARCHAR(200),
	@Descriptions VARCHAR(300),
	@InstitutionId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	INSERT INTO Departments
	(
		Departments.name,
		Departments.descriptions,
		Departments.institution_id,
		Departments.entryby_id,
		Departments.entryDate
	)
	VALUES
	(
		@Name,
		@Descriptions,
		@InstitutionId,
		@EntryById,
		@EntryDate
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveDesignations]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveDesignations]
	@Name VARCHAR(100),
	@InstitutionId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	INSERT INTO Designations
	(
		Designations.name,
		Designations.institution_id,
		Designations.entryBy_id,
		Designations.entrydate
	)VALUES
	(
		@Name,
		@InstitutionId,
		@EntryById,
		@EntryDate
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveFaculty]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SaveFaculty]
	@Username VARCHAR(300),
	@NIDNumber VARCHAR(20),
	@DesignationId INT,
	@DepartmentId INT,
	
	@FullName VARCHAR(60),
	@FathersName VARCHAR(60),
	@MothersName VARCHAR(60),
	@Gender VARCHAR(6),
	@DateOfBirth DATE,
	@Nationality VARCHAR(50),
	@Image VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(300),
	@Address VARCHAR(300),

	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive bit,
	@InstitutionId INT,

	@EntryById INT,
	@EntryDate DATETIME

AS
BEGIN
	BEGIN TRANSACTION;
		
		INSERT INTO Login
		(
			Login.username,
			Login.password,
			Login.role,
			Login.isActive,
			Login.institution_id
		)
		VALUES
		(
			@Username,
			@Password,
			@Role,
			@IsActive,
			@InstitutionId
		);

		INSERT INTO PersonalInformations
		(
			PersonalInformations.username,
			PersonalInformations.fullName,
			PersonalInformations.fatherName,
			PersonalInformations.motherName,
			PersonalInformations.gender,
			PersonalInformations.dateofBirth,
			PersonalInformations.nationality,
			PersonalInformations.image,
			PersonalInformations.contact,
			PersonalInformations.email,
			PersonalInformations.address
		)
		VALUES
		(
			@Username,
			@FullName,
			@FathersName,
			@MothersName,
			@Gender,
			@DateOfBirth,
			@Nationality,
			@Image,
			@Contact,
			@Email,
			@Address
		);

		INSERT INTO Faculty

		(
			Faculty.username,
			Faculty.nidNumber,
			Faculty.designation_id,
			Faculty.department_id,
			Faculty.entryby_id, 
			Faculty.entryDate
		)
		VALUES
		(
			@Username, 
			@NIDNumber,
			@DesignationId,
			@DepartmentId,
			@EntryById,
			@EntryDate
		);

	COMMIT TRANSACTION; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveInstitution]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveInstitution]
(
	@Name VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(200),
	@Address VARCHAR(300),
	@Logo VARCHAR(300),
	@RegistrationDate DATETIME,
	@IsActive bit
)
AS
BEGIN
	INSERT INTO Institutions
	(
		Institutions.name,
		Institutions.contact,
		Institutions.email,
		Institutions.address,
		Institutions.logo,
		Institutions.registrationDate,
		Institutions.isActive
	)
	VALUES
	(
		@Name,
		@Contact,
		@Email,
		@Address,
		@Logo,
		@RegistrationDate,
		@IsActive
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveLogin]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveLogin]
	@Username VARCHAR(300),
	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive bit,
	@LastLoginDate DateTime,
	@InstitutionId INT
AS
BEGIN
	INSERT INTO Login
	(
		Login.username,
		Login.password,
		Login.role,
		Login.isActive,
		Login.lastLoginDate,
		Login.institution_id
	)
	VALUES
	(
		@Username,
		@Password,
		@Role,
		@IsActive,
		@LastLoginDate,
		@InstitutionId
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SavePrograms]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SavePrograms] 
	@Name VARCHAR(200),
	@Descriptions VARCHAR(300),
	@DepartmentsId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	INSERT INTO Programs
	(
		Programs.name,
		Programs.descriptions,
		Programs.department_id,
		Programs.entryby_id,
		Programs.entryDate
	)
	VALUES
	(
		@Name,
		@Descriptions,
		@DepartmentsId,
		@EntryById,
		@EntryDate
	);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveStudent]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveStudent]
	@Username VARCHAR(300),
	@GuardianName VARCHAR(60),
	@GuardianContact VARCHAR(14),
	@GuardianAddress VARCHAR(300),

	@ProgramsId INT,
	
	@FullName VARCHAR(60),
	@FathersName VARCHAR(60),
	@MothersName VARCHAR(60),
	@Gender VARCHAR(6),
	@DateOfBirth DATE,
	@Nationality VARCHAR(50),
	@Image VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(300),
	@Address VARCHAR(300),

	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive bit,
	@InstitutionId INT,

	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN 
	BEGIN TRANSACTION;
		INSERT INTO Login
			(
				Login.username,
				Login.password,
				Login.role,
				Login.isActive,
				Login.institution_id
			)
			VALUES
			(
				@Username,
				@Password,
				@Role,
				@IsActive,
				@InstitutionId
			);

		INSERT INTO PersonalInformations
			(
				PersonalInformations.username,
				PersonalInformations.fullName,
				PersonalInformations.fatherName,
				PersonalInformations.motherName,
				PersonalInformations.gender,
				PersonalInformations.dateofBirth,
				PersonalInformations.nationality,
				PersonalInformations.image,
				PersonalInformations.contact,
				PersonalInformations.email,
				PersonalInformations.address
			)
			VALUES
			(
				@Username,
				@FullName,
				@FathersName,
				@MothersName,
				@Gender,
				@DateOfBirth,
				@Nationality,
				@Image,
				@Contact,
				@Email,
				@Address
			);

		INSERT INTO Students

			(
				Students.username,
				Students.program_id,
				Students.guardianName,
				Students.guardianContact,
				Students.guardianAddress,
				Students.entryby_id, 
				Students.entryDate
			)
			VALUES
			(
				@Username, 
				@ProgramsId,
				@GuardianName,
				@GuardianContact,
				@GuardianAddress,
				@EntryById,
				@EntryDate
			);
	COMMIT TRANSACTION; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveTracer]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveTracer]
	@Actor_id INT,
	@ActionName VARCHAR(100),
	@TableName VARCHAR(100),
	@ActionAppliedId INT,
	@ActionTime DATETIME,
	@InstitutionId INT

AS
BEGIN
	
    INSERT INTO Tracer
    (
        Tracer.actor_id,
        Tracer.actionName,
        Tracer.tableName,
        Tracer.actionApplied_id,
        Tracer.actionTime,
        Tracer.institution_id
    )
    VALUES
    (
        @Actor_id,
        @ActionName,
        @TableName,
        @ActionAppliedId,
        @ActionTime,
        @InstitutionId
    );
    
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateDepartments]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateDepartments] 
	@Id INT,
	@Name VARCHAR(200),
	@Descriptions VARCHAR(300),
	@InstitutionId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	UPDATE Departments
	SET
		Departments.name = @Name,
		Departments.descriptions = @Name,
		Departments.institution_id = @InstitutionId,
		Departments.entryby_id = @EntryById,
		Departments.entryDate = @EntryDate
	WHERE Departments.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateDesignations]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateDesignations]
	@Id INT,
	@Name VARCHAR(100),
	@InstitutionId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	UPDATE Designations
	SET 
		Designations.name = @Name,
		Designations.institution_id = @InstitutionId,
		Designations.entryBy_id = @EntryById,
		Designations.entrydate = @EntryDate
	WHERE 
	Designations.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateFaculty]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_UpdateFaculty]
	
	@Id INT,
	@Username VARCHAR(300),
	@NIDNumber VARCHAR(20),
	@DesignationId INT,
	@DepartmentId INT,
	
	@FullName VARCHAR(60),
	@FathersName VARCHAR(60),
	@MothersName VARCHAR(60),
	@Gender VARCHAR(6),
	@DateOfBirth DATE,
	@Nationality VARCHAR(50),
	@Image VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(300),
	@Address VARCHAR(300),

	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive bit,
	@InstitutionId INT,

	@EntryById INT,
	@EntryDate DATETIME

AS
BEGIN
	BEGIN TRANSACTION;
		
		UPDATE  Login
		SET
			Login.username = @Username,
			Login.password = @Password,
			Login.role = @Role,
			Login.isActive = @IsActive,
			Login.institution_id = @InstitutionId
		WHERE 
		Login.username = @Username;


		UPDATE PersonalInformations
		SET
			PersonalInformations.username = @Username,
			PersonalInformations.fullName = @FullName,
			PersonalInformations.fatherName = @FathersName,
			PersonalInformations.motherName = @MothersName,
			PersonalInformations.gender = @Gender,
			PersonalInformations.dateofBirth = @DateOfBirth,
			PersonalInformations.nationality = @Nationality,
			PersonalInformations.image = @Image,
			PersonalInformations.contact = @Contact,
			PersonalInformations.email = @Email,
			PersonalInformations.address = @Email
		WHERE 
		PersonalInformations.username = @Username;

		UPDATE Faculty
		SET
			Faculty.username = @Username,
			Faculty.nidNumber = @NIDNumber,
			Faculty.designation_id = @DesignationId,
			Faculty.department_id = @DepartmentId,
			Faculty.entryby_id = @EntryById, 
			Faculty.entryDate = @EntryDate
		WHERE 
		Faculty.id = @Id;

	COMMIT TRANSACTION; 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateInstitution]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateInstitution]
(
	@Id INT,
	@Name VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(200),
	@Address VARCHAR(300),
	@Logo VARCHAR(300),
	@RegistrationDate DATETIME,
	@IsActive bit
)
AS
BEGIN
	UPDATE Institutions
	SET
		Institutions.name = @Name,
		Institutions.contact= @Contact,
		Institutions.email = @Email,
		Institutions.address= @Address,
		Institutions.logo= @Logo,
		Institutions.registrationDate = @RegistrationDate,
		Institutions.isActive = @IsActive
	
	WHERE Institutions.id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateLogin]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateLogin]
	@Username VARCHAR(300),
	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive BIT,
	@LastLoginDate DateTime,
	@InstitutionId INT
AS
BEGIN
	UPDATE Login
	SET
		Login.password =@Password,
		Login.role = @Role,
		Login.isActive= @IsActive,
		Login.lastLoginDate= @LastLoginDate,
		Login.institution_id = @InstitutionId
	
	WHERE Login.username = @Username;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePrograms]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdatePrograms] 
	@Id INT,
	@Name VARCHAR(200),
	@Descriptions VARCHAR(300),
	@DepartmentsId INT,
	@EntryById INT,
	@EntryDate DATETIME
AS
BEGIN
	UPDATE Programs
	SET
		Programs.name			= @Name,
		Programs.descriptions	= @Name,
		Programs.department_id	= @DepartmentsId,
		Programs.entryby_id		= @EntryById,
		Programs.entryDate		= @EntryDate
	WHERE Programs.id		= @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateStudent]    Script Date: 7/25/2020 6:18:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateStudent]
	@Id INT,
	@Username VARCHAR(300),
	@ProgramsId INT,

	@GuardianName VARCHAR(60),
	@GuardianContact VARCHAR(14),
	@GuardianAddress VARCHAR(300),

	
	@FullName VARCHAR(60),
	@FathersName VARCHAR(60),
	@MothersName VARCHAR(60),
	@Gender VARCHAR(6),
	@DateOfBirth DATE,
	@Nationality VARCHAR(50),
	@Image VARCHAR(200),
	@Contact VARCHAR(14),
	@Email VARCHAR(300),
	@Address VARCHAR(300),

	@Password VARCHAR(300),
	@Role VARCHAR(20),
	@IsActive BIT,
	@InstitutionId INT,

	@EntryById INT,
	@EntryDate DATETIME

AS
BEGIN
	BEGIN TRANSACTION;
		
		UPDATE  Login
		SET
			Login.username	= @Username,
			Login.password	= @Password,
			Login.role		= @Role,
			Login.isActive	= @IsActive,
			Login.institution_id	= @InstitutionId
		WHERE 
		Login.username = @Username;


		UPDATE PersonalInformations
		SET
			PersonalInformations.username		= @Username,
			PersonalInformations.fullName		= @FullName,
			PersonalInformations.fatherName		= @FathersName,
			PersonalInformations.motherName		= @MothersName,
			PersonalInformations.gender			= @Gender,
			PersonalInformations.dateofBirth	= @DateOfBirth,
			PersonalInformations.nationality	= @Nationality,
			PersonalInformations.image			= @Image,
			PersonalInformations.contact		= @Contact,
			PersonalInformations.email			= @Email,
			PersonalInformations.address		= @Email
		WHERE 
		PersonalInformations.username = @Username;

		UPDATE Students
		SET
			Students.username			= @Username,
			Students.program_id			= @ProgramsId,
			Students.guardianName		= @GuardianName,
			Students.guardianContact	= @GuardianContact,
			Students.guardianAddress	= @GuardianAddress,

			Students.entryby_id = @EntryById, 
			Students.entryDate = @EntryDate
		WHERE 
		Students.id = @Id;

	COMMIT TRANSACTION; 
END
GO
