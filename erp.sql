USE [master]
GO
/****** Object:  Database [erptest]    Script Date: 05/12/2017 16:34:41 ******/
CREATE DATABASE [erptest] ON  PRIMARY 
( NAME = N'erptest', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\erptest.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'erptest_log', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\erptest_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [erptest] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [erptest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [erptest] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [erptest] SET ANSI_NULLS OFF
GO
ALTER DATABASE [erptest] SET ANSI_PADDING OFF
GO
ALTER DATABASE [erptest] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [erptest] SET ARITHABORT OFF
GO
ALTER DATABASE [erptest] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [erptest] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [erptest] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [erptest] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [erptest] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [erptest] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [erptest] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [erptest] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [erptest] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [erptest] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [erptest] SET  DISABLE_BROKER
GO
ALTER DATABASE [erptest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [erptest] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [erptest] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [erptest] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [erptest] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [erptest] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [erptest] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [erptest] SET  READ_WRITE
GO
ALTER DATABASE [erptest] SET RECOVERY SIMPLE
GO
ALTER DATABASE [erptest] SET  MULTI_USER
GO
ALTER DATABASE [erptest] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [erptest] SET DB_CHAINING OFF
GO
USE [erptest]
GO
/****** Object:  User [erpuser]    Script Date: 05/12/2017 16:34:41 ******/
CREATE USER [erpuser] FOR LOGIN [erpuser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[vacation_policies]    Script Date: 05/12/2017 16:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vacation_policies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[service_years] [int] NOT NULL,
	[days] [int] NOT NULL,
 CONSTRAINT [PK_vacation_policies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee_types]    Script Date: 05/12/2017 16:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_types](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_employee_types] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[employee_types] ([id], [name]) VALUES (1, N'Regular')
INSERT [dbo].[employee_types] ([id], [name]) VALUES (2, N'HR')
INSERT [dbo].[employee_types] ([id], [name]) VALUES (3, N'Manager')
/****** Object:  Table [dbo].[company_holidays]    Script Date: 05/12/2017 16:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company_holidays](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[holiday_date] [datetime] NOT NULL,
 CONSTRAINT [PK_company_holidays] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employees]    Script Date: 05/12/2017 16:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[password] [varbinary](50) NULL,
	[type_id] [int] NOT NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[phone] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
 CONSTRAINT [PK_employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_employees_type] ON [dbo].[employees] 
(
	[type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[employees] ON
INSERT [dbo].[employees] ([id], [name], [surname], [email], [password], [type_id], [start_date], [end_date], [phone], [position]) VALUES (1, N'test', N'hr', N'email@uu.io', 0x05ACE3BBFCB5AA6A9C822D518E673B440F328EBF, 2, CAST(0x00009CF100000000 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[employees] ([id], [name], [surname], [email], [password], [type_id], [start_date], [end_date], [phone], [position]) VALUES (2, N'some', N'surname', N'me@me.to', 0x05ACE3BBFCB5AA6A9C822D518E673B440F328EBF, 1, CAST(0x0000A04700000000 AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[employees] OFF
/****** Object:  Table [dbo].[vacation_requests]    Script Date: 05/12/2017 16:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vacation_requests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[approved] [bit] NOT NULL,
 CONSTRAINT [PK_vacation_requests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_vacation_requests_employee] ON [dbo].[vacation_requests] 
(
	[employee_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[uspAddEmployee]    Script Date: 05/12/2017 16:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[uspAddEmployee]
	@name NVARCHAR(50) = '',
	@surname NVARCHAR(50) = '',
	@email NVARCHAR(50),
	@password varbinary(50),
	@type INT,
	@startdate DATETIME = GETDATE,
	@enddate DATETIME = NULL,
	@phone NVARCHAR(50) = NULL,
	@position NVARCHAR(50) = NULL,
	@error NVARCHAR(500) OUTPUT,
	@pk_emp INT OUTPUT,
	@inserted BIT OUTPUT
AS
BEGIN TRANSACTION
	BEGIN TRY
		INSERT INTO employees (name, surname, email, [password], [type_id], [start_date], end_date, phone, position) 
		VALUES (@name, @surname, @email, @password, @type, @startdate, @enddate,@phone, @position)
		SET @pk_emp = ( SELECT @@IDENTITY )
		SET @inserted = 1
	END TRY
	
	BEGIN CATCH
		SET @error = '<b>ErrorNumber</b> : ' + CAST(ERROR_NUMBER() AS CHAR(10)) + '<br/><b>ErrorSeverity</b> : ' + CAST(ERROR_SEVERITY() AS CHAR(10)) + 
		'<br/><b>ErrorState</b> : ' + CAST(ERROR_STATE() AS CHAR(10)) + '<br/><b>ErrorProcedure</b> : ' + ERROR_PROCEDURE() + 
		'<br/><b>ErrorLine</b> : ' + CAST(ERROR_LINE() AS CHAR(10)) + '<br/><b>ErrorMessage</b> : ' + ERROR_MESSAGE()
		SET @inserted = 0
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0 BEGIN
		--SET @error = ''
		COMMIT TRANSACTION
	END
GO
/****** Object:  Default [DF_employees_type_id]    Script Date: 05/12/2017 16:34:42 ******/
ALTER TABLE [dbo].[employees] ADD  CONSTRAINT [DF_employees_type_id]  DEFAULT ((1)) FOR [type_id]
GO
/****** Object:  ForeignKey [FK_employees_employee_types]    Script Date: 05/12/2017 16:34:42 ******/
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_employee_types] FOREIGN KEY([type_id])
REFERENCES [dbo].[employee_types] ([id])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_employee_types]
GO
/****** Object:  ForeignKey [FK_vacation_requests_employees]    Script Date: 05/12/2017 16:34:42 ******/
ALTER TABLE [dbo].[vacation_requests]  WITH CHECK ADD  CONSTRAINT [FK_vacation_requests_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([id])
GO
ALTER TABLE [dbo].[vacation_requests] CHECK CONSTRAINT [FK_vacation_requests_employees]
GO
