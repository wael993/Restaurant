-- create and use Database
IF DB_ID('La_Trattoria_del_Postillione') IS NULL
DROP database La_Trattoria_del_Postillione
go

USE La_Trattoria_del_Postillione;
go

-- create tables
IF OBJECT_ID('Rechnung_element') IS NOT NULL
  DROP TABLE Rechnung_element;
GO

IF OBJECT_ID('Rechnung') IS NOT NULL
  DROP TABLE Rechnung;
GO

IF OBJECT_ID('Speise') IS NOT NULL
  DROP TABLE Speise;
GO

IF OBJECT_ID('Mitarbeiter') IS NOT NULL
  DROP TABLE Mitarbeiter;
GO



CREATE TABLE Mitarbeiter (
	Mitarbeiter_id INT identity(1000,1) PRIMARY KEY,
	Vorname NVARCHAR (50) NOT NULL,
	Nachname NVARCHAR (50) NOT NULL,
	Email NVARCHAR (255),
	Telefon NVARCHAR (25)
);



CREATE TABLE Speise (
	Produkt_ID INT identity(2000,1) PRIMARY KEY,
	Produkt_Name NVARCHAR (255) NOT NULL,
	Preis DECIMAL (10, 2) NOT NULL,
	Beschreibung NVARCHAR (255)
);



CREATE TABLE Rechnung(
	Rechnung_id INT identity(3000,1) PRIMARY KEY,
	Rechnung_status nvarchar(20),
	--Rechnung status: nicht bezahlt; bezahlt;
	Rechnung_datum DATE NOT NULL,
	Mitarbeiter_id INT NOT NULL,
	Tisch_ID INT,
	CONSTRAINT fk_Mitarbeiter FOREIGN KEY (Mitarbeiter_id)
			REFERENCES Mitarbeiter(Mitarbeiter_id)
);


CREATE TABLE Rechnung_element (
	Rechnung_element_id INT identity(9000,1) PRIMARY KEY,
	Rechnung_id INT,
	Produkt_ID INT NOT NULL,
	Anzahl INT NOT NULL,
	CONSTRAINT fk_Speise FOREIGN KEY (Produkt_ID)
			REFERENCES Speise(Produkt_ID),
	CONSTRAINT fk_Rechnung FOREIGN KEY (Rechnung_ID)
			REFERENCES Rechnung(Rechnung_ID)
);

