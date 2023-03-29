USE EMPLOYEE_DEV_0001
/*
	description : 사원정보 관리 테이블
*/
IF OBJECT_ID('TblEmployee') IS NOT NULL
 BEGIN
    DROP TABLE TblEmployee
 END
GO
CREATE TABLE TblEmployee
(
    _email			    VARCHAR(50)    							NOT NULL	-- 이메일
   ,_name		    	NVARCHAR(100)							NOT NULL	-- 이름
   ,_tel    		    VARCHAR(20)							    NOT NULL	-- 전화번호
   ,_joined				DATE									NOT NULL	-- 입사일자
)
GO

CREATE CLUSTERED INDEX IxTblEmployee
    ON TblEmployee ( _name )
GO


/*********************************************************************
				description : SP 실행시 error 캐치용
**********************************************************************/
IF OBJECT_ID('TblDBError') IS NOT NULL
 BEGIN
    DROP TABLE TblDBError
 END
GO
CREATE TABLE TblDBError
(
    _spName				NVARCHAR(100)	NOT NULL		-- 에러 sp명
   ,_errorMsg    		NVARCHAR(1000)	NOT NULL		-- 에러 메시지
   ,_registerDate		DATETIME		DEFAULT(GETDATE())	NOT NULL
)
GO
