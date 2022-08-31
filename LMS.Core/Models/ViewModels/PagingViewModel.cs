using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class PagingViewModel<T>
    {
        //[JsonPropertyName("curr_page")]
        public int CurrentPage { get; set; }
        //[JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        //[JsonPropertyName("page_size")]
        public int PageSize { get; set; }
        //[JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        //
        public IEnumerable<T> Items { get; set; }
        //

        public int NumberOfUnreadNotification { get; set; }

        public PagingViewModel(IEnumerable<T> items, int count, int currentPage, int pageSize,
            int numberOfUnreadNotification = 0)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
            NumberOfUnreadNotification = numberOfUnreadNotification;
        }
    }
}
