using System.Collections.Generic;
using System.Configuration;


namespace Access_GeoGo.Data.Configuration {

    public class GeoGo_UserConfig
    {
        public string Name { get; set; } = null;
        public string Database { get; set; } = null;
        public GeotabAuthConfig Geotab_Auth { get; set; } = new GeotabAuthConfig();
        public DBConfig DB_Config { get; set; } = new DBConfig();
        public class GeotabAuthConfig
        {
            public string Username { get; set; } = null;
            public string Password { get; set; } = null;
            public string Database { get; set; } = null;
        }
        public class DBConfig
        {
            public string Table { get; set; } = null;
            public string GTStatus_Default { get; set; } = "New";
            public DBColumnsConfig Columns { get; set; } = new DBColumnsConfig();
            public class DBColumnsConfig
            {
                public string Id { get; set; } = null;
                public string GTStatus { get; set; } = null;
                public string Time { get; set; } = null;
                public string Vehicle { get; set; } = null;
                public string Odometer { get; set; } = null;
                public string EngineHrs { get; set; } = null;
                public string Latitude { get; set; } = null;
                public string Longitude { get; set; } = null;
                public string Driver { get; set; } = null;
            }
        }
    }
    public class GeoGo_UserFeeds : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public UserCollection Users
        {
            get
            {
                UserCollection userCollection = (UserCollection)base[""];
                return userCollection;
            }
        }
    }

    public class UserCollection : ConfigurationElementCollection
    {
        public UserCollection()
        {
            UserConfigElement details = (UserConfigElement)CreateNewElement();
            if (details.Name != "")
            {
                Add(details);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new UserConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserConfigElement)element).Name;
        }

        public UserConfigElement this[int index]
        {
            get
            {
                return (UserConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public UserConfigElement this[string name]
        {
            get
            {
                return (UserConfigElement)BaseGet(name);
            }
        }

        public int IndexOf(UserConfigElement details)
        {
            return BaseIndexOf(details);
        }

        public void Add(UserConfigElement details)
        {
            BaseAdd(details);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(UserConfigElement details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override string ElementName
        {
            get { return "User"; }
        }
    }

    public class UserConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("DataFeeds", IsDefaultCollection = false)]
        public DataFeedCollection DataFeeds
        {
            get { return (DataFeedCollection)base["DataFeeds"]; }
        }
    }

    public class DataFeedCollection : ConfigurationElementCollection
    {
        public new DataFeedConfigElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (DataFeedConfigElement)BaseGet(name);
            }
        }

        public DataFeedConfigElement this[int index]
        {
            get { return (DataFeedConfigElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Feed.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DataFeedConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataFeedConfigElement)element).Feed;
        }

        protected override string ElementName
        {
            get { return "DataFeed"; }
        }
    }
    public class DataFeedConfigElement : ConfigurationElement
    {
        public DataFeedConfigElement()
        {
        }

        public DataFeedConfigElement(string feed, string token)
        {
            this.Feed = feed;
            this.Token = token;
        }

        [ConfigurationProperty("feed", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Feed
        {
            get { return (string)this["feed"]; }
            set { this["feed"] = value; }
        }

        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get { return (string)this["token"]; }
            set { this["token"] = value; }
        }
    }
}