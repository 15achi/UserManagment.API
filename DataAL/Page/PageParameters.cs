

namespace DataAL.Page
{
    public abstract class PageParameters
    {
        const int maxPageSize = 20;
        private int _PageNamber = 1;
        private int _PageSize = 3;

        public int PageNamber
        {
            get
            {
                return _PageNamber;
            }
            set
            {
                _PageNamber = value;
            }
        }
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
