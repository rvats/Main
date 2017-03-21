/******************************************************************************************
 * Author: Rahul Vats
 * Date Created: 03/21/2017
 * Purpose: To create the DB for the application
 * This script creates the Reference Schema
******************************************************************************************/
SET IDENTITY_INSERT  [Reference].[Gender] ON
GO

--Declare Table Variable
DECLARE @tblGender table (
	[GenderID]     INT,
	[Gender]  NVARCHAR(100),
	[GenderCode] NVARCHAR(10) NULL
);

--Insert Data into Table Variable
INSERT INTO @tblGender ([GenderID],[Gender],LegacyCode,LegacyCodeDescription,[SortOrder],[IsActive],[ModifiedBy],[ModifiedOn]) VALUES
('1','Male','M'),
('2','Female','F'),
('3','Unknown','U');

--Merge Table Variable with DB Table As Target and Table Variable as Source
MERGE INTO [Reference].[Gender] AS TARGET
USING
(
       SELECT * FROM @tblGender
) AS SOURCE
ON SOURCE.[GenderID] = TARGET.[GenderID]
WHEN MATCHED THEN
	UPDATE SET 
		[Gender] = SOURCE.[Gender], 
		[GenderCode]= SOURCE.[GenderCode]
WHEN NOT MATCHED BY TARGET THEN
	INSERT (
		[GenderID],
		[Gender],
		[GenderCode]
	)
	VALUES (
	SOURCE.[GenderID],
	SOURCE.[Gender],
	SOURCE.[GenderCode]
);

SET IDENTITY_INSERT  [Reference].[Gender] OFF
GO