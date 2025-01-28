USE [master]
GO
/****** Object:  Database [DuaSaudaraFinance]    Script Date: 01/12/2021 11:33:28 ******/
CREATE DATABASE [DuaSaudaraFinance] ON  PRIMARY 
( NAME = N'DuaSaudaraFinance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DuaSaudaraFinance.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DuaSaudaraFinance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DuaSaudaraFinance_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DuaSaudaraFinance] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DuaSaudaraFinance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DuaSaudaraFinance] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET ARITHABORT OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DuaSaudaraFinance] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DuaSaudaraFinance] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DuaSaudaraFinance] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET  DISABLE_BROKER
GO
ALTER DATABASE [DuaSaudaraFinance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DuaSaudaraFinance] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DuaSaudaraFinance] SET  READ_WRITE
GO
ALTER DATABASE [DuaSaudaraFinance] SET RECOVERY FULL
GO
ALTER DATABASE [DuaSaudaraFinance] SET  MULTI_USER
GO
ALTER DATABASE [DuaSaudaraFinance] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DuaSaudaraFinance] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'DuaSaudaraFinance', N'ON'
GO
USE [DuaSaudaraFinance]
GO
/****** Object:  StoredProcedure [dbo].[spDELETE_DaftarTransaksi_button2_Click]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spDELETE_DaftarTransaksi_button2_Click]
GO
/****** Object:  StoredProcedure [dbo].[spINSERT_TambahTransaksi_button1_Click]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spINSERT_TambahTransaksi_button1_Click]
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_DaftarTransaksi_fillListView1]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spSELECT_DaftarTransaksi_fillListView1]
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_DaftarTransaksi_fillTextBox1]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spSELECT_DaftarTransaksi_fillTextBox1]
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_fillComboBox1]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spSELECT_TambahTransaksi_fillComboBox1]
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_fillComboBox2]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spSELECT_TambahTransaksi_fillComboBox2]
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_TambahTransaksi_Load]    Script Date: 01/12/2021 11:33:30 ******/
DROP PROCEDURE [dbo].[spSELECT_TambahTransaksi_TambahTransaksi_Load]
GO
/****** Object:  Table [dbo].[tblParameter]    Script Date: 01/12/2021 11:33:29 ******/
DROP TABLE [dbo].[tblParameter]
GO
/****** Object:  Table [dbo].[tblPaymentType]    Script Date: 01/12/2021 11:33:29 ******/
DROP TABLE [dbo].[tblPaymentType]
GO
/****** Object:  Table [dbo].[tblTransaction]    Script Date: 01/12/2021 11:33:29 ******/
DROP TABLE [dbo].[tblTransaction]
GO
/****** Object:  Table [dbo].[tblTransactionItem]    Script Date: 01/12/2021 11:33:29 ******/
DROP TABLE [dbo].[tblTransactionItem]
GO
/****** Object:  User [developer]    Script Date: 01/12/2021 11:33:28 ******/
DROP USER [developer]
GO
/****** Object:  User [developer]    Script Date: 01/12/2021 11:33:28 ******/
CREATE USER [developer] FOR LOGIN [developer] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[tblTransactionItem]    Script Date: 01/12/2021 11:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTransactionItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TransName] [varchar](50) NULL,
	[IsIn] [bit] NULL,
	[TemplateAmount] [money] NULL,
 CONSTRAINT [PK_tblTransactionItem] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblTransactionItem] ON
INSERT [dbo].[tblTransactionItem] ([id], [TransName], [IsIn], [TemplateAmount]) VALUES (1, N'Pemasukan Lain', 1, 0.0000)
INSERT [dbo].[tblTransactionItem] ([id], [TransName], [IsIn], [TemplateAmount]) VALUES (2, N'Pengeluaran Lain', 0, 0.0000)
INSERT [dbo].[tblTransactionItem] ([id], [TransName], [IsIn], [TemplateAmount]) VALUES (3, N'Bayar Tukang Jahit Kori', 0, 80000.0000)
INSERT [dbo].[tblTransactionItem] ([id], [TransName], [IsIn], [TemplateAmount]) VALUES (4, N'Bayar Tukang Jahit Angga', 0, 80000.0000)
INSERT [dbo].[tblTransactionItem] ([id], [TransName], [IsIn], [TemplateAmount]) VALUES (5, N'Tanpa Barcode Penjualan Baju OK', 1, 180000.0000)
SET IDENTITY_INSERT [dbo].[tblTransactionItem] OFF
/****** Object:  Table [dbo].[tblTransaction]    Script Date: 01/12/2021 11:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTransaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateTrans] [datetime] NULL,
	[TransItemId] [int] NULL,
	[Amount] [money] NULL,
	[TransDesc] [varchar](max) NULL,
	[PaymentTypeId] [int] NULL,
 CONSTRAINT [PK_tblTransaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblTransaction] ON
INSERT [dbo].[tblTransaction] ([id], [DateTrans], [TransItemId], [Amount], [TransDesc], [PaymentTypeId]) VALUES (1, CAST(0x0000AC18018B73F0 AS DateTime), 1, 1000000.0000, N'Testing', NULL)
INSERT [dbo].[tblTransaction] ([id], [DateTrans], [TransItemId], [Amount], [TransDesc], [PaymentTypeId]) VALUES (8, CAST(0x0000AC6300D68A44 AS DateTime), 1, 75000.0000, N'asdfsdfgsd', NULL)
INSERT [dbo].[tblTransaction] ([id], [DateTrans], [TransItemId], [Amount], [TransDesc], [PaymentTypeId]) VALUES (13, CAST(0x0000ACA9007EC138 AS DateTime), 1, 100000.0000, N'', 1)
SET IDENTITY_INSERT [dbo].[tblTransaction] OFF
/****** Object:  Table [dbo].[tblPaymentType]    Script Date: 01/12/2021 11:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPaymentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeName] [varchar](20) NOT NULL,
	[IsIn] [tinyint] NULL,
	[Sorting] [int] NULL,
 CONSTRAINT [PK_tblPaymentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblPaymentType] ON
INSERT [dbo].[tblPaymentType] ([Id], [PaymentTypeName], [IsIn], [Sorting]) VALUES (1, N'Cash', 2, 1)
INSERT [dbo].[tblPaymentType] ([Id], [PaymentTypeName], [IsIn], [Sorting]) VALUES (2, N'Transfer BCA', 2, 2)
INSERT [dbo].[tblPaymentType] ([Id], [PaymentTypeName], [IsIn], [Sorting]) VALUES (3, N'Debit BCA', 1, 3)
INSERT [dbo].[tblPaymentType] ([Id], [PaymentTypeName], [IsIn], [Sorting]) VALUES (4, N'Debit BNI', 1, 4)
SET IDENTITY_INSERT [dbo].[tblPaymentType] OFF
/****** Object:  Table [dbo].[tblParameter]    Script Date: 01/12/2021 11:33:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParameter](
	[ParamName] [varchar](50) NULL,
	[ParamValue] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tblParameter] ([ParamName], [ParamValue]) VALUES (N'LastAmount', N'10000')
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_TambahTransaksi_Load]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSELECT_TambahTransaksi_TambahTransaksi_Load]
	@TransId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		tbl1.DateTrans,
		tbl1.TransDesc,
		tbl1.Amount,
		tbl1.TransItemId,
		tbl2.IsIn	
	FROM tblTransaction tbl1
		LEFT JOIN tblTransactionItem tbl2
			ON tbl1.TransItemId = tbl2.id
	WHERE tbl1.id = @TransId
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_fillComboBox2]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSELECT_TambahTransaksi_fillComboBox2]
	@IsIn int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		Id,
		PaymentTypeName
	FROM tblPaymentType
	WHERE (IsIn = @IsIn OR IsIn = 2)
	ORDER BY Sorting
END
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_TambahTransaksi_fillComboBox1]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSELECT_TambahTransaksi_fillComboBox1]
	@IsIn int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		id,
		TransName
	FROM tblTransactionItem
	WHERE (IsIn = @IsIn OR 2 = @IsIn)
END
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_DaftarTransaksi_fillTextBox1]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSELECT_DaftarTransaksi_fillTextBox1]
	@StartDate varchar(8),
	@EndDate varchar(8)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    DECLARE @LastAmount money
    
    SELECT
		@LastAmount = ParamValue    
    FROM tblParameter
    WHERE ParamName = 'LastAmount'
      
    
    
	SELECT
		ISNULL(SUM(CASE WHEN tbl2.IsIn = 0 THEN -1*tbl1.Amount ELSE tbl1.Amount END),0) + @LastAmount AS CurrentAmount	
	FROM tblTransaction tbl1
		LEFT JOIN tblTransactionItem tbl2
			ON tbl1.TransItemId = tbl2.id
	WHERE CONVERT(VARCHAR(8),tbl1.DateTrans,112) <= @EndDate
	
END
GO
/****** Object:  StoredProcedure [dbo].[spSELECT_DaftarTransaksi_fillListView1]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSELECT_DaftarTransaksi_fillListView1]
	@StartDate varchar(8),
	@EndDate varchar(8),
	@IsIn int,
	@TransItemId int,
	@CashOnly int
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	
	
	DECLARE @LastAmount money
    
    SELECT
		@LastAmount = ISNULL(ParamValue,0)
    FROM tblParameter
    WHERE ParamName = 'LastAmount'
    
    SELECT
		@LastAmount = ISNULL(SUM(CASE WHEN tbl2.IsIn = 0 THEN -1*tbl1.Amount ELSE tbl1.Amount END),0) + @LastAmount
	FROM tblTransaction tbl1
		LEFT JOIN tblTransactionItem tbl2
			ON tbl1.TransItemId = tbl2.id
	WHERE tbl1.DateTrans < @StartDate
	
	
	-----------------------------------------------------------------------------------------------------------------
	
	IF @CashOnly = 0
		BEGIN
		
		
			SELECT
				0 AS Id,
				@StartDate AS DateTrans,
				'Setor' AS TransName,
				'' AS Payment,
				@LastAmount AS Masuk,
				0 AS Keluar,
				'' AS TransDesc
				
			UNION
			
			

			SELECT
				tbl1.id,
				tbl1.DateTrans,
				tbl2.TransName,
				tbl3.PaymentTypeName,
				CASE WHEN tbl2.IsIn = 1 THEN tbl1.Amount ELSE 0 END Masuk,
				CASE WHEN tbl2.IsIn = 0 THEN tbl1.Amount ELSE 0 END Keluar,
				tbl1.TransDesc    
			FROM tblTransaction tbl1
				LEFT JOIN tblTransactionItem tbl2
					ON tbl1.TransItemId = tbl2.id
				LEFT JOIN tblPaymentType tbl3
					ON tbl1.PaymentTypeId = tbl3.Id
			WHERE CONVERT(VARCHAR(8), tbl1.DateTrans,112) >= @StartDate
				AND CONVERT(VARCHAR(8), tbl1.DateTrans,112) <= @EndDate
				AND (tbl2.IsIn = @IsIn OR 2 = @IsIn)
				AND (tbl1.TransItemId = @TransItemId OR 0 = @TransItemId)
				
		END
	ELSE
		BEGIN
			SELECT
				0 AS Id,
				@StartDate AS DateTrans,
				'Setor' AS TransName,
				'' AS Payment,
				@LastAmount AS Masuk,
				0 AS Keluar,
				'' AS TransDesc
				
			UNION
			
			

			SELECT
				tbl1.id,
				tbl1.DateTrans,
				tbl2.TransName,
				tbl3.PaymentTypeName,
				CASE WHEN tbl2.IsIn = 1 THEN tbl1.Amount ELSE 0 END Masuk,
				CASE WHEN tbl2.IsIn = 0 THEN tbl1.Amount ELSE 0 END Keluar,
				tbl1.TransDesc    
			FROM tblTransaction tbl1
				LEFT JOIN tblTransactionItem tbl2
					ON tbl1.TransItemId = tbl2.id
				LEFT JOIN tblPaymentType tbl3
					ON tbl1.PaymentTypeId = tbl3.Id
			WHERE CONVERT(VARCHAR(8), tbl1.DateTrans,112) >= @StartDate
				AND CONVERT(VARCHAR(8), tbl1.DateTrans,112) <= @EndDate
				AND (tbl2.IsIn = @IsIn OR 2 = @IsIn)
				AND (tbl1.TransItemId = @TransItemId OR 0 = @TransItemId)
				AND PaymentTypeId = 0
		
		END
		
END
GO
/****** Object:  StoredProcedure [dbo].[spINSERT_TambahTransaksi_button1_Click]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spINSERT_TambahTransaksi_button1_Click]
	@DateTrans datetime,
	@TransItemId int,
	@Amount money,
	@TransDesc varchar(max),
	@TransId int,
	@PaymentTypeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	--------------------------------------------------------------------	
	
	IF @TransId <> 0
		BEGIN
			DELETE FROM tblTransaction WHERE id = @TransId		
		END
	
	INSERT INTO tblTransaction VALUES (
	
		@DateTrans,
		@TransItemId,
		@Amount,
		@TransDesc,
		@PaymentTypeId
	
	)
END
GO
/****** Object:  StoredProcedure [dbo].[spDELETE_DaftarTransaksi_button2_Click]    Script Date: 01/12/2021 11:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDELETE_DaftarTransaksi_button2_Click]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM tblTransaction
	WHERE id = @Id
	
END
GO
