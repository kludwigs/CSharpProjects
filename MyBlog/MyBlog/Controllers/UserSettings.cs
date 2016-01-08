using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MyBlog.Controllers
{
    public class UserApp
    {
        private static UserApp _instance = null;
        private int _NumPosts = -1;
        private UserSettings _userSettings;

        private UserSettings UserSettings
        {
            get
            {
                return _userSettings ?? (_userSettings = (UserSettings)ConfigurationManager.GetSection("UserSettings")
                    ?? new UserSettings
                    {
                        NumPosts = new UserSettings.PostsPerPage { number = 4 }
                    });
            }
        }

        public static UserApp Instance
        {
            get { return _instance ?? (_instance = new UserApp()); }
        }

        public int NumPosts
        {
            get
            {
                if (_NumPosts == -1)
                    _NumPosts = UserSettings.NumPosts.number;
                return _NumPosts;
            }
            set { _NumPosts = value; }
        }


    }


    public class UserSettings : ConfigurationSection
    {
        #region fields

        private PostsPerPage _PostsPerPage;

        #endregion

        #region properties

        [ConfigurationProperty("PostsPerPage")]
        public PostsPerPage NumPosts
        {
            get
            {
                return _PostsPerPage ?? (_PostsPerPage
                    = (PostsPerPage)this["PostsPerPage"]);
            }
            set { _PostsPerPage = value; }
        }

        #endregion

        public class PostsPerPage : ConfigurationElement
        {

            [ConfigurationProperty("number", DefaultValue = "4")]
            public int number
            {
                get { return (int)this["number"]; }
                set { this["number"] = number; }
            }
        }
    }

}