--
-- Create Table    : 'Adresse'   
-- AdresseID       :  
--
--
-- Create Table    : 'Adresse'   
-- AdresseID       :  
--
CREATE TABLE Adresse (
    AdresseID      BIGINT NOT NULL IDENTITY,
[Vejnavn] NVARCHAR(50) NOT NULL, 
    [Husnummer] NVARCHAR(10) NOT NULL, 
    [Bynavn] NVARCHAR(50) NOT NULL, 
    [Postnummer ] INT NOT NULL, 
    CONSTRAINT pk_Adresse PRIMARY KEY CLUSTERED (AdresseID))