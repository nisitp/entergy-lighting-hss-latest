using Geocoding;
using Geocoding.Google;
using security_lighting_microsite.models.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace security_lighting_microsite.models
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string BestTimeToCall { get; set; }
        public string AccountType { get; set; }
        public string OpCo { get; set; }
        public string NewOrExisting { get; set; }
        public string FixtureRecommenderResult { get; set; }


        /// <summary>
        /// [Deprecated]
        /// This function is not currently being used since the form is now asking the users for this information
        /// </summary>
        public void GeocodeZipCode()
        {
            if (string.IsNullOrWhiteSpace(Zip))
            {
                throw new ArgumentNullException("A ZIP code must be provided in order to geocode the city/state.");
            }

            GoogleGeocoder geocoder = new GoogleGeocoder();

            try
            {
                IEnumerable<GoogleAddress> result = geocoder.Geocode(Zip);
                this.City = result.Where(x => x[GoogleAddressType.Locality] != null).Select(x => x[GoogleAddressType.Locality].ShortName).First();
                this.State = result.Where(x => x[GoogleAddressType.AdministrativeAreaLevel1] != null).Select(x => x[GoogleAddressType.AdministrativeAreaLevel1].ShortName).First();
            }
            catch
            {                
                this.City = "Unknown";
                this.State = "Unknown";
            }
        }

        public string Send()
        {
            var message = new MailMessage();

            message.From = new MailAddress("securitylighting@entergy.com", "New Security Lighting Lead");

            message.To.Add(Opco.GetOpcoEmailAddress(this.OpCo));
            // message.To.Add("george@hotsauceatl.com");
            message.Subject = string.Format("New Security Lighting lead from {0} {1}", this.FirstName, this.LastName);

            message.Body = string.Format(
              @"Name: {0} {1}
                  Email: {2}
                  Address: 
                  {3}
                  {4}, {5}, {6}
                  Phone: {7}
                  Best time to call: {8}
                  Account type: {9}
                  Project type: {11}
                  Fixture Recommender Result: {10}",
              FirstName,
              LastName,
              Email,
              Address,
              City,
              State,
              Zip,
              Phone,
              BestTimeToCall,
              AccountType,
              FixtureRecommenderResult,
              NewOrExisting
            );
            try
            {
                new SmtpClient().Send(message);
                return "success";
            }
            catch (Exception ex)
            {
                return "failure";
            }
        }
    }
}