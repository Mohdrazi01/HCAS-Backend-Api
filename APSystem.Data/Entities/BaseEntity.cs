using System;

namespace APSystem.Data.Entities
{
    public class BaseEntity
    {
        public bool IsActive {get;set;}
        public DateTime? CreatedDate {get;set;}
        public DateTime? ModifiedDate {get;set;}
        public int? CreatedBy {get;set;}
        public int? ModifiedBy {get;set;}
    }
}