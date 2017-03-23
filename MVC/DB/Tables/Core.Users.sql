/**************************************************************************************************************************
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.

The bucket count should be set to about two times the 
maximum expected number of distinct values in the 
index key, rounded up to the nearest power of two.
--------------------------------------------------------------------------------------------------------------------------
-- Table:		[Core].[Users]
-- Author:		Rahul Vats
-- Date:		03/15/2017
-- Purpose:		Store the User details
-- Notes:		This table is a generic design that stores most relevant/apprpriate user information 
-- Depends:		n/a (or any dependencies such as other procs or functions)
-- REVISION HISTORY ---------------------------------------------------------------------------------------------------
-- MM/DD/YYYY	Rahul Vats	Initial creation.
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/
CREATE TABLE [Core].[Users] (
    [UserID]              INT              IDENTITY (1, 1) NOT NULL,
    [UserName]            NVARCHAR (100)   Not NULL,
    [Password]            NVARCHAR (255)   NULL,
    --[IsTemporaryPassword] BIT              CONSTRAINT [DF_Users_IsTemporaryPassword] DEFAULT ((1)) NOT NULL,
    [IsInternal]          BIT              NULL,
    [IsActive]            BIT              CONSTRAINT [DF_Users_IsActive] DEFAULT ((1)) NOT NULL,
    [ModifiedBy]          INT              NOT NULL,
    [ModifiedOn]          DATETIME         DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]           INT              DEFAULT ((0)) NOT NULL,
    [CreatedOn]           DATETIME         DEFAULT (getutcdate()) NOT NULL,
    [EffectiveFromDate]   DATETIME         NULL,
	[EffectiveToDate]     DATETIME         NULL,
    CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED ([UserID] ASC)
    --CONSTRAINT [IX_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
) WITH (MEMORY_OPTIMIZED = ON)
GO
--CREATE NONCLUSTERED INDEX [IX_Users_IsActive_UserName_UserID] ON [Core].[Users]
--(
--	[IsActive] ASC,
--	[UserName] ASC,
--	[UserID] ASC
--)
--INCLUDE ( 	
--	[Password],
--	[EffectiveToDate],
--	[ModifiedBy],
--	[ModifiedOn]
--) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
--GO