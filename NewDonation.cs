using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityAssist2017MVC.Models
{
    public class NewDonation
    {
        public string DonationKey { set; get; }
        public string DonationDate { set; get; }
        public string DonationAmount { set; get; }
        public string DontionConfirmationCode { set; get; }

    }
}