using Microsoft.EntityFrameworkCore;
using FinanceManager.Models;
using System;
using System.Linq;

namespace FinanceManager.Services
{
    public class FiltrationServiceExp
    {
        public static IQueryable<Expense> filtration(AppDBContent context, string searchstr, decimal? priceLow, decimal? priceHigh, DateTime? minDate, DateTime? maxDate)
        {
            var expenses = from t in context.Expenses.Include(i => i.User)
                           select t;
            priceLow = priceLow == null ? context.Expenses.Min(i => i.Amount) : priceLow;
            priceHigh = priceHigh == null ? context.Expenses.Max(i => i.Amount) : priceHigh;
            minDate = minDate == null ? context.Expenses.Min(i => i.Date) : minDate;
            maxDate = maxDate == null ? context.Expenses.Max(i => i.Date) : maxDate;
            if (!String.IsNullOrEmpty(searchstr)) { expenses = expenses.Where(i => i.FromWhere.ToUpper().Contains(searchstr.ToUpper())); }
            if (priceLow != null || priceHigh != null) { expenses = expenses.Where(i => (i.Amount <= priceHigh) && (i.Amount >= priceLow)); }
            if (minDate != null || maxDate != null) { expenses = expenses.Where(i => (i.Date >= minDate) && (i.Date <= maxDate)); }
            return expenses;
        }
    }
}
