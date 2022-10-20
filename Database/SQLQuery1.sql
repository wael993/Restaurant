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

-- Insert Data 
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Belgische Waffeln',15.30,'Puderzucker und Nutella oder  Puderzucker und Vanilleeis oder  Nutella und Vanilleeis')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Apfelküchle',12.90,'Vanillesauce und Sahne oder  Vanilleeis und Vanillesauce')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Fritas',5.20,'warme, gefüllte Teigtaschen mit Vollmilchschokolade')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Tiramisu',7.50,'Eine italienische Versuchung')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Cinnamon French Toast',11.10,'Eine himmlische Köstlichkeit mit Vanillesauce')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Breakfast Bowl',7.39,'Joghurt mit Fru?chten der Saison und Granola')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Rührei',5.30,'Champignons und Bread Sticks (vegetarisch) oder Bacon und Bread Sticks')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Avocado Toast',7.50,'Eine italienische Versuchung')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Brooklyn Sandwich',3.90,'mit Schinken, Gouda und Tomaten')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Veggie Sandwich',6.50,'mit Hartka?se, Grillgemu?se, Tomaten und Rucola')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Pasta del mare',12.50,'Orecchiette in Tomatensauce mit Oliven und Meeresfrüchten')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Veggie Pasta ',10.55,'Orecchiette in Tomatensauce mit Antipastigemüse, Oliven und Grana Padano')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Putenschnitzel',16.50,'paniertes Putenschnitzel mit Spätzle oder Pommes und einer feinen Rahmsauce')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Salat Mista',5.58,'gemischter Salat mit Gurken, Tomaten und Mais')
INSERT INTO Speise(Produkt_Name,Preis,Beschreibung) VALUES('Funghi Salat ',6.10,'gemischter Salat mit gebratenen Champignons & Mais')



INSERT INTO Mitarbeiter(Vorname,Nachname, Email, Telefon) VALUES('Layla','Schmitt','Layla.Schmitt@my-mauritius.com','017664655999')
INSERT INTO Mitarbeiter(Vorname,Nachname, Email, Telefon) VALUES('markus','Habeb','markus.Habeb@my-mauritius.com','017632585999')
INSERT INTO Mitarbeiter(Vorname,Nachname, Email, Telefon) VALUES('Thomas','Braun','Thomas.Braun@my-mauritius.com','017664655999')
INSERT INTO Mitarbeiter(Vorname,Nachname, Email, Telefon) VALUES('markus','Habeb','markus.Habeb@my-mauritius.com','017632585999')


INSERT INTO Rechnung(Rechnung_status, Rechnung_datum, Mitarbeiter_id,Tisch_ID) VALUES('bezahlt','20201006',1000,5);
INSERT INTO Rechnung(Rechnung_status, Rechnung_datum, Mitarbeiter_id,Tisch_ID) VALUES('nicht bezahlt','20201007',1001,3);
INSERT INTO Rechnung(Rechnung_status, Rechnung_datum, Mitarbeiter_id,Tisch_ID) VALUES(' bezahlt','20201007',1002,7);
INSERT INTO Rechnung(Rechnung_status, Rechnung_datum, Mitarbeiter_id,Tisch_ID) VALUES('nicht bezahlt','20201008',1002,9);


INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3000,2001,2);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3000,2001,3);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3000,2003,5);

INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3001,2004,1);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3001,2009,2);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3001,2002,2);

INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3002,2001,1);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3002,2013,1);

INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3003,2013,2);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3003,2007,2);
INSERT INTO Rechnung_element(Rechnung_id, Produkt_ID, Anzahl) VALUES(3003,2009,2);

Select * from Rechnung_element
