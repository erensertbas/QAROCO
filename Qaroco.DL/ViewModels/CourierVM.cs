using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qaroco.DL.ViewModels
{
    [DataContract]
    public class CourierVM
    {
        [DataMember]
        public int CourierId { get; set; }
        [DataMember]
        public Nullable<int> RFId { get; set; }

        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public Nullable<int> AddressId { get; set; }
        [DataMember]
        public Nullable<int> RoleId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string TcNo { get; set; }
		[DataMember]
		public DateTime BirthYear { get; set; }
		[DataMember]
        public string Phone { get; set; }
        [DataMember]
        public Nullable<bool> UserActiveStatus { get; set; }
    }
}
