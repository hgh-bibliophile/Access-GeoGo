﻿using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate.ObjectModel.Engine;
using System;
using System.Text.RegularExpressions;

namespace Access_GeoGo.Data
{
    public struct DBEntryParams
    {
        public string Vehicle { get; set; }
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class DBEntry
    {
        /// <summary>
        /// The db entry id
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// The db entry timestamp
        /// </summary>
        public readonly DateTime Timestamp;
        /// <summary>
        /// The vehicle/device name
        /// </summary>
        public readonly string Vehicle;
        /// <summary>
        /// Initializes a new instance of the <see cref="DBEntry"/> class.
        /// </summary>
        /// <param name="entry">The <see cref="DBEntryParams"/> object</param>
        public DBEntry(DBEntryParams entry)
        {
            Id = entry.Id;
            Timestamp = entry.Timestamp;
            Vehicle = Regex.Replace(entry.Vehicle, @"\s\d*", "");
        }
    }
    class DeviceEntry
    {
        public readonly DBEntry Entry;
        public readonly Device Device;
        public DeviceEntry(DBEntry entry, Device device)
        {
            Entry = entry;
            Device = device;
        }
    }
     public struct GeoGo_EntryData {
        public DBEntry Entry { get; set; }
        public Device Device { get; set; }
        public StatusData DeviceMileage { get; set; }
        public StatusData DeviceEngineHours { get; set; }
        public LogRecord DeviceLocation { get; set; }
    }
    class GeoGo_Entry
    {
        /// <summary>
        /// The unique ID of the db entry
        /// </summary>
        public readonly int EntryID;
        /// <summary>
        /// The date/timestamp of the entry & readings
        /// </summary>
        public readonly DateTime Timestamp;  //MUST BE EQUAL
        /// <summary>
        /// The name of the device/vehicle
        /// </summary>
        public readonly string DeviceName;
        /// <summary>
        /// The odometer reading
        /// </summary>
        public readonly string Miles;
        /// <summary>
        /// The engine hours reading
        /// </summary>
        public readonly string Hours;
        /// <summary>
        /// The latitude of the device location
        /// </summary>
        public readonly string Latitude;
        /// <summary>
        /// The longitude of the device location
        /// </summary>
        public readonly string Longitude;
        /// <summary>
        /// The location string with <see cref="Latitude"/> and <see cref="Longitude"/>
        /// </summary>
        public readonly string Location;
        public GeoGo_Entry(GeoGo_EntryData data)
        {
            EntryID = data.Entry.Id;
            Timestamp = data.Entry.Timestamp;
            DeviceName = data.Entry.Vehicle;
            Latitude = data.DeviceLocation.Latitude.ToString();
            Longitude = data.DeviceLocation.Longitude.ToString();
            Location = $"{Latitude}, {Longitude}";
            //Miles = data.DeviceMileage.Data.ToString();
            // Hours = data.DeviceEngineHours.Data.ToString();
            
            double miles = Convert.ToDouble(data.DeviceMileage.Data) / Convert.ToDouble(1609.34);
            decimal hours = Convert.ToDecimal(data.DeviceEngineHours.Data) / Convert.ToDecimal(3600);
            double rMiles = Math.Round(miles);
            decimal rHours = Math.Round(hours);
            //Miles = String.Format("{0:#,###0}", rMiles);
            Miles = rMiles.ToString();
            if (rMiles == 0)
                Miles = (-2).ToString();
            Hours = String.Format("{0:#,###0}", rHours);
        }
    }
}
