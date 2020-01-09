using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB2
{
    public class HDCQueries
    {
        public static string DiscountCouponQuery = "select giftId, amount from hdc.giftCertificate where certificateType = 8 and usedFlag !=1 and amount < 11 limit 1;";
        public static string MerchandiseCreditQuery = "select giftId, amount from hdc.giftCertificate where certificateType = 10 and usedFlag !=1 and amount < 11 limit 1; ";
        public static string UserQuery = "select prefix, name, streetAddress, apt, city, state, zipCode, phone, altPhone, email, businessName, userName, password from global.customerTemp where name like";
        public static string GuestUserdefaultShipToQuery = "select customerId from global.customerTemp where defaultShipToFlag = (select customerId from global.customerTemp where email like '{0}')";
        public static string RegisteredUserdefaultShipToQuery = "select customerId from global.customerTemp where defaultShipToFlag = (select customerId from global.customerTemp where userName like '{0}')";
        public static string ShoppingCartQuery = "select skuNo, orderNumber, shipToId, shipMethod from hdc.shoppingCartTemp where orderNumber = {0}";
        public static string GuestUserCustomerIdQuery = "select customerId from global.customerTemp where email like '{0}'";
        public static string RegisteredUserCustomerIdQuery = "select customerId from global.customerTemp where userName like '{0}'";
        public static string OrderDetailQuery = "select customerId, orderPrice, shipCost, shipAddCost, transDate from hdc.orderDetail where orderId = {0} ";
        public static string OrderHeaderQuery = "select customerId, productDollars, shippingDollars, taxDollars, orderDate from hdc.orderHeader where orderId = {0}";
        public static string SoldOutItemQuery = "select baseId from hdc.inventory where stockStatus = 'Sold Out' limit 1";
        public static string EmailSignUpQuery = "select firstName, lastName from global.emailRequest where emailAddress like '{0}'";
        public static string GetEmailQuery = "select emailAddress from global.emailRequest where emailAddress like '{0}' limit 1";
    }
}
