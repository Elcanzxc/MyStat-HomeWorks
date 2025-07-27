
CREATE DATABASE MyBank

USE MyBank

CREATE TABLE UserAccount(
  [Id] INT PRIMARY KEY IDENTITY(1,1),
  [Name] NVARCHAR(MAX) NOT NULL CHECK(LEN(Name) > 2),
  [Balance] MONEY NOT NULL CHECK(Balance >= 0)
)

INSERT INTO UserAccount
VALUES (N'Elcan' , 15000),
       (N'Samir' , 2000)


CREATE OR ALTER PROCEDURE SendMoney
   @SenderId INT,
   @OwnerId INT,
   @Value MONEY
AS
 BEGIN


    IF @SenderId NOT IN ( SELECT Id FROM UserAccount)
       BEGIN
            PRINT 'Sender Id not correct'
            RETURN
       END

    
    
    IF @OwnerId NOT IN ( SELECT Id FROM UserAccount)
       BEGIN
           PRINT 'Owner Id not correct'
           RETURN
       END
    
    IF @OwnerId = @SenderId 
       BEGIN
           PRINT 'You can not send money to yourself'
           RETURN
       END


    IF @Value <= 0
       BEGIN
           PRINT 'you cannot send a negative or zero amount '
           RETURN
       END

    IF @Value > ( SELECT UA.Balance 
                FROM UserAccount UA
                WHERE UA.Id = @SenderId
                )
        BEGIN
           PRINT 'You dont have enough funds'
           RETURN
        END
      

    BEGIN TRANSACTION

      BEGIN TRY
         
         UPDATE UserAccount 
         SET Balance = Balance - @Value
         WHERE Id = @SenderId


         UPDATE UserAccount 
         SET Balance = Balance + @Value
         WHERE Id = @OwnerId

         COMMIT TRANSACTION

      END TRY

      BEGIN CATCH
          	    IF @@TRANCOUNT > 0
				ROLLBACK
      END CATCH 
 END

EXEC SendMoney 2 , 1, 5999

SELECT * FROM UserAccount