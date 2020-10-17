using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Qaroco.SL
{
	[DataContract]
	public class BlogViewModel
	{
		[DataMember]
		public int BlogId { get; set; }
		[DataMember]
		public Nullable<int> UserId { get; set; }
		[DataMember]
		public string BlogTitle { get; set; }
		[DataMember]
		public string BlogContent { get; set; }
	}
}