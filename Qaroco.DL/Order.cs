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
    public partial class Order
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public Nullable<int> CustomerId { get; set; }
        [DataMember]
        public Nullable<int> CourierId { get; set; }
        [DataMember]
        public Nullable<int> ProductId { get; set; }
        [DataMember]
        public Nullable<int> ShippingTypeId { get; set; }
        [DataMember]
        public string OrderNote { get; set; }
        [DataMember]
        public Nullable<bool> OrderStatus { get; set; }
        [DataMember]
        public Nullable<int> LocationId { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OrderDate { get; set; }

    }
}
