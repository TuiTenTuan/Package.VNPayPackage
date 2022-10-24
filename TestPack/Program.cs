// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Net;
using VNPayPackage.Enums;
using VNPayPackage.Models;
using VNPayPackage.Ulits;

Console.WriteLine("Hello, World!");
string tmmCode = "20OSMDB3";
string hashKey = "ZSWAMVCUPIENXWPVVVDTMMRUOCIOKUNG";
string baseURL = @"https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";

IPAddress iP = new IPAddress(new byte[] { 117, 13, 15, 128 });

VnpPay p = new VnpPay(VNPCommand.Pay, tmmCode, 10000, iP, "Test Thanh Toan 10k", "http://localhost:5123/return", "123456");

string url = p.CreatePayUrl(baseURL, hashKey);

Process.Start("chrome.exe", $@"-incognito --app={url}");
