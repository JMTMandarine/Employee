/*
    Description : uspGetRecruitPostList SP Temp Tvp
*/
IF OBJECT_ID('TblTypeEmployee') IS NOT NULL
 BEGIN
    DROP TYPE TblTypeEmployee
 END
GO
CREATE TYPE TblTypeEmployee AS TABLE
(
    _name		    	NVARCHAR(100)							NOT NULL	-- 이름
   ,_email			    VARCHAR(50)    							NOT NULL	-- 이메일
   ,_tel    		    VARCHAR(20)							    NOT NULL	-- 전화번호
   ,_joined				DATE									NOT NULL	-- 입사일자
)