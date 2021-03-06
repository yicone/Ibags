﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace Ibags.API.App_Start
{
    public class Token
    {
        public Token(string accountId, string fromIP)
        {
            AccountId = accountId;
            IP = fromIP;
        }

        public string AccountId { get; private set; }
        public string IP { get; private set; }

        public string Encrypt()
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=WebAPI-Token");
            return cryptographyHelper.Encrypt(certificate, this.ToString());
        }

        public override string ToString()
        {
            return String.Format("AccountId={0};IP={1}", this.AccountId, this.IP);
        }

        public static Token Decrypt(string encryptedToken)
        {
            CryptographyHelper cryptographyHelper = new CryptographyHelper();
            X509Certificate2 certificate = cryptographyHelper.GetX509Certificate("CN=WebAPI-Token");
            string decrypted = cryptographyHelper.Decrypt(certificate, encryptedToken);

            //Splitting it to dictionary
            Dictionary<string, string> dictionary = decrypted.ToDictionary();
            return new Token(dictionary["AccountId"], dictionary["IP"]);
        }
    }
}