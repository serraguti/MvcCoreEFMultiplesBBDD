using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreEFMultiplesBBDD.Models
{
    [Table("V_EMPLEADOS")]
    public class EmpleadoView
    {
        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
        [Column("DNOMBRE")]
        public string Departamento { get; set; }
        [Column("LOC")]
        public string Localidad { get; set; }
        [Column("DEPT_NO")]
        public int IdDepartamento { get; set; }
    }
}
