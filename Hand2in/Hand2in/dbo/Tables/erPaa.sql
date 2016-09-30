--
-- Create Table    : 'erPaa'   
-- AdresseID       :  (references Adresse.AdresseID)
-- PersonNummer    :  (references Person.PersonNummer)
--
--
-- Create Table    : 'erPaa'   
-- AdresseID       :  (references Adresse.AdresseID)
-- PersonNummer    :  (references Person.PersonNummer)
--
CREATE TABLE erPaa (
    AdresseID      BIGINT NOT NULL,
    PersonNummer   BIGINT NOT NULL,
[Type] NVARCHAR(50) NULL, 
    CONSTRAINT pk_erPaa PRIMARY KEY CLUSTERED (AdresseID,PersonNummer),
CONSTRAINT fk_erPaa FOREIGN KEY (AdresseID)
    REFERENCES Adresse (AdresseID)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_erPaa2 FOREIGN KEY (PersonNummer)
    REFERENCES Person (PersonNummer)
    ON DELETE CASCADE
    ON UPDATE CASCADE)