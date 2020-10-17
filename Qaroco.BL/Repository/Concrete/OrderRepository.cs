using Qaroco.BL.Repository.Abstract;
using Qaroco.DL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qaroco.BL.Repository.Concrete
{
    public class OrderRepository : BaseRepository<Order>
    {
        public bool _UpdateOrder(Order order)
        {
            db.Orders.Attach(order);
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            var result= db.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
