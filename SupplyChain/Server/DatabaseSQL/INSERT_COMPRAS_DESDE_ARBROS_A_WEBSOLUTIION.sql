USE [WEBSOLUTIION]
GO

INSERT INTO [dbo].[Compras]
           ([NUMERO]
           ,[CG_PREP]
           ,[CG_ORDEN]
           ,[CG_MAT]
           ,[DES_MAT]
           ,[TIPO]
           ,[TILDE]
           ,[TILDE1]
           ,[TILDE2]
           ,[TILDE3]
           ,[NECESARIO]
           ,[SOLICITADO]
           ,[AUTORIZADO]
           ,[UNID]
           ,[CG_DEN]
           ,[UNID1]
           ,[PRECIO]
           ,[BON]
           ,[MONEDA]
           ,[CG_PROVE]
           ,[DES_PROVE]
           ,[FE_PREV]
           ,[FE_REAL]
           ,[FE_VENC]
           ,[FE_CIERRE]
           ,[CONDVEN]
           ,[CG_CUENT]
           ,[DIASVIGE]
           ,[CANTLOTE]
           ,[CANTMIN]
           ,[ESPECIFICA]
           ,[ESPEGEN]
           ,[ESTADO]
           ,[FE_DISP]
           ,[ABIERTOPREPARACION]
           ,[NUMREQ]
           ,[FE_REQ]
           ,[FE_AUTREQ]
           ,[CG_PROVEREQ]
           ,[OBSEREQ]
           ,[MARCAREQ]
           ,[AVANCE]
           ,[TXTOBSERVADO]
           ,[TXTCORREGIDO]
           ,[USUARIO_AUT]
           ,[FE_AUT]
           ,[FE_CIERREREQ]
           ,[USUREQ]
           ,[ESTADO_CAB]
           ,[ESTADO_IT]
           ,[NECESARIO_ORI]
           ,[NUM_SOLCOT]
           ,[SOLICITADO_ORI]
           ,[MODIF_INGRESO]
           ,[PENDIENTE]
           ,[OBSERVACIONES]
           ,[CG_CIA]
           ,[USUARIO])
Select [NUMERO]
           ,[CG_PREP]
           ,[CG_ORDEN]
           ,[CG_MAT]
           ,[DES_MAT]
           ,[TIPO]
           ,[TILDE]
           ,[TILDE1]
           ,[TILDE2]
           , CASE WHEN [TILDE3] IS NULL THEN '' ELSE [TILDE3] END
           ,[NECESARIO]
           ,[SOLICITADO]
           ,[AUTORIZADO]
           ,[UNID]
           ,[CG_DEN]
           ,[UNID1]
           ,[PRECIO]
           ,[BON]
           ,[MONEDA]
           ,NROCLTE
           ,[DES_PROVE]
           ,[FE_PREV]
           ,[FE_REAL]
           ,[FE_VENC]
           ,[FE_CIERRE]
           ,[CONDVEN]
           ,[CG_CUENT]
           ,[DIASVIGE]
           ,[CANTLOTE]
           ,[CANTMIN]
           ,[ESPECIFICA]
           ,[ESPEGEN]
           ,[ESTADO]
           ,[FE_DISP]
           ,[ABIERTOPREPARACION]
           ,[NUMREQ]
           ,[FE_REQ]
           ,[FE_AUTREQ]
           ,[CG_PROVEREQ]
           ,[OBSEREQ]
           ,[MARCAREQ]
           ,[AVANCE]
           ,[TXTOBSERVADO]
           ,[TXTCORREGIDO]
           ,[USUARIO_AUT]
           ,[FE_AUT]
           ,[FE_CIERREREQ]
           ,[USUREQ]
           ,[ESTADO_CAB]
           ,[ESTADO_IT]
           ,[NECESARIO_ORI]
           ,[NUM_SOLCOT]
           ,[SOLICITADO_ORI]
           ,[MODIF_INGRESO]
           ,[PENDIENTE]
           ,[OBSERVACIONES]
           ,[CG_CIA]
           ,[USUARIO] From Arbros.dbo.COMPRAS