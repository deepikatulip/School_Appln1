using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Utils
{
    public class Utility
    {
        #region Elastic Email Functions

        /// <summary>
        /// Submit emails
        /// </summary>
        /// <param name="apikey">ApiKey that gives you access to our SMTP and HTTP API's.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="from">From email address</param>
        /// <param name="fromName">Display name for from email address</param>
        /// <param name="sender">Email address of the sender</param>
        /// <param name="senderName">Display name sender</param>
        /// <param name="msgFrom">Optional parameter. Sets FROM MIME header.</param>
        /// <param name="msgFromName">Optional parameter. Sets FROM name of MIME header.</param>
        /// <param name="replyTo">Email address to reply to</param>
        /// <param name="replyToName">Display name of the reply to address</param>
        /// <param name="to">List of email recipients (each email is treated separately, like a BCC). Separated by comma or semicolon. We suggest using the "msgTo" parameter if backward compatibility with API version 1 is not a must.</param>
        /// <param name="msgTo">Optional parameter. Will be ignored if the 'to' parameter is also provided. List of email recipients (visible to all other recipients of the message as TO MIME header). Separated by comma or semicolon.</param>
        /// <param name="msgCC">Optional parameter. Will be ignored if the 'to' parameter is also provided. List of email recipients (visible to all other recipients of the message as CC MIME header). Separated by comma or semicolon.</param>
        /// <param name="msgBcc">Optional parameter. Will be ignored if the 'to' parameter is also provided. List of email recipients (each email is treated seperately). Separated by comma or semicolon.</param>
        /// <param name="lists">The name of a contact list you would like to send to. Separate multiple contact lists by commas or semicolons.</param>
        /// <param name="segments">The name of a segment you would like to send to. Separate multiple segments by comma or semicolon. Input "0" for "All Contacts".</param>
        /// <param name="mergeSourceFilename">File name one of attachments which is a CSV list of Recipients.</param>
        /// <param name="channel">An ID field (max 191 chars) that can be used for reporting [will default to HTTP API or SMTP API]</param>
        /// <param name="bodyHtml">Html email body</param>
        /// <param name="bodyText">Text email body</param>
        /// <param name="charset">Text value of charset encoding for example: iso-8859-1, windows-1251, utf-8, us-ascii, windows-1250 and more…</param>
        /// <param name="charsetBodyHtml">Sets charset for body html MIME part (overrides default value from charset parameter)</param>
        /// <param name="charsetBodyText">Sets charset for body text MIME part (overrides default value from charset parameter)</param>
        /// <param name="encodingType">0 for None, 1 for Raw7Bit, 2 for Raw8Bit, 3 for QuotedPrintable, 4 for Base64 (Default), 5 for Uue  note that you can also provide the text version such as "Raw7Bit" for value 1.  NOTE: Base64 or QuotedPrintable is recommended if you are validating your domain(s) with DKIM.</param>
        /// <param name="template">The name of an email template you have created in your account.</param>
        /// <param name="attachmentFiles">Attachment files. These files should be provided with the POST multipart file upload, not directly in the request's URL. Should also include merge CSV file</param>
        /// <param name="headers">Optional Custom Headers. Request parameters prefixed by headers_ like headers_customheader1, headers_customheader2. Note: a space is required after the colon before the custom header value. headers_customheader1=customheader1: header-value1 headers_customheader2 = customheader2: header-value2</param>
        /// <param name="postBack">Optional header returned in notifications.</param>
        /// <param name="merge">Request parameters prefixed by merge_ like merge_firstname, merge_lastname. If sending to a template you can send merge_ fields to merge data with the template. Template fields are entered with {firstname}, {lastname} etc.</param>
        /// <param name="timeOffSetMinutes">Number of minutes in the future this email should be sent</param>
        /// <returns>ApiTypes.EmailSend</returns>
        public static ApiTypes.EmailSend Send(string apiKey, string subject = null, string from = null, string fromName = null, string sender = null, string senderName = null, string msgFrom = null, string msgFromName = null, string replyTo = null, string replyToName = null, IEnumerable<string> to = null, string[] msgTo = null, string[] msgCC = null, string[] msgBcc = null, IEnumerable<string> lists = null, IEnumerable<string> segments = null, string mergeSourceFilename = null, string channel = null, string bodyHtml = null, string bodyText = null, string charset = null, string charsetBodyHtml = null, string charsetBodyText = null, ApiTypes.EncodingType encodingType = ApiTypes.EncodingType.None, string template = null, IEnumerable<ApiTypes.FileData> attachmentFiles = null, Dictionary<string, string> headers = null, string postBack = null, Dictionary<string, string> merge = null, string timeOffSetMinutes = null)
        {
            var emailGw = ConfigurationManager.AppSettings["EmailGateway"];

            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", apiKey);
            if (subject != null) values.Add("subject", subject);
            if (from != null) values.Add("from", from);
            if (fromName != null) values.Add("fromName", fromName);
            if (sender != null) values.Add("sender", sender);
            if (senderName != null) values.Add("senderName", senderName);
            if (msgFrom != null) values.Add("msgFrom", msgFrom);
            if (msgFromName != null) values.Add("msgFromName", msgFromName);
            if (replyTo != null) values.Add("replyTo", replyTo);
            if (replyToName != null) values.Add("replyToName", replyToName);
            if (to != null) values.Add("to", string.Join(",", to));
            if (msgTo != null)
            {
                foreach (string _item in msgTo)
                {
                    values.Add("msgTo", _item.ToString());
                }
            }
            if (msgCC != null)
            {
                foreach (string _item in msgCC)
                {
                    values.Add("msgCC", _item.ToString());
                }
            }
            if (msgBcc != null)
            {
                foreach (string _item in msgBcc)
                {
                    values.Add("msgBcc", _item.ToString());
                }
            }
            if (lists != null) values.Add("lists", string.Join(",", lists));
            if (segments != null) values.Add("segments", string.Join(",", segments));
            if (mergeSourceFilename != null) values.Add("mergeSourceFilename", mergeSourceFilename);
            if (channel != null) values.Add("channel", channel);
            if (bodyHtml != null) values.Add("bodyHtml", bodyHtml);
            if (bodyText != null) values.Add("bodyText", bodyText);
            if (charset != null) values.Add("charset", charset);
            if (charsetBodyHtml != null) values.Add("charsetBodyHtml", charsetBodyHtml);
            if (charsetBodyText != null) values.Add("charsetBodyText", charsetBodyText);
            if (encodingType != ApiTypes.EncodingType.None) values.Add("encodingType", encodingType.ToString());
            if (template != null) values.Add("template", template);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> _item in headers)
                {
                    values.Add("headers_" + _item.Key, _item.Value);
                }
            }
            if (postBack != null) values.Add("postBack", postBack);
            if (merge != null)
            {
                foreach (KeyValuePair<string, string> _item in merge)
                {
                    values.Add("merge_" + _item.Key, _item.Value);
                }
            }
            if (timeOffSetMinutes != null) values.Add("timeOffSetMinutes", timeOffSetMinutes);
            byte[] apiResponse = ApiUtilities.HttpPostFile(emailGw + "/email/send", attachmentFiles == null ? null : attachmentFiles.ToList(), values);
            ApiResponse<ApiTypes.EmailSend> apiRet = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<ApiTypes.EmailSend>>(Encoding.UTF8.GetString(apiResponse));
            if (!apiRet.success) throw new ApplicationException(apiRet.error);
            return apiRet.Data;
        }

        public static class ApiUtilities
        {
            public static byte[] HttpPostFile(string url, List<ApiTypes.FileData> fileData, NameValueCollection parameters)
            {
                try
                {
                    string boundary = DateTime.Now.Ticks.ToString("x");
                    byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                    wr.ContentType = "multipart/form-data; boundary=" + boundary;
                    wr.Method = "POST";
                    wr.KeepAlive = true;
                    wr.Credentials = CredentialCache.DefaultCredentials;
                    wr.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                    wr.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    Stream rs = wr.GetRequestStream();

                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    foreach (string key in parameters.Keys)
                    {
                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, parameters[key]);
                        byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                        rs.Write(formitembytes, 0, formitembytes.Length);
                    }

                    if (fileData != null)
                    {
                        foreach (var file in fileData)
                        {
                            rs.Write(boundarybytes, 0, boundarybytes.Length);
                            string headerTemplate = "Content-Disposition: form-data; name=\"filefoobarname\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n";
                            string header = string.Format(headerTemplate, file.FileName, file.ContentType);
                            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
                            rs.Write(headerbytes, 0, headerbytes.Length);
                            rs.Write(file.Content, 0, file.Content.Length);
                        }
                    }
                    byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    rs.Write(trailer, 0, trailer.Length);
                    rs.Close();

                    using (WebResponse wresp = wr.GetResponse())
                    {
                        MemoryStream response = new MemoryStream();
                        wresp.GetResponseStream().CopyTo(response);
                        return response.ToArray();
                    }
                }
                catch (WebException webError)
                {
                    // Throw exception with actual error message from response
                    throw new WebException(((HttpWebResponse)webError.Response).StatusDescription, webError, webError.Status, webError.Response);
                }
            }

            public static byte[] HttpPutFile(string url, ApiTypes.FileData fileData, NameValueCollection parameters)
            {
                try
                {
                    string queryString = BuildQueryString(parameters);

                    if (queryString.Length > 0) url += "?" + queryString.ToString();

                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                    wr.ContentType = fileData.ContentType ?? "application/octet-stream";
                    wr.Method = "PUT";
                    wr.KeepAlive = true;
                    wr.Credentials = CredentialCache.DefaultCredentials;
                    wr.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                    wr.Headers.Add("Content-Disposition: attachment; filename=\"" + fileData.FileName + "\"; size=" + fileData.Content.Length);
                    wr.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    Stream rs = wr.GetRequestStream();
                    rs.Write(fileData.Content, 0, fileData.Content.Length);

                    using (WebResponse wresp = wr.GetResponse())
                    {
                        MemoryStream response = new MemoryStream();
                        wresp.GetResponseStream().CopyTo(response);
                        return response.ToArray();
                    }
                }
                catch (WebException webError)
                {
                    // Throw exception with actual error message from response
                    throw new WebException(((HttpWebResponse)webError.Response).StatusDescription, webError, webError.Status, webError.Response);
                }
            }

            public static ApiTypes.FileData HttpGetFile(string url, NameValueCollection parameters)
            {
                try
                {
                    string queryString = BuildQueryString(parameters);

                    if (queryString.Length > 0) url += "?" + queryString.ToString();

                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                    wr.Method = "GET";
                    wr.KeepAlive = true;
                    wr.Credentials = CredentialCache.DefaultCredentials;
                    wr.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                    wr.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    using (WebResponse wresp = wr.GetResponse())
                    {
                        MemoryStream response = new MemoryStream();
                        wresp.GetResponseStream().CopyTo(response);
                        if (response.Length == 0) throw new FileNotFoundException();
                        string cds = wresp.Headers["Content-Disposition"];
                        if (cds == null)
                        {
                            // This is a special case for critical exceptions
                            ApiResponse<string> apiRet = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<string>>(Encoding.UTF8.GetString(response.ToArray()));
                            if (!apiRet.success) throw new ApplicationException(apiRet.error);
                            return null;
                        }
                        else
                        {
                            ContentDisposition cd = new ContentDisposition(cds);
                            ApiTypes.FileData fileData = new ApiTypes.FileData();
                            fileData.Content = response.ToArray();
                            fileData.ContentType = wresp.ContentType;
                            fileData.FileName = cd.FileName;
                            return fileData;
                        }
                    }
                }
                catch (WebException webError)
                {
                    // Throw exception with actual error message from response
                    throw new WebException(((HttpWebResponse)webError.Response).StatusDescription, webError, webError.Status, webError.Response);
                }
            }

            static string BuildQueryString(NameValueCollection parameters)
            {
                if (parameters == null || parameters.Count == 0)
                    return null;

                StringBuilder query = new StringBuilder();
                string amp = string.Empty;
                foreach (string key in parameters.AllKeys)
                {
                    foreach (string value in parameters.GetValues(key))
                    {
                        query.Append(amp);
                        query.Append(WebUtility.UrlEncode(key));
                        query.Append("=");
                        query.Append(WebUtility.UrlEncode(value));
                        amp = "&";
                    }
                }

                return query.ToString();
            }

        }


        public static class ApiTypes
        {
            /// <summary>
            /// File response from the server
            /// </summary>
            public class FileData
            {
                /// <summary>
                /// File content
                /// </summary>
                public byte[] Content { get; set; }

                /// <summary>
                /// MIME content type, optional for uploads
                /// </summary>
                public string ContentType { get; set; }

                /// <summary>
                /// Name of the file this class contains
                /// </summary>
                public string FileName { get; set; }

                /// <summary>
                /// Saves this file to given destination
                /// </summary>
                /// <param name="path">Path string exluding file name</param>
                public void SaveToDirectory(string path)
                {
                    File.WriteAllBytes(Path.Combine(path, FileName), Content);
                }

                /// <summary>
                /// Saves this file to given destination
                /// </summary>
                /// <param name="pathWithFileName">Path string including file name</param>
                public void SaveTo(string pathWithFileName)
                {
                    File.WriteAllBytes(pathWithFileName, Content);
                }

                /// <summary>
                /// Reads a file to this class instance
                /// </summary>
                /// <param name="pathWithFileName">Path string including file name</param>
                public void ReadFrom(string pathWithFileName)
                {
                    Content = File.ReadAllBytes(pathWithFileName);
                    FileName = Path.GetFileName(pathWithFileName);
                    ContentType = null;
                }

                /// <summary>
                /// Creates a new FileData instance from a file
                /// </summary>
                /// <param name="pathWithFileName">Path string including file name</param>
                /// <returns></returns>
                public static FileData CreateFromFile(string pathWithFileName)
                {
                    FileData fileData = new FileData();
                    fileData.ReadFrom(pathWithFileName);
                    return fileData;
                }
            }


#pragma warning disable 0649
            /// <summary>
            /// Detailed information about your account
            /// </summary>
            public class Account
            {
                /// <summary>
                /// Code used for tax purposes.
                /// </summary>
                public string TaxCode;

                /// <summary>
                /// Public key for limited access to your account such as contact/add so you can use it safely on public websites.
                /// </summary>
                public string PublicAccountID;

                /// <summary>
                /// ApiKey that gives you access to our SMTP and HTTP API's.
                /// </summary>
                public string ApiKey;

                /// <summary>
                /// Second ApiKey that gives you access to our SMTP and HTTP API's.  Used mainly for changing ApiKeys without disrupting services.
                /// </summary>
                public string ApiKey2;

                /// <summary>
                /// True, if account is a subaccount. Otherwise, false
                /// </summary>
                public bool IsSub;

                /// <summary>
                /// The number of subaccounts this account has.
                /// </summary>
                public long SubAccountsCount;

                /// <summary>
                /// Number of status: 1 - Active
                /// </summary>
                public int StatusNumber;

                /// <summary>
                /// Account status: Active
                /// </summary>
                public string StatusFormatted;

                /// <summary>
                /// Type of account: 1 for Transactional Email, 2 for Marketing Email.
                /// </summary>
                public ApiTypes.AccountType AccountType;

                /// <summary>
                /// URL form for payments.
                /// </summary>
                public string PaymentFormUrl;

                /// <summary>
                /// URL to your logo image.
                /// </summary>
                public string LogoUrl;

                /// <summary>
                /// HTTP address of your website.
                /// </summary>
                public string Website;

                /// <summary>
                /// True: Turn on or off ability to send mails under your brand. Otherwise, false
                /// </summary>
                public bool EnablePrivateBranding;

                /// <summary>
                /// Address to your support.
                /// </summary>
                public string SupportLink;

                /// <summary>
                /// Subdomain for your rebranded service
                /// </summary>
                public string PrivateBrandingUrl;

                /// <summary>
                /// First name.
                /// </summary>
                public string FirstName;

                /// <summary>
                /// Last name.
                /// </summary>
                public string LastName;

                /// <summary>
                /// Company name.
                /// </summary>
                public string Company;

                /// <summary>
                /// First line of address.
                /// </summary>
                public string Address1;

                /// <summary>
                /// Second line of address.
                /// </summary>
                public string Address2;

                /// <summary>
                /// City.
                /// </summary>
                public string City;

                /// <summary>
                /// State or province.
                /// </summary>
                public string State;

                /// <summary>
                /// Zip/postal code.
                /// </summary>
                public string Zip;

                /// <summary>
                /// Numeric ID of country.
                /// </summary>
                public int? CountryID;

                /// <summary>
                /// Phone number
                /// </summary>
                public string Phone;

                /// <summary>
                /// Proper email address.
                /// </summary>
                public string Email;

                /// <summary>
                /// URL for affiliating.
                /// </summary>
                public string AffiliateLink;

                /// <summary>
                /// Numeric reputation
                /// </summary>
                public double Reputation;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public long TotalEmailsSent;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public long? MonthlyEmailsSent;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public decimal Credit;

                /// <summary>
                /// Amount of email credits
                /// </summary>
                public int EmailCredits;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public decimal PricePerEmail;

                /// <summary>
                /// Why your clients are receiving your emails.
                /// </summary>
                public string DeliveryReason;

                /// <summary>
                /// URL for making payments.
                /// </summary>
                public string AccountPaymentUrl;

                /// <summary>
                /// Address of SMTP server.
                /// </summary>
                public string Smtp;

                /// <summary>
                /// Address of alternative SMTP server.
                /// </summary>
                public string SmtpAlternative;

                /// <summary>
                /// Status of automatic payments configuration.
                /// </summary>
                public string AutoCreditStatus;

                /// <summary>
                /// When AutoCreditStatus is Enabled, the credit level that triggers the credit to be recharged.
                /// </summary>
                public decimal AutoCreditLevel;

                /// <summary>
                /// When AutoCreditStatus is Enabled, the amount of credit to be recharged.
                /// </summary>
                public decimal AutoCreditAmount;

                /// <summary>
                /// Amount of emails account can send daily
                /// </summary>
                public int DailySendLimit;

                /// <summary>
                /// Creation date.
                /// </summary>
                public DateTime DateCreated;

                /// <summary>
                /// True, if you have enabled link tracking. Otherwise, false
                /// </summary>
                public bool LinkTracking;

                /// <summary>
                /// Type of content encoding
                /// </summary>
                public string ContentTransferEncoding;

                /// <summary>
                /// Amount of Litmus credits
                /// </summary>
                public decimal LitmusCredits;

                /// <summary>
                /// Enable advanced tools on your Account.
                /// </summary>
                public bool EnableContactFeatures;

            }

            /// <summary>
            /// Basic overview of your account
            /// </summary>
            public class AccountOverview
            {
                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public long TotalEmailsSent;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public decimal Credit;

                /// <summary>
                /// Cost of 1000 emails
                /// </summary>
                public decimal CostPerThousand;

                /// <summary>
                /// Number of messages in progress
                /// </summary>
                public long InProgressCount;

                /// <summary>
                /// Number of contacts currently with blocked status of Unsubscribed, Complaint, Bounced or InActive
                /// </summary>
                public long BlockedContactsCount;

                /// <summary>
                /// Numeric reputation
                /// </summary>
                public double Reputation;

                /// <summary>
                /// Number of contacts
                /// </summary>
                public long ContactCount;

                /// <summary>
                /// Number of created campaigns
                /// </summary>
                public long CampaignCount;

                /// <summary>
                /// Number of available templates
                /// </summary>
                public long TemplateCount;

                /// <summary>
                /// Number of created subaccounts
                /// </summary>
                public long SubAccountCount;

                /// <summary>
                /// Number of active referrals
                /// </summary>
                public long ReferralCount;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum AccountType
            {
                /// <summary>
                /// Account is used for sending transactional mail only.
                /// </summary>
                Transactional = 1,

                /// <summary>
                /// Account is used for sending bulk marketing email
                /// </summary>
                Marketing = 2,

            }

            /// <summary>
            /// Lists advanced sending options of your account.
            /// </summary>
            public class AdvancedOptions
            {
                /// <summary>
                /// True, if you want to track clicks. Otherwise, false
                /// </summary>
                public bool EnableClickTracking;

                /// <summary>
                /// True, if you want to track by link tracking. Otherwise, false
                /// </summary>
                public bool EnableLinkClickTracking;

                /// <summary>
                /// True, if text BODY of message should be created automatically. Otherwise, false
                /// </summary>
                public bool AutoTextFormat;

                /// <summary>
                /// True, if you want bounce notifications returned. Otherwise, false
                /// </summary>
                public bool EmailNotificationForError;

                /// <summary>
                /// True, if you want to send web notifications for sent email. Otherwise, false
                /// </summary>
                public bool WebNotificationForSent;

                /// <summary>
                /// True, if you want to send web notifications for opened email. Otherwise, false
                /// </summary>
                public bool WebNotificationForOpened;

                /// <summary>
                /// True, if you want to send web notifications for clicked email. Otherwise, false
                /// </summary>
                public bool WebNotificationForClicked;

                /// <summary>
                /// True, if you want to send web notifications for unsubscribed email. Otherwise, false
                /// </summary>
                public bool WebnotificationForUnsubscribed;

                /// <summary>
                /// True, if you want to send web notifications for complaint email. Otherwise, false
                /// </summary>
                public bool WebNotificationForAbuse;

                /// <summary>
                /// True, if you want to send web notifications for bounced email. Otherwise, false
                /// </summary>
                public bool WebNotificationForError;

                /// <summary>
                /// True, if you want to receive low credit email notifications. Otherwise, false
                /// </summary>
                public bool LowCreditNotification;

                /// <summary>
                /// True, if you want inbound email to only process contacts from your account. Otherwise, false
                /// </summary>
                public bool InboundContactsOnly;

                /// <summary>
                /// True, if this account is a sub-account. Otherwise, false
                /// </summary>
                public bool IsSubAccount;

                /// <summary>
                /// True, if this account resells Elastic Email. Otherwise, false.
                /// </summary>
                public bool IsOwnedByReseller;

                /// <summary>
                /// True, if you want to enable list-unsubscribe header. Otherwise, false
                /// </summary>
                public bool EnableUnsubscribeHeader;

                /// <summary>
                /// True, if you want to apply custom headers to your emails. Otherwise, false
                /// </summary>
                public bool AllowCustomHeaders;

                /// <summary>
                /// Email address to send a copy of all email to.
                /// </summary>
                public string BccEmail;

                /// <summary>
                /// Type of content encoding
                /// </summary>
                public string ContentTransferEncoding;

                /// <summary>
                /// True, if you want to receive bounce email notifications. Otherwise, false
                /// </summary>
                public string EmailNotification;

                /// <summary>
                /// URL address to receive web notifications to parse and process.
                /// </summary>
                public string WebNotificationUrl;

                /// <summary>
                /// URL used for tracking action of inbound emails
                /// </summary>
                public string HubCallbackUrl;

                /// <summary>
                /// Domain you use as your inbound domain
                /// </summary>
                public string InboundDomain;

                /// <summary>
                /// True, if account has tooltips active. Otherwise, false
                /// </summary>
                public bool EnableUITooltips;

                /// <summary>
                /// True, if you want to use Advanced Tools.  Otherwise, false
                /// </summary>
                public bool EnableContactFeatures;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum APIKeyAction
            {
                /// <summary>
                /// Add an additional APIKey to your Account.
                /// </summary>
                Add = 1,

                /// <summary>
                /// Change this APIKey to a new one.
                /// </summary>
                Change = 2,

                /// <summary>
                /// Delete this APIKey
                /// </summary>
                Delete = 3,

            }

            /// <summary>
            /// Attachment data
            /// </summary>
            public class Attachment
            {
                /// <summary>
                /// Name of your file.
                /// </summary>
                public string FileName;

                /// <summary>
                /// ID number of your attachment
                /// </summary>
                public string ID;

                /// <summary>
                /// Size of your attachment.
                /// </summary>
                public int Size;

            }

            /// <summary>
            /// Blocked Contact - Contact returning Hard Bounces
            /// </summary>
            public class BlockedContact
            {
                /// <summary>
                /// Proper email address.
                /// </summary>
                public string Email;

                /// <summary>
                /// Name of status: Active, Engaged, Inactive, Abuse, Bounced, Unsubscribed.
                /// </summary>
                public string Status;

                /// <summary>
                /// RFC error message
                /// </summary>
                public string FriendlyErrorMessage;

                /// <summary>
                /// Last change date
                /// </summary>
                public string DateUpdated;

            }

            /// <summary>
            /// Summary of bounced categories, based on specified date range.
            /// </summary>
            public class BouncedCategorySummary
            {
                /// <summary>
                /// Number of messages marked as SPAM
                /// </summary>
                public long Spam;

                /// <summary>
                /// Number of blacklisted messages
                /// </summary>
                public long BlackListed;

                /// <summary>
                /// Number of messages flagged with 'No Mailbox'
                /// </summary>
                public long NoMailbox;

                /// <summary>
                /// Number of messages flagged with 'Grey Listed'
                /// </summary>
                public long GreyListed;

                /// <summary>
                /// Number of messages flagged with 'Throttled'
                /// </summary>
                public long Throttled;

                /// <summary>
                /// Number of messages flagged with 'Timeout'
                /// </summary>
                public long Timeout;

                /// <summary>
                /// Number of messages flagged with 'Connection Problem'
                /// </summary>
                public long ConnectionProblem;

                /// <summary>
                /// Number of messages flagged with 'SPF Problem'
                /// </summary>
                public long SpfProblem;

                /// <summary>
                /// Number of messages flagged with 'Account Problem'
                /// </summary>
                public long AccountProblem;

                /// <summary>
                /// Number of messages flagged with 'DNS Problem'
                /// </summary>
                public long DnsProblem;

                /// <summary>
                /// Number of messages flagged with 'WhiteListing Problem'
                /// </summary>
                public long WhitelistingProblem;

                /// <summary>
                /// Number of messages flagged with 'Code Error'
                /// </summary>
                public long CodeError;

                /// <summary>
                /// Number of messages flagged with 'Not Delivered'
                /// </summary>
                public long NotDelivered;

                /// <summary>
                /// Number of manually cancelled messages
                /// </summary>
                public long ManualCancel;

                /// <summary>
                /// Number of messages flagged with 'Connection terminated'
                /// </summary>
                public long ConnectionTerminated;

            }

            /// <summary>
            /// Campaign
            /// </summary>
            public class Campaign
            {
                /// <summary>
                /// ID number of selected Channel.
                /// </summary>
                public int? ChannelID;

                /// <summary>
                /// Campaign's name
                /// </summary>
                public string Name;

                /// <summary>
                /// Name of campaign's status
                /// </summary>
                public ApiTypes.CampaignStatus Status;

                /// <summary>
                /// List of Segment and List IDs, comma separated
                /// </summary>
                public string[] Targets;

                /// <summary>
                /// Number of event, triggering mail sending
                /// </summary>
                public ApiTypes.CampaignTriggerType TriggerType;

                /// <summary>
                /// Date of triggered send
                /// </summary>
                public DateTime? TriggerDate;

                /// <summary>
                /// True, if campaign should be delayed. Otherwise, false.
                /// </summary>
                public double TriggerDelay;

                /// <summary>
                /// When your next automatic mail will be sent, in days
                /// </summary>
                public double TriggerFrequency;

                /// <summary>
                /// Date of send
                /// </summary>
                public int TriggerCount;

                /// <summary>
                /// ID number of transaction
                /// </summary>
                public int TriggerChannelID;

                /// <summary>
                /// Data for filtering event campaigns such as specific link addresses.
                /// </summary>
                public string TriggerData;

                /// <summary>
                /// What should be checked for choosing the winner: opens or clicks
                /// </summary>
                public ApiTypes.SplitOptimization SplitOptimization;

                /// <summary>
                /// Number of minutes between sends during optimization period
                /// </summary>
                public int SplitOptimizationMinutes;

                /// <summary>
                /// 
                /// </summary>
                public List<ApiTypes.CampaignTemplate> CampaignTemplates;

            }

            /// <summary>
            /// Channel
            /// </summary>
            public class CampaignChannel
            {
                /// <summary>
                /// ID number of selected Channel.
                /// </summary>
                public int ChannelID;

                /// <summary>
                /// Filename
                /// </summary>
                public string Name;

                /// <summary>
                /// True, if you are sending a campaign. Otherwise, false.
                /// </summary>
                public bool IsCampaign;

                /// <summary>
                /// ID number of mailer
                /// </summary>
                public int? MailerID;

                /// <summary>
                /// Date of creation in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// Name of campaign's status
                /// </summary>
                public ApiTypes.CampaignStatus Status;

                /// <summary>
                /// Date of last activity on account
                /// </summary>
                public DateTime? LastActivity;

                /// <summary>
                /// Datetime of last action done on campaign.
                /// </summary>
                public DateTime? LastProcessed;

                /// <summary>
                /// Id number of parent channel
                /// </summary>
                public int ParentChannelID;

                /// <summary>
                /// List of Segment and List IDs, comma separated
                /// </summary>
                public string[] Targets;

                /// <summary>
                /// Number of event, triggering mail sending
                /// </summary>
                public ApiTypes.CampaignTriggerType TriggerType;

                /// <summary>
                /// Date of triggered send
                /// </summary>
                public DateTime? TriggerDate;

                /// <summary>
                /// True, if campaign should be delayed. Otherwise, false.
                /// </summary>
                public double TriggerDelay;

                /// <summary>
                /// When your next automatic mail will be sent, in days
                /// </summary>
                public double TriggerFrequency;

                /// <summary>
                /// Date of send
                /// </summary>
                public int TriggerCount;

                /// <summary>
                /// ID number of transaction
                /// </summary>
                public int TriggerChannelID;

                /// <summary>
                /// Data for filtering event campaigns such as specific link addresses.
                /// </summary>
                public string TriggerData;

                /// <summary>
                /// What should be checked for choosing the winner: opens or clicks
                /// </summary>
                public ApiTypes.SplitOptimization SplitOptimization;

                /// <summary>
                /// Number of minutes between sends during optimization period
                /// </summary>
                public int SplitOptimizationMinutes;

                /// <summary>
                /// ID number of template.
                /// </summary>
                public int? TemplateID;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string TemplateSubject;

                /// <summary>
                /// Default From: email address.
                /// </summary>
                public string TemplateFromEmail;

                /// <summary>
                /// Default From: name.
                /// </summary>
                public string TemplateFromName;

                /// <summary>
                /// Default Reply: email address.
                /// </summary>
                public string TemplateReplyEmail;

                /// <summary>
                /// Default Reply: name.
                /// </summary>
                public string TemplateReplyName;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public int ClickedCount;

                /// <summary>
                /// Total emails opened.
                /// </summary>
                public int OpenedCount;

                /// <summary>
                /// Overall number of recipients
                /// </summary>
                public int RecipientCount;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public int SentCount;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public int FailedCount;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public int UnsubscribedCount;

                /// <summary>
                /// Abuses - mails sent to user without their consent
                /// </summary>
                public int FailedAbuse;

                /// <summary>
                /// List of CampaignTemplate for sending A-X split testing.
                /// </summary>
                public List<ApiTypes.CampaignChannel> TemplateChannels;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum CampaignStatus
            {
                /// <summary>
                /// Campaign is logically deleted and not returned by API or interface calls.
                /// </summary>
                Deleted = -1,

                /// <summary>
                /// Campaign is curently active and available.
                /// </summary>
                Active = 0,

                /// <summary>
                /// Campaign is currently being processed for delivery.
                /// </summary>
                Processing = 1,

                /// <summary>
                /// Campaign is currently sending.
                /// </summary>
                Sending = 2,

                /// <summary>
                /// Campaign has completed sending.
                /// </summary>
                Completed = 3,

                /// <summary>
                /// Campaign is currently paused and not sending.
                /// </summary>
                Paused = 4,

                /// <summary>
                /// Campaign has been cancelled during delivery.
                /// </summary>
                Cancelled = 5,

                /// <summary>
                /// Campaign is save as draft and not processing.
                /// </summary>
                Draft = 6,

            }

            /// <summary>
            /// 
            /// </summary>
            public class CampaignTemplate
            {
                /// <summary>
                /// ID number of selected Channel.
                /// </summary>
                public int? ChannelID;

                /// <summary>
                /// Name of campaign's status
                /// </summary>
                public ApiTypes.CampaignStatus Status;

                /// <summary>
                /// ID number of mailer
                /// </summary>
                public int? MailerID;

                /// <summary>
                /// ID number of template.
                /// </summary>
                public int? TemplateID;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string TemplateSubject;

                /// <summary>
                /// Default From: email address.
                /// </summary>
                public string TemplateFromEmail;

                /// <summary>
                /// Default From: name.
                /// </summary>
                public string TemplateFromName;

                /// <summary>
                /// Default Reply: email address.
                /// </summary>
                public string TemplateReplyEmail;

                /// <summary>
                /// Default Reply: name.
                /// </summary>
                public string TemplateReplyName;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum CampaignTriggerType
            {
                /// <summary>
                /// 
                /// </summary>
                SendNow = 1,

                /// <summary>
                /// 
                /// </summary>
                FutureScheduled = 2,

                /// <summary>
                /// 
                /// </summary>
                OnAdd = 3,

                /// <summary>
                /// 
                /// </summary>
                OnOpen = 4,

                /// <summary>
                /// 
                /// </summary>
                OnClick = 5,

            }

            /// <summary>
            /// SMTP and HTTP API channel for grouping email delivery
            /// </summary>
            public class Channel
            {
                /// <summary>
                /// Descriptive name of the channel.
                /// </summary>
                public string Name;

                /// <summary>
                /// The date the channel was added to your account.
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// The date the channel was last sent through.
                /// </summary>
                public DateTime? LastActivity;

                /// <summary>
                /// The number of email jobs this channel has been used with.
                /// </summary>
                public int JobCount;

                /// <summary>
                /// The number of emails that have been clicked within this channel.
                /// </summary>
                public int ClickedCount;

                /// <summary>
                /// The number of emails that have been opened within this channel.
                /// </summary>
                public int OpenedCount;

                /// <summary>
                /// The number of emails attempted to be sent within this channel.
                /// </summary>
                public int RecipientCount;

                /// <summary>
                /// The number of emails that have been sent within this channel.
                /// </summary>
                public int SentCount;

                /// <summary>
                /// The number of emails that have been bounced within this channel.
                /// </summary>
                public int FailedCount;

                /// <summary>
                /// The number of emails that have been unsubscribed within this channel.
                /// </summary>
                public int UnsubscribedCount;

                /// <summary>
                /// The number of emails that have been marked as abuse or complaint within this channel.
                /// </summary>
                public int FailedAbuse;

                /// <summary>
                /// The total cost for emails/attachments within this channel.
                /// </summary>
                public decimal Cost;

            }

            /// <summary>
            /// FileResponse compression format
            /// </summary>
            public enum CompressionFormat
            {
                /// <summary>
                /// No compression
                /// </summary>
                None = 0,

                /// <summary>
                /// Zip compression
                /// </summary>
                Zip = 1,

            }

            /// <summary>
            /// Contact
            /// </summary>
            public class Contact
            {
                /// <summary>
                /// Date of creation in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// Proper email address.
                /// </summary>
                public string Email;

                /// <summary>
                /// First name.
                /// </summary>
                public string FirstName;

                /// <summary>
                /// Last name.
                /// </summary>
                public string LastName;

                /// <summary>
                /// Title
                /// </summary>
                public string Title;

                /// <summary>
                /// Name of organization
                /// </summary>
                public string OrganizationName;

                /// <summary>
                /// City.
                /// </summary>
                public string City;

                /// <summary>
                /// Name of country.
                /// </summary>
                public string Country;

                /// <summary>
                /// State or province.
                /// </summary>
                public string State;

                /// <summary>
                /// Zip/postal code.
                /// </summary>
                public string Zip;

                /// <summary>
                /// Phone number
                /// </summary>
                public string Phone;

                /// <summary>
                /// Date of birth in YYYY-MM-DD format
                /// </summary>
                public DateTime? BirthDate;

                /// <summary>
                /// Your gender
                /// </summary>
                public string Gender;

                /// <summary>
                /// Name of status: Active, Engaged, Inactive, Abuse, Bounced, Unsubscribed.
                /// </summary>
                public ApiTypes.ContactStatus Status;

                /// <summary>
                /// RFC Error code
                /// </summary>
                public int? BouncedErrorCode;

                /// <summary>
                /// RFC error message
                /// </summary>
                public string BouncedErrorMessage;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public int TotalSent;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public int TotalFailed;

                /// <summary>
                /// Total emails opened.
                /// </summary>
                public int TotalOpened;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public int TotalClicked;

                /// <summary>
                /// Date of first failed message
                /// </summary>
                public DateTime? FirstFailedDate;

                /// <summary>
                /// Number of fails in sending to this Contact
                /// </summary>
                public int LastFailedCount;

                /// <summary>
                /// Last change date
                /// </summary>
                public DateTime DateUpdated;

                /// <summary>
                /// Source of URL of payment
                /// </summary>
                public ApiTypes.ContactSource Source;

                /// <summary>
                /// RFC Error code
                /// </summary>
                public int? ErrorCode;

                /// <summary>
                /// RFC error message
                /// </summary>
                public string FriendlyErrorMessage;

                /// <summary>
                /// IP address
                /// </summary>
                public string CreatedFromIP;

                /// <summary>
                /// Yearly revenue for the contact
                /// </summary>
                public decimal Revenue;

                /// <summary>
                /// Number of purchases contact has made
                /// </summary>
                public int PurchaseCount;

                /// <summary>
                /// Mobile phone number
                /// </summary>
                public string MobileNumber;

                /// <summary>
                /// Fax number
                /// </summary>
                public string FaxNumber;

                /// <summary>
                /// Biography for Linked-In
                /// </summary>
                public string LinkedInBio;

                /// <summary>
                /// Number of Linked-In connections
                /// </summary>
                public int LinkedInConnections;

                /// <summary>
                /// Biography for Twitter
                /// </summary>
                public string TwitterBio;

                /// <summary>
                /// User name for Twitter
                /// </summary>
                public string TwitterUsername;

                /// <summary>
                /// URL for Twitter photo
                /// </summary>
                public string TwitterProfilePhoto;

                /// <summary>
                /// Number of Twitter followers
                /// </summary>
                public int TwitterFollowerCount;

                /// <summary>
                /// Unsubscribed date in YYYY-MM-DD format
                /// </summary>
                public DateTime? UnsubscribedDate;

                /// <summary>
                /// Industry contact works in
                /// </summary>
                public string Industry;

                /// <summary>
                /// Number of employees
                /// </summary>
                public int NumberOfEmployees;

                /// <summary>
                /// Annual revenue of contact
                /// </summary>
                public decimal? AnnualRevenue;

                /// <summary>
                /// Date of first purchase in YYYY-MM-DD format
                /// </summary>
                public DateTime? FirstPurchase;

                /// <summary>
                /// Date of last purchase in YYYY-MM-DD format
                /// </summary>
                public DateTime? LastPurchase;

                /// <summary>
                /// Free form field of notes
                /// </summary>
                public string Notes;

                /// <summary>
                /// Website of contact
                /// </summary>
                public string WebsiteUrl;

                /// <summary>
                /// Number of page views
                /// </summary>
                public int PageViews;

                /// <summary>
                /// Number of website visits
                /// </summary>
                public int Visits;

                /// <summary>
                /// Number of messages sent last month
                /// </summary>
                public int? LastMonthSent;

                /// <summary>
                /// Date this contact last opened an email
                /// </summary>
                public DateTime? LastOpened;

                /// <summary>
                /// 
                /// </summary>
                public DateTime? LastClicked;

                /// <summary>
                /// Your gravatar hash for image
                /// </summary>
                public string GravatarHash;

            }

            /// <summary>
            /// Collection of lists and segments
            /// </summary>
            public class ContactCollection
            {
                /// <summary>
                /// Lists which contain the requested contact
                /// </summary>
                public List<ApiTypes.ContactContainer> Lists;

                /// <summary>
                /// Segments which contain the requested contact
                /// </summary>
                public List<ApiTypes.ContactContainer> Segments;

            }

            /// <summary>
            /// List's or segment's short info
            /// </summary>
            public class ContactContainer
            {
                /// <summary>
                /// ID of the list/segment
                /// </summary>
                public int ID;

                /// <summary>
                /// Name of the list/segment
                /// </summary>
                public string Name;

            }

            /// <summary>
            /// History of chosen Contact
            /// </summary>
            public class ContactHistory
            {
                /// <summary>
                /// ID of history of selected Contact.
                /// </summary>
                public long ContactHistoryID;

                /// <summary>
                /// Type of event occured on this Contact.
                /// </summary>
                public string EventType;

                /// <summary>
                /// Numeric code of event occured on this Contact.
                /// </summary>
                public int EventTypeValue;

                /// <summary>
                /// Formatted date of event.
                /// </summary>
                public string EventDate;

                /// <summary>
                /// Name of selected channel.
                /// </summary>
                public string ChannelName;

                /// <summary>
                /// Name of template.
                /// </summary>
                public string TemplateName;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum ContactSource
            {
                /// <summary>
                /// Source of the contact is not known.
                /// </summary>
                Unknown = 0,

                /// <summary>
                /// Contact was inputted from the website interface.
                /// </summary>
                ManualInput = 1,

                /// <summary>
                /// Contact was uploaded from the website interface.
                /// </summary>
                ListUpload = 2,

                /// <summary>
                /// Contact was added from a public web form.
                /// </summary>
                WebForm = 3,

                /// <summary>
                /// Contact was added from an API call.
                /// </summary>
                APICall = 4,

            }

            /// <summary>
            /// 
            /// </summary>
            public enum ContactStatus
            {
                /// <summary>
                /// Number of engaged contacts
                /// </summary>
                Engaged = -1,

                /// <summary>
                /// Number of active contacts
                /// </summary>
                Active = 0,

                /// <summary>
                /// Number of bounced messages
                /// </summary>
                Bounced = 1,

                /// <summary>
                /// Number of unsubscribed messages
                /// </summary>
                Unsubscribed = 2,

                /// <summary>
                /// Abuses - mails sent to user without their consent
                /// </summary>
                Abuse = 3,

                /// <summary>
                /// Number of inactive contacts
                /// </summary>
                Inactive = 4,

            }

            /// <summary>
            /// Number of Contacts, grouped by Status;
            /// </summary>
            public class ContactStatusCounts
            {
                /// <summary>
                /// Number of engaged contacts
                /// </summary>
                public long Engaged;

                /// <summary>
                /// Number of active contacts
                /// </summary>
                public long Active;

                /// <summary>
                /// Number of complaint messages
                /// </summary>
                public long Complaint;

                /// <summary>
                /// Number of unsubscribed messages
                /// </summary>
                public long Unsubscribed;

                /// <summary>
                /// Number of bounced messages
                /// </summary>
                public long Bounced;

                /// <summary>
                /// Number of inactive contacts
                /// </summary>
                public long Inactive;

            }

            /// <summary>
            /// Daily summary of log status, based on specified date range.
            /// </summary>
            public class DailyLogStatusSummary
            {
                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public string Date;

                /// <summary>
                /// Proper email address.
                /// </summary>
                public int Email;

                /// <summary>
                /// Number of SMS
                /// </summary>
                public int Sms;

                /// <summary>
                /// Number of delivered messages
                /// </summary>
                public int Delivered;

                /// <summary>
                /// Number of opened messages
                /// </summary>
                public int Opened;

                /// <summary>
                /// Number of clicked messages
                /// </summary>
                public int Clicked;

                /// <summary>
                /// Number of unsubscribed messages
                /// </summary>
                public int Unsubscribed;

                /// <summary>
                /// Number of complaint messages
                /// </summary>
                public int Complaint;

                /// <summary>
                /// Number of bounced messages
                /// </summary>
                public int Bounced;

                /// <summary>
                /// Number of inbound messages
                /// </summary>
                public int Inbound;

                /// <summary>
                /// Number of manually cancelled messages
                /// </summary>
                public int ManualCancel;

                /// <summary>
                /// Number of messages flagged with 'Not Delivered'
                /// </summary>
                public int NotDelivered;

            }

            /// <summary>
            /// Domain data, with information about domain records.
            /// </summary>
            public class DomainDetail
            {
                /// <summary>
                /// Name of selected domain.
                /// </summary>
                public string Domain;

                /// <summary>
                /// True, if domain is used as default. Otherwise, false,
                /// </summary>
                public bool DefaultDomain;

                /// <summary>
                /// True, if SPF record is verified
                /// </summary>
                public bool Spf;

                /// <summary>
                /// True, if DKIM record is verified
                /// </summary>
                public bool Dkim;

                /// <summary>
                /// True, if MX record is verified
                /// </summary>
                public bool MX;

                /// <summary>
                /// 
                /// </summary>
                public bool DMARC;

                /// <summary>
                /// True, if tracking CNAME record is verified
                /// </summary>
                public bool IsRewriteDomainValid;

                /// <summary>
                /// True, if verification is available
                /// </summary>
                public bool Verify;

            }

            /// <summary>
            /// Detailed information about email credits
            /// </summary>
            public class EmailCredits
            {
                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime Date;

                /// <summary>
                /// Amount of money in transaction
                /// </summary>
                public decimal Amount;

                /// <summary>
                /// Source of URL of payment
                /// </summary>
                public string Source;

                /// <summary>
                /// Free form field of notes
                /// </summary>
                public string Notes;

            }

            /// <summary>
            /// 
            /// </summary>
            public class EmailJobFailedStatus
            {
                /// <summary>
                /// 
                /// </summary>
                public string Address;

                /// <summary>
                /// 
                /// </summary>
                public string Error;

                /// <summary>
                /// RFC Error code
                /// </summary>
                public int ErrorCode;

                /// <summary>
                /// 
                /// </summary>
                public string Category;

            }

            /// <summary>
            /// 
            /// </summary>
            public class EmailJobStatus
            {
                /// <summary>
                /// ID number of your attachment
                /// </summary>
                public string ID;

                /// <summary>
                /// Name of status: submitted, complete, in_progress
                /// </summary>
                public string Status;

                /// <summary>
                /// 
                /// </summary>
                public int RecipientsCount;

                /// <summary>
                /// 
                /// </summary>
                public List<ApiTypes.EmailJobFailedStatus> Failed;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public int FailedCount;

                /// <summary>
                /// Number of delivered messages
                /// </summary>
                public List<string> Delivered;

                /// <summary>
                /// 
                /// </summary>
                public int DeliveredCount;

                /// <summary>
                /// 
                /// </summary>
                public List<string> Pending;

                /// <summary>
                /// 
                /// </summary>
                public int PendingCount;

                /// <summary>
                /// Number of opened messages
                /// </summary>
                public List<string> Opened;

                /// <summary>
                /// Total emails opened.
                /// </summary>
                public int OpenedCount;

                /// <summary>
                /// Number of clicked messages
                /// </summary>
                public List<string> Clicked;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public int ClickedCount;

                /// <summary>
                /// Number of unsubscribed messages
                /// </summary>
                public List<string> Unsubscribed;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public int UnsubscribedCount;

                /// <summary>
                /// 
                /// </summary>
                public List<string> AbuseReports;

                /// <summary>
                /// 
                /// </summary>
                public int AbuseReportsCount;

                /// <summary>
                /// List of all MessageIDs for this job.
                /// </summary>
                public List<string> MessageIDs;

            }

            /// <summary>
            /// 
            /// </summary>
            public class EmailSend
            {
                /// <summary>
                /// ID number of transaction
                /// </summary>
                public string TransactionID;

                /// <summary>
                /// Unique identifier for this email.
                /// </summary>
                public string MessageID;

            }

            /// <summary>
            /// Status information of the specified email
            /// </summary>
            public class EmailStatus
            {
                /// <summary>
                /// Email address this email was sent from.
                /// </summary>
                public string From;

                /// <summary>
                /// Email address this email was sent to.
                /// </summary>
                public string To;

                /// <summary>
                /// Date the email was submitted.
                /// </summary>
                public DateTime Date;

                /// <summary>
                /// Name of email's status
                /// </summary>
                public ApiTypes.LogJobStatus Status;

                /// <summary>
                /// Date of last status change.
                /// </summary>
                public DateTime StatusChangeDate;

                /// <summary>
                /// Detailed error or bounced message.
                /// </summary>
                public string ErrorMessage;

                /// <summary>
                /// ID number of transaction
                /// </summary>
                public Guid TransactionID;

            }

            /// <summary>
            /// Email details formatted in json
            /// </summary>
            public class EmailView
            {
                /// <summary>
                /// Body (text) of your message.
                /// </summary>
                public string Body;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string Subject;

                /// <summary>
                /// Starting date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public string From;

            }

            /// <summary>
            /// Encoding type for the email headers
            /// </summary>
            public enum EncodingType
            {
                /// <summary>
                /// Encoding of the email is provided by the sender and not altered.
                /// </summary>
                UserProvided = -1,

                /// <summary>
                /// No endcoding is set for the email.
                /// </summary>
                None = 0,

                /// <summary>
                /// Encoding of the email is in Raw7bit format.
                /// </summary>
                Raw7bit = 1,

                /// <summary>
                /// Encoding of the email is in Raw8bit format.
                /// </summary>
                Raw8bit = 2,

                /// <summary>
                /// Encoding of the email is in QuotedPrintable format.
                /// </summary>
                QuotedPrintable = 3,

                /// <summary>
                /// Encoding of the email is in Base64 format.
                /// </summary>
                Base64 = 4,

                /// <summary>
                /// Encoding of the email is in Uue format.
                /// </summary>
                Uue = 5,

            }

            /// <summary>
            /// Record of exported data from the system.
            /// </summary>
            public class Export
            {
                /// <summary>
                /// 
                /// </summary>
                public Guid PublicExportID;

                /// <summary>
                /// Date the export was created
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// Type of export
                /// </summary>
                public string Type;

                /// <summary>
                /// Current status of export
                /// </summary>
                public string Status;

                /// <summary>
                /// Long description of the export
                /// </summary>
                public string Info;

                /// <summary>
                /// Name of the file
                /// </summary>
                public string Filename;

                /// <summary>
                /// Link to download the export
                /// </summary>
                public string Link;

            }

            /// <summary>
            /// Type of export
            /// </summary>
            public enum ExportFileFormats
            {
                /// <summary>
                /// Export in comma separated values format.
                /// </summary>
                Csv = 1,

                /// <summary>
                /// Export in xml format
                /// </summary>
                Xml = 2,

                /// <summary>
                /// Export in json format
                /// </summary>
                Json = 3,

            }

            /// <summary>
            /// 
            /// </summary>
            public class ExportLink
            {
                /// <summary>
                /// Direct URL to the exported file
                /// </summary>
                public string Link;

            }

            /// <summary>
            /// Current status of export
            /// </summary>
            public enum ExportStatus
            {
                /// <summary>
                /// Export had an error and can not be downloaded.
                /// </summary>
                Error = -1,

                /// <summary>
                /// Export is currently loading and can not be downloaded.
                /// </summary>
                Loading = 0,

                /// <summary>
                /// Export is currently available for downloading.
                /// </summary>
                Ready = 1,

                /// <summary>
                /// Export is no longer available for downloading.
                /// </summary>
                Expired = 2,

            }

            /// <summary>
            /// Number of Exports, grouped by export type
            /// </summary>
            public class ExportTypeCounts
            {
                /// <summary>
                /// 
                /// </summary>
                public long Log;

                /// <summary>
                /// 
                /// </summary>
                public long Contact;

                /// <summary>
                /// Json representation of a campaign
                /// </summary>
                public long Campaign;

                /// <summary>
                /// True, if you have enabled link tracking. Otherwise, false
                /// </summary>
                public long LinkTracking;

                /// <summary>
                /// Json representation of a survey
                /// </summary>
                public long Survey;

            }

            /// <summary>
            /// Object containig tracking data.
            /// </summary>
            public class LinkTrackingDetails
            {
                /// <summary>
                /// Number of items.
                /// </summary>
                public int Count;

                /// <summary>
                /// True, if there are more detailed data available. Otherwise, false
                /// </summary>
                public bool MoreAvailable;

                /// <summary>
                /// 
                /// </summary>
                public List<ApiTypes.TrackedLink> TrackedLink;

            }

            /// <summary>
            /// List of Contacts, with detailed data about its contents.
            /// </summary>
            public class List
            {
                /// <summary>
                /// ID number of selected list.
                /// </summary>
                public int ListID;

                /// <summary>
                /// Name of your list.
                /// </summary>
                public string ListName;

                /// <summary>
                /// Number of items.
                /// </summary>
                public int Count;

                /// <summary>
                /// ID code of list
                /// </summary>
                public Guid? PublicListID;

                /// <summary>
                /// Date of creation in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// True: Allow unsubscribing from this list. Otherwise, false
                /// </summary>
                public bool AllowUnsubscribe;

                /// <summary>
                /// Query used for filtering.
                /// </summary>
                public string Rule;

            }

            /// <summary>
            /// Detailed information about litmus credits
            /// </summary>
            public class LitmusCredits
            {
                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime Date;

                /// <summary>
                /// Amount of money in transaction
                /// </summary>
                public decimal Amount;

            }

            /// <summary>
            /// Logs for selected date range
            /// </summary>
            public class Log
            {
                /// <summary>
                /// Starting date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public DateTime? From;

                /// <summary>
                /// Ending date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public DateTime? To;

                /// <summary>
                /// Number of recipients
                /// </summary>
                public List<ApiTypes.Recipient> Recipients;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum LogJobStatus
            {
                /// <summary>
                /// Email has been submitted successfully and is queued for sending.
                /// </summary>
                ReadyToSend = 1,

                /// <summary>
                /// Email has soft bounced and is scheduled to retry.
                /// </summary>
                WaitingToRetry = 2,

                /// <summary>
                /// Email is currently sending.
                /// </summary>
                Sending = 3,

                /// <summary>
                /// Email has errored or bounced for some reason.
                /// </summary>
                Error = 4,

                /// <summary>
                /// Email has been successfully delivered.
                /// </summary>
                Sent = 5,

                /// <summary>
                /// Email has been opened by the recipient.
                /// </summary>
                Opened = 6,

                /// <summary>
                /// Email has had at least one link clicked by the recipient.
                /// </summary>
                Clicked = 7,

                /// <summary>
                /// Email has been unsubscribed by the recipient.
                /// </summary>
                Unsubscribed = 8,

                /// <summary>
                /// Email has been complained about or marked as spam by the recipient.
                /// </summary>
                AbuseReport = 9,

            }

            /// <summary>
            /// Summary of log status, based on specified date range.
            /// </summary>
            public class LogStatusSummary
            {
                /// <summary>
                /// Starting date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public string From;

                /// <summary>
                /// Ending date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public string To;

                /// <summary>
                /// Overall duration
                /// </summary>
                public double Duration;

                /// <summary>
                /// Number of recipients
                /// </summary>
                public long Recipients;

                /// <summary>
                /// Number of emails
                /// </summary>
                public long EmailTotal;

                /// <summary>
                /// Number of SMS
                /// </summary>
                public long SmsTotal;

                /// <summary>
                /// Number of delivered messages
                /// </summary>
                public long Delivered;

                /// <summary>
                /// Number of bounced messages
                /// </summary>
                public long Bounced;

                /// <summary>
                /// Number of messages in progress
                /// </summary>
                public long InProgress;

                /// <summary>
                /// Number of opened messages
                /// </summary>
                public long Opened;

                /// <summary>
                /// Number of clicked messages
                /// </summary>
                public long Clicked;

                /// <summary>
                /// Number of unsubscribed messages
                /// </summary>
                public long Unsubscribed;

                /// <summary>
                /// Number of complaint messages
                /// </summary>
                public long Complaints;

                /// <summary>
                /// Number of inbound messages
                /// </summary>
                public long Inbound;

                /// <summary>
                /// Number of manually cancelled messages
                /// </summary>
                public long ManualCancel;

                /// <summary>
                /// Number of messages flagged with 'Not Delivered'
                /// </summary>
                public long NotDelivered;

                /// <summary>
                /// ID number of template used
                /// </summary>
                public bool TemplateChannel;

            }

            /// <summary>
            /// Overall log summary information.
            /// </summary>
            public class LogSummary
            {
                /// <summary>
                /// Summary of log status, based on specified date range.
                /// </summary>
                public ApiTypes.LogStatusSummary LogStatusSummary;

                /// <summary>
                /// Summary of bounced categories, based on specified date range.
                /// </summary>
                public ApiTypes.BouncedCategorySummary BouncedCategorySummary;

                /// <summary>
                /// Daily summary of log status, based on specified date range.
                /// </summary>
                public List<ApiTypes.DailyLogStatusSummary> DailyLogStatusSummary;

            }

            /// <summary>
            /// Queue of notifications
            /// </summary>
            public class NotificationQueue
            {
                /// <summary>
                /// Creation date.
                /// </summary>
                public string DateCreated;

                /// <summary>
                /// Date of last status change.
                /// </summary>
                public string StatusChangeDate;

                /// <summary>
                /// Actual status.
                /// </summary>
                public string NewStatus;

                /// <summary>
                /// 
                /// </summary>
                public string Reference;

                /// <summary>
                /// Error message.
                /// </summary>
                public string ErrorMessage;

                /// <summary>
                /// Number of previous delivery attempts
                /// </summary>
                public string RetryCount;

            }

            /// <summary>
            /// Detailed information about existing money transfers.
            /// </summary>
            public class Payment
            {
                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime Date;

                /// <summary>
                /// Amount of money in transaction
                /// </summary>
                public decimal Amount;

                /// <summary>
                /// Source of URL of payment
                /// </summary>
                public string Source;

            }

            /// <summary>
            /// Private IP Address
            /// </summary>
            public class PrivateIP
            {
                /// <summary>
                /// Assigned Private IP address.
                /// </summary>
                public string IPAddress;

                /// <summary>
                /// Link to Sender Score for this IP address to view external reputation.
                /// </summary>
                public string SenderScore;

                /// <summary>
                /// Link to MX ToolBox blacklist check for this IP address.
                /// </summary>
                public string MXToolBox;

                /// <summary>
                /// Configuration information to set up a custom rDNS A record.
                /// </summary>
                public string rDNSConfiguration;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum QuestionType
            {
                /// <summary>
                /// 
                /// </summary>
                RadioButtons = 1,

                /// <summary>
                /// 
                /// </summary>
                DropdownMenu = 2,

                /// <summary>
                /// 
                /// </summary>
                Checkboxes = 3,

                /// <summary>
                /// 
                /// </summary>
                LongAnswer = 4,

                /// <summary>
                /// 
                /// </summary>
                Textbox = 5,

                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                Date = 6,

            }

            /// <summary>
            /// Detailed information about message recipient
            /// </summary>
            public class Recipient
            {
                /// <summary>
                /// True, if message is SMS. Otherwise, false
                /// </summary>
                public bool IsSms;

                /// <summary>
                /// ID number of selected message.
                /// </summary>
                public string MsgID;

                /// <summary>
                /// Ending date for search in YYYY-MM-DDThh:mm:ss format.
                /// </summary>
                public string To;

                /// <summary>
                /// Name of recipient's status: Submitted, ReadyToSend, WaitingToRetry, Sending, Bounced, Sent, Opened, Clicked, Unsubscribed, AbuseReport
                /// </summary>
                public string Status;

                /// <summary>
                /// Name of selected Channel.
                /// </summary>
                public string Channel;

                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public string Date;

                /// <summary>
                /// Content of message, HTML encoded
                /// </summary>
                public string Message;

                /// <summary>
                /// True, if message category should be shown. Otherwise, false
                /// </summary>
                public bool ShowCategory;

                /// <summary>
                /// ID of message category
                /// </summary>
                public string MessageCategory;

                /// <summary>
                /// Date of last status change.
                /// </summary>
                public string StatusChangeDate;

                /// <summary>
                /// Date of next try
                /// </summary>
                public string NextTryOn;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string Subject;

                /// <summary>
                /// Default From: email address.
                /// </summary>
                public string FromEmail;

                /// <summary>
                /// ID of certain mail job
                /// </summary>
                public string JobID;

                /// <summary>
                /// True, if message is a SMS and status is not yet confirmed. Otherwise, false
                /// </summary>
                public bool SmsUpdateRequired;

                /// <summary>
                /// Content of message
                /// </summary>
                public string TextMessage;

                /// <summary>
                /// Comma separated ID numbers of messages.
                /// </summary>
                public string MessageSid;

            }

            /// <summary>
            /// Referral details for this account.
            /// </summary>
            public class Referral
            {
                /// <summary>
                /// Current amount of dolars you have from referring.
                /// </summary>
                public decimal CurrentReferralCredit;

                /// <summary>
                /// Number of active referrals.
                /// </summary>
                public long CurrentReferralCount;

            }

            /// <summary>
            /// Detailed sending reputation of your account.
            /// </summary>
            public class ReputationDetail
            {
                /// <summary>
                /// Overall reputation impact, based on the most important factors.
                /// </summary>
                public ApiTypes.ReputationImpact Impact;

                /// <summary>
                /// Percent of Complaining users - those, who do not want to receive email from you.
                /// </summary>
                public double AbusePercent;

                /// <summary>
                /// Percent of Unknown users - users that couldn't be found
                /// </summary>
                public double UnknownUsersPercent;

                /// <summary>
                /// Penalty from messages marked as spam.
                /// </summary>
                public double AverageSpamScore;

                /// <summary>
                /// Percent of Bounced users
                /// </summary>
                public double FailedSpamPercent;

                /// <summary>
                /// Points from quantity of your emails.
                /// </summary>
                public double RepEmailsSent;

                /// <summary>
                /// Average reputation.
                /// </summary>
                public double AverageReputation;

                /// <summary>
                /// Actual price level.
                /// </summary>
                public double PriceLevelReputation;

                /// <summary>
                /// Reputation needed to change pricing.
                /// </summary>
                public double NextPriceLevelReputation;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public string PriceLevel;

                /// <summary>
                /// True, if tracking domain is correctly configured. Otherwise, false.
                /// </summary>
                public bool TrackingDomainValid;

                /// <summary>
                /// True, if sending domain is correctly configured. Otherwise, false.
                /// </summary>
                public bool SenderDomainValid;

            }

            /// <summary>
            /// Reputation history of your account.
            /// </summary>
            public class ReputationHistory
            {
                /// <summary>
                /// Creation date.
                /// </summary>
                public string DateCreated;

                /// <summary>
                /// Percent of Complaining users - those, who do not want to receive email from you.
                /// </summary>
                public double AbusePercent;

                /// <summary>
                /// Percent of Unknown users - users that couldn't be found
                /// </summary>
                public double UnknownUsersPercent;

                /// <summary>
                /// Penalty from messages marked as spam.
                /// </summary>
                public double AverageSpamScore;

                /// <summary>
                /// Points from proper setup of your account
                /// </summary>
                public double SetupScore;

                /// <summary>
                /// Points from quantity of your emails.
                /// </summary>
                public double RepEmailsSent;

                /// <summary>
                /// Numeric reputation
                /// </summary>
                public double Reputation;

            }

            /// <summary>
            /// Overall reputation impact, based on the most important factors.
            /// </summary>
            public class ReputationImpact
            {
                /// <summary>
                /// Abuses - mails sent to user without their consent
                /// </summary>
                public double Abuse;

                /// <summary>
                /// Users, that could not be reached.
                /// </summary>
                public double UnknownUsers;

                /// <summary>
                /// Penalty from messages marked as spam.
                /// </summary>
                public double AverageSpamScore;

                /// <summary>
                /// Content analysis.
                /// </summary>
                public double ServerFilter;

                /// <summary>
                /// Total emails sent.
                /// </summary>
                public double TotalEmailSent;

                /// <summary>
                /// Tracking domain.
                /// </summary>
                public double TrackingDomain;

                /// <summary>
                /// Sending domain.
                /// </summary>
                public double SenderDomain;

            }

            /// <summary>
            /// Information about Contact Segment, selected by RULE.
            /// </summary>
            public class Segment
            {
                /// <summary>
                /// ID number of your segment.
                /// </summary>
                public int SegmentID;

                /// <summary>
                /// ID of selected account.
                /// </summary>
                public int AccountID;

                /// <summary>
                /// Filename
                /// </summary>
                public string Name;

                /// <summary>
                /// Query used for filtering.
                /// </summary>
                public string Rule;

                /// <summary>
                /// Number of items from last check.
                /// </summary>
                public long LastCount;

                /// <summary>
                /// History of segment information.
                /// </summary>
                public List<ApiTypes.SegmentHistory> History;

            }

            /// <summary>
            /// Segment History
            /// </summary>
            public class SegmentHistory
            {
                /// <summary>
                /// ID number of history.
                /// </summary>
                public int SegmentHistoryID;

                /// <summary>
                /// ID number of your segment.
                /// </summary>
                public int SegmentID;

                /// <summary>
                /// ID of selected account.
                /// </summary>
                public int AccountID;

                /// <summary>
                /// Date in YYYY-MM-DD format
                /// </summary>
                public int Day;

                /// <summary>
                /// Number of items.
                /// </summary>
                public long Count;

                /// <summary>
                /// 
                /// </summary>
                public long EngagedCount;

                /// <summary>
                /// 
                /// </summary>
                public long ActiveCount;

                /// <summary>
                /// 
                /// </summary>
                public long BouncedCount;

                /// <summary>
                /// Total emails clicked
                /// </summary>
                public long UnsubscribedCount;

                /// <summary>
                /// 
                /// </summary>
                public long AbuseCount;

                /// <summary>
                /// 
                /// </summary>
                public long InactiveCount;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum SendingPermission
            {
                /// <summary>
                /// Sending not allowed.
                /// </summary>
                None = 0,

                /// <summary>
                /// Allow sending via SMTP only.
                /// </summary>
                Smtp = 1,

                /// <summary>
                /// Allow sending via HTTP API only.
                /// </summary>
                HttpApi = 2,

                /// <summary>
                /// Allow sending via SMTP and HTTP API.
                /// </summary>
                SmtpAndHttpApi = 3,

                /// <summary>
                /// Allow sending via the website interface only.
                /// </summary>
                Interface = 4,

                /// <summary>
                /// Allow sending via SMTP and the website interface.
                /// </summary>
                SmtpAndInterface = 5,

                /// <summary>
                /// Allow sendnig via HTTP API and the website interface.
                /// </summary>
                HttpApiAndInterface = 6,

                /// <summary>
                /// Sending allowed via SMTP, HTTP API and the website interface.
                /// </summary>
                All = 255,

            }

            /// <summary>
            /// Spam check of specified message.
            /// </summary>
            public class SpamCheck
            {
                /// <summary>
                /// Total spam score from
                /// </summary>
                public string TotalScore;

                /// <summary>
                /// Date in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public string Date;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string Subject;

                /// <summary>
                /// Default From: email address.
                /// </summary>
                public string FromEmail;

                /// <summary>
                /// ID number of selected message.
                /// </summary>
                public string MsgID;

                /// <summary>
                /// Name of selected channel.
                /// </summary>
                public string ChannelName;

                /// <summary>
                /// 
                /// </summary>
                public List<ApiTypes.SpamRule> Rules;

            }

            /// <summary>
            /// Single spam score
            /// </summary>
            public class SpamRule
            {
                /// <summary>
                /// Spam score
                /// </summary>
                public string Score;

                /// <summary>
                /// Name of rule
                /// </summary>
                public string Key;

                /// <summary>
                /// Description of rule.
                /// </summary>
                public string Description;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum SplitOptimization
            {
                /// <summary>
                /// Number of opened messages
                /// </summary>
                Opened = 0,

                /// <summary>
                /// Number of clicked messages
                /// </summary>
                Clicked = 1,

            }

            /// <summary>
            /// Subaccount. Contains detailed data of your Subaccount.
            /// </summary>
            public class SubAccount
            {
                /// <summary>
                /// ID of selected account.
                /// </summary>
                public string AccountID;

                /// <summary>
                /// Public key for limited access to your account such as contact/add so you can use it safely on public websites.
                /// </summary>
                public string PublicAccountID;

                /// <summary>
                /// ApiKey that gives you access to our SMTP and HTTP API's.
                /// </summary>
                public string ApiKey;

                /// <summary>
                /// Proper email address.
                /// </summary>
                public string Email;

                /// <summary>
                /// ID number of mailer
                /// </summary>
                public string MailerID;

                /// <summary>
                /// Type of account: 1 for Transactional Email, 2 for Marketing Email.
                /// </summary>
                public ApiTypes.AccountType AccountType;

                /// <summary>
                /// Date of last activity on account
                /// </summary>
                public string LastActivity;

                /// <summary>
                /// Amount of email credits
                /// </summary>
                public string EmailCredits;

                /// <summary>
                /// True, if account needs credits to send emails. Otherwise, false
                /// </summary>
                public bool RequiresEmailCredits;

                /// <summary>
                /// Amount of credits added to account automatically
                /// </summary>
                public double MonthlyRefillCredits;

                /// <summary>
                /// True, if account needs credits to buy templates. Otherwise, false
                /// </summary>
                public bool RequiresTemplateCredits;

                /// <summary>
                /// Amount of Litmus credits
                /// </summary>
                public decimal LitmusCredits;

                /// <summary>
                /// True, if account is able to send template tests to Litmus. Otherwise, false
                /// </summary>
                public bool EnableLitmusTest;

                /// <summary>
                /// True, if account needs credits to send emails. Otherwise, false
                /// </summary>
                public bool RequiresLitmusCredits;

                /// <summary>
                /// True, if account can buy templates on its own. Otherwise, false
                /// </summary>
                public bool EnablePremiumTemplates;

                /// <summary>
                /// True, if account can request for private IP on its own. Otherwise, false
                /// </summary>
                public bool EnablePrivateIPRequest;

                /// <summary>
                /// Amount of emails sent from this account
                /// </summary>
                public long TotalEmailsSent;

                /// <summary>
                /// Percent of Unknown users - users that couldn't be found
                /// </summary>
                public double UnknownUsersPercent;

                /// <summary>
                /// Percent of Complaining users - those, who do not want to receive email from you.
                /// </summary>
                public double AbusePercent;

                /// <summary>
                /// Percent of Bounced users
                /// </summary>
                public double FailedSpamPercent;

                /// <summary>
                /// Numeric reputation
                /// </summary>
                public double Reputation;

                /// <summary>
                /// Amount of emails account can send daily
                /// </summary>
                public long DailySendLimit;

                /// <summary>
                /// Name of account's status: Deleted, Disabled, UnderReview, NoPaymentsAllowed, NeverSignedIn, Active, SystemPaused
                /// </summary>
                public string Status;

            }

            /// <summary>
            /// Detailed account settings.
            /// </summary>
            public class SubAccountSettings
            {
                /// <summary>
                /// Proper email address.
                /// </summary>
                public string Email;

                /// <summary>
                /// True, if account needs credits to send emails. Otherwise, false
                /// </summary>
                public bool RequiresEmailCredits;

                /// <summary>
                /// True, if account needs credits to buy templates. Otherwise, false
                /// </summary>
                public bool RequiresTemplateCredits;

                /// <summary>
                /// Amount of credits added to account automatically
                /// </summary>
                public double MonthlyRefillCredits;

                /// <summary>
                /// Amount of Litmus credits
                /// </summary>
                public decimal LitmusCredits;

                /// <summary>
                /// True, if account is able to send template tests to Litmus. Otherwise, false
                /// </summary>
                public bool EnableLitmusTest;

                /// <summary>
                /// True, if account needs credits to send emails. Otherwise, false
                /// </summary>
                public bool RequiresLitmusCredits;

                /// <summary>
                /// Maximum size of email including attachments in MB's
                /// </summary>
                public int EmailSizeLimit;

                /// <summary>
                /// Amount of emails account can send daily
                /// </summary>
                public int DailySendLimit;

                /// <summary>
                /// Maximum number of contacts the account can havelkd
                /// </summary>
                public int MaxContacts;

                /// <summary>
                /// True, if account can request for private IP on its own. Otherwise, false
                /// </summary>
                public bool EnablePrivateIPRequest;

                /// <summary>
                /// True, if you want to use Advanced Tools.  Otherwise, false
                /// </summary>
                public bool EnableContactFeatures;

                /// <summary>
                /// Sending permission setting for account
                /// </summary>
                public ApiTypes.SendingPermission SendingPermission;

            }

            /// <summary>
            /// A survey object
            /// </summary>
            public class Survey
            {
                /// <summary>
                /// Survey identifier
                /// </summary>
                public Guid PublicSurveyID;

                /// <summary>
                /// Creation date.
                /// </summary>
                public DateTime DateCreated;

                /// <summary>
                /// Last change date
                /// </summary>
                public DateTime? DateUpdated;

                /// <summary>
                /// Filename
                /// </summary>
                public string Name;

                /// <summary>
                /// Activate, delete, or pause your survey
                /// </summary>
                public ApiTypes.SurveyStatus Status;

                /// <summary>
                /// Number of results count
                /// </summary>
                public int ResultCount;

                /// <summary>
                /// Survey's steps info
                /// </summary>
                public List<ApiTypes.SurveyStep> SurveyStep;

                /// <summary>
                /// URL of the survey
                /// </summary>
                public string SurveyLink;

            }

            /// <summary>
            /// Object with the single answer's data
            /// </summary>
            public class SurveyResultAnswerInfo
            {
                /// <summary>
                /// Answer's content
                /// </summary>
                public string content;

                /// <summary>
                /// Identifier of the step
                /// </summary>
                public int surveystepid;

                /// <summary>
                /// Identifier of the answer of the step
                /// </summary>
                public string surveystepanswerid;

            }

            /// <summary>
            /// Single answer's data with user's specific info
            /// </summary>
            public class SurveyResultInfo
            {
                /// <summary>
                /// Identifier of the result
                /// </summary>
                public string SurveyResultID;

                /// <summary>
                /// IP address
                /// </summary>
                public string CreatedFromIP;

                /// <summary>
                /// Completion date
                /// </summary>
                public DateTime DateCompleted;

                /// <summary>
                /// Start date
                /// </summary>
                public DateTime DateStart;

                /// <summary>
                /// Answers for the survey
                /// </summary>
                public List<ApiTypes.SurveyResultAnswerInfo> SurveyResultAnswers;

            }

            /// <summary>
            /// Summary with all the answers
            /// </summary>
            public class SurveyResultsSummary
            {
                /// <summary>
                /// Answers' statistics
                /// </summary>
                public Dictionary<string, int> Answers;

                /// <summary>
                /// Open answers for the question
                /// </summary>
                public List<string> OpenAnswers;

            }

            /// <summary>
            /// Data on the survey's result
            /// </summary>
            public class SurveyResultsSummaryInfo
            {
                /// <summary>
                /// Number of items.
                /// </summary>
                public int Count;

                /// <summary>
                /// Summary statistics
                /// </summary>
                public Dictionary<int, ApiTypes.SurveyResultsSummary> Summary;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum SurveyStatus
            {
                /// <summary>
                /// The survey is deleted
                /// </summary>
                Deleted = -1,

                /// <summary>
                /// The survey is not receiving result for now
                /// </summary>
                Paused = 0,

                /// <summary>
                /// The survey is active and receiving answers
                /// </summary>
                Active = 1,

            }

            /// <summary>
            /// Survey's single step info with the answers
            /// </summary>
            public class SurveyStep
            {
                /// <summary>
                /// Identifier of the step
                /// </summary>
                public int SurveyStepID;

                /// <summary>
                /// Type of the step
                /// </summary>
                public ApiTypes.SurveyStepType SurveyStepType;

                /// <summary>
                /// Type of the question
                /// </summary>
                public ApiTypes.QuestionType QuestionType;

                /// <summary>
                /// Answer's content
                /// </summary>
                public string Content;

                /// <summary>
                /// Is the answer required
                /// </summary>
                public bool Required;

                /// <summary>
                /// Sequence of the answers
                /// </summary>
                public int Sequence;

                /// <summary>
                /// Answer object of the step
                /// </summary>
                public List<ApiTypes.SurveyStepAnswer> SurveyStepAnswer;

            }

            /// <summary>
            /// Single step's answer object
            /// </summary>
            public class SurveyStepAnswer
            {
                /// <summary>
                /// Identifier of the answer of the step
                /// </summary>
                public string SurveyStepAnswerID;

                /// <summary>
                /// Answer's content
                /// </summary>
                public string Content;

                /// <summary>
                /// Sequence of the answers
                /// </summary>
                public int Sequence;

            }

            /// <summary>
            /// 
            /// </summary>
            public enum SurveyStepType
            {
                /// <summary>
                /// 
                /// </summary>
                PageBreak = 1,

                /// <summary>
                /// 
                /// </summary>
                Question = 2,

                /// <summary>
                /// 
                /// </summary>
                TextMedia = 3,

                /// <summary>
                /// 
                /// </summary>
                ConfirmationPage = 4,

                /// <summary>
                /// 
                /// </summary>
                ExpiredPage = 5,

            }

            /// <summary>
            /// Template
            /// </summary>
            public class Template
            {
                /// <summary>
                /// ID number of template.
                /// </summary>
                public int TemplateID;

                /// <summary>
                /// 0 for API connections
                /// </summary>
                public ApiTypes.TemplateType TemplateType;

                /// <summary>
                /// Filename
                /// </summary>
                public string Name;

                /// <summary>
                /// Date of creation in YYYY-MM-DDThh:ii:ss format
                /// </summary>
                public DateTime DateAdded;

                /// <summary>
                /// CSS style
                /// </summary>
                public string Css;

                /// <summary>
                /// Default subject of email.
                /// </summary>
                public string Subject;

                /// <summary>
                /// Default From: email address.
                /// </summary>
                public string FromEmail;

                /// <summary>
                /// Default From: name.
                /// </summary>
                public string FromName;

                /// <summary>
                /// HTML code of email (needs escaping).
                /// </summary>
                public string BodyHtml;

                /// <summary>
                /// Text body of email.
                /// </summary>
                public string BodyText;

                /// <summary>
                /// ID number of original template.
                /// </summary>
                public int OriginalTemplateID;

                /// <summary>
                /// Enum: 0 - private, 1 - public, 2 - mockup
                /// </summary>
                public ApiTypes.TemplateScope TemplateScope;

            }

            /// <summary>
            /// List of templates
            /// </summary>
            public class TemplateList
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public enum TemplateScope
            {
                /// <summary>
                /// Template is available for this account only.
                /// </summary>
                Private = 0,

                /// <summary>
                /// Template is available for this account and it's sub-accounts.
                /// </summary>
                Public = 1,

            }

            /// <summary>
            /// 
            /// </summary>
            public enum TemplateType
            {
                /// <summary>
                /// Template supports any valid HTML
                /// </summary>
                RawHTML = 0,

                /// <summary>
                /// Template is created and can only be modified in drag and drop editor
                /// </summary>
                DragDropEditor = 1,

            }

            /// <summary>
            /// Information about tracking link and its clicks.
            /// </summary>
            public class TrackedLink
            {
                /// <summary>
                /// URL clicked
                /// </summary>
                public string Link;

                /// <summary>
                /// Number of clicks
                /// </summary>
                public string Clicks;

                /// <summary>
                /// Percent of clicks
                /// </summary>
                public string Percent;

            }

            /// <summary>
            /// Account usage
            /// </summary>
            public class Usage
            {
            }


#pragma warning restore 0649

        }

        internal static ApiTypes.Account GetEmailCredits(string Uri, string Uid, String Pwd)
        {
            WebClient client = new CustomWebClient();
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", Pwd);
            byte[] apiResponse = client.UploadValues(Uri + "/account/load", values);
            ApiResponse<ApiTypes.Account> apiRet = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<ApiTypes.Account>>(Encoding.UTF8.GetString(apiResponse));
            if (!apiRet.success) throw new ApplicationException(apiRet.error);
            return apiRet.Data;
        }


        internal class CustomWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                return request;
            }
        }


        internal class ApiResponse<T>
        {
            public bool success = false;
            public string error = null;
            public T Data
            {
                get;
                set;
            }
        }

        public static int EmailCreditBalance(string Uid, string Pwd)
        {
            var emailGw = ConfigurationManager.AppSettings["EmailGateway"];
            var emailUid = Uid;
            var emailPwd = Pwd;

            int creditBalance = 0;
            try
            {
                ApiTypes.Account acct = Utility.GetEmailCredits(emailGw, Uid, Pwd);
                creditBalance = acct.EmailCredits;
            }
            catch (Exception ex)
            {
                creditBalance = 0;
            }
            return creditBalance;


        }

        #endregion Elatic Email Functions
    }
}
