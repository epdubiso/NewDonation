using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunityAssist2017MVC.Models;

namespace CommunityAssist2017MVC.Controllers
{
    public class NewDonationController : Controller
    {
        CommunityAssist2017Entities db = new CommunityAssist2017Entities();
        // GET: NewDonation
        public ActionResult Index()
        {
            if (Session["personkey"] == null)
            {
                Message m = new Message();
                m.MessageText = "Must be logged in to Donate";
                return RedirectToAction("Result", m);


            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "DonationKey, DonationDate, DonationAmount, DontionConfirmationCode")]NewDonation nd)
        {

            CommunityAssist2017Entities db = new CommunityAssist2017Entities();


            int loginResult = db.Donations(nd.DonationAmount, nd.DonationDate, nd.DontionConfirmationCode);
            if (loginResult != -1)
            {
                var uid = (from np in db.People
                           where np.PersonEmail.Equals(nd.do)
                           select np.PersonKey).FirstOrDefault();
                int npKey = (int)uid;
                Session["personkey"] = npKey;

                Message msg = new Message();
                msg.MessageText = "Thank you," + lc.UserName +
                  "for logging in. You can now" +
                    "donate or apply for assistance";
                

                Donation d = new Donation();
            d.DonationConfirmationCode = Guid.NewGuid();
            db.Donations.Add(d);
            db.SaveChanges();


            Message m = new Message();
            m.MessageText = "Thank you, the donation has been added";

            return View("Result", m);

        }

            public ActionResult Result(Message m)

        {

            return View(m);
        }

    }
}