using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;
using System.Net;
using Newtonsoft.Json;


namespace YelpAPI
{

    public class PairOfLocTerm
    {
        string loc;
        string term;
        long user_id;
        string screenName;
        public string Loc { get { return this.loc; } set { this.loc = value; } }
        public string Term { get { return this.term; } set { this.term = value; } }
        public long UserId { get { return this.user_id; } set { this.user_id = value; } }
        public string ScreenName { get { return this.screenName; } set { this.screenName = value; } }
    }

    public static class SingleService
    {
        static string _consumerKey = "*************************";
        static string _consumerSecret = "*************************";
        static string _accessToken = "*************************";
        static string _accessTokenSecret = "*************************";
        static CoreTweet.Tokens tokens = null;

        static CoreTweet.Tokens getTokens()
        {
            if(tokens == null){
                tokens = CoreTweet.Tokens.Create(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
            }
            return tokens;
        }

        public static List<PairOfLocTerm> getOAuthAndReplies()
        {
            var tokens = SingleService.getTokens();   
            var home = tokens.Statuses.HomeTimeline();
            var mentions = tokens.Statuses.MentionsTimeline();
            List<PairOfLocTerm> replies = new List<PairOfLocTerm>();

            foreach(var m in mentions)
            {
                PairOfLocTerm val = new PairOfLocTerm();
                string[] vals =  m.Text.Split(' ');
                val.Loc = vals[1];
                val.Term = vals[2];
                val.ScreenName = m.InReplyToScreenName;
                if (m.InReplyToStatusId != null)
                {
                    val.UserId = (long)m.InReplyToStatusId;
                }
                replies.Add(val);
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
