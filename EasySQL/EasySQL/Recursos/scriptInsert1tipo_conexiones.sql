USE [usuarios]
GO

INSERT INTO [dbo].[tipo_conexion]
           ([id_tipo]
           ,[nombre]
           ,[puerto_defecto])
     VALUES
           (1, 'Microsoft SQL', 1433)
GO

USE [usuarios]
GO

INSERT INTO [dbo].[tipo_conexion]
           ([id_tipo]
           ,[nombre]
           ,[puerto_defecto])
     VALUES
           (2, 'MySQL', 3306)
GO


