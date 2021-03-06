/*
   martedì 4 agosto 201500:21:06
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
	DROP COLUMN LastUserEdit
GO
ALTER TABLE dbo.tblPosts SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
