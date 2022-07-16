using TransportScale.Dto.Pagination;

namespace TransportScale.Client.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public Metadata MetaData { get; set; }
    }
}
