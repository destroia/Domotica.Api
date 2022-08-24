alter PROCEDURE  CreateCuenta(
@Id uniqueidentifier,
@Email nvarchar(max),
@Cel nvarchar(max),
@Pais nvarchar(max),
@Password nvarchar(max),
@Date datetime2
)
AS 
BEGIN  

 
If exists (Select *  from [dbo].[Cuentas] where [dbo].[Cuentas].Email = @Email)
	BEGIN
		Select @Id = id  from [dbo].[Cuentas] where [dbo].[Cuentas].Email = @Email
			--update Cuentas set Email = @Email, Celular = @Cel, Pais = @Pais,Password = @Password where id = @Id and Email = @Email
				Select *  from [dbo].[Cuentas] where [dbo].[Cuentas].Email = @Email and id = @Id
				END

else

 	insert into [dbo].[Cuentas] (id,Email,Password,Pais,Celular,Fecha) values(@Id,@Email,@Password,@Pais,@Cel,@Date)
		Select *  from [dbo].[Cuentas] where [dbo].[Cuentas].Email = @Email
 

END

exec CreateCuenta '60000000-0000-0000-0000-000000000000','7','3128626687','Colombia','13212312','2020-01-01'
                  
use SparkDB
SELECT TOP 1000 Cuentas.[Id] as CuentaId
      ,[Email]
      ,[Password]
      ,[Pais]
      ,[Celular]
      ,[Fecha],
	  Lugares.Id as LugarId,
	  Lugares.Nombre as LugarNombre,
	  LugarRegiones.Id as RegionId,
	  LugarRegiones.Nombre as RegionNombre

  FROM [SparkDB].[dbo].[Cuentas]
   inner join [SparkDB].[dbo].Lugares
   on Lugares.CuentaId = Cuentas.Id
   inner join [SparkDB].[dbo].LugarRegiones
   on Lugares.Id = LugarRegiones.LugarId
select * from cuentas
