--
-- Create Table    : 'Person'   
-- PersonNummer    :  
-- AdresseID       :  (references Adresse.AdresseID)
--
--
-- Create Table    : 'Person'   
-- PersonNummer    :  
-- AdresseID       :  (references Adresse.AdresseID)
--
CREATE TABLE Person (
    PersonNummer   BIGINT NOT NULL,
    AdresseID      BIGINT NOT NULL,
[Type] NVARCHAR(50) NOT NULL, 
    [Fornavn] NVARCHAR(50) NOT NULL, 
    [Mellemnavn] NVARCHAR(50) NULL, 
    [Efternavn] NVARCHAR(50) NOT NULL, 
    CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonNummer),
CONSTRAINT fk_Person FOREIGN KEY (AdresseID)
    REFERENCES Adresse (AdresseID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)