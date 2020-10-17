using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qaroco.DL.ViewModels
{
	[DataContract]
	public class BlogCommentVM
	{

		[DataMember]
		public int BlogCommentId { get; set; }
		[DataMember]
		public Nullable<int> BlogId { get; set; }
		[DataMember]
		public Nullable<int> UserId { get; set; }
		[DataMember]
		public string BlogTitle { get; set; }
		[DataMember]
		public string BlogContent { get; set; }

	}
}
