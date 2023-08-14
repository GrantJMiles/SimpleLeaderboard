-- Drop the database 'SimpleLeaderboard'
-- Connect to the 'master' database to run this snippet
USE master
GO
IF EXISTS (
  SELECT name
   FROM sys.databases
   WHERE name = N'SimpleLeaderboard'
)
DROP DATABASE SimpleLeaderboard
GO
CREATE DATABASE SimpleLeaderboard;
GO