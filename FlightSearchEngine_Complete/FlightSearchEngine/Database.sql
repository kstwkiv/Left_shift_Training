-- ============================================================
-- FLIGHT SEARCH ENGINE - Complete Database Script
-- ============================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FlightSearchDB')
    CREATE DATABASE FlightSearchDB;
GO

USE FlightSearchDB;
GO

-- ============================================================
-- TABLE: Flights
-- ============================================================
IF OBJECT_ID('dbo.Flights', 'U') IS NOT NULL DROP TABLE dbo.Flights;
GO

CREATE TABLE dbo.Flights (
    FlightId       INT             PRIMARY KEY IDENTITY(1,1),
    FlightName     NVARCHAR(100)   NOT NULL,
    FlightType     NVARCHAR(50)    NOT NULL,
    Source         NVARCHAR(100)   NOT NULL,
    Destination    NVARCHAR(100)   NOT NULL,
    PricePerSeat   DECIMAL(18,2)   NOT NULL
);
GO

-- ============================================================
-- TABLE: Hotels
-- ============================================================
IF OBJECT_ID('dbo.Hotels', 'U') IS NOT NULL DROP TABLE dbo.Hotels;
GO

CREATE TABLE dbo.Hotels (
    HotelId       INT             PRIMARY KEY IDENTITY(1,1),
    HotelName     NVARCHAR(100)   NOT NULL,
    HotelType     NVARCHAR(50)    NOT NULL,
    Location      NVARCHAR(100)   NOT NULL,
    PricePerDay   DECIMAL(18,2)   NOT NULL
);
GO

-- ============================================================
-- SAMPLE DATA: Flights
-- ============================================================
INSERT INTO dbo.Flights (FlightName, FlightType, Source, Destination, PricePerSeat) VALUES
('Air India AI-101',    'Domestic',      'Mumbai',    'Delhi',     4500.00),
('Air India AI-202',    'Domestic',      'Delhi',     'Mumbai',    4500.00),
('IndiGo 6E-300',       'Domestic',      'Mumbai',    'Bangalore', 3800.00),
('IndiGo 6E-301',       'Domestic',      'Bangalore', 'Mumbai',    3800.00),
('SpiceJet SG-500',     'Domestic',      'Delhi',     'Kolkata',   5200.00),
('SpiceJet SG-501',     'Domestic',      'Kolkata',   'Delhi',     5200.00),
('Vistara UK-800',      'Domestic',      'Mumbai',    'Chennai',   4100.00),
('Vistara UK-801',      'Domestic',      'Chennai',   'Mumbai',    4100.00),
('Emirates EK-500',     'International', 'Mumbai',    'Dubai',    18000.00),
('Emirates EK-501',     'International', 'Delhi',     'Dubai',    17500.00),
('Singapore SQ-400',    'International', 'Mumbai',    'Singapore',22000.00),
('British Airways BA-200','International','Delhi',    'London',   45000.00),
('Air France AF-100',   'International', 'Mumbai',    'Paris',    42000.00),
('Lufthansa LH-700',    'International', 'Delhi',     'Frankfurt',40000.00);
GO

-- ============================================================
-- SAMPLE DATA: Hotels (one per city)
-- ============================================================
INSERT INTO dbo.Hotels (HotelName, HotelType, Location, PricePerDay) VALUES
('The Oberoi Mumbai',       'Luxury',    'Mumbai',    12000.00),
('The Imperial Delhi',      'Luxury',    'Delhi',     10000.00),
('ITC Gardenia Bangalore',  'Luxury',    'Bangalore',  8500.00),
('The Leela Palace Chennai','Luxury',    'Chennai',    9000.00),
('ITC Royal Bengal Kolkata','Luxury',    'Kolkata',    8000.00),
('Burj Al Arab Dubai',      'Luxury',    'Dubai',     35000.00),
('Marina Bay Sands Singapore','Luxury', 'Singapore', 28000.00),
('The Savoy London',        'Luxury',    'London',    40000.00),
('Le Meurice Paris',        'Luxury',    'Paris',     38000.00),
('Adlon Kempinski Frankfurt','Luxury',   'Frankfurt', 25000.00);
GO

-- ============================================================
-- STORED PROCEDURE: sp_GetSources
-- ============================================================
IF OBJECT_ID('dbo.sp_GetSources', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetSources;
GO

CREATE PROCEDURE dbo.sp_GetSources
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Source
    FROM dbo.Flights
    ORDER BY Source;
END
GO

-- ============================================================
-- STORED PROCEDURE: sp_GetDestinations
-- ============================================================
IF OBJECT_ID('dbo.sp_GetDestinations', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_GetDestinations;
GO

CREATE PROCEDURE dbo.sp_GetDestinations
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Destination
    FROM dbo.Flights
    ORDER BY Destination;
END
GO

-- ============================================================
-- STORED PROCEDURE: sp_SearchFlights
-- ============================================================
IF OBJECT_ID('dbo.sp_SearchFlights', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_SearchFlights;
GO

CREATE PROCEDURE dbo.sp_SearchFlights
    @Source      NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons     INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        f.FlightId,
        f.FlightName,
        f.FlightType,
        f.Source,
        f.Destination,
        (f.PricePerSeat * @Persons) AS TotalCost
    FROM dbo.Flights f
    WHERE f.Source      = @Source
      AND f.Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- ============================================================
-- STORED PROCEDURE: sp_SearchFlightsWithHotels
-- ============================================================
IF OBJECT_ID('dbo.sp_SearchFlightsWithHotels', 'P') IS NOT NULL DROP PROCEDURE dbo.sp_SearchFlightsWithHotels;
GO

CREATE PROCEDURE dbo.sp_SearchFlightsWithHotels
    @Source      NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons     INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        f.FlightId,
        f.FlightName,
        f.Source,
        f.Destination,
        h.HotelName,
        ((f.PricePerSeat * @Persons) + h.PricePerDay) AS TotalCost
    FROM dbo.Flights f
    INNER JOIN dbo.Hotels h ON h.Location = f.Destination
    WHERE f.Source      = @Source
      AND f.Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- ============================================================
-- Verification Queries
-- ============================================================
PRINT 'Database setup complete.';
SELECT 'Flights' AS TableName, COUNT(*) AS RecordCount FROM dbo.Flights
UNION ALL
SELECT 'Hotels', COUNT(*) FROM dbo.Hotels;
GO
