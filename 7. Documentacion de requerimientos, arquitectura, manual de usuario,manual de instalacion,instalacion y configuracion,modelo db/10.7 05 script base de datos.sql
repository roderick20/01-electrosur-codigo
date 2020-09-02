USE [master]
GO

CREATE DATABASE [Electrosur]
GO
-- Electrosur : nombre de la base de datos
-- ElectrosurLogin: usuario de la base de datos

CREATE LOGIN [ElectrosurLogin] WITH PASSWORD=N'Aladino09', DEFAULT_DATABASE=[Electrosur]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [ElectrosurLogin]
GO
USE [Electrosur]
GO
CREATE USER [ElectrosurLogin] FOR LOGIN [ElectrosurLogin]
GO
ALTER USER [ElectrosurLogin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ElectrosurLogin]
GO

GO
CREATE TABLE [dbo].[pdpOPEpOperacion](
	[OPEid_operacion] [bigint] IDENTITY(1,1) NOT NULL,
	[OPEcreado] [datetime] NOT NULL,
	[USRid_usuario] [bigint] NOT NULL,
	[OPEsuministro] [bigint] NOT NULL,
	[OPEmonto] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_pdpOPEpOperacion] PRIMARY KEY CLUSTERED 
(
	[OPEid_operacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pdpPAGpPago](
	[PAGid_pago] [bigint] IDENTITY(1,1) NOT NULL,
	[USRid_usuario] [bigint] NOT NULL,
	[OPEid_operacion] [bigint] NOT NULL,
	[PAGnumero_recibo] [bigint] NOT NULL,
	[PAGcodigo_recibo] [nvarchar](50) NOT NULL,
	[PAGperiodo] [nvarchar](7) NOT NULL,
	[OPEmonto] [decimal](8, 2) NOT NULL,
	[OPEsuministro] [bigint] NOT NULL,
	[PAGmetodo_pago] [int] NOT NULL,
	[PAGcreado] [datetime] NOT NULL,
 CONSTRAINT [PK_pdpPAGpPago] PRIMARY KEY CLUSTERED 
(
	[PAGid_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pdpUSRtUsuarioDelSistema](
	[USRid_usuario] [bigint] IDENTITY(1,1) NOT NULL,
	[USRunique_id] [uniqueidentifier] NOT NULL,
	[USRtipo_documento] [nvarchar](18) NOT NULL,
	[USRnumero_documento] [nvarchar](12) NOT NULL,
	[USRnombre] [nvarchar](100) NOT NULL,
	[USRapellido_paterno] [nvarchar](100) NOT NULL,
	[USRapellido_materno] [nvarchar](100) NOT NULL,
	[USRcorreo_primario] [nvarchar](100) NOT NULL,
	[USRcorreo_secundario] [nvarchar](100) NOT NULL,
	[USRtelefono] [nvarchar](12) NOT NULL,
	[USRcontrasena] [nvarchar](50) NOT NULL,
	[USRestado] [bit] NOT NULL,
	[USRcreado] [datetime] NOT NULL,
	[USRmodificado] [datetime] NOT NULL,
	[USRultimo_acceso] [datetime] NOT NULL,
	[USRconfirmacion_correo] [bit] NOT NULL,
	[USRrecuperar_contrasena] [bit] NOT NULL,
 CONSTRAINT [PK_pdpUSRtUsuarioDelSistema] PRIMARY KEY CLUSTERED 
(
	[USRid_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[pdpOPEpOperacion]  WITH CHECK ADD  CONSTRAINT [FK_pdpOPEpOperacion_pdpUSRtUsuarioDelSistema] FOREIGN KEY([USRid_usuario])
REFERENCES [dbo].[pdpUSRtUsuarioDelSistema] ([USRid_usuario])
GO
ALTER TABLE [dbo].[pdpOPEpOperacion] CHECK CONSTRAINT [FK_pdpOPEpOperacion_pdpUSRtUsuarioDelSistema]
GO
ALTER TABLE [dbo].[pdpPAGpPago]  WITH CHECK ADD  CONSTRAINT [FK_pdpPAGpPago_pdpOPEpOperacion] FOREIGN KEY([OPEid_operacion])
REFERENCES [dbo].[pdpOPEpOperacion] ([OPEid_operacion])
GO
ALTER TABLE [dbo].[pdpPAGpPago] CHECK CONSTRAINT [FK_pdpPAGpPago_pdpOPEpOperacion]
GO
ALTER TABLE [dbo].[pdpPAGpPago]  WITH CHECK ADD  CONSTRAINT [FK_pdpPAGpPago_pdpUSRtUsuarioDelSistema] FOREIGN KEY([USRid_usuario])
REFERENCES [dbo].[pdpUSRtUsuarioDelSistema] ([USRid_usuario])
GO
ALTER TABLE [dbo].[pdpPAGpPago] CHECK CONSTRAINT [FK_pdpPAGpPago_pdpUSRtUsuarioDelSistema]
GO

