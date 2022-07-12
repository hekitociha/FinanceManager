using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManager;
using FinanceManager.Models;
using FinanceManager.Interfaces;
using FinanceManager.Filters;
using FinanceManager.Services;
using FinanceManager.Helpers;

namespace FinanceManager.Controllers
{
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDBContent _context;
        private IUriService uriService;

        public ExpensesController(AppDBContent context)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses([FromQuery] PaginationFilter filter, string searchstr, decimal? priceLow, decimal? priceHigh, DateTime? minDate, DateTime? maxDate)
        {
            var expenses = FiltrationServiceExp.filtration(_context, searchstr, priceLow, priceHigh, minDate, maxDate);
            var route = Request.Path.Value;
            var expensesCount = expenses.ToList();
            expenses = expenses.Include(e => e.User)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            var expensesList = expenses.ToList();
            var totalRecords = expensesCount.Count();
            var pagedResponse = PaginationHelper.CreatePagedReponse<Expense>(expensesList, filter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }


        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route ("new")]
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
          if (_context.Expenses == null)
          {
              return Problem("Entity set 'AppDBContent.Expenses'  is null.");
          }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (_context.Expenses == null)
            {
                return NotFound();
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return (_context.Expenses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
