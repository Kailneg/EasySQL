/*****
Insertando conexiones para usuario 2 manolo
******/

USE [usuarios]
GO

INSERT INTO [dbo].[conexion]
           ([id_tipo_conexion]
           ,[id_usuario]
           ,[nombre]
           ,[direccion]
           ,[puerto]
           ,[usuario]
           ,[contrasenia]
           ,[fecha_creacion])
     VALUES
           (0
           ,2
           ,'ManolinBBDD'
           ,'localhost/Manolin'
           ,12345
           ,'Integrated Security'
           ,''
           ,default)
GO

INSERT INTO [dbo].[conexion]
           ([id_tipo_conexion]
           ,[id_usuario]
           ,[nombre]
           ,[direccion]
           ,[puerto]
           ,[usuario]
           ,[contrasenia]
           ,[fecha_creacion])
     VALUES
           (1
           ,2
           ,'La mejor bbdd'
           ,'localhost/MySQLServer'
           ,3306
           ,'manolin'
           ,'manolito'
           ,default)
GO

INSERT INTO [dbo].[conexion]
           ([id_tipo_conexion]
           ,[id_usuario]
           ,[nombre]
           ,[direccion]
           ,[puerto]
           ,[usuario]
           ,[contrasenia]
           ,[fecha_creacion])
     VALUES
           (1
           ,2
           ,'La segunda mejor bbd'
           ,'localhost/MySQLServer2'
           ,3306
           ,'manolin2'
           ,'manolito2'
           ,default)
GO