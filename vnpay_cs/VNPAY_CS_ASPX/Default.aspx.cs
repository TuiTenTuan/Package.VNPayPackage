﻿using System;
using System.Configuration;
using log4net;
using VNPAY_CS_ASPX.Models;

namespace VNPAY_CS_ASPX
{
    public partial class _Default : System.Web.UI.Page
    {
        private static readonly ILog log =
          LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtOrderDesc.Text = "Nhap noi dung thanh toan";
                txtExpire.Text = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss");

            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                lblMessage.Text = "Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config";
                return;
            }
            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = 100000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
            order.OrderDesc = txtOrderDesc.Text;
            order.CreatedDate = DateTime.Now;
            string locale = cboLanguage.SelectedItem.Value;
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (cboBankCode.SelectedItem != null && !string.IsNullOrEmpty(cboBankCode.SelectedItem.Value))
            {
                vnpay.AddRequestData("vnp_BankCode", cboBankCode.SelectedItem.Value);
            }
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            if (!string.IsNullOrEmpty(locale))
            {
                vnpay.AddRequestData("vnp_Locale", locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", orderCategory.SelectedItem.Value); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            vnpay.AddRequestData("vnp_ExpireDate", txtExpire.Text);
            //Billing
            vnpay.AddRequestData("vnp_Bill_Mobile", txt_billing_mobile.Text.Trim());
            vnpay.AddRequestData("vnp_Bill_Email", txt_billing_email.Text.Trim());
            var fullName = txt_billing_fullname.Text.Trim();
            if (!String.IsNullOrEmpty(fullName))
            {
                var indexof = fullName.IndexOf(' ');
                vnpay.AddRequestData("vnp_Bill_FirstName", fullName.Substring(0, indexof));
                vnpay.AddRequestData("vnp_Bill_LastName", fullName.Substring(indexof + 1, fullName.Length - indexof - 1));
            }
            vnpay.AddRequestData("vnp_Bill_Address", txt_inv_addr1.Text.Trim());
            vnpay.AddRequestData("vnp_Bill_City", txt_bill_city.Text.Trim());
            vnpay.AddRequestData("vnp_Bill_Country", txt_bill_country.Text.Trim());
            vnpay.AddRequestData("vnp_Bill_State", "");

            // Invoice

            vnpay.AddRequestData("vnp_Inv_Phone", txt_inv_mobile.Text.Trim());
            vnpay.AddRequestData("vnp_Inv_Email", txt_inv_email.Text.Trim());
            vnpay.AddRequestData("vnp_Inv_Customer", txt_inv_customer.Text.Trim());
            vnpay.AddRequestData("vnp_Inv_Address", txt_inv_addr1.Text.Trim());
            vnpay.AddRequestData("vnp_Inv_Company", txt_inv_company.Text);
            vnpay.AddRequestData("vnp_Inv_Taxcode", txt_inv_taxcode.Text);
            vnpay.AddRequestData("vnp_Inv_Type", cbo_inv_type.SelectedItem.Value);

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            //Response.Redirect(paymentUrl);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + paymentUrl + "','_newtab');", true);

            string value = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=10000000&=22+L%C3%A1ng+H%E1%BA%A1%2C+Ph%C6%B0%E1%BB%9Dng+L%C3%A1ng+H%E1%BA%A1%2C+Qu%E1%BA%ADn+%C4%90%E1%BB%91ng+%C4%90a%2C+TP+H%C3%A0+N%E1%BB%99i&vnp_Bill_City=H%C3%A0+N%E1%BB%99i&vnp_Bill_Country=VN&vnp_Bill_Email=vnpaytest%40vnpay.vn&vnp_Bill_FirstName=Nguyen&vnp_Bill_LastName=Van+A&vnp_Bill_Mobile=0123456789&vnp_Command=pay&vnp_CreateDate=20221024004920&vnp_CurrCode=VND&vnp_ExpireDate=20221024010358&vnp_Inv_Address=22+L%C3%A1ng+H%E1%BA%A1%2C+Ph%C6%B0%E1%BB%9Dng+L%C3%A1ng+H%E1%BA%A1%2C+Qu%E1%BA%ADn+%C4%90%E1%BB%91ng+%C4%90a%2C+TP+H%C3%A0+N%E1%BB%99i&vnp_Inv_Company=C%C3%B4ng+ty+C%E1%BB%95+ph%E1%BA%A7n+gi%E1%BA%A3i+ph%C3%A1p+Thanh+to%C3%A1n+Vi%E1%BB%87t+Nam&vnp_Inv_Customer=Nguyen+Van+A&vnp_Inv_Email=vnpaytest%40vnpay.vn&vnp_Inv_Phone=02437764668&vnp_Inv_Taxcode=0102182292&vnp_Inv_Type=O&vnp_IpAddr=%3A%3A1&vnp_Locale=vn&vnp_OrderInfo=Thanh+toan+don+hang%3A638021693603310261&vnp_OrderType=topup&vnp_ReturnUrl=http%3A%2F%2Flocalhost%3A16262%2Fvnpay_return.aspx&vnp_TmnCode=20OSMDB3&vnp_TxnRef=638021693603310261&vnp_Version=2.1.0";

            lblMessage.Text = Utils.HmacSHA512(vnp_HashSecret, value) + "\n" + paymentUrl;
        }


    }
}