using System;

namespace SimpleInventoryAPI.DataAccess
{
    public abstract class BaseEntity<T>
    {
        public T Id                   { get; set; }
        public bool IsDeleted         { get; set; } = false;
        public string CreatedBy       { get; set; }
        public DateTime CreatedDate   { get; set; } = DateTime.UtcNow;
        public string ModifiedBy      { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetModifyByAndModifyDate(string modifyBy)
        {
            ModifiedBy   = modifyBy;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
