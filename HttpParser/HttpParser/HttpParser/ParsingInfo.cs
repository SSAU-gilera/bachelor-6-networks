
namespace HttpParser
{
    class ParsingInfo
    {

        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public string MinURL { get; set; }
        public string MaxURL { get; set; }
        public int PagesCount { get; set; }
        public int TotalSize { get; set; }
        public  string Status { get; set; }

        public ParsingInfo(
                            int minSize,
                            int maxSize,
                            string minURL,
                            string maxURL,
                            int pagesCount,
                            int totalSize,
                            string status)
        {
            MinSize = minSize;
            MaxSize = maxSize;
            MinURL = minURL;
            MaxURL = maxURL;
            PagesCount = pagesCount;
            TotalSize = totalSize;
            Status = status;
        }

        public ParsingInfo() { }
    }
}
