using Newtonsoft.Json;

namespace FlightTracker.Models
{
    public class FlightResults
    {
        [JsonProperty("Ac")]
        public List<FlightInfo> NearbyPlanes { get; set; }
        public long? Total { get; set; }
        public long? Ctime { get; set; }
        public long? Distmax { get; set; }
        public long? Ptime { get; set; }
    }

    public class FlightInfo
    {
        public string Postime { get; set; }
        [JsonProperty("Icao")]
        public string ID { get; set; }
        public string Reg { get; set; }
        public string Type { get; set; }
        public long? Wtc { get; set; }
        public string Spd { get; set; }
        public long? Altt { get; set; }
        public string Alt { get; set; }
        public string Galt { get; set; }
        public string Talt { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public long? Vsit { get; set; }
        public string Vsi { get; set; }
        public long? Trkh { get; set; }
        public string Ttrk { get; set; }
        public string Trak { get; set; }
        public string Sqk { get; set; }
        public string Call { get; set; }
        public long? Gnd { get; set; }
        public long? Trt { get; set; }
        public long? Pos { get; set; }
        public long? Mlat { get; set; }
        public long? Tisb { get; set; }
        public long? Sat { get; set; }
        [JsonProperty("Opicao")]
        public string Operator { get; set; }
        public string Cou { get; set; }
        public long? Mil { get; set; }
        public long? Interested { get; set; }
        public string Dst { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
