using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using MySqlConnector;

#region VIEWS Y PROCEDURES

//create view V_EMPLEADOS as
//select IFNULL(EMP.EMP_NO, 0) as EMP_NO,
//  EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//  , DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//  from EMP
//  inner join DEPT
//  on EMP.DEPT_NO=DEPT.DEPT_NO;

//DELIMITER $$
//create procedure SP_ALL_EMPLEADOS()
//begin
//	select * from V_EMPLEADOS;
//end$$
//DELIMITER ;

//call SP_ALL_EMPLEADOS();

//DELIMITER //
//create procedure SP_DETAILS_EMPLEADO
//(IN p_idempleado int)
//begin
//	select * from V_EMPLEADOS
//    where EMP_NO=p_idempleado;
//end//
//DELIMITER;

//call SP_DETAILS_EMPLEADO(7839);

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosMySql : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "call SP_ALL_EMPLEADOS()";
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            string sql = "call SP_DETAILS_EMPLEADO(@p_idempleado)";
            MySqlParameter pamId = new MySqlParameter("@p_idempleado", idEmpleado);
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql, pamId);
            EmpleadoView empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;
        }
    }
}
