-- Creating a Master Key
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '!AimTWI030126!';
GO

-- We need to create a self-signed certificate
CREATE CERTIFICATE [EncryptionCertificateTWI] 
WITH SUBJECT = 'EncryptionTWI'
GO

-- Creating an Encryption Certificate with a separate key
CREATE CERTIFICATE [EncryptionCertificateWithKey]
	ENCRYPTION BY PASSWORD = 'aombqkh>rjDz|vkbuvukguowmsFT7_&#$!~<`oir1Ciysn_E'
	WITH SUBJECT = 'EncryptionTWI'
GO

-- Create a symmetric key
CREATE SYMMETRIC KEY SymmetricTWIKey
WITH ALGORITHM = AES_192
ENCRYPTION BY CERTIFICATE [EncryptionCertificateTWI]
GO

-- Usage or opening with? a key
--OPEN SYMMETRIC KEY  SymmetricTWIKey DECRYPTION BY CERTIFICATE [EncryptionCertificateTWI]

/*

To encrypt: select EncryptByKey(Key_GUID('SymmetricTWIKey'), @word) as encrypted 
To decrypt: select convert(varchar(100),DECRYPTBYKEY(@encrypted)) as decrypted  

Test Script is below - 

SELECT EncryptByKey(Key_GUID('SymmetricTWIKey'), 'hello')
declare @word varchar(100), @encrypted varbinary(2000)
set @word = 'hello'
set @encrypted = EncryptByKey(Key_GUID('SymmetricTWIKey'), @word)
select @encrypted
select convert(varchar(100),DECRYPTBYKEY(@encrypted)) as decrypted  

*/