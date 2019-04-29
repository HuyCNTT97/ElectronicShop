using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Common
{
    [Serializable]
    public class UserLogin
    {
        private long userID;
        private string userAccount;

        public long UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public string UserAccount
        {
            get
            {
                return userAccount;
            }

            set
            {
                userAccount = value;
            }
        }
    }
}