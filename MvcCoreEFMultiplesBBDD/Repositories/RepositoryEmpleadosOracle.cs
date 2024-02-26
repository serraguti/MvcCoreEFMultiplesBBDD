using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region VIEWS PROCEDURES

//create or replace view v_empleados as
//select nvl(EMP.EMP_NO, 0) as EMP_NO,
//  EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//  , DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//  from EMP
//  inner join DEPT
//  on EMP.DEPT_NO=DEPT.DEPT_NO;

//create or replace procedure SP_ALL_EMPLEADOS
//(p_cursor_empleados out sys_refcursor)
//as
//begin
//  open p_cursor_empleados for
//  select * from v_empleados;
//end;

//create or replace procedure SP_DETAILS_EMPLEADO
//(p_cursor_empleados out sys_refcursor,
//p_idempleado EMP.EMP_NO%TYPE)
//as
//begin
//  open p_cursor_empleados for
//  select * from V_EMPLEADOS
//  where EMP_NO=p_idempleado;
//end;


#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += " SP_ALL_EMPLEADOS(:p_cursor_empleados);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            //COMO ES UN TIPO DE ORACLE PROPIO (Cursor) DEBEMOS
            //PONERLO DE FORMA MANUAL
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            string sql = "begin ";
            sql += " SP_DETAILS_EMPLEADO (:p_cursor_empleados, :p_idempleado);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            OracleParameter pamId = new OracleParameter("p_idempleado", idEmpleado);
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql, pamCursor, pamId);
            EmpleadoView empleado = consulta.AsEnumerable().FirstOrDefault();
            return empleado;
        }
    }
}