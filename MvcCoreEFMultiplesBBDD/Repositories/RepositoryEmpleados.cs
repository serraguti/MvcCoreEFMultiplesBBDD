using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Models;

#region VIEW

//create view v_empleados
//as
//	select isnull(EMP.EMP_NO, 0) as EMP_NO,
//    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO
//	, DEPT.DNOMBRE, DEPT.LOC, DEPT.DEPT_NO
//	from EMP
//	inner join DEPT
//	on EMP.DEPT_NO=DEPT.DEPT_NO
//go

#endregion

namespace MvcCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            var consulta = from datos in this.context.EmpleadosView
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
