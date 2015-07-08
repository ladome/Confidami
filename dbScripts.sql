USE [master]
GO
/****** Object:  Database [Confidami]    Script Date: 08/07/2015 20:26:43 ******/
CREATE DATABASE [Confidami]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Confidami', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Confidami.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Confidami_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Confidami_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Confidami] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Confidami].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Confidami] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Confidami] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Confidami] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Confidami] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Confidami] SET ARITHABORT OFF 
GO
ALTER DATABASE [Confidami] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Confidami] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Confidami] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Confidami] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Confidami] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Confidami] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Confidami] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Confidami] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Confidami] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Confidami] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Confidami] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Confidami] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Confidami] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Confidami] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Confidami] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Confidami] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Confidami] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Confidami] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Confidami] SET  MULTI_USER 
GO
ALTER DATABASE [Confidami] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Confidami] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Confidami] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Confidami] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Confidami] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Confidami]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCategory]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCategory](
	[IdCategory] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tblCategory] PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPosts]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPosts](
	[IdPost] [int] IDENTITY(1,1) NOT NULL,
	[IdCategory] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL CONSTRAINT [DF_tblPosts_Title]  DEFAULT (''),
	[Body] [text] NOT NULL CONSTRAINT [DF_tblPosts_Body]  DEFAULT (''),
	[SlugUrl] [varchar](100) NULL,
	[Deleted] [bit] NOT NULL CONSTRAINT [DF_tblPosts_Deleted]  DEFAULT ((0)),
	[IdStatus] [smallint] NOT NULL CONSTRAINT [DF_tblPosts_Status]  DEFAULT ((0)),
	[Timestamp] [datetime] NOT NULL CONSTRAINT [DF_tblPosts_Timestamp]  DEFAULT (getdate()),
	[TimestampApprovation] [datetime] NULL,
 CONSTRAINT [PK_tblPosts] PRIMARY KEY CLUSTERED 
(
	[IdPost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPostsAttachments]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPostsAttachments](
	[IdPostAttachment] [int] IDENTITY(1,1) NOT NULL,
	[IdPost] [int] NOT NULL,
	[FileName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_tblPostsAttachments] PRIMARY KEY CLUSTERED 
(
	[IdPostAttachment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPostStatus]    Script Date: 08/07/2015 20:26:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPostStatus](
	[IdStatus] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201506152045032_InitialCreate', N'Confidami.Web.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C5B6FE336167E5F60FF83A0A7DD456AE5D2194C037B06A993EC069D5C30CEB4FB36A025DA2146A254894A1314FD657DE84FEA5FE8A144C9122FBAD88AED14058AB178F8F1F0F023797878983F7FFF63FCE129F0AD471C2724A413FB6874685B98BAA147E87262A76CF1CD3BFBC3FB7FFE637CE1054FD68F85DC0997839A3499D80F8C45A78E93B80F3840C928206E1C26E1828DDC307090173AC78787DF3947470E06081BB02C6BFC29A58C0438FB013FA7217571C452E45F871EF613F11D4A6619AA7583029C44C8C5131B6417C4430119FD84E7A35CDEB6CE7C82409719F617B685280D1962A0E9E9E704CF581CD2E52C820FC8BF7F8E30C82D909F60D183D39578D7CE1C1EF3CE38AB8A05949B262C0C7A021E9D08EB3872F5B56C6C97D603FB5D809DD933EF7566C3897DE5E1ECD3A7D00703C80D9E4EFD980B4FECEBB289B324BAC16C54541CE5909731C0FD12C65F4755C403AB73BD83924DC7A343FEDF81354D7D96C6784271CA62E41F5877E9DC27EE0FF8F93EFC8AE9E4E468BE3879F7E62DF24EDE7E8B4FDE547B0A7D05B9DA07F8741787118E4137BC28FB6F5B4EBD9E23572CAB55EAE456012EC1C4B0AD6BF4F411D3257B802973FCCEB62EC913F68A2F825C9F2981790495589CC2CF9BD4F7D1DCC765B9D3D826FF7F43ABC76FDE0ED2EA0D7A24CB6CE8A5F661E2C430AF3E613F2B4D1E48944FAFDA787F1162977118F0DF757EE5A55F66611ABBBC33A151E41EC54BCCEADA8D9D15793B519A430D4FEB0275FFA9CD3555E9AD15E51D5A6726144D6C7B3614FABE6CBB9D19771645307819B5B8459A08A7DBAE46527DA047556A459FA3AEF4A1D0ADBFF36A781120E20FB01C7668251B8A38C0652FBF0F817C88F6D6F90E2509AC06DEFF50F2D0A03AFC7300D567D84D6320E98CA1207AF1D6EE1E428A6FD260CEB9BFBDB6061B9AFB5FC24BE4B230BEA0BCD6C6781F43F76B98B20BEA9D23863F33B700E43FEF49D01D601075CE5C1727C92590197BD3105CED02F08AB293E3DE707C89DAB53B32F51109F4FE88B4987E2944573E895E42F14B0C623ADFA449D58FE192D06EAA16A266557389565585585F553958374D85A459D14CA055CF5C6A306F2F1BA1E1DDBD0C76FFFDBDCD366FD35A5031E30C5648FC5F4C710CCB98778718C3315D8D4097756317CE42367CBCD117DF9BB2967E447E3A74536BCD866C11187E3664B0FB3F1B3235E1F323F1B857D2E1105408037C2779FDF9AA7DCE499A6D7B3AD4BAB9EDC6B7B30698A6CB5992842EC9668126FC25821775FDC187B3DA2319796FE46808740C884EF896075FA06FB64CAA5B7A8E7DCCB075E6E6E1C1294A5CE4A966840E793D142B76548D62ABA8485DB9FF286D02D371CC2B217E084A60A612CAD46941A84B22E4B75A49AAD9710BE37D2FDB904BCE7184296FB0D5125D1AD70741B802653BD2A0B45968EC5418D74C4483D76A1AF336177635EE4A6C622B9C6CF19D0DBC14FEDB8B10B3D9625B2067B349BA28600CE8ED82A0E2ACD29500F2C165DF082A9D980C04152ED556085AB7D80E085A37C9AB23687E44ED3AFED27975DFE8593F286F7F5B6F34D70EB859B3C79E5133F73DA10E831A3856E9793EE785F889690E67A0A7389F25C2D59529C2C16798D543362B7F57EB873ACD2032899A0057446B011557810A9032A17A2857C4F21AB5135E440FD822EED6082BD67E09B6C20115BB7A255A11345F9CCAE4EC74FA287B56B2412179A7C3420547430879F1AA77BC83514C7159D5305D7CE13EDE70A56362301A0CD4E2B91A8C547466702B15D46CB792CE21EBE3926D6425C97D3258A9E8CCE056121C6D3792C629E8E1166C64A2FA163ED0642B221DE56E53968D9D3C574A7C183B86A4AAF1358A22429795242BF1C59A890CAB6F66FD138F821CC371134DFE51A96DD9120B63B4C45229BF2DF7F0258913768E189A231EE7997A8122A6DD5B0DCB7FD16475FB5407B1D8070A69FE6F11E3D55DE0D7765BD51D112897D0C780FB3459205DC3007D758BA7BD211FC59AD8FD34F4D3809A5D2C73EDFC06AF5A3FFFA2228C1D497FC58552ECA538BA75E3771A1A755A0C364CA50FB3FE5099214C062F3CD0AAC94D5EA919A5085255514C81AB9D0D9DC999E9395CB2A7D87FB45A115E666E89F4942A80F8D413A392E1A08055CABAA3D69350AA98F592EE8852A64915522AEAA165359FA4A664B5602D3C8345F512DD5B503348AAE86A6977644D2E49155A53BC06B64667B9AC3BAA26DDA40AAC29EE8EBDCA3D9197D13DDEBD8CE7970DB6AFFC90BBD9FE65C07899357198EDAF72975F05AA7CEE89256EEB1530F17D2FF9643CE96DC0A73CBAB1199F0C18E6D5A7760F5E5F7C1A2FEFCD98B5CBEDDA02DF74B96FC6EBC7DA17E58672D49345CAD6CB239F74B41B8B6356974735D2B92B17B1ADC28CB0B93F270C07232E309AFDEC4F7D82F9525E085C234A1638617942877D7C78742CBDCAD99F17324E9278BEE6986A7A26531FB32DE466D14714BB0F28563325367845B2025582D057D4C34F13FBD7ACD66916CFE0FFCA3E1F5857C9674A7E4EA1E03E4EB1F59B9AF9394C567DF3516B4FDF4074B7EAD5FFBFE4550FACDB1866CCA97528D9729D11AEBF8CE8A54D5E75036DD67E2FF17A2754ED218216559A10EBBF3B981336C89B8342CB7F05E8E9DF7D55D3BE2BD80851F3766028BC414C687A1BB00E96F15D80073F59F62EA05F67F5EF04D651CDF84680D0FE60F20B81EECB505173875B8DE654B48D2529B3736B86F546E996BBDE9B9444EC8D26BA9A6CDD036E8384EA3598F1CA729107DB1D35A9C68361EF92DA2F9E5FBC2F29C5AB648FDD66126F3379B8E172E86F9533BC07596E9AAC9DDD67066F9B6BA648EE9EA757F6CBFFDD33B2895CAEDD67F96E9B6CA630EF9E93AD572EEF9E716D57FBE78E99D6790BDD7966AE9A6464B891D1C582DB326FF3C0399CF0E7219020F728F30793FA54AFA634D596065722E646CD396672C3CAC451DA55249A9BEDD757B1E1377656C834376BC8CC6C6A5BACFF8D6D0B99E6B60DF98EBBC819D6661CEAF2B85BD6B1A654A8D794235CEB494B4A7A9BCFDA78BDFE9A528207314A6DF618EE885F4F06F020261972EAF4C8F855AF7B61EFACFCA545D8BF13B25C41F0BFBB48B15BDB354B992BBA088BCD5BD2A810912234D798210FB6D4B3989105721914F31873F6E23B8BDBF19B8E39F6AEE86DCAA29441977130F76B012FEE0434B59FA535D7751EDF46D91F2F19A20BA026E1B1F95BFA7D4A7CAFD4FB521313324070EF424474F958321ED95D3E97483721ED0824CC573A45F738887C004B6EE90C3DE2757403FA7DC44BE43EAF22802690F681A89B7D7C4ED0324641223056F5E12770D80B9EDEFF0546810E2370540000, N'6.1.3-40302')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e37e63a7-02bb-4bea-a31a-efe0be7cc6de', N'admin')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bedd6618-0a60-47c3-b81e-80a17df18d3c', N'e37e63a7-02bb-4bea-a31a-efe0be7cc6de')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bedd6618-0a60-47c3-b81e-80a17df18d3d', N'e37e63a7-02bb-4bea-a31a-efe0be7cc6de')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'22b975b5-e2af-4651-9926-996324dfd9ad', N'domenicolafauci@gmail.com', 0, N'AJ/JXuB1MOvIsxNDjM4rcYK1SyhxnlCOft7puXGL6PC8+iWjDTbrZWJQ4Fav8FYsDg==', N'1acdbfea-d524-43cd-b110-837e0de9f459', NULL, 0, 0, NULL, 1, 0, N'domenicolafauci@gmail.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bedd6618-0a60-47c3-b81e-80a17df18d3c', N'admin@admin.it', 0, N'ADwiywyhCfAX2HAiLEy7ZbyRIAmiZVpWA+9zMCOCgGWpg3k3h7WB2n/zDrjL2ln2GQ==', N'466e5e27-7b11-416a-bb54-cd514a9e67d8', NULL, 0, 0, NULL, 1, 0, N'admin@admin.it')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bedd6618-0a60-47c3-b81e-80a17df18d3d', N'rovida@admin.it', 0, N'ADwiywyhCfAX2HAiLEy7ZbyRIAmiZVpWA+9zMCOCgGWpg3k3h7WB2n/zDrjL2ln2GQ==', N'466e5e27-7b11-416a-bb54-cd514a9e67d8', NULL, 0, 0, NULL, 1, 0, N'rovida@admin.it')
INSERT [dbo].[tblCategory] ([IdCategory], [Description]) VALUES (1, N'Scuola')
INSERT [dbo].[tblCategory] ([IdCategory], [Description]) VALUES (2, N'Pubblica Amministrazione')
INSERT [dbo].[tblCategory] ([IdCategory], [Description]) VALUES (3, N'Sessuale')
SET IDENTITY_INSERT [dbo].[tblPosts] ON 

INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1005, 1, N'dsad', N'dasd', N'', 0, 2, CAST(N'2015-07-01 21:04:54.267' AS DateTime), CAST(N'2015-07-04 01:32:11.567' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1006, 1, N'aaaaaaaaaa', N'aaaaaaaaaaa', N'', 0, 2, CAST(N'2015-07-01 21:04:59.123' AS DateTime), CAST(N'2015-07-04 01:32:31.570' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1007, 3, N'dasd', N'dasd', N'', 0, 2, CAST(N'2015-07-01 21:05:06.353' AS DateTime), CAST(N'2015-07-04 01:32:29.960' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1008, 2, N'zeta', N'86', N'', 0, 2, CAST(N'2015-07-03 20:33:20.137' AS DateTime), CAST(N'2015-07-04 01:32:07.623' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1009, 1, N'wewqewq', N'wewqeq', N'', 0, 2, CAST(N'2015-07-03 22:14:58.987' AS DateTime), CAST(N'2015-07-04 01:32:09.257' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1010, 2, N'daorera', N'dsadas', N'', 0, 2, CAST(N'2015-07-03 23:03:23.803' AS DateTime), CAST(N'2015-07-04 01:31:46.450' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1011, 2, N'test', N'test', N'', 0, 2, CAST(N'2015-07-04 01:34:13.607' AS DateTime), CAST(N'2015-07-04 16:54:32.853' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1012, 2, N'abuso1', N'ciao', N'', 0, 2, CAST(N'2015-07-04 16:55:18.933' AS DateTime), CAST(N'2015-07-05 01:31:25.257' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1014, 1, N'dsd', N'sdadas', N'', 0, 2, CAST(N'2015-07-04 22:51:32.090' AS DateTime), CAST(N'2015-07-05 01:31:37.857' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1015, 3, N'gdgfd', N'gdfg', N'', 0, 2, CAST(N'2015-07-04 23:32:23.690' AS DateTime), CAST(N'2015-07-05 01:31:37.657' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1016, 1, N'dsad', N'ddasd', N'', 0, 2, CAST(N'2015-07-04 23:33:34.953' AS DateTime), CAST(N'2015-07-05 01:31:37.283' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1017, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:09:00.963' AS DateTime), CAST(N'2015-07-05 01:31:36.917' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1018, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:11:20.190' AS DateTime), CAST(N'2015-07-05 01:31:36.743' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1019, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:14:05.897' AS DateTime), CAST(N'2015-07-05 01:31:36.423' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1020, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:15:21.693' AS DateTime), CAST(N'2015-07-05 01:31:36.073' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1021, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:15:48.240' AS DateTime), CAST(N'2015-07-05 01:31:35.880' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1022, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:16:14.683' AS DateTime), CAST(N'2015-07-05 01:31:35.693' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1023, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:18:56.830' AS DateTime), CAST(N'2015-07-05 01:31:35.493' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1024, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:19:22.610' AS DateTime), CAST(N'2015-07-05 01:31:35.280' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1025, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:25:15.287' AS DateTime), CAST(N'2015-07-05 01:31:35.057' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1026, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:27:02.280' AS DateTime), CAST(N'2015-07-05 01:31:34.690' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1027, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:27:49.267' AS DateTime), CAST(N'2015-07-05 01:31:34.203' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1028, 1, N'dsad', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 00:31:11.963' AS DateTime), CAST(N'2015-07-05 01:31:33.570' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1044, 1, N'dsadsa', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 02:46:06.383' AS DateTime), CAST(N'2015-07-05 02:49:40.993' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1045, 1, N'dsadsa', N'dsadas', N'', 0, 2, CAST(N'2015-07-05 02:47:09.177' AS DateTime), CAST(N'2015-07-05 02:49:39.457' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1046, 1, N'testa', N'testa', N'', 0, 2, CAST(N'2015-07-05 02:48:48.540' AS DateTime), CAST(N'2015-07-05 02:49:36.157' AS DateTime))
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1047, 1, N'dsad', N'dasda', N'', 0, 0, CAST(N'2015-07-05 02:51:47.007' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1048, 1, N'dsadsa', N'dsads', N'', 0, 0, CAST(N'2015-07-05 03:06:37.583' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1049, 1, N'sdasa', N'dsadsa', N'', 0, 0, CAST(N'2015-07-05 03:09:41.247' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1050, 1, N'dasd', N'dsada', N'', 0, 0, CAST(N'2015-07-05 03:18:40.977' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1051, 1, N'dasd', N'dsada', N'', 0, 0, CAST(N'2015-07-05 03:19:03.713' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1052, 1, N'dasd', N'dsada', N'', 0, 0, CAST(N'2015-07-05 03:19:50.313' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1053, 1, N'dasd', N'dsada', N'', 0, 0, CAST(N'2015-07-05 03:20:13.880' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1054, 1, N'dsad', N'dsad', N'', 0, 0, CAST(N'2015-07-05 03:22:00.380' AS DateTime), NULL)
INSERT [dbo].[tblPosts] ([IdPost], [IdCategory], [Title], [Body], [SlugUrl], [Deleted], [IdStatus], [Timestamp], [TimestampApprovation]) VALUES (1055, 1, N'dasda', N'dsada', N'', 0, 0, CAST(N'2015-07-05 03:35:52.350' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblPosts] OFF
SET IDENTITY_INSERT [dbo].[tblPostsAttachments] ON 

INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (7, 1044, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (8, 1045, N'9 Coop - Lettera Incarico Professionale MARRONE.pdf')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (9, 1046, N'immagine000.jpg')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (10, 1046, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (11, 1046, N'Nuovo documento di testo.txt')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (12, 1047, N'9 Coop - Lettera Incarico Professionale MARRONE.pdf')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (13, 1048, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (14, 1049, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (15, 1050, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (16, 1051, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (17, 1052, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (18, 1053, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (19, 1054, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
INSERT [dbo].[tblPostsAttachments] ([IdPostAttachment], [IdPost], [FileName]) VALUES (20, 1055, N'9 Coop - Lettera Incarico Professionale MARRONE.doc')
SET IDENTITY_INSERT [dbo].[tblPostsAttachments] OFF
INSERT [dbo].[tblPostStatus] ([IdStatus], [Description]) VALUES (0, N'OnApprovation')
INSERT [dbo].[tblPostStatus] ([IdStatus], [Description]) VALUES (1, N'Approved')
INSERT [dbo].[tblPostStatus] ([IdStatus], [Description]) VALUES (2, N'Rejected')
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 08/07/2015 20:26:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 08/07/2015 20:26:43 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 08/07/2015 20:26:43 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 08/07/2015 20:26:43 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 08/07/2015 20:26:43 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 08/07/2015 20:26:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [Confidami] SET  READ_WRITE 
GO
