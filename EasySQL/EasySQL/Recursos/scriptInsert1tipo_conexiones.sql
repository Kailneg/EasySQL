USE [usuarios]
GO

INSERT INTO [dbo].[tipo_conexion]
           ([id_tipo]
           ,[nombre]
           ,[puerto_defecto])
     VALUES
           (0, 'Microsoft SQL', 1433)
GO

USE [usuarios]
GO

INSERT INTO [dbo].[tipo_conexion]
           ([id_tipo]
           ,[nombre]
           ,[puerto_defecto])
     VALUES
           (1, 'MySQL', 3306)
GO


