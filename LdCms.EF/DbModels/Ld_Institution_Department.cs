using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Department
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string ParentID { get; set; }
        public string NodePath { get; set; }
        public int? RankID { get; set; }
        public int? SortID { get; set; }
        public string SortPath { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
