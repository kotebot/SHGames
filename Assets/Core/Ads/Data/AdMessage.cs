namespace Core.Ads.Data
{
    public struct AdMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }

        public AdMessage(string id, string message, Result result)
        {
            Id = id;
            Message = message;
            Result = result;
        }
    }
}