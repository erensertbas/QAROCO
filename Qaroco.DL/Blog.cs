//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Qaroco.DL
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    [DataContract]
    public partial class Blog
    {
        [DataMember]
        public int BlogId { get; set; }
        [DataMember]
        public Nullable<int> UserId { get; set; }
        [DataMember]
        public string BlogTitle { get; set; }
        [DataMember]
        public string BlogContent { get; set; }
        [DataMember]
        public Nullable<int> ViewCount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DateOfUpload { get; set; }
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
    }
}
