using FinanceManager.Filters;
using System;

namespace FinanceManager.Interfaces
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
