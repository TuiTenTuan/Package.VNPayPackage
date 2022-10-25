// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Http;
using System.Net;

Console.WriteLine("Hello, World!");
string tmmCode = "20OSMDB3";
string hashKey = "ZSWAMVCUPIENXWPVVVDTMMRUOCIOKUNG";
string baseURL = @"https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
string baseURLApi = @"https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
IPAddress iP = new IPAddress(new byte[] { 117, 13, 15, 128 });

//VnpPay p = new VnpPay(VNPCommand.Pay, tmmCode, 10000, iP, "Test Thanh Toan 10k", "http://localhost:5123/return", DateTime.Now.Ticks.ToString());

//string url = p.CreatePayUrl(baseURL, hashKey);

////Process.Start("chrome.exe", $@"{url}");

//DateTime date = DateTime.Now;

//string dataCheck = $@"{date.Ticks}|2.1.0|querydr|{tmmCode}|638022869373137314|20221025092857|{date.ToString("yyyyMMddHHmmss")}|123.21.236.206|Test Thanh Toan 10k";

//object querydr = new 
//{
//    vnp_RequestId = date.Ticks,
//    vnp_Version = "2.1.0",
//    vnp_Command = "querydr",
//    vnp_TmnCode = tmmCode,
//    vnp_TxnRef = 638022869373137314,
//    vnp_OrderInfo = "Test Thanh Toan 10k",
//    vnp_TransactionDate = 20221025092857,
//    vnp_CreateDate = date.ToString("yyyyMMddHHmmss"),
//    vnp_IpAddr = "123.21.236.206",
//    vnp_SecureHash = Functions.HmacSHA512(hashKey, dataCheck)
//};

//string jsonConvert = JsonConvert.SerializeObject(querydr);

//HttpClient httpClient = new HttpClient();

//StringContent content = new StringContent(jsonConvert, Encoding.UTF8, "Application/json");

//var result = httpClient.PostAsync(baseURLApi, content).Result;

//string resultContent = result.Content.ReadAsStringAsync().Result;

string input = @"http://localhost:5123/return?vnp_Amount=1000000&vnp_BankCode=NCB&vnp_BankTranNo=VNP13862543&vnp_CardType=ATM&vnp_OrderInfo=Test+Thanh+Toan+10k&vnp_PayDate=20221025093012&vnp_ResponseCode=00&vnp_TmnCode=20OSMDB3&vnp_TransactionNo=13862543&vnp_TransactionStatus=00&vnp_TxnRef=638022869373137314&vnp_SecureHash=009c79fba24bf850ba81604420ceb8ed6a9ec50fe748ea559e1b4055d522b93c2fefde777afbf11a15e8061b78bca0d9edd340cd7377fce6ee74ba118ffdbb8e";

QueryString query = QueryString.FromUriComponent(new Uri(input));

Console.WriteLine("");
