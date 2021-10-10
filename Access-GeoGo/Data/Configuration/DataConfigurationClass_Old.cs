using System.Collections.Generic;
using System.Configuration;

namespace Access_GeoGo.Data.Configuration.Old
{
    public class GeoGo_UserConfig
    {
        public string User { get; set; }
        public string Database { get; set; }
        public GeotabConfig Geotab { get; set; }
        public class GeotabConfig
        {
            public Dictionary<string, string> Auth { get; set; }
            public Dictionary<string, string> DataFeeds { get; set; }
        }          
    }
    



    public class GeoGo_DataFeeds : ConfigurationSection
    {
        /*
        [ConfigurationProperty("Databases")]
        [ConfigurationCollection(typeof(DatabaseCollection), AddItemName = "Database")]
        public DatabaseCollection Databases
        {
            get
            {
                return (DatabaseCollection)this["Databases"];
            }
        }
        [ConfigurationProperty("DataFeeds")]
        [ConfigurationCollection(typeof(DataFeedCollection), AddItemName = "DataFeed")]
        public DataFeedCollection DataFeeds
        {
            get
            {
                return (DataFeedCollection)this["DataFeeds"];
            }
        }*/
        [ConfigurationProperty("Users")]
        [ConfigurationCollection(typeof(UserCollection), AddItemName = "User")]
        public UserCollection User
        {
            get
            {
                return (UserCollection)this["Users"];
            }
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    /*
    public class DatabaseCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("currentUser", DefaultValue = "Daddy")]
        public string CurrentUser
        {
            get
            {
                return (string)base["currentUser"];
            }
            set
            {
                base["currentUser"] = value;
            }
        }
        public DatabaseElement this[int index]
        {
            get
            {
                return (DatabaseElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        public new DatabaseElement this[string key]
        {
            get
            {
                return (DatabaseElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));
                BaseAdd(value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseElement)element).User;
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    public class DatabaseElement : ConfigurationElement
    {
        [ConfigurationProperty("user", IsRequired = true, IsKey = true)]
        public string User
        {
            get
            {
                return (string)base["user"];
            }
            set
            {
                base["user"] = value;
            }
        }
        [ConfigurationProperty("file", IsRequired = true)]
        public string File
        {
            get
            {
                return (string)base["file"];
            }
            set
            {
                base["file"] = value;
            }
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    
    */
    public class DataFeedCollection : ConfigurationElementCollection
    {
        public DataFeedElement this[int index]
        {
            get
            {
                return (DataFeedElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        public new DataFeedElement this[string key]
        {
            get
            {
                return (DataFeedElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));
                BaseAdd(value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new DataFeedElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataFeedElement)element).Feed;
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    public class DataFeedElement : ConfigurationElement
    {
        [ConfigurationProperty("feed", IsRequired = true, IsKey = true)]
        public string Feed
        {
            get
            {
                return (string)base["feed"];
            }
            set
            {
                base["feed"] = value;
            }
        }
        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get
            {
                return (string)base["token"];
            }
            set
            {
                base["token"] = value;
            }
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    public class UserCollection : ConfigurationElementCollection
    {





        public UserElement this[int index]
        {
            get
            {
                return (UserElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }
        public new UserElement this[string key]
        {
            get
            {
                return (UserElement)BaseGet(key);
            }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));
                BaseAdd(value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new UserElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserElement)element).Name;
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    public class UserElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
        public override bool IsReadOnly()
        {
            return false;
        }
    }
}