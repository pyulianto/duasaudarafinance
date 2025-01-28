USE [DuaSaudaraFinance]
GO
/****** Object:  Table [dbo].[tblAttributeSetBarang]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeSetBarang]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeSetBarang]
GO
/****** Object:  Table [dbo].[tblAttributeLengan]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeLengan]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeLengan]
GO
/****** Object:  Table [dbo].[tblAttributeJenisKelamin]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeJenisKelamin]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeJenisKelamin]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKWarna]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeBajuOKWarna]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeBajuOKWarna]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKSet]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeBajuOKSet]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeBajuOKSet]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKJenis]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeBajuOKJenis]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeBajuOKJenis]
GO
/****** Object:  Table [dbo].[tblAttributeBahan]    Script Date: 9/29/2023 1:07:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAttributeBahan]') AND type in (N'U'))
DROP TABLE [dbo].[tblAttributeBahan]
GO
/****** Object:  Table [dbo].[tblAttributeBahan]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeBahan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaBahan] [varchar](30) NOT NULL,
 CONSTRAINT [PK_tblAttributeBahan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKJenis]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeBajuOKJenis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaJenisBajuJaga] [varchar](100) NULL,
 CONSTRAINT [PK_tblAttributeJenisBajuJaga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKSet]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeBajuOKSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaSetBajuOK] [varchar](100) NULL,
 CONSTRAINT [PK_tblAttributeSetBajuOK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeBajuOKWarna]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeBajuOKWarna](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaWarnaBajuJaga] [varchar](100) NULL,
 CONSTRAINT [PK_tblAttributeWarnaBajuJaga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeJenisKelamin]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeJenisKelamin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaJenisKelamin] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblAttributeJenisKelamin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeLengan]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeLengan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaLengan] [varchar](30) NOT NULL,
 CONSTRAINT [PK_tblAttributeLengan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAttributeSetBarang]    Script Date: 9/29/2023 1:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttributeSetBarang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaSetBarang] [varchar](100) NULL,
 CONSTRAINT [PK_tblAttributeSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblAttributeBahan] ON 

INSERT [dbo].[tblAttributeBahan] ([Id], [NamaBahan]) VALUES (1, N'Serat')
INSERT [dbo].[tblAttributeBahan] ([Id], [NamaBahan]) VALUES (2, N'Halus')
INSERT [dbo].[tblAttributeBahan] ([Id], [NamaBahan]) VALUES (3, N'Platinum')
SET IDENTITY_INSERT [dbo].[tblAttributeBahan] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKJenis] ON 

INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (1, N'Standard')
INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (2, N'Toyobo')
INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (3, N'Denim')
INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (4, N'Cargo Toyobo')
INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (5, N'Allura')
INSERT [dbo].[tblAttributeBajuOKJenis] ([Id], [NamaJenisBajuJaga]) VALUES (6, N'Joger Cargo')
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKJenis] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKSet] ON 

INSERT [dbo].[tblAttributeBajuOKSet] ([Id], [NamaSetBajuOK]) VALUES (1, N'Stel Baju Pendek')
INSERT [dbo].[tblAttributeBajuOKSet] ([Id], [NamaSetBajuOK]) VALUES (2, N'Stel Baju Panjang')
INSERT [dbo].[tblAttributeBajuOKSet] ([Id], [NamaSetBajuOK]) VALUES (3, N'Baju Pendek Saja')
INSERT [dbo].[tblAttributeBajuOKSet] ([Id], [NamaSetBajuOK]) VALUES (4, N'Baju Panjang Saja')
INSERT [dbo].[tblAttributeBajuOKSet] ([Id], [NamaSetBajuOK]) VALUES (1002, N'Celana Saja')
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKSet] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKWarna] ON 

INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (1, N'Marun')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (2, N'Baby Blue')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (3, N'Silver Grey')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (4, N'Fuji')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (5, N'Deep Tosca')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (6, N'Lavender Lilac')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (7, N'Biru Elektrik')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (8, N'Hitam')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (9, N'Baby Pink')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (10, N'Dongker')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (11, N'Dastie Pink')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (12, N'Torquise')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (13, N'Putih')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (14, N'Lime')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (15, N'Teracota')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (16, N'Kenari')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (17, N'Army')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (18, N'Magenta')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (19, N'Matcha')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (20, N'Khaki (Cokmud)')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (21, N'Blue Kdrama')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (22, N'Purple BTS')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (23, N'Camel')
INSERT [dbo].[tblAttributeBajuOKWarna] ([Id], [NamaWarnaBajuJaga]) VALUES (24, N'Abu Corak')
SET IDENTITY_INSERT [dbo].[tblAttributeBajuOKWarna] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeJenisKelamin] ON 

INSERT [dbo].[tblAttributeJenisKelamin] ([Id], [NamaJenisKelamin]) VALUES (1, N'Cowo')
INSERT [dbo].[tblAttributeJenisKelamin] ([Id], [NamaJenisKelamin]) VALUES (2, N'Cewe')
SET IDENTITY_INSERT [dbo].[tblAttributeJenisKelamin] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeLengan] ON 

INSERT [dbo].[tblAttributeLengan] ([Id], [NamaLengan]) VALUES (1, N'Pendek')
INSERT [dbo].[tblAttributeLengan] ([Id], [NamaLengan]) VALUES (2, N'Panjang')
SET IDENTITY_INSERT [dbo].[tblAttributeLengan] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAttributeSetBarang] ON 

INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (1, N'Platinum Baju + Celana Resleting')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (2, N'Platinum Baju + Celana Karet Keliling')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (3, N'Platinum Baju Saja')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (4, N'Platinum Celana Resleting')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (5, N'Platinum Celana Karet Keliling')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (6, N'Drill Baju + Celana Resleting')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (7, N'Drill Baju + Celana Karet Keliling')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (8, N'Drill Baju Saja')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (9, N'Drill Celana Resleting')
INSERT [dbo].[tblAttributeSetBarang] ([Id], [NamaSetBarang]) VALUES (10, N'Drill Celana Karet Keliling')
SET IDENTITY_INSERT [dbo].[tblAttributeSetBarang] OFF
GO
