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
    public partial class ProductInfo
    {
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public Nullable<int> Weight { get; set; }
        [DataMember]
        public Nullable<int> Height { get; set; }
        [DataMember]
        public Nullable<int> Width { get; set; }
        [DataMember]
        public Nullable<int> Size { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ProductDate { get; set; }

    }
}