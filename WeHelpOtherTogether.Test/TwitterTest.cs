using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using WeHelpOtherTogether.Web.Services;

namespace WeHelpOtherTogether.Test
{
    
    [TestClass]
    public class TwitterTest
    {
        string      ConsumerKey = "twjZs5MZyL3zV4la0WsVUYl0l",                               //"YOUR-CONSUMER-KEY", //HLQtlFfFYR9FkX6CIa667V5Ii
                    ConsumerKeySecret = "sfILzDXzeGqXcFvjyeTqpciOOnYCE2l3daeOEMw1biSeG8LrN5",       //"YOUR-CONSUMER-KEY-SECRET", //BDfzgpeXmQWcVLVfxkSGAuB8AqfvW7LwskDo1fDJzQhZtJ3It7
                    AccessToken = "1386909265458122756-i6W9LEzBPOjmllPGSLQ0TEUbknVDd7",              //"YOUR-ACCESS-TOKEN", //1386909265458122756-Pm3wDpMV7zpZdA1gFKUL6htGONI9kj
                    AccessTokenSecret = "Cg1sDbtn6XxONw4y4baRcPncQgge23ZMlrN3fVu8bWkC7";             //"YOUR-ACCESS-TOKEN-SECRET"; //JDBMIgaK23fW7l4HUpQFdopo7DdhWsCXVk01n6VffD82c

           //BRT - AAAAAAAAAAAAAAAAAAAAABuNPAEAAAAA%2BlETwGpwc%2F7Rsw2MRcUtwiWtJUs%3DeRBzLqXrn6TcsCgadtCuNUToECeXHjAGtjTqqUKRQMggB1lEYK



        [TestMethod]
        public void TestPublishToTwitterWithImage()
        {
            var twitter = new Twitter(ConsumerKey, ConsumerKeySecret, AccessToken, AccessTokenSecret);
            string response = twitter.PublishToTwitter("Text With Image", "/path/to/some/image.png");
            Console.WriteLine(response);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestPublishToTwitterTextOnly()
        {
            var twitter = new Twitter(ConsumerKey, ConsumerKeySecret, AccessToken, AccessTokenSecret);
            var rez = Task.Run(async () =>
            {
                var response = await twitter.TweetText("Text Only", string.Empty);
                return response;
            });

            Console.WriteLine(rez.Result.Item1);
            var rezJson = JObject.Parse(rez.Result.Item2);
            Console.WriteLine(rezJson);

            string error = rezJson["errors"] == null ? "OK" : rezJson["errors"][0]["message"].Value<string>();
            Console.WriteLine(error);

            var status = string.IsNullOrEmpty(rezJson["id_str"].ToString());

            Assert.IsFalse(status);
        }

    }
   
}
