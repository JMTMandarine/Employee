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
    _name		    	NVARCHAR(100)							NOT NULL	-- �̸�
   ,_email			    VARCHAR(50)    							NOT NULL	-- �̸���
   ,_tel    		    VARCHAR(20)							    NOT NULL	-- ��ȭ��ȣ
   ,_joined				DATE									NOT NULL	-- �Ի�����
)