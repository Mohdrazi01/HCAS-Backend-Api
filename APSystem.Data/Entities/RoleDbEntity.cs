using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblRole")]
    public class RoleDbEntity : BaseEntity
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }
}