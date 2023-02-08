using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Azzuha.Common
{
    //public class QuickBooksHelper
    //{
    //    static string redirectURI = ConfigurationManager.AppSettings["redirectURI"];
    //    static string clientID = ConfigurationManager.AppSettings["clientID"];
    //    static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
    //    static string env = ConfigurationManager.AppSettings["appEnvironment"];
    //    //static OAuth2Client client = new OAuth2Client(clientID, clientSecret, redirectURI, env);
    //    static string logPath = ConfigurationManager.AppSettings["logPath"];
    //    static string realmId = "4620816365164188290";
    //    static string authCode;

    //    //Generate authorize url to get the OAuth2 code
    //    public static string GetAcessTokenQbook()
    //    {
    //        string accessToken = "";
    //        string getAccessToken = @"Select QbookAccessToken from [dbo].[AdminSystemConfigurations]";
    //        using (IDbConnection db = GetConnection.Connection())
    //        {
    //            db.Open();
    //            string token = db.Query<string>(getAccessToken).FirstOrDefault();
    //            if (!String.IsNullOrEmpty(token))
    //            {
    //                accessToken = token;
    //            }
    //            else
    //            {
    //                accessToken = "";
    //            }
    //        }
    //        return accessToken;
    //    }
    //    public static bool UpdateAcessTokenQbook(string accessToken)
    //    {
    //        bool done = false;
    //        string getAccessToken = @"Update AdminSystemConfigurations Set QbookAccessToken = @token";
    //        using (IDbConnection db = GetConnection.Connection())
    //        {
    //            db.Open();
    //            int token = db.Execute(getAccessToken, new { token = accessToken });
    //            if (token > 0)
    //            {
    //                done = true;
    //            }
    //            else
    //            {
    //                done = false;
    //            }
    //        }
    //        return done;
    //    }
    //    public static string GetAccessToken()
    //    {

    //        var oauth2Client = new OAuth2Client(clientID,
    //                clientSecret,
    //                // Redirect not used but matches entry for app
    //                //fullUri.ToString(),
    //                "http://localhost:1397",
    //                "sandbox"); // environment is “sandbox” or “production”

    //        var previousRefreshToken = GetAcessTokenQbook();
    //        var tokenResp = oauth2Client.RefreshTokenAsync(previousRefreshToken.ToString());
    //        tokenResp.Wait();
    //        var data = tokenResp.Result;

    //        if (!String.IsNullOrEmpty(data.Error) || String.IsNullOrEmpty(data.RefreshToken) ||
    //              String.IsNullOrEmpty(data.AccessToken))
    //        {
    //            throw new Exception("Refresh token failed - " + data.Error);
    //        }

    //        // If we've got a new refresh_token store it in the file
    //        if (previousRefreshToken != data.RefreshToken)
    //        {
    //            Console.WriteLine("Writing new refresh token : " + data.RefreshToken);
    //            UpdateAcessTokenQbook(data.RefreshToken);

    //        }
    //        return data.AccessToken;
    //    }
    //    static public ServiceContext GetServiceContext()
    //    {
    //        var accessToken = GetAccessToken(); // Code from above
    //        var oauthValidator = new OAuth2RequestValidator(accessToken);
    //        ServiceContext qboContext = new ServiceContext(realmId,
    //                IntuitServicesType.QBO, oauthValidator);
    //        return qboContext;
    //    }
    //    internal static Customer CreateCustomer(ServiceContext context, UserCustomer userCustomer)
    //    {

    //        //String guid = Guid.NewGuid().ToString("N");
    //        Customer customer = new Customer();
    //        //customer.Taxable = false;
    //        //customer.TaxableSpecified = true;
    //        PhysicalAddress billAddr = new PhysicalAddress();
    //        billAddr.Line1 = userCustomer.State;
    //        billAddr.City = userCustomer.City;
    //        billAddr.Country = userCustomer.Country;
    //        //billAddr.CountrySubDivisionCode = "92";
    //        //billAddr.PostalCode = "54000";
    //        customer.BillAddr = billAddr;
    //        // customer.Id = userCustomer.CustomerId;
    //        PhysicalAddress shipAddr = new PhysicalAddress();
    //        //shipAddr.Line1 = "DHA";
    //        //shipAddr.Line2 = "Line2";
    //        //shipAddr.Line3 = "Line3";
    //        //shipAddr.Line4 = "Line4";
    //        //shipAddr.Line5 = "Line5";
    //        //shipAddr.City = "City";
    //        //shipAddr.Country = "Country";

    //        //shipAddr.CountrySubDivisionCode = "92";
    //        //shipAddr.PostalCode = "54000";

    //        //customer.ShipAddr = shipAddr;

    //        //List<PhysicalAddress> otherAddrList = new List<PhysicalAddress>();
    //        //PhysicalAddress otherAddr = new PhysicalAddress();
    //        //otherAddr.Line1 = "Line1";
    //        //otherAddr.Line2 = "Line2";
    //        //otherAddr.Line3 = "Line3";
    //        //otherAddr.Line4 = "Line4";
    //        //otherAddr.Line5 = "Line5";
    //        //otherAddr.City = "City";
    //        //otherAddr.Country = "Country";

    //        //otherAddr.CountrySubDivisionCode = "CountrySubDivisionCode";
    //        //otherAddr.PostalCode = "PostalCode";

    //        //otherAddrList.Add(otherAddr);
    //        //customer.OtherAddr = otherAddrList.ToArray();

    //        //customer.Notes = "Notes";
    //        //customer.Job = false;
    //        //customer.JobSpecified = true;
    //        //customer.BillWithParent = false;
    //        //customer.BillWithParentSpecified = true;

    //        customer.Balance = new Decimal(0);
    //        customer.BalanceSpecified = true;


    //        //customer.BalanceWithJobs = new Decimal(100.00);
    //        //customer.BalanceWithJobsSpecified = true;

    //        //customer.PreferredDeliveryMethod = "Print";
    //        //customer.ResaleNum = "ResaleNum";

    //        //customer.Title = "Mr";
    //        customer.GivenName = userCustomer.FirstName;
    //        customer.MiddleName = userCustomer.LastName;
    //        //customer.FamilyName = "Messagemuse";
    //        //customer.Suffix = "Suffix";
    //        customer.FullyQualifiedName = userCustomer.FirstName + " " + userCustomer.LastName + "-" + userCustomer.CustomerId;//"Name_";//+ //customerId;
    //        //customer.CompanyName = "Messagemuse";
    //        customer.DisplayName = userCustomer.FirstName + " " + userCustomer.LastName + "-" + userCustomer.CustomerId;//"Name_";//+ //customerId;
    //        //customer.PrintOnCheckName = "nachee";

    //        customer.Active = true;
    //        customer.ActiveSpecified = true;
    //        TelephoneNumber primaryPhone = new TelephoneNumber();

    //        primaryPhone.FreeFormNumber = userCustomer.PhoneNumber;

    //        customer.PrimaryPhone = primaryPhone;
    //        //TelephoneNumber alternatePhone = new TelephoneNumber();

    //        //alternatePhone.FreeFormNumber = "FreeFormNumber";

    //        //customer.AlternatePhone = alternatePhone;
    //        //TelephoneNumber mobile = new TelephoneNumber();

    //        //mobile.FreeFormNumber = "FreeFormNumber";

    //        //customer.Mobile = mobile;
    //        //TelephoneNumber fax = new TelephoneNumber();

    //        //fax.FreeFormNumber = "FreeFormNumber";

    //        //customer.Fax = fax;
    //        EmailAddress primaryEmailAddr = new EmailAddress();
    //        primaryEmailAddr.Address = userCustomer.EmailAddr;

    //        customer.PrimaryEmailAddr = primaryEmailAddr;
    //        //string cusId = "847";
    //        //customer.Id = cusId;

    //        //WebSiteAddress webAddr = new WebSiteAddress();
    //        //webAddr.URI = "http://uri.com";

    //        //customer.WebAddr = webAddr;

    //        DataService service = new DataService(context);
    //        var customeReturn = service.Add(customer);
    //        // string customerid = inv.Id;
    //        return customeReturn;
    //    }

    //    internal static Invoice CreateInvoice(ServiceContext context, CreateInvoiveRequestDto data)
    //    {
    //        //US Invoice
    //        //Customer customer = Helper.FindOrAdd<Customer>(context, new Customer());
    //        //TaxCode taxCode = Helper.FindOrAdd<TaxCode>(context, new TaxCode());
    //        //Account account = Helper.FindOrAddAccount(context, AccountTypeEnum.AccountsReceivable, AccountClassificationEnum.Liability);

    //        Invoice invoice = new Invoice();
    //        //invoice.Deposit = new Decimal(50.00);
    //        //invoice.DepositSpecified = true;
    //        // ites ids
    //        //genral tickets 19
    //        //PPV 22
    //        //Genral Donation 23

    //        invoice.CustomerRef = new ReferenceType()
    //        {
    //            Value = SystemGlobal.GetQBookCustomerId(data.userId)
    //        };

    //        //invoice.DueDate = DateTime.UtcNow.Date;
    //        invoice.DueDateSpecified = false;


    //        //invoice.TotalAmt = new Decimal(100.00);
    //        //invoice.TotalAmtSpecified = true;

    //        //invoice.ApplyTaxAfterDiscount = false;
    //        //invoice.ApplyTaxAfterDiscountSpecified = true;

    //        //invoice.PrintStatus = PrintStatusEnum.NotSet;
    //        //invoice.PrintStatusSpecified = true;
    //        //invoice.EmailStatus = EmailStatusEnum.NotSet;
    //        //invoice.EmailStatusSpecified = true;

    //        EmailAddress billEmail = new EmailAddress();
    //        billEmail.Address = data.email;
    //        billEmail.Default = true;
    //        billEmail.DefaultSpecified = true;
    //        billEmail.Tag = "Tag";
    //        invoice.BillEmail = billEmail;


    //        //EmailAddress billEmailcc = new EmailAddress();
    //        //billEmailcc.Address = @"nasir.habib3700@gmail.com";
    //        //billEmailcc.Default = true;
    //        //billEmailcc.DefaultSpecified = true;
    //        //billEmailcc.Tag = "Tag";
    //        //invoice.BillEmailCc = billEmailcc;

    //        //EmailAddress billEmailbcc = new EmailAddress();
    //        //billEmailbcc.Address = @"nasir@messagemuse.com";
    //        //billEmailbcc.Default = true;
    //        //billEmailbcc.DefaultSpecified = true;
    //        //billEmailbcc.Tag = "Tag";
    //        //invoice.BillEmailBcc = billEmailbcc;


    //        //invoice.Balance = new Decimal(100.00);
    //        //invoice.BalanceSpecified = true;

    //        //invoice.TxnDate = DateTime.UtcNow.Date;
    //        //invoice.TxnDateSpecified = true;
    //        List<Line> lineList = new List<Line>();
    //        foreach (var item in data.saleItems)
    //        {

    //            Line line = new Line();
    //            line.Description = item.Description + " for event" + data.eventName;// eventName
    //            line.Amount = item.Amount;
    //            line.AmountSpecified = true;
    //            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
    //            line.DetailTypeSpecified = true;
    //            SalesItemLineDetail saleItem = new SalesItemLineDetail();
    //            // saleItem.ItemRef = new ReferenceType()
    //            // {
    //            //     //name = "Services",
    //            //     Value = "19",//item.Id
    //            // };
    //            saleItem.Qty = item.Qty;
    //            //// saleItem.ItemElementName = ItemChoiceType.UnitPrice;// unitPrice
    //            saleItem.QtySpecified = true;
    //            line.DetailTypeSpecified = true;
    //            line.AnyIntuitObject = saleItem;
    //            lineList.Add(line);
    //        }
    //        invoice.Line = lineList.ToArray();
    //        invoice.TrackingNum = data.eventId.ToString();// eventId

    //        //invoice.TxnTaxDetail = new TxnTaxDetail()
    //        //{
    //        //    TotalTax = Convert.ToDecimal(10),
    //        //    TotalTaxSpecified = true
    //        //};


    //        DataService service = new DataService(context);
    //        var inv = service.Add(invoice);

    //        return invoice;
    //    }
    //}
}
