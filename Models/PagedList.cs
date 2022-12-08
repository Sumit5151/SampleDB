namespace SampleDB.Models
{
    public class PagedList
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling(TotalCount / (double)PageSize); }
        }

    }
}
