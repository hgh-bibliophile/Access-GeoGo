using System.Configuration;

namespace Access_GeoGo.Data.Configuration
{
    public class GeoGo_UserConfig
    {
        public string Name { get; set; }
        public string Database { get; set; }
        public GeotabAuthConfig Geotab_Auth { get; set; } = new GeotabAuthConfig();
        public DBConfig DB_Config { get; set; } = new DBConfig();

        public class GeotabAuthConfig
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Database { get; set; }
        }

        public class DBConfig
        {
            public string Table { get; set; }
            public string GTStatus_Default { get; set; } = "New";
            public DBColumnsConfig Columns { get; set; } = new DBColumnsConfig();

            public class DBColumnsConfig
            {
                public string Id { get; set; }
                public string GTStatus { get; set; }
                public string Time { get; set; }
                public string Vehicle { get; set; }
                public string Odometer { get; set; }
                public string EngineHrs { get; set; }
                public string Latitude { get; set; }
                public string Longitude { get; set; }
                public string Driver { get; set; }
            }
        }
    }

    public class GeoGo_UserFeeds : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public UserCollection Users => (UserCollection)base[""];
    }

    public class UserCollection : ConfigurationElementCollection
    {
        public UserCollection()
        {
            UserConfigElement details = (UserConfigElement)CreateNewElement();
            if (details.Name != "")
                Add(details);
        }

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override ConfigurationElement CreateNewElement() => new UserConfigElement();

        protected override object GetElementKey(ConfigurationElement element) => ((UserConfigElement)element).Name;

        public UserConfigElement this[int index]
        {
            get => (UserConfigElement)BaseGet(index);
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public UserConfigElement this[string name] => (UserConfigElement)BaseGet(name);

        public int IndexOf(UserConfigElement details) => BaseIndexOf(details);

        public void Add(UserConfigElement details) => BaseAdd(details);

        protected override void BaseAdd(ConfigurationElement element) => BaseAdd(element, false);

        public void Remove(UserConfigElement details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Name);
        }

        public void RemoveAt(int index) => BaseRemoveAt(index);

        public void Remove(string name) => BaseRemove(name);

        public void Clear() => BaseClear();

        protected override string ElementName => "User";
    }

    public class UserConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }

        [ConfigurationProperty("DataFeeds", IsDefaultCollection = false)]
        public DataFeedCollection DataFeeds => (DataFeedCollection)base["DataFeeds"];
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

        public DataFeedConfigElement this[int index] => (DataFeedConfigElement)BaseGet(index);

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < Count; idx++)
            {
                if (this[idx].Feed.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override ConfigurationElement CreateNewElement() => new DataFeedConfigElement();

        protected override object GetElementKey(ConfigurationElement element) => ((DataFeedConfigElement)element).Feed;

        protected override string ElementName => "DataFeed";
    }

    public class DataFeedConfigElement : ConfigurationElement
    {
        public DataFeedConfigElement()
        {
        }

        public DataFeedConfigElement(string feed, string token)
        {
            Feed = feed;
            Token = token;
        }

        [ConfigurationProperty("feed", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Feed
        {
            get => (string)this["feed"];
            set => this["feed"] = value;
        }

        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get => (string)this["token"];
            set => this["token"] = value;
        }
    }
}