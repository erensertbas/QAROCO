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
    public partial class Log
    {
        [DataMember]
        public int LogId { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LogDate { get; set; }
        [DataMember]
        public string LogDescription { get; set; }
        [DataMember]
        public string LogEmail { get; set; }
        [DataMember]
        public string LogIp { get; set; }
    }
}
