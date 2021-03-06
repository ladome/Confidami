/*
   martedì 4 agosto 201500:38:41
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
CREATE TABLE dbo.Tmp_tblPostsEdit
	(
	IdPostEdit int NOT NULL,
	LastUserEdit datetime NULL,
	Email varchar(100) NULL,
	SecretKey varchar(100) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_tblPostsEdit SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.tblPostsEdit)
	 EXEC('INSERT INTO dbo.Tmp_tblPostsEdit (IdPostEdit, LastUserEdit, Email, SecretKey)
		SELECT IdPostEdit, LastUserEdit, Email, SecretKey FROM dbo.tblPostsEdit WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.tblPostsEdit
GO
EXECUTE sp_rename N'dbo.Tmp_tblPostsEdit', N'tblPostsEdit', 'OBJECT' 
GO
ALTER TABLE dbo.tblPostsEdit ADD CONSTRAINT
	PK_tblPostsEdit PRIMARY KEY CLUSTERED 
	(
	IdPostEdit
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.tblPostsEdit', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.tblPostsEdit', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.tblPostsEdit', 'Object', 'CONTROL') as Contr_Per 