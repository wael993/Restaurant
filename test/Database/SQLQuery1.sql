-- create and use Database
IF DB_ID('La_Trattoria_del_Postillione') IS NULL
DROP database La_Trattoria_del_Postillione
go

USE La_Trattoria_del_Postillione;
go

-- create and use Database
IF DB_ID('Rechnung_element') IS NOT NULL
create database Restaurant_Bestellungen
go

USE Restaurant_Bestellungen;
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
