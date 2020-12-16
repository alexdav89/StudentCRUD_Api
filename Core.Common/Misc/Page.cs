namespace Core.Common.Misc
{
    public class Page<T>
    {
        public T Result { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
    }
}
