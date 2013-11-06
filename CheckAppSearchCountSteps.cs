using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Apps_Gallery_Record_Count
{
    [Binding]
    public class CheckAppSearchCountSteps
    {
        private string _theUrl;
        private string _theResponse;


        [Given(@"alteryx is running at ""(.*)""")]
        public void GivenAlteryxIsRunningAt(string alteryxUrl)
        {
            _theUrl = alteryxUrl;
        }

        [When(@"I search for application at ""(.*)"" with search term ""(.*)""")]
        public void WhenISearchForApplicationAtWithSearchTerm(string apiurl, string searchterm)
        {
            string search = Regex.Replace(searchterm, @"\s+", "+");
            string Url = _theUrl + "/" + apiurl + "?search=" + search;
            WebRequest webRequest = System.Net.WebRequest.Create(Url);
            WebResponse response = webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new System.IO.StreamReader(responseStream);
            string responseFromServer = reader.ReadToEnd();
            _theResponse = responseFromServer;

        }

        [Then(@"I see record-count is (.*)")]
        public void ThenISeeRecord_CountIs(int expectedcount)
        {
            var dict = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(_theResponse);
            int count = dict["recordCount"];
            Assert.IsTrue(count == expectedcount);
        }
    }
}


