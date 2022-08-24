use sparkDB
--GetMacOnlyMac
alter PROCEDURE GetMacOnlyMac(
@Mac nvarchar(max),
@Id uniqueidentifier
)
AS
BEGIN
	
	IF EXISTS( SELECT Id,Dispositivos.MacAddress from Dispositivos where MacAddress = @Mac)
		SELECT Id,Dispositivos.MacAddress from Dispositivos where MacAddress = @Mac
	Else 
		Insert into Dispositivos (Id,CuentaId,LugarRegionId,Tipo,Estado,Fecha,MacAddress)
		values(@Id, '00000000-0000-0000-0000-000000000000',
					'00000000-0000-0000-0000-000000000000',
						'Init','Init',GETdate(),@Mac);
		SELECT Id,Dispositivos.MacAddress from Dispositivos where MacAddress = @Mac
END
GO
exec GetMacOnlyMac 1232224

--InsertDispositovoMac
alter PROCEDURE InsertDispositovoMac(
@Id uniqueidentifier,
@Mac nvarchar(max)

)
AS
BEGIN
	
	Insert into Dispositivos (id,CuentaId,LugarRegionId,Tipo,Estado,Fecha,MacAddress)
	 values(@Id, '00000000-0000-0000-0000-000000000000',
					'00000000-0000-0000-0000-000000000000',
						'Init','Init',GETdate(),@Mac);
END
GO
exec InsertDispositovoMac '10000000-0000-0000-0000-000000000000','123'

--ListDispositivos
CREATE PROCEDURE  ListDispositivos
@page int,  
@rows int 
AS 
BEGIN  

 
 SELECT Dispositivos.MacAddress,
 COUNT(*) OVER() TotalRecords
 FROM Dispositivos
 order by Fecha
 OFFSET (@page - 1)*@rows ROWS                  
 FETCH NEXT @rows ROWS ONLY
 
END
exec ListDispositivos 1,20

alter PROCEDURE  CountDispositivos

AS 
BEGIN  
 Select count(*) as TotalRecords from Dispositivos
END

exec CountDispositivos
--ListDispositivosAll

CREATE PROCEDURE  ListDispositivosAll

AS 
BEGIN  

 
 SELECT Dispositivos.MacAddress
 FROM Dispositivos
 order by Fecha

 
END

exec ListDispositivosAll
--MachDispocitivo

alter PROCEDURE MachDispocitivo(
@Id uniqueidentifier,
@Mac nvarchar(max) ,
@Cuenta uniqueidentifier,
@Region uniqueidentifier,
@Type nvarchar(max) 
)
AS
BEGIN
	
	 SELECT   @Id = id from Dispositivos where MacAddress = @Mac
		if exists (select * from Dispositivos where id = @Id and MacAddress = @Mac )
			update Dispositivos set CuentaId = @Cuenta,  lugarregionid =  @Region ,estado = 'Mach',tipo = @type where id = @Id and MacAddress = @Mac 
				Select * from Dispositivos where id = @Id and MacAddress = @Mac
END
GO
exec MachDispocitivo '10000000-0000-0000-0000-000000000000','12','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','G'
select * from Dispositivos




create procedure GetDispositivoByMac(
@Mac nvarchar(max))
as
begin


	SELECT [Id]
      ,[CuentaId]
      ,[LugarRegionId]
      ,[Tipo]
      ,[Estado]
      ,[Fecha]
      ,[MacAddress]
      ,[FechaMact]
  FROM [SparkDB].[dbo].[Dispositivos] Where MacAddress = @Mac
END
GO
exec GetDispositivoByMac '40:f5:20:07:2f:e7''ASASDASD' '(','ASASDASD','40:f5:20:07:2f:e7')'