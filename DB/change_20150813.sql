/*
   giovedì 13 agosto 201512:32:00
   User: sa
   Server: CHIARAPC\SQLEXPRESS
   Database: Confidami
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Title
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Body
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Status
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Timestamp
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Userid
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_Deleted
GO
ALTER TABLE dbo.tblPosts
	DROP CONSTRAINT DF_tblPosts_EditCode
GO
CREATE TABLE dbo.Tmp_tblPosts
	(
	IdPost int NOT NULL IDENTITY (1, 1),
	IdCategory int NOT NULL,
	Title varchar(50) NOT NULL,
	Body varchar(MAX) NOT NULL,
	SlugUrl varchar(100) NULL,
	IdStatus smallint NOT NULL,
	Timestamp datetime NOT NULL,
	TimestampApprovation datetime NULL,
	Userid varchar(50) NOT NULL,
	Deleted bit NOT NULL,
	EditCode varchar(20) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_tblPosts SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Title DEFAULT ('') FOR Title
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Body DEFAULT ('') FOR Body
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Status DEFAULT ((0)) FOR IdStatus
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Timestamp DEFAULT (getdate()) FOR Timestamp
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Userid DEFAULT ('00000000-0000-0000-0000-000000000000') FOR Userid
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_Deleted DEFAULT ((0)) FOR Deleted
GO
ALTER TABLE dbo.Tmp_tblPosts ADD CONSTRAINT
	DF_tblPosts_EditCode DEFAULT ('') FOR EditCode
GO
SET IDENTITY_INSERT dbo.Tmp_tblPosts ON
GO
IF EXISTS(SELECT * FROM dbo.tblPosts)
	 EXEC('INSERT INTO dbo.Tmp_tblPosts (IdPost, IdCategory, Title, Body, SlugUrl, IdStatus, Timestamp, TimestampApprovation, Userid, Deleted, EditCode)
		SELECT IdPost, IdCategory, Title, CONVERT(varchar(MAX), Body), SlugUrl, IdStatus, Timestamp, TimestampApprovation, Userid, Deleted, EditCode FROM dbo.tblPosts WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_tblPosts OFF
GO
DROP TABLE dbo.tblPosts
GO
EXECUTE sp_rename N'dbo.Tmp_tblPosts', N'tblPosts', 'OBJECT' 
GO
ALTER TABLE dbo.tblPosts ADD CONSTRAINT
	PK_tblPosts PRIMARY KEY CLUSTERED 
	(
	IdPost
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.tblPosts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.tblPosts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.tblPosts', 'Object', 'CONTROL') as Contr_Per 