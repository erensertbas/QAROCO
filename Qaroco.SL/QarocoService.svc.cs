using Qaroco.BL.Repository.Concrete;
using Qaroco.DL;
using Qaroco.DL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Qaroco.SL
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class QarocoService : IQarocoService
    {
        UserRepository userRepository = new UserRepository();
        LogRepository logRepository = new LogRepository();
        MessageSystemRepository msrepository = new MessageSystemRepository();
        BlogRepository blogRepository = new BlogRepository();
        BlogCommentRepository blogCommentRepository = new BlogCommentRepository();
        PictureRepository pictureRepository = new PictureRepository();
        CargoTransactionRepository cargoRepository = new CargoTransactionRepository();
        OrderRepository orderRepository = new OrderRepository();
        ProductInfoRepository productRepository = new ProductInfoRepository();
        LocationRepository locationRepository = new LocationRepository();

        #region AddresOperations

        public bool AddressAdd(Address model)
        {
            throw new NotImplementedException();
        }

        public bool AddressDelete(Address model)
        {
            throw new NotImplementedException();
        }

        public List<Address> AddressList()
        {
            throw new NotImplementedException();
        }
        #endregion

        public bool SendCargo(CargoVM model)
        {
            var _product = productRepository.Add(new ProductInfo
            {
                Height = model._ProductInfo.Height,
                Price = model._ProductInfo.Price,
                Size = model._ProductInfo.Size,
                Weight = model._ProductInfo.Weight,
                Width = model._ProductInfo.Width,
                ProductDate = model._ProductInfo.ProductDate
            });

            var product = productRepository.GetByFilterx(p => p.ProductDate == model._ProductInfo.ProductDate || p.Weight == model._ProductInfo.Weight);

            var _location = locationRepository.Add(new Location
            {
                Address = model._Location.Address,   // address - Description - lat - lng - locationDate 
                Description = model._Location.Description,
                Lat = model._Location.Lat,
                Lng = model._Location.Lng,
                LocationDate = model._Location.LocationDate
            });

            var location = locationRepository.GetByFilterx(l => l.LocationDate == model._Location.LocationDate || l.Address == model._Location.Address && l.Description == model._Location.Description);

            var _order = orderRepository.Add(new Order
            {
                CustomerId = model._Order.CustomerId,
                LocationId = location.LocationId,
                OrderDate = model._Order.OrderDate,
                OrderNote = model._Order.OrderNote,
                //Birisi Aldığında true olur
                OrderStatus = false,
                ProductId = product.ProductId

            });

            if (_product && _location && _order)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCargo(Order model)
        {

            var _order = orderRepository.GetById(model.OrderId);
            _order.CourierId = model.CourierId;
            _order.CustomerId = model.CustomerId;
            _order.LocationId = model.LocationId;
            _order.OrderDate = model.OrderDate;
            _order.OrderId = model.OrderId;
            _order.OrderNote = model.OrderNote;
            _order.OrderStatus = model.OrderStatus;
            _order.ProductId = model.ProductId;
            _order.ShippingTypeId = model.ShippingTypeId;
           
            return orderRepository.Update(_order, model.OrderId);

        }

        public List<CargoOrderVM> ListCargoByCourier(int id)
        {
            List<CargoOrderVM> cargoOrderVM = new List<CargoOrderVM>();
            var order = orderRepository.GetByFilter(x => x.CourierId == id);
            foreach (var item in order)
            {
                cargoOrderVM.Add(new CargoOrderVM
                {
                    _Order = item,
                    _Location = locationRepository.GetByFilterx(x => x.LocationId == item.LocationId),
                    _ProductInfo = productRepository.GetByFilterx(x => x.ProductId == item.ProductId)
                });
            }
            return cargoOrderVM;
        }
        public List<CargoOrderVM> ListCargoByCustomer(int id)
        {
            List<CargoOrderVM> cargoOrderVM = new List<CargoOrderVM>();
            var order = orderRepository.GetByFilter(x => x.CustomerId == id);
            foreach (var item in order)
            {
                cargoOrderVM.Add(new CargoOrderVM
                {
                    _Order = item,
                    _Location = locationRepository.GetByFilterx(x => x.LocationId == item.LocationId),
                    _ProductInfo = productRepository.GetByFilterx(x => x.ProductId == item.ProductId)
                });
            }
            return cargoOrderVM;
        }
        public List<CargoOrderVM> ListCargo()
        {
            List<CargoOrderVM> cargoOrderVM = new List<CargoOrderVM>();
            var order = orderRepository.GetByFilter(x => x.CourierId == null);
            foreach (var item in order)
            {
                cargoOrderVM.Add(new CargoOrderVM
                {
                    _Order = item,
                    _Location = locationRepository.GetByFilterx(x => x.LocationId == item.LocationId),
                    _ProductInfo = productRepository.GetByFilterx(x => x.ProductId == item.ProductId)
                });
            }
            return cargoOrderVM;
        }
        #region BlogOperations


        public bool BlogDelete(int id)
        {
            throw new NotImplementedException();
        }

        public Blog BlogDetail(int id)
        {
            var deger = blogRepository.GetByFilterx(x => x.BlogId == id);
            return deger;
        }
        public bool BlogAdd(BlogVM model)
        {
            return blogRepository.Add(new Blog
            {
                BlogContent = model._Blog.BlogContent,
                BlogId = model._Blog.BlogId,
                UserId = 1,
                BlogTitle = model._Blog.BlogTitle,
                DateOfUpload = model._Blog.DateOfUpload

            });
        }
        public List<Blog> BlogList()
        {
            var blogs = blogRepository.GetAll();
            var model = new List<Blog>();
            foreach (var item in blogs)
            {
                model.Add(new Blog
                {
                    BlogContent = item.BlogContent,
                    BlogId = item.BlogId,
                    BlogTitle = item.BlogTitle,
                    UserId = item.UserId,
                    CategoryName = item.CategoryName,
                    DateOfUpload = item.DateOfUpload,
                    TagName = item.TagName,
                    ViewCount = item.ViewCount
                });
            }

            return model;
        }
        public List<BlogVM> BlogVMList()
        {
            var blogs = blogRepository.GetAll();
            var model = new List<BlogVM>();
            foreach (var item in blogs)
            {
                model.Add(new BlogVM
                {
                    _Blog = item
                });
            }
            for (int i = 0; i < blogs.Count; i++)
            {
                if (model[i] != null)
                {
                    int id = model[i]._Blog.BlogId;
                    model[i]._BlogComment = blogCommentRepository.GetByFilter(x => x.BlogId == id).ToList();
                    model[i]._Picture = pictureRepository.GetByFilterx(x => x.BlogId == id);
                }
            }

            return model;
        }
        #endregion

        #region BlogCommentOperations

        public bool BlogCommentAdd(BlogComment model)
        {
            return blogCommentRepository.Add(new BlogComment
            {
                BlogCommentId = model.BlogCommentId,
                BlogId = model.BlogId,
                UserId = model.UserId,
                Comment = model.Comment
            });

        }

        public bool BlogCommentDelete(BlogComment model)
        {
            throw new NotImplementedException();
        }

        public List<BlogComment> BlogCommentList(int id)
        {
            return blogCommentRepository.GetByFilter(x => x.BlogId == id);
        }
        #endregion

        #region CargoTransactionOperations

        public bool CargoTransactionAdd(CargoVM model)
        {

            throw new NotImplementedException();
        }



        public bool CargoTransactionDelete(CargoVM model)
        {
            throw new NotImplementedException();
        }

        public List<CargoVM> CargoTransactionList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CityOperations

        public List<City> CityList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CourierOperations

        CourierRepository courierRepository = new CourierRepository();

        public bool CourierAdd(CourierVM model)
        {
            userRepository.Add(new User { UserActiveStatus = true, AddressId = model.AddressId, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Password = model.Password, Phone = model.Phone, RoleId = 5, TcNo = model.TcNo, BirthYear = model.BirthYear });
            User User = userRepository.GetByFilterx(x => x.TcNo == model.TcNo);
            return courierRepository.Add(new Courier { UserId = User.UserId, RFId = 1 });
        }

        public bool CourierDelete(Courier model)
        {
            throw new NotImplementedException();
        }

        public bool CourierUpdate(User model)
        {
            var user = userRepository.GetById(model.UserId);
            user.BirthYear = model.BirthYear;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
            user.Phone = model.Phone;
            user.TcNo = model.TcNo;
            user.AddressId = model.AddressId;
            user.RoleId = 5;
            user.UserActiveStatus = model.UserActiveStatus;
            user.UserId = model.UserId;
            user.PictureId = model.PictureId;
            return userRepository.Update(user, model.UserId);

        }

        public List<Courier> CourierList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CourierStatisticOperations

        public bool CourierStatisticAdd(CourierStatistic model)
        {
            throw new NotImplementedException();
        }

        public bool CourierStatisticDelete(CourierStatistic model)
        {
            throw new NotImplementedException();
        }

        public List<CourierStatistic> CourierStatisticList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CustomerOperations

        CustomerRepository customerRepository = new CustomerRepository();

        public bool CustomerAdd(CustomerVM model)
        {
            userRepository.Add(new User { UserActiveStatus = true, AddressId = model.AddressId, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Password = model.Password, Phone = model.Phone, RoleId = 4, TcNo = model.TcNo, BirthYear = model.BirthYear });
            User User = userRepository.GetByFilterx(x => x.TcNo == model.TcNo);
            return customerRepository.Add(new Customer { UserId = User.UserId });
        }
        public bool CustomerEdit(User model)
        {
            var user = userRepository.GetById(model.UserId);
            user.BirthYear = model.BirthYear;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
            user.Phone = model.Phone;
            user.TcNo = model.TcNo;
            user.AddressId = model.AddressId;
            user.RoleId = 4;
            user.UserActiveStatus = true;
            user.UserId = model.UserId;
            user.PictureId = model.PictureId;
            return userRepository.Update(user, model.UserId);
        }

        public bool CustomerDelete(Customer model)
        {
            throw new NotImplementedException();
        }

        public List<Customer> CustomerList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DistrictOperations

        public List<District> DistrictList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region MessageSystemOperations

        public bool MessageSystemAdd(MessageSystem model)
        {
            return msrepository.Add(model);

        }

        public bool MessageSystemDelete(int id)
        {
            return msrepository.DeleteById(id);
        }

        public List<MessageSystem> MessageSystemList()
        {
            return msrepository.GetAll();
        }

        public MessageSystem MessageSystemDetail(int id)
        {
            return msrepository.GetByFilterx(x => x.MessageId == id);
        }

        #endregion

        #region OrderOperations

        public bool OrderAdd(Order model)
        {
            throw new NotImplementedException();
        }

        public bool OrderUpdate(Order model)
        {
            throw new NotImplementedException();
        }

        public bool OrderDelete(Order model)
        {
            throw new NotImplementedException();
        }

        public List<Order> OrderList(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PictureOperations

        public bool PictureAdd(Picture model)
        {
            throw new NotImplementedException();
        }

        public bool PictureDelete(Picture model)
        {
            throw new NotImplementedException();
        }

        public List<Picture> PictureList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ProductInfoOperations

        public bool ProductInfoAdd(ProductInfo model)
        {
            throw new NotImplementedException();
        }

        public bool ProductInfoDelete(ProductInfo model)
        {
            throw new NotImplementedException();
        }

        public List<ProductInfo> ProductInfoList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RegisterFolderOperations

        public bool RegisterFolderAdd(RegisterFolder model)
        {
            throw new NotImplementedException();
        }

        public bool RegisterFolderDelete(RegisterFolder model)
        {
            throw new NotImplementedException();
        }

        public List<RegisterFolder> RegisterFolderList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RoleOperations

        public List<Role> RoleList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ShippingTypeOperations

        public List<ShippingType> ShippingTypeList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserBankOperations

        public List<UserBank> UserBankList()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserCardInfoOperations

        public bool UserCardInfoAdd(UserCardInfo model)
        {
            throw new NotImplementedException();
        }

        public bool UserCardInfoDelete(UserCardInfo model)
        {
            throw new NotImplementedException();
        }

        public List<UserCardInfo> UserCardInfoList()
        {
            throw new NotImplementedException();
        }

        public User UserLogin(User user)
        {
            return userRepository.Login(user.Email, user.Password);
        }


        #endregion

        #region UserOperations
        public User UserListId(int id)
        {
            return userRepository.GetByFilterx(x => x.UserId == id);


        }
        public bool UserEdit(User model)
        {
            var user = userRepository.GetById(model.UserId);
            user.BirthYear = model.BirthYear;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
            user.Phone = model.Phone;
            user.TcNo = model.TcNo;
            user.AddressId = model.AddressId;
            user.RoleId = model.RoleId;
            user.UserActiveStatus = model.UserActiveStatus;
            user.UserId = model.UserId;
            user.PictureId = model.PictureId;
            return userRepository.Update(user, model.UserId);
        }


        public List<User> UserList()
        {
            return userRepository.GetAll();
        }



        #endregion

        #region LogOperations
        public bool LogAdd(Log model)
        {
            return logRepository.Add(model);
        }

        public List<Log> LogList(string email)
        {
            return logRepository.GetByFilter(x => x.LogEmail == email);
        }

        public User UserLog(string mail)
        {
            return userRepository.GetByFilterx(x => x.Email == mail);
        }

		#endregion


	}
}
