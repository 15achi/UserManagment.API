using DataAL.Entities.Models;

namespace DataAL.Page
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool hasNext => CurrentPage < TotalPages;
        public PagedList(List<T> items, int count, int pagenumber, int pagesize)
        {
            TotalCount = count;
            PageSize = pagesize;
            CurrentPage = pagenumber;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }

        public void Returns(List<CountryDto> countryDtos)
        {
            throw new NotImplementedException();
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNamber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNamber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            return new PagedList<T>(items, count, pageNamber, pageSize);
        }
    }
}
