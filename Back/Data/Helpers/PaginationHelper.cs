﻿using FinanceManager.Filters;
using FinanceManager.Interfaces;
using FinanceManager.Wrappers;
using FinanceManager.Models;
using System;
using System.Collections.Generic;

namespace FinanceManager.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(IEnumerable<T> pagedData, PaginationFilter filter, int totalRecords, IUriService uriService, string route)
        {
            PagedResponse<IEnumerable<T>> respose = new PagedResponse<IEnumerable<T>>(pagedData, filter.PageNumber, filter.PageSize);
            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), route)
                : null;
            respose.PreviousPage =
                filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
