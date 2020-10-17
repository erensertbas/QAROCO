using Qaroco.BL.Repository.Abstract;
using Qaroco.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Qaroco.BL.Repository.Concrete
{
    public class UserRepository : BaseRepository<User>
    {
        public User Login(string email, string password)
        {
            return db.Set<User>().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    } 
}
