USE [pruebas]
GO

/****** Object:  Table [dbo].[TablaMuchasColumnas]    Script Date: 07.06.2018 20:42:17 ******/
DROP TABLE [dbo].[TablaMuchasColumnas]
GO

/****** Object:  Table [dbo].[TablaMuchasColumnas]    Script Date: 07.06.2018 20:42:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TablaMuchasColumnas](
	[dinero] [bigint] NULL,
	[nombre] [nvarchar](28) NULL,
	[apellido] [nvarchar](max) NULL,
	[fecha] [datetime] NULL,
	[fechapequena] [smalldatetime] NULL,
	[datos] [xml] NULL,
	[mapa] [geography] NULL,
	[figura] [geometry] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


