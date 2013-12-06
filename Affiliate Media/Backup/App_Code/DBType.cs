using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AffiliateMedia
{
    public class DBType
    {
        private static string _main = "main";
        private static string _archive = "archive";
        private static string _onUser = "on_user";
        private static string _wareHouse = "warehouse";
        private static string _deposit = "deposit";
        private static string _replicate = "replicate";
        private static string _affiliate_12bet = "affiliate_12bet";

        public static string MAIN
        {
            get
            {
                return _main;
            }
        }

        //public static string ARCHIVE
        //{
        //    get
        //    {
        //        return _archive;
        //    }
        //}

        public static string AFFILIATE_12BET
        {
            get
            {
                return _affiliate_12bet;
            }
        }
        public static string ON_USER
        {
            get
            {
                return _onUser;
            }
        }

        //public static string WAREHOUSE
        //{
        //    get
        //    {
        //        return _wareHouse;
        //    }
        //}

        public static string DEPOSIT
        {
            get
            {
                return _deposit;
            }
        }
        public static string REPLICATE
        {
            get
            {
                return _replicate;
            }
        }
    }
}
