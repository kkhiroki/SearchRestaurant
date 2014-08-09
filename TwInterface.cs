using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;
using System.Net;
using Newtonsoft.Json;


namespace YelpAPI
{

    public static class SingleService
    {
        static string _consumerKey = "";
        static string _consumerSecret = "";
        static string _accessToken = "";
        static string _accessTokenSecret = "";
        static CoreTweet.Tokens tokens = null;

        static CoreTweet.Tokens getTokens()
        {
            if(tokens == null){
                tokens = CoreTweet.Tokens.Create(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
            }
            return tokens;
        }

        public static List<Status> getOAuthAndReplies()
        {
            var tokens = SingleService.getTokens();   
            var home = tokens.Statuses.HomeTimeline();
            var mentions = tokens.Statuses.MentionsTimeline();
            List<Status> replies = new List<Status>();
            
            foreach(var m in mentions)
            {
                replies.Add(m);
            }
            return replies;
        }

        public static void ReplyTo(string screenName, string text)
        {
            var tokens = SingleService.getTokens();
            Dictionary<string,object> d = new Dictionary<string, object>();

            d.Add("status","@"+screenName+" "+text);
            
            tokens.Statuses.Update(d);

        }
    }
}
