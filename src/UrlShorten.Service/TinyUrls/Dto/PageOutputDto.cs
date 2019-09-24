using System;
using System.Collections.Generic;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class PageOutputDto<T> where T: class
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }


        public PageOutputDto(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }


        public int GetPageSize(int take)
        {
            return (int) Math.Ceiling(decimal.Divide(TotalCount, take));
        }
    }
}