namespace CallForHelp.DTO
{
    public class Request
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string RequestorId { get; set; }
        public string Name { get; set; }
        public string RegId { get; set; }
    }
}