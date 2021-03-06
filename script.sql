USE [master]
GO
/****** Object:  Database [RecipeDB]    Script Date: 24/07/2022 01:46:50 ******/
CREATE DATABASE [RecipeDB]
GO
ALTER DATABASE [RecipeDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RecipeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RecipeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RecipeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RecipeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RecipeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RecipeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RecipeDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RecipeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RecipeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RecipeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RecipeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RecipeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RecipeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RecipeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RecipeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RecipeDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RecipeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RecipeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RecipeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RecipeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RecipeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RecipeDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [RecipeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RecipeDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RecipeDB] SET  MULTI_USER 
GO
ALTER DATABASE [RecipeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RecipeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RecipeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RecipeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RecipeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RecipeDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RecipeDB', N'ON'
GO
ALTER DATABASE [RecipeDB] SET QUERY_STORE = OFF
GO
USE [RecipeDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CollectionRecipes]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionRecipes](
	[CollectionId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
 CONSTRAINT [PK_CollectionRecipes] PRIMARY KEY CLUSTERED 
(
	[CollectionId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[CollectionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Collections] PRIMARY KEY CLUSTERED 
(
	[CollectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[CommentStatus] [int] NOT NULL,
	[ParentCommentId] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeaturedTags]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeaturedTags](
	[FeaturedTagId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_FeaturedTags] PRIMARY KEY CLUSTERED 
(
	[FeaturedTagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[IngredientId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reactions]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reactions](
	[UserId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
	[ReactionType] [int] NOT NULL,
 CONSTRAINT [PK_Reactions] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Duration] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[RecipeStatus] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Steps]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Steps](
	[StepId] [int] IDENTITY(1,1) NOT NULL,
	[Order] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[RecipeId] [int] NOT NULL,
 CONSTRAINT [PK_Steps] PRIMARY KEY CLUSTERED 
(
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24/07/2022 01:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Avatar] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
	[RequestToVIP] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220711223854_InitialDB', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220719135047_CommentReplies', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220722082838_Reports', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220722111821_ReactionTypes', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220723111836_PremiumStatus', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (1, 2, 3, N'Very good', 2, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (2, 2, 3, N'help', 2, 1)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (3, 2, 3, N'haha', 2, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (4, 2, 3, N'aa', 2, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (5, 2, 3, N'kk', 0, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (6, 2, 420, N'aaa', 2, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (7, 2, 3, N'aa', 0, NULL)
INSERT [dbo].[Comments] ([CommentId], [UserId], [RecipeId], [Text], [CommentStatus], [ParentCommentId]) VALUES (8, 2, 3, N'aa', 0, 5)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipes] ON 

INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (3, N'Cake', N'Delicious', N'2 hours', NULL, CAST(N'2022-12-02T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (320, N'Kimberly Parker', N'Hamish Mcconnell', N'Sarah Skinner', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (321, N'Damian Thornton', N'Amena Rosa', N'Candice Ford', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (322, N'Dane Rasmussen', N'Cally Anderson', N'Elaine Cunningham', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (323, N'Tyrone Dillard', N'Kiara Mcgee', N'Gary Burgess', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (324, N'Brett Koch', N'Asher Bowman', N'Hashim Richmond', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (325, N'Ruby Juarez', N'Graham Lyons', N'Diana Brooks', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (326, N'Otto Mercer', N'Brent Lane', N'Audra Lara', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (327, N'Imani Cardenas', N'Vincent William', N'Mechelle Rodgers', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (328, N'Dustin Nunez', N'Cecilia Gray', N'Jael Stout', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (329, N'Timothy Jackson', N'Erich Fernandez', N'Ishmael Fisher', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (330, N'Mufutau Lynn', N'Wyatt Nash', N'Ciaran Evans', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (331, N'Teegan Parks', N'Angela Baker', N'August Cash', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (332, N'Larissa Wilkinson', N'Ferdinand Gomez', N'Vernon Landry', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (333, N'Caldwell Rowland', N'Tamara Peterson', N'Sonya Greer', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (334, N'Bradley Fuentes', N'Dexter Hensley', N'Paloma Tucker', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (335, N'Eleanor Lamb', N'Honorato Diaz', N'Indira Valenzuela', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (336, N'Leonard Whitney', N'Jin Ryan', N'Zane Robertson', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (337, N'Amelia Boone', N'Gwendolyn Grant', N'Clio Burgess', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (338, N'John Dyer', N'Roary Bell', N'Raja Caldwell', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (339, N'Regina Gillespie', N'Lacy Sharp', N'Delilah Shaffer', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (340, N'Serina Giles', N'Joseph Shelton', N'Hop Copeland', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (341, N'Shad Oneil', N'Charissa Ray', N'Kennedy Conrad', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (342, N'Nelle Kaufman', N'Daquan Knowles', N'Bruno Duke', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (343, N'Justina Puckett', N'Amos Cooley', N'Talon Woodward', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (344, N'Leah Rosales', N'Abdul Whitehead', N'Flavia Klein', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (345, N'Cheryl Green', N'Hedwig Christensen', N'Zia Simon', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (346, N'Isadora Mooney', N'Cooper Shelton', N'Octavia Alvarado', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (347, N'Delilah Barrera', N'Elvis Maxwell', N'Abbot Carpenter', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (348, N'Samson Terry', N'Freya Peterson', N'Unity Wallace', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (349, N'Raphael Langley', N'Patrick Justice', N'Lana Porter', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (350, N'Roanna Vazquez', N'Melyssa Adams', N'Octavius Alvarado', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (351, N'Valentine Head', N'Brent Taylor', N'Teagan Hoover', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (352, N'Idona Hart', N'Olga Mccoy', N'Matthew Chang', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (353, N'Basia Mueller', N'Catherine Douglas', N'Aretha Albert', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (354, N'Chandler Taylor', N'Darius Mcdowell', N'Mohammad Bauer', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (355, N'Shelley Wooten', N'Dorian Ferguson', N'Nerea William', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (356, N'John Ross', N'Warren Wooten', N'Sonia Jimenez', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (357, N'Yvonne Ward', N'Fitzgerald Humphrey', N'Larissa House', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (358, N'Chloe Gross', N'Hillary Mcneil', N'Laith Goodwin', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (359, N'Jordan Goodwin', N'Shoshana Dixon', N'Aiko Flowers', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (360, N'Quinlan Williamson', N'Ali Melendez', N'Lesley Weber', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (361, N'Jocelyn Foley', N'Callum Alvarez', N'Ishmael Sykes', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (362, N'Debra Faulkner', N'Owen Castaneda', N'Henry Durham', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (363, N'Yasir Jordan', N'Mia Dillard', N'Amena Griffith', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (364, N'Audra Shields', N'Sybil Shepard', N'Lillith Cotton', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (365, N'Cullen Raymond', N'Kasimir Robinson', N'Raphael Browning', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (366, N'Inez Gilliam', N'Ivy Dixon', N'Noelle Ramirez', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (367, N'Martena Ellison', N'Matthew Britt', N'Zeph Jones', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (368, N'Vielka Nielsen', N'Silas Talley', N'Sawyer Hebert', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (369, N'Lee Weiss', N'Quinlan Gibson', N'Brenden Bolton', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (370, N'Addison Miranda', N'Hermione Wilder', N'William Strong', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (371, N'Bethany Mcdaniel', N'Edan Klein', N'Zane Holt', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (372, N'Beck Cote', N'Grady Pope', N'Mira Long', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (373, N'Tamekah Hull', N'Sacha Santos', N'Kelly Kirkland', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (374, N'Levi Morales', N'Xander Long', N'Fredericka Ward', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (375, N'Felix Kent', N'Arsenio Calhoun', N'Velma Bates', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (376, N'Belle Henry', N'Herman Ingram', N'Harding Joyce', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (377, N'Jelani Clayton', N'Madison Juarez', N'Sacha Buchanan', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (378, N'Brett Mcintyre', N'Lunea Mercado', N'Bertha Sheppard', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (379, N'Jonah Henry', N'Ginger Bright', N'MacKensie Figueroa', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (380, N'Jescie Knowles', N'Gavin Harrell', N'Hedwig Guzman', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (381, N'Fuller Donaldson', N'Marny Grimes', N'Tarik Patel', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (382, N'Zachary Bryant', N'Edan Moses', N'Veronica Cortez', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (383, N'Adara Lindsay', N'Shea Reed', N'Timon Robles', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (384, N'Uriah Chambers', N'Amelia Key', N'Cora Gray', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (385, N'Liberty George', N'Boris Stein', N'Isaiah Horne', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (386, N'Kitra King', N'Harrison Ball', N'Cole Alexander', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (387, N'Melinda Joyner', N'Oleg Goodman', N'Allegra Oliver', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (388, N'Blake Ayala', N'Glenna Bentley', N'Orli Taylor', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (389, N'Brock Heath', N'Jason Dorsey', N'Micah Tanner', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (390, N'Candace Mcdowell', N'Travis Cortez', N'Xena Morrow', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (391, N'Iris Snider', N'Martina Holt', N'Olympia Randall', NULL, CAST(N'2022-07-15T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (392, N'Kevin Barnes', N'Hamilton Mercado', N'Drake Henry', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (393, N'Yardley Snider', N'Kirsten Snow', N'Kirk Riggs', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (394, N'Jelani Burns', N'Brennan Steele', N'Myles Case', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (395, N'Hyatt Poole', N'Emerson Dillard', N'Debra Cameron', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (396, N'Hasad Lane', N'Quynn Garcia', N'Devin Baxter', NULL, CAST(N'2022-07-19T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (397, N'Dolan Matthews', N'Zachary Bonner', N'Eaton Solomon', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (398, N'Allistair Weeks', N'Lysandra Goff', N'Isaac Gallagher', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (399, N'Abraham Ware', N'Marshall Valdez', N'Samantha Stafford', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (400, N'Lillian Wilder', N'Rajah Ochoa', N'Blythe Clarke', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (401, N'Amity Hubbard', N'Isaac Farley', N'Dieter Foreman', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (402, N'Harper Lowe', N'Alden Anderson', N'Caesar Hardin', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (403, N'Coby Harris', N'Galvin Herman', N'Madaline Murphy', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (404, N'Palmer Carter', N'Kyra Wells', N'Dalton Alston', NULL, CAST(N'2022-07-17T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (405, N'Helen Wise', N'Courtney Parker', N'Erin Parsons', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (406, N'Anthony Hamilton', N'Emmanuel Hendricks', N'Quemby Carpenter', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (407, N'Bo Curtis', N'August Higgins', N'Xaviera Frye', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (408, N'Alfonso Charles', N'Lacota Oneil', N'Bertha Nolan', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (409, N'Colette Sherman', N'Caryn Schwartz', N'Kelly Sweet', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (410, N'Rhoda Mueller', N'Jaden Holcomb', N'Rajah Elliott', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (411, N'Bree Carter', N'Sebastian Duke', N'Diana Ford', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (412, N'Malcolm Curry', N'Patience Wilkins', N'Ahmed Newton', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (413, N'Amos Charles', N'Keiko Tyler', N'Leonard Wynn', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (414, N'Imelda Douglas', N'Bernard Arnold', N'Noelani Bradley', NULL, CAST(N'2022-07-21T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (415, N'Denise Cantrell', N'Zelda Mcbride', N'Justina Phelps', NULL, CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (416, N'Phelan Bradley', N'Larissa Yates', N'Maisie Copeland', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (417, N'Abel Clarke', N'Colorado Riddle', N'Miranda Quinn', NULL, CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), 1, 2)
GO
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (418, N'Tiger Hubbard', N'Jameson Martin', N'Kane Dale', NULL, CAST(N'2022-07-18T00:00:00.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (419, N'Jolie Gilmore', N'Xantha Hatfield', N'Daniel Bright', NULL, CAST(N'2022-07-16T00:00:00.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[Recipes] ([RecipeId], [Name], [Description], [Duration], [Image], [DateCreated], [RecipeStatus], [UserId]) VALUES (420, NULL, NULL, NULL, NULL, CAST(N'2022-07-23T15:12:48.0260868' AS DateTime2), 0, 2)
SET IDENTITY_INSERT [dbo].[Recipes] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, N'Premium User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Email], [Password], [Name], [Avatar], [RoleId], [RequestToVIP]) VALUES (2, N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'John', N'pepe-sad-comfy-wearing-confident-face-mask-thumbnail.png', 1, 0)
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Name], [Avatar], [RoleId], [RequestToVIP]) VALUES (3, N'premium', N'123456', N'thuy', NULL, 3, 0)
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Name], [Avatar], [RoleId], [RequestToVIP]) VALUES (4, N'normal', N'E10ADC3949BA59ABBE56E057F20F883E', N'giang', NULL, 2, 0)
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Name], [Avatar], [RoleId], [RequestToVIP]) VALUES (5, N'hue', N'E10ADC3949BA59ABBE56E057F20F883E', N'hue', N'', 3, 1)
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Name], [Avatar], [RoleId], [RequestToVIP]) VALUES (6, N'adf', N'E10ADC3949BA59ABBE56E057F20F883E', N'avc', N'avatar.png', 2, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_CollectionRecipes_RecipeId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_CollectionRecipes_RecipeId] ON [dbo].[CollectionRecipes]
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Collections_UserId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Collections_UserId] ON [dbo].[Collections]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_ParentCommentId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_ParentCommentId] ON [dbo].[Comments]
(
	[ParentCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_RecipeId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_RecipeId] ON [dbo].[Comments]
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Ingredients_RecipeId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Ingredients_RecipeId] ON [dbo].[Ingredients]
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notifications_UserId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_UserId] ON [dbo].[Notifications]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reactions_RecipeId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Reactions_RecipeId] ON [dbo].[Reactions]
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recipes_UserId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Recipes_UserId] ON [dbo].[Recipes]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reports_CommentId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Reports_CommentId] ON [dbo].[Reports]
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Steps_RecipeId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Steps_RecipeId] ON [dbo].[Steps]
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 24/07/2022 01:46:51 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reactions] ADD  DEFAULT ((0)) FOR [ReactionType]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [RequestToVIP]
GO
ALTER TABLE [dbo].[CollectionRecipes]  WITH CHECK ADD  CONSTRAINT [FK_CollectionRecipes_Collections_CollectionId] FOREIGN KEY([CollectionId])
REFERENCES [dbo].[Collections] ([CollectionId])
GO
ALTER TABLE [dbo].[CollectionRecipes] CHECK CONSTRAINT [FK_CollectionRecipes_Collections_CollectionId]
GO
ALTER TABLE [dbo].[CollectionRecipes]  WITH CHECK ADD  CONSTRAINT [FK_CollectionRecipes_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CollectionRecipes] CHECK CONSTRAINT [FK_CollectionRecipes_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Collections]  WITH CHECK ADD  CONSTRAINT [FK_Collections_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Collections] CHECK CONSTRAINT [FK_Collections_Users_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments_ParentCommentId] FOREIGN KEY([ParentCommentId])
REFERENCES [dbo].[Comments] ([CommentId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments_ParentCommentId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users_UserId]
GO
ALTER TABLE [dbo].[Reactions]  WITH CHECK ADD  CONSTRAINT [FK_Reactions_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
GO
ALTER TABLE [dbo].[Reactions] CHECK CONSTRAINT [FK_Reactions_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Reactions]  WITH CHECK ADD  CONSTRAINT [FK_Reactions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reactions] CHECK CONSTRAINT [FK_Reactions_Users_UserId]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Users_UserId]
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_Comments_CommentId] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([CommentId])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_Comments_CommentId]
GO
ALTER TABLE [dbo].[Steps]  WITH CHECK ADD  CONSTRAINT [FK_Steps_Recipes_RecipeId] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Steps] CHECK CONSTRAINT [FK_Steps_Recipes_RecipeId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
USE [master]
GO
ALTER DATABASE [RecipeDB] SET  READ_WRITE 
GO
