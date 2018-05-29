/*****
Insertando conexiones para usuario 1
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
           (1
           ,1
           ,'Mi Microsoft SQL'
           ,'localhost/SQLALE'
           ,1433
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
           (2
           ,1
           ,'Mi MySQL'
           ,'localhost/MySQLServer'
           ,3306
           ,'root'
           ,'root'
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
           ,1
           ,'cocina'
           ,'localhost/SQLALE'
           ,1433
           ,'Integrated Security'
           ,''
           ,default)
GO