//using Newtonsoft.Json;
//using PagosPublico.Models.ModelView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
//using System.Web.Script.Serialization;

namespace PagosVisaWeb.Models
{
    public class VisaInterfaz
    {
        static String error = "";
    }

    class ClsTokenSession
    {
        public String sessionKey { get; set; }
        public long expirationTime { get; set; }
    }

    public class Header
    {
        public string ecoreTransactionUUID { get; set; }
        public long ecoreTransactionDate { get; set; }
        public int millis { get; set; }
    }

    public class Fulfillment
    {
        public string channel { get; set; }
        public string merchantId { get; set; }
        public string terminalId { get; set; }
        public string captureType { get; set; }
        public bool countable { get; set; }
        public bool fastPayment { get; set; }
        public string signature { get; set; }
    }

    public class Order
    {
        public string tokenId { get; set; }
        public string purchaseNumber { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string externalTransactionId { get; set; }
        public decimal authorizedAmount { get; set; }
        public string authorizationCode { get; set; }
        public string actionCode { get; set; }
        public string traceNumber { get; set; }
        public string transactionDate { get; set; }
        public string transactionId { get; set; }
    }

    public class Token
    {
        public string tokenId { get; set; }
        public string ownerId { get; set; }
        public string expireOn { get; set; }
    }

    public class DataMap
    {
        public string CURRENCY { get; set; }
        public string TERMINAL { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string ACTION_CODE { get; set; }
        public string TRACE_NUMBER { get; set; }
        public string CARD_TOKEN { get; set; }
        public string ECI_DESCRIPTION { get; set; }
        public string ECI { get; set; }
        public string ID_RESOLUTOR { get; set; }
        public string SIGNATURE { get; set; }
        public string CARD { get; set; }
        public string MERCHANT { get; set; }
        public string BRAND { get; set; }
        public string STATUS { get; set; }
        public string ACTION_DESCRIPTION { get; set; }
        public string ADQUIRENTE { get; set; }
        public string ID_UNICO { get; set; }
        public string AMOUNT { get; set; }
        public string PROCESS_CODE { get; set; }
        public string VAULT_BLOCK { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string AUTHORIZATION_CODE { get; set; }
    }

    public class RootObject
    {
        public Header header { get; set; }
        public Fulfillment fulfillment { get; set; }
        public Order order { get; set; }
        public Token token { get; set; }
        public DataMap dataMap { get; set; }
    }

    //------------------------------------------
    public class HeaderError
    {
        public string ecoreTransactionUUID { get; set; }
        public long ecoreTransactionDate { get; set; }
        public int millis { get; set; }
    }

    public class DataError
    {
        public string CURRENCY { get; set; }
        public string TRANSACTION_DATE { get; set; }
        public string ACTION_CODE { get; set; }
        public string STATUS { get; set; }
        public string ACTION_DESCRIPTION { get; set; }
        public string TRACE_NUMBER { get; set; }
        public string AMOUNT { get; set; }
        public string ECI { get; set; }
        public string SIGNATURE { get; set; }
        public string CARD { get; set; }
        public string BRAND { get; set; }
        public string MERCHANT { get; set; }
    }

    public class RootObjectError
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public HeaderError header { get; set; }
        public DataError data { get; set; }
    }

}