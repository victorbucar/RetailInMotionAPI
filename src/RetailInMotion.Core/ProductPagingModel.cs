﻿using RetailInMotion.Core.Entities;
using System.Collections.Generic;

namespace RetailInMotion.Core
{
    public class ProductPagingModel
    {
        const int maxPageSize = 20;  
  
        public int pageNumber { get; set; } = 1;  
  
        public int _pageSize { get; set; } = 10;  
  
        public int pageSize  
        {  
  
            get { return _pageSize; }  
            set  
            {  
                _pageSize = (value > maxPageSize) ? maxPageSize : value;  
            }  
        }  
    }
}