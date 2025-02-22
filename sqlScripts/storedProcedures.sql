﻿CREATE DATABASE [StudentManager]
GO

USE [StudentManager]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 07/03/2020 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddStudent]    Script Date: 07/03/2020 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudent](@student_name as varchar(50), @student_surname as varchar(50), @student_birth_date as date, @student_guid as uniqueidentifier)
AS
SET NOCOUNT ON;
INSERT INTO Student (Name, Lastname, Birthday, Guid)
VALUES (@student_name, @student_surname, @student_birth_date, @student_guid);
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 07/03/2020 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudent](@id as int)
AS
SET NOCOUNT ON;
DELETE FROM Student
WHERE Id = @id;
GO
/****** Object:  StoredProcedure [dbo].[GetAllStudents]    Script Date: 07/03/2020 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllStudents]
AS
SET NOCOUNT ON;
SELECT * FROM Student;
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 07/03/2020 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudent](@id as int, @student_name as varchar(50), @student_surname as varchar(50), @student_birth_date as date)
AS
SET NOCOUNT ON;
UPDATE Student
SET Name = @student_name, Lastname = @student_surname, Birthday = @student_birth_date
WHERE Id = @id;
GO
