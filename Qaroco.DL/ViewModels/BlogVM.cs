using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qaroco.DL.ViewModels
{
	[DataContract]
	public class BlogVM
	{
        [DataMember]
        public Blog _Blog { get; set; }
        [DataMember]
        public List<BlogComment> _BlogComment { get; set; }
        [DataMember]
        public Picture _Picture { get; set; }
    }
}
