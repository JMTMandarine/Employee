/**************************************************************************************************/
/**************************************************************************************************/
IF OBJECT_ID('uspGetEmployeeList') IS NOT NULL
 BEGIN
    DROP  PROCEDURE uspGetEmployeeList
 END
GO
CREATE PROCEDURE uspGetEmployeeList
	 @page                   INT
    ,@pageSize               INT
AS
/*
    \description    사원 리스트 출력 SP
    \author			이태형
    \update     
*/
BEGIN

    DECLARE @resultCode		INT				= 0 -- SP Error Code
	DECLARE @resultMsg		NVARCHAR(1000)  = N''
	DECLARE @startRowIndex  INT				= (@page - 1) * @pageSize

    BEGIN TRY

		-- 키워드 검색이 있을 경우
		SELECT   Employee._name
				,Employee._email
				,Employee._tel
				,CONVERT(VARCHAR(10), Employee._joined, 23) AS _joined
			FROM TblEmployee AS Employee
			ORDER BY Employee._name ASC
			OFFSET @startRowIndex ROWS FETCH NEXT @pageSize ROWS ONLY 

    END TRY
    BEGIN CATCH

        SET @resultCode = -99
		SET @resultMsg  = ERROR_MESSAGE()
        GOTO LABEL_END

    END CATCH

LABEL_END:
    --IF(0 = @resultCode)
    --  BEGIN
    --      COMMIT TRAN
    --  END
    --ELSE
    --  BEGIN
    --      ROLLBACK TRAN
    --  END
	IF(-99 = @resultCode)
	 BEGIN
		EXEC uspInsertDBError N'uspGetEmployeeList', @resultMsg
	 END
END
GO

/**************************************************************************************************/
/**************************************************************************************************/
IF OBJECT_ID('uspGetEmployee') IS NOT NULL
 BEGIN
    DROP  PROCEDURE uspGetEmployee
 END
GO
CREATE PROCEDURE uspGetEmployee
	 @name                   NVARCHAR(100)
AS
/*
    \description    사원 정보 출력 SP - name 검색
    \author			이태형
    \update     
*/
BEGIN

    DECLARE @resultCode		INT				= 0 -- SP Error Code
	DECLARE @resultMsg		NVARCHAR(1000)  = N''

    BEGIN TRY

		-- 키워드 검색이 있을 경우
		SELECT Employee._name
			,Employee._email
			,Employee._tel
			,CONVERT(VARCHAR(10), Employee._joined, 23) AS _joined
			FROM TblEmployee AS Employee
			WHERE Employee._name = @name
			

    END TRY
    BEGIN CATCH

        SET @resultCode = -99
		SET @resultMsg  = ERROR_MESSAGE()
        GOTO LABEL_END

    END CATCH

LABEL_END:
    --IF(0 = @resultCode)
    --  BEGIN
    --      COMMIT TRAN
    --  END
    --ELSE
    --  BEGIN
    --      ROLLBACK TRAN
    --  END
	IF(-99 = @resultCode)
	 BEGIN
		EXEC uspInsertDBError N'uspGetEmployee', @resultMsg
	 END
END
GO

/**************************************************************************************************/
/**************************************************************************************************/
IF OBJECT_ID('uspInsertEmployee') IS NOT NULL
 BEGIN
    DROP  PROCEDURE uspInsertEmployee
 END
GO
CREATE PROCEDURE uspInsertEmployee
	 @employeeList		TblTypeEmployee	READONLY

	,@resultMsg         NVARCHAR(1000)	OUTPUT
	,@resultCode        INT				OUTPUT
AS
/*
    \description    인원 정보 입력 SP
    \author			이태형
    \update     
*/
BEGIN

    SET @resultCode	= 0 -- SP Error Code
	SET @resultMsg	= N''

	BEGIN TRAN uspuspInsertEmployee_tx1   -- 트랜잭션

    BEGIN TRY
		
		/*
			이메일 중복 검증
			1. input 데이터 검증
			2. @resultMsg에 이미 들어가 있는지 테이블 데이터와 확인
		*/
		SELECT @resultMsg = STRING_AGG(DataList._email, ', ')
			FROM (SELECT DISTINCT DataList._email
						FROM @employeeList AS DataList
					    GROUP BY DataList._email
						HAVING COUNT(*) > 1) AS DataList
		IF(@resultMsg <> N'')
		 BEGIN
			SET @resultCode = -1
			SET @resultMsg  = N' Duplicate Email exists :' + @resultMsg
			GOTO LABEL_END
		 END


		SELECT @resultMsg = STRING_AGG(DataList._email, ', ')
			FROM (SELECT DISTINCT DataList._email
						FROM TblEmployee AS Employee
							INNER JOIN @employeeList AS DataList
							ON Employee._email = DataList._email) AS DataList

		IF(@resultMsg <> N'')
		 BEGIN
			SET @resultCode = -2
			SET @resultMsg  = N'Email already exists :' + @resultMsg
			GOTO LABEL_END
		 END

		INSERT INTO TblEmployee(_name
								, _email
								, _tel
								, _joined)
				SELECT _name
					, _email
					, _tel
					, _joined
					FROM @employeeList

    END TRY
    BEGIN CATCH

		SET @resultMsg  = ERROR_MESSAGE()
        SET @resultCode = -99
        GOTO LABEL_END

    END CATCH

LABEL_END:

    IF(0 = @resultCode)
      BEGIN
          COMMIT TRAN
      END
    ELSE
      BEGIN
          ROLLBACK TRAN
      END

	IF(-99 = @resultCode)
	 BEGIN
		EXEC uspInsertDBError N'uspInsertRecruitPost', @resultMsg
	 END
END
GO

/**************************************************************************************************/
/**************************************************************************************************/
IF OBJECT_ID('uspInsertDBError') IS NOT NULL
 BEGIN
    DROP  PROCEDURE uspInsertDBError
 END
GO
CREATE PROCEDURE uspInsertDBError
	 @spName		NVARCHAR(100)			
    ,@errorMsg    	NVARCHAR(1000)
AS
/*
    \description    DB Error Log Insert 용
*/
BEGIN
    BEGIN TRY
		INSERT INTO TblDBError(_spName, _errorMsg)
			VALUES(@spName, @errorMsg)

    END TRY
    BEGIN CATCH

    END CATCH

LABEL_END:
END
GO