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
    
    public partial class DS_Ads
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DS_Ads()
        {
            this.DS_AdItems = new HashSet<DS_AdItems>();
            this.DS_ContentAds = new HashSet<DS_ContentAds>();
        }
    
        public decimal id { get; set; }
        public string title { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<short> type { get; set; }
        public Nullable<int> max_minutes { get; set; }
        public Nullable<int> passed_minutes { get; set; }
        public Nullable<decimal> companyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_AdItems> DS_AdItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_ContentAds> DS_ContentAds { get; set; }
    }
}