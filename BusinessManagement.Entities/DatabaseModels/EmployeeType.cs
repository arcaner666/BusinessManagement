using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employees = new HashSet<Employee>();
        }

        public short EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
