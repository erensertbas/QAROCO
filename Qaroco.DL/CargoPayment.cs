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
    public partial class CargoPayment
    {
        [DataMember]
        public int PaymentId { get; set; }
        [DataMember]
        public Nullable<int> UCardId { get; set; }
        [DataMember]
        public Nullable<int> CourierId { get; set; }
        [DataMember]
        public Nullable<decimal> CargoTotal { get; set; }
    }
}
