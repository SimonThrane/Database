--
-- Create Table    : 'har'   
-- PersonNummer    :  (references Person.PersonNummer)
-- TelefonNummerID :  (references Telefon.TelefonNummerID)
-- type            :  
--
--
-- Create Table    : 'har'   
-- PersonNummer    :  (references Person.PersonNummer)
-- TelefonNummerID :  (references Telefon.TelefonNummerID)
-- type            :  
--
CREATE TABLE har (
    PersonNummer   BIGINT NOT NULL,
    TelefonNummerID BIGINT NOT NULL,
    type           NVARCHAR(20) NOT NULL,
CONSTRAINT pk_har PRIMARY KEY CLUSTERED (PersonNummer,TelefonNummerID),
CONSTRAINT fk_har FOREIGN KEY (PersonNummer)
    REFERENCES Person (PersonNummer)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
CONSTRAINT fk_har2 FOREIGN KEY (TelefonNummerID)
    REFERENCES Telefon (TelefonNummerID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)