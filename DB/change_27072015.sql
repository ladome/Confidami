ALTER TABLE tblPostsAttachments ADD Timestamp datetime NULL;
ALTER TABLE tblTempAttachments ADD Timestamp datetime NULL;
ALTER TABLE tblPosts ADD Userid datetime default ('00000000-0000-0000-0000-000000000000');