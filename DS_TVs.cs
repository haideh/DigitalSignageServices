//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class DS_TVs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DS_TVs()
        {
            this.DS_TVContentAds = new HashSet<DS_TVContentAds>();
            this.DS_TVContents = new HashSet<DS_TVContents>();
        }
    
        public decimal id { get; set; }
        public Nullable<decimal> category_id { get; set; }
        public Nullable<decimal> alarm_id { get; set; }
        public string title { get; set; }
        public Nullable<long> datetime { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<short> type { get; set; }
        public string description { get; set; }
        public string ip { get; set; }
        public Nullable<short> isDirty { get; set; }
        public Nullable<long> lastAlive { get; set; }
        public Nullable<int> x { get; set; }
        public Nullable<int> y { get; set; }
        public Nullable<decimal> companyId { get; set; }
    
        public virtual DS_Alarms DS_Alarms { get; set; }
        public virtual DS_TVCategories DS_TVCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_TVContentAds> DS_TVContentAds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_TVContents> DS_TVContents { get; set; }
    }
}
