using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;

#region VIEWS Y PROCEDURES

//create view v_empleados
//as
//	select isnull(EMP.EMP_NO, 0) as EMP_NO,
//    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//	, DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//	from EMP
//	inner join DEPT
//	on EMP.DEPT_NO=DEPT.DEPT_NO
//go

//create procedure SP_ALL_EMPLEADOS
//as
//	select * from v_empleados
//go

//create procedure SP_DETAILS_EMPLEADO
//(@idempleado int)
//as
//	select * from v_empleados 
//	where EMP_NO=@idempleado
//go

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosSQLServer: IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosSQLServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            string sql = "SP_DETAILS_EMPLEADO @idempleado";
            SqlParameter pamId = new SqlParameter("@idempleado", idEmpleado);
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql, pamId);
            EmpleadoView empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;
        }
    }
}
