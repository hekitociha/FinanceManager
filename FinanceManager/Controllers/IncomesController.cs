using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManager;
using FinanceManager.Models;
using FinanceManager.Filters;
using FinanceManager.Services;
using FinanceManager.Helpers;
using FinanceManager.Interfaces;

namespace FinanceManager.Controllers
{
    [Route("api/incomes")]
    public class IncomesController : ControllerBase
    {
        private readonly AppDBContent _context;
        private IUriService uriService;

        public IncomesController(AppDBContent context)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Incomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetExpenses([FromQuery] PaginationFilter filter, string searchstr, decimal? priceLow, decimal? priceHigh, DateTime? minDate, DateTime? maxDate)
        {
            var incomes = FiltrationServiceInc.filtration(_context, searchstr, priceLow, priceHigh, minDate, maxDate);
            var route = Request.Path.Value;
            var incomesCount = incomes.ToList();
            incomes = incomes.Include(e => e.User)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
            var incomesList = incomes.ToList();
            var totalRecords = incomesCount.Count();
            var pagedResponse = PaginationHelper.CreatePagedReponse<Income>(incomesList, filter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        // PUT: api/Incomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
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

        // POST: api/Incomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route ("new")]
        [HttpPost]
        public async Task<ActionResult<Expense>> PostIncome(Income income)
        {
          if (_context.Incomes == null)
          {
              return Problem("Entity set 'AppDBContent.Incomes'  is null.");
          }
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.Id }, income);
        }

        // DELETE: api/Incomes/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            if (_context.Incomes == null)
            {
                return NotFound();
            }
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return (_context.Incomes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
