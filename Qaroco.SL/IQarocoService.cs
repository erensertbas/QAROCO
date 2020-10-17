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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IQarocoService
    {
        ////User
        //[OperationContract]
        //bool UserAdd(User model);
        //[OperationContract]
        //bool UserDelete(User model);
        [WebInvoke(UriTemplate = "UserLogin/User",RequestFormat =WebMessageFormat.Json, Method ="POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        User UserLogin(User user);
        [OperationContract]
        [WebInvoke(UriTemplate ="User/UserEdit", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UserEdit(User model);
        [OperationContract]
        [WebGet(UriTemplate = "User/UserListId?id={id}", ResponseFormat = WebMessageFormat.Json)]
        User UserListId(int id);
        [WebInvoke(UriTemplate = "User/UserLog", RequestFormat = WebMessageFormat.Json, Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        User UserLog(string mail);

		[OperationContract]
		[WebGet(UriTemplate = "User/UserList", ResponseFormat = WebMessageFormat.Json)]
		List<User>UserList();

		///*Update olacak.*/
		///*Update olacak.*/
		///*Update olacak.*/

		//Address
		[OperationContract]
        [WebInvoke(UriTemplate = "Address/AddressAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool AddressAdd(Address model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Address/AddressDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool AddressDelete(Address model);
        [OperationContract]
        [WebGet(UriTemplate = "Address/AddressList", ResponseFormat = WebMessageFormat.Json)]
        List<Address> AddressList();

        //BlogComment
        [OperationContract]
        [WebInvoke(UriTemplate = "BlogComment/BlogCommentAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BlogCommentAdd(BlogComment model);
        [OperationContract]
        [WebInvoke(UriTemplate = "BlogComment/BlogCommentDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BlogCommentDelete(BlogComment model);
        [OperationContract]
        [WebGet(UriTemplate = "BlogComment/BlogCommentList?id={id}", ResponseFormat = WebMessageFormat.Json)]
        List<BlogComment> BlogCommentList(int id);
        /*Update olacak.*/

        //Blog
        [OperationContract]
        [WebInvoke(UriTemplate = "Blog/BlogAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BlogAdd(BlogVM model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Blog/BlogDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool BlogDelete(int id);
        [OperationContract]
        [WebGet(UriTemplate = "Blog/BlogList", ResponseFormat = WebMessageFormat.Json)]
        List<Blog> BlogList();
        [OperationContract]
        [WebGet(UriTemplate = "Blog/BlogVMList", ResponseFormat = WebMessageFormat.Json)]
        List<BlogVM> BlogVMList();

        [WebGet(UriTemplate = "Blog/BlogDetail?id={id}", ResponseFormat = WebMessageFormat.Json)]
        Blog BlogDetail(int id);
        /*Update olacak.*/

        //CargoPayment

        //CargoTransaction
        [OperationContract]
        [WebInvoke(UriTemplate = "CargoTransaction/CargoTransactionAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CargoTransactionAdd(CargoVM model);
        [OperationContract]
        [WebInvoke(UriTemplate = "CargoTransaction/CargoTransactionDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CargoTransactionDelete(CargoVM model);
        [OperationContract]
        [WebGet(UriTemplate = "CargoTransaction/CargoTransactionList", ResponseFormat = WebMessageFormat.Json)]
        List<CargoVM> CargoTransactionList();
        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateCargo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateCargo(Order model);
        /*Update olacak.*/

        //City     
        [OperationContract]
        [WebGet(UriTemplate = "City/CityList", ResponseFormat = WebMessageFormat.Json)]
        List<City> CityList();

        //Courier
        [OperationContract]
        [WebInvoke(UriTemplate = "Courier/CourierAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CourierAdd(CourierVM model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Courier/CourierDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CourierDelete(Courier model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Courier/CourierUpdate", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CourierUpdate(User model);
        [OperationContract]
        [WebGet(UriTemplate = "Courier/CourierList", ResponseFormat = WebMessageFormat.Json)]
        List<Courier> CourierList();

        //CourierStatistic
        [OperationContract]
        [WebInvoke(UriTemplate = "CourierStatistic/CourierStatisticAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CourierStatisticAdd(CourierStatistic model);
        [OperationContract]
        [WebInvoke(UriTemplate = "CourierStatistic/CourierStatisticDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CourierStatisticDelete(CourierStatistic model);
        [OperationContract]
        [WebGet(UriTemplate = "CourierStatistic/CourierStatisticList", ResponseFormat = WebMessageFormat.Json)]
        List<CourierStatistic> CourierStatisticList();
        /*Update olacak.*/

        //Customer
        [OperationContract]
        [WebInvoke(UriTemplate = "Customer/CustomerAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CustomerAdd(CustomerVM model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Customer/CustomerDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CustomerDelete(Customer model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Customer/CustomerEdit", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CustomerEdit(User model);
        [OperationContract]
        [WebGet(UriTemplate = "Customer/CustomerList", ResponseFormat = WebMessageFormat.Json)]
        List<Customer> CustomerList();

        //District
        [OperationContract]
        [WebGet(UriTemplate = "District/DistrictList", ResponseFormat = WebMessageFormat.Json)]
        List<District> DistrictList();

        //MessageSystem
        [OperationContract]
        [WebInvoke(UriTemplate = "MessageSystem/MessageSystemAdd", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool MessageSystemAdd(MessageSystem model);
        [OperationContract]
        [WebInvoke(UriTemplate = "MessageSystem/MessageSystemDelete?id={id}", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		bool MessageSystemDelete(int id);
		[OperationContract]
        [WebGet(UriTemplate = "MessageSystem/MessageSystemList", ResponseFormat = WebMessageFormat.Json)]
        List<MessageSystem> MessageSystemList();
        [OperationContract]
        [WebGet(UriTemplate = "MessageSystem/MessageSystemDetail?id={id}", ResponseFormat = WebMessageFormat.Json)]
        MessageSystem MessageSystemDetail(int id);
        /*Upadate olacak*/

        //Order
        [OperationContract]
        [WebInvoke(UriTemplate = "Order/OrderAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool OrderAdd(Order model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Order/OrderDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool OrderDelete(Order model);
        [OperationContract]
        [WebGet(UriTemplate = "Order/OrderList?userId={userId}&roleId={roleId}", ResponseFormat = WebMessageFormat.Json)]
        List<Order> OrderList(int userId, int roleId);
		[OperationContract]
		[WebInvoke(UriTemplate = "Order/OrderUpdate", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		bool OrderUpdate(Order model);

		// Send Cargo
		[OperationContract]
		[WebInvoke(UriTemplate = "SendCargo", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		bool SendCargo(CargoVM model);

        [OperationContract]
        [WebGet(UriTemplate = "Order/ListCargoByCourier?id={id}", ResponseFormat = WebMessageFormat.Json)]
        List<CargoOrderVM> ListCargoByCourier(int id);

        [OperationContract]
        [WebGet(UriTemplate = "Order/ListCargoByCustomer?id={id}", ResponseFormat = WebMessageFormat.Json)]
        List<CargoOrderVM> ListCargoByCustomer(int id);

        [OperationContract]
        [WebGet(UriTemplate = "Order/ListCargo", ResponseFormat = WebMessageFormat.Json)]
        List<CargoOrderVM> ListCargo();

        //Picture
        [OperationContract]
        [WebInvoke(UriTemplate = "Picture/PictureAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool PictureAdd(Picture model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Picture/PictureDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool PictureDelete(Picture model);
        [OperationContract]
        [WebGet(UriTemplate = "Picture/PictureList", ResponseFormat = WebMessageFormat.Json)]
        List<Picture> PictureList();
        /*Update*/

        //ProductInfo
        [OperationContract]
        [WebInvoke(UriTemplate = "Product/ProductInfoAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool ProductInfoAdd(ProductInfo model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Product/ProductInfoDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool ProductInfoDelete(ProductInfo model);
        [OperationContract]
        [WebGet(UriTemplate = "Product/ProductInfoList", ResponseFormat = WebMessageFormat.Json)]
        List<ProductInfo> ProductInfoList();
        /*Update*/

        //RegisterFolder
        [OperationContract]
        [WebInvoke(UriTemplate = "RegisterFolder/RegisterFolderAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool RegisterFolderAdd(RegisterFolder model);
        [OperationContract]
        [WebInvoke(UriTemplate = "RegisterFolder/RegisterFolderDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool RegisterFolderDelete(RegisterFolder model);
        [OperationContract]
        [WebGet(UriTemplate = "RegisterFolder/RegisterFolderList", ResponseFormat = WebMessageFormat.Json)]
        List<RegisterFolder> RegisterFolderList();
        /*Update*/

        //Role
        [OperationContract]
        [WebGet(UriTemplate = "Role/RoleList", ResponseFormat = WebMessageFormat.Json)]
        List<Role> RoleList();

        //ShippingType
        [OperationContract]
        [WebGet(UriTemplate = "ShippingTypeList", ResponseFormat = WebMessageFormat.Json)]
        List<ShippingType> ShippingTypeList();

        //UserBank
        [OperationContract]
        [WebGet(UriTemplate = "UserBankList", ResponseFormat = WebMessageFormat.Json)]
        List<UserBank> UserBankList();

        //UserCardInfo
        [OperationContract]
        [WebInvoke(UriTemplate = "UserCardInfo/UserCardInfoAdd", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UserCardInfoAdd(UserCardInfo model);
        [OperationContract]
        [WebInvoke(UriTemplate = "UserCardInfo/UserCardInfoDelete", Method = "POST",RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UserCardInfoDelete(UserCardInfo model);
        [OperationContract]
        [WebGet(UriTemplate = "UserCardInfo/UserCardInfoList", ResponseFormat = WebMessageFormat.Json)]
        List<UserCardInfo> UserCardInfoList();
        /*Update*/

        /*Log*/
        [OperationContract]
        [WebInvoke(UriTemplate = "Log/LogAdd", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool LogAdd(Log model);
        [OperationContract]
        [WebInvoke(UriTemplate = "Log/LogList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Log> LogList(string email);

	}
}
