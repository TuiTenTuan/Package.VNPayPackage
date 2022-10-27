// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using VNPayPackage.Models;
using VNPayPackage.Ulits;

Console.WriteLine("Hello, World!");
string tmmCode = "20OSMDB3";
string hashKey = "ZSWAMVCUPIENXWPVVVDTMMRUOCIOKUNG";
string baseURL = @"https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
string baseURLApi = @"https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
IPAddress iP = new IPAddress(new byte[] { 123, 21, 236, 206 });

//VnpPay p = new VnpPay(VNPCommand.Pay, tmmCode, 10000, iP, "Test Thanh Toan 10k", "http://localhost:5123/return", DateTime.Now.Ticks.ToString());

//string url = p.CreatePayUrl(baseURL, hashKey);

////Process.Start("chrome.exe", $@"{url}");



VnpCheckTransaction checkTransaction = new VnpCheckTransaction(tmmCode, "638022869373137314", "Test Thanh Toan 10k", 13862543, DateTime.ParseExact("20221025092857", "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture), iP);

checkTransaction.SecureHash = checkTransaction.CheckSum(hashKey);

string jsonConvert = JsonConvert.SerializeObject(checkTransaction.ConvertToObjectString());

StringContent content = new StringContent(jsonConvert);

HttpClient client = new HttpClient();

var result = client.PostAsync(baseURLApi, content).Result;

string contentResult = result.Content.ReadAsStringAsync().Result;

contentResult = contentResult.Replace("{", "").Replace("}", "").Replace("\"", "");

var tempResultSplit = contentResult.Split(",");

string checkHash = "486ba7e84d9246de82248122e95cdc6c|querydr|00|QueryDR Success|20OSMDB3|638022869373137314|1000000|NCB|20221025093012|13862543|01|00|Test Thanh Toan 10k||";

string hashcheck = Functions.HmacSHA512(hashKey, checkHash);


Console.WriteLine("");
