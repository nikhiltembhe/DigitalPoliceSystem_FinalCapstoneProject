/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [PoliceDB].[dbo].[AspNetRoles]
  SELECT *
  FROM [PoliceDB].[dbo].[AspNetUsers]
  SELECT *
  FROM [PoliceDB].[dbo].[AspNetUserRoles]

  DELETE 
  FROM [PoliceDB].[dbo].[AspNetUserRoles]
  DELETE 
  FROM [PoliceDB].[dbo].[AspNetUsers]
  DELETE 
  FROM [PoliceDB].[dbo].[AspNetRoles]

