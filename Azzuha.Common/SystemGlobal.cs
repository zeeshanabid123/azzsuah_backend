using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Azzuha.Common
{
    public class SystemGlobal
    {
        public static bool isMethodInUse = false;
        public static bool isPaymentMethodInUse = false;

        public static Guid GetId()
        {
            return Guid.NewGuid();
        }

        public static string GetRandomNumber6Digit()
        {
            return new Random().Next(0, 999999).ToString("D6");
        }
        public decimal DiffrenceInMunites(DateTime startTime, DateTime endTime)
        {
            return Convert.ToDecimal(endTime.Subtract(startTime).TotalMinutes);
        }
    }
}
