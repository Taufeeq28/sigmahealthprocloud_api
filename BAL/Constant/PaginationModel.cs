
namespace BAL.Constant
{
    public class PaginationModel<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string? PagingDetails { get; set; }
        public string? ShowingDetails { get; set; }
        public List<T> ?Items { get; set; }
    }
}
