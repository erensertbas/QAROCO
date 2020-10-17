using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Qaroco.DL.ViewModels
{
	[DataContract]
	public class CargoVM
	{
		[DataMember]
		public Order _Order { get; set; }
		[DataMember]
		public Location _Location { get; set; }
		[DataMember]
		public ProductInfo _ProductInfo { get; set; }

		[DataMember]
		public CargoTransaction _CargoTransaction { get; set; }



	}
}
