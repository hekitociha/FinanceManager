using Microsoft.EntityFrameworkCore;
using FinanceManager.Models;
using System;
using System.Linq;

namespace FinanceManager.Services
{
    public class FiltrationServiceInc
    {
        public static IQueryable<Income> filtration(AppDBContent context, string searchstr, decimal? priceLow, decimal? priceHigh, DateTime? minDate, DateTime? maxDate)
        {
            var incomes = from t in context.Incomes.Include(i => i.User)
                          select t;
            priceLow = priceLow == null ? context.Incomes.Min(i => i.Amount) : priceLow;
            priceHigh = priceHigh == null ? context.Incomes.Max(i => i.Amount) : priceHigh;
            minDate = minDate == null ? context.Incomes.Min(i => i.Date) : minDate;
            maxDate = maxDate == null ? context.Incomes.Max(i => i.Date) : maxDate;
            if (!String.IsNullOrEmpty(searchstr)) { incomes = incomes.Where(i => i.FromWhere.ToUpper().Contains(searchstr.ToUpper())); }
            if (priceLow != null || priceHigh != null) { incomes = incomes.Where(i => (i.Amount <= priceHigh) && (i.Amount >= priceLow)); }
            if (minDate != null || maxDate != null) { incomes = incomes.Where(i => (i.Date >= minDate) && (i.Date <= maxDate)); }
            return incomes;
        }
    }
}
