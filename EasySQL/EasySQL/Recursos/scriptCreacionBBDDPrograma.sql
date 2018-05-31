/*
	Crea la Base de datos
*/
CREATE DATABASE usuarios

/*
	Crea la tabla usuario
*/
USE [usuarios]
GO

ALTER TABLE [dbo].[usuario] DROP CONSTRAINT [DF_usuario_fecha_creacion]
GO

DROP TABLE [dbo].[usuario]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
	[contrasenia] [nvarchar](30) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[usuario] ADD  CONSTRAINT [DF_usuario_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO

/*
	Crea la tabla tipo_conexion
*/
USE [usuarios]
GO

DROP TABLE [dbo].[tipo_conexion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tipo_conexion](
	[id_tipo] [int] NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
	[puerto_defecto] [int] NOT NULL,
 CONSTRAINT [PK_tipo_conexion] PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*************************
	Crea la tabla conexion
**************************/
USE [usuarios]
GO

ALTER TABLE [dbo].[conexion] DROP CONSTRAINT [FK_conexion_usuario]
GO

ALTER TABLE [dbo].[conexion] DROP CONSTRAINT [FK_conexion_tipoconexion]
GO

ALTER TABLE [dbo].[conexion] DROP CONSTRAINT [DF_conexion_fecha_creacion]
GO

DROP TABLE [dbo].[conexion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[conexion](
	[id_conexion] [int] IDENTITY(1,1) NOT NULL,
	[id_tipo_conexion] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[nombre] [nvarchar](30) NOT NULL,
	[direccion] [nvarchar](50) NOT NULL,
	[puerto] [int] NOT NULL,
	[usuario] [nvarchar](50) NOT NULL,
	[contrasenia] [nvarchar](50) NULL,
	[fecha_creacion] [datetime] NULL,
 CONSTRAINT [PK_conexion] PRIMARY KEY CLUSTERED 
(
	[id_conexion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[conexion] ADD  CONSTRAINT [DF_conexion_fecha_creacion]  DEFAULT (getdate()) FOR [fecha_creacion]
GO

ALTER TABLE [dbo].[conexion]  WITH CHECK ADD  CONSTRAINT [FK_conexion_tipoconexion] FOREIGN KEY([id_tipo_conexion])
REFERENCES [dbo].[tipo_conexion] ([id_tipo])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[conexion] CHECK CONSTRAINT [FK_conexion_tipoconexion]
GO

ALTER TABLE [dbo].[conexion]  WITH CHECK ADD  CONSTRAINT [FK_conexion_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[conexion] CHECK CONSTRAINT [FK_conexion_usuario]
GO