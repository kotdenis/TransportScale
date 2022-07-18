﻿namespace TransportScale.Dto.Pagination
{
    public class JournalParameters
    {
        public JournalParameters()
        {
            
        }
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 0;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
