using Geotab.Checkmate.ObjectModel;
using Geotab.Checkmate.ObjectModel.Engine;
using System;
using System.Text.RegularExpressions;

namespace Access_GeoGo.Data
{
    public struct DbEntryParams
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Vehicle { get; set; }
    }

    public class DbEntry
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
        /// Initializes a new instance of the <see cref="DbEntry"/> class.
        /// </summary>
        /// <param name="entry">The <see cref="DbEntryParams"/> object</param>
        public DbEntry(DbEntryParams entry)
        {
            Id = entry.Id;
            Timestamp = entry.Timestamp;
            Vehicle = Regex.Replace(entry.Vehicle, @"\s\d*", "");
        }
    }

    internal class DeviceEntry
    {
        public readonly DbEntry Entry;
        public readonly Device Device;

        public DeviceEntry(DbEntry entry, Device device)
        {
            Entry = entry;
            Device = device;
        }
    }

    public struct GeoGoEntryData
    {
        public DbEntry Entry { get; set; }
        public Device Device { get; set; }
        public StatusData DeviceMileage { get; set; }
        public StatusData DeviceEngineHours { get; set; }
        public LogRecord DeviceLocation { get; set; }
        public string Driver { get; set; }
    }

    internal class GeoGoEntry
    {
        /// <summary>
        /// The unique ID of the db entry
        /// </summary>
        public readonly int EntryId;

        /// <summary>
        /// The Geotab status of the db entry
        /// </summary>
        public readonly string GtStatus;

        /// <summary>
        /// The date/timestamp of the entry & readings
        /// </summary>
        public readonly DateTime Timestamp;  //? ?? MUST BE EQUAL ??

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

        /// <summary>
        /// The current driver of the device at the date/timestamp of the entry
        /// </summary>
        public readonly string Driver;
        
        public GeoGoEntry(GeoGoEntryData data)
        {
            EntryId = data.Entry.Id;
            Timestamp = data.Entry.Timestamp;
            DeviceName = data.Entry.Vehicle;
            Driver = data.Driver;
            Latitude = data.DeviceLocation.Latitude.ToString();
            Longitude = data.DeviceLocation.Longitude.ToString();
            Location = $"{Latitude}, {Longitude}";

            double miles = Convert.ToDouble(data.DeviceMileage.Data) / Convert.ToDouble(1609.344);
            decimal hours = Convert.ToDecimal(data.DeviceEngineHours.Data) / Convert.ToDecimal(3600);
            double rMiles = Math.Round(miles);
            decimal rHours = Math.Round(hours);
            Miles = rMiles == 0 ? null : rMiles.ToString();
            Hours = rHours == 0 ? null : rHours.ToString();
            GtStatus = rMiles == 0 ? "GTDeviceNoOdo" : "GTOdometer";
        }
    }
}