using microservices_monthly_summary.Database;
using microservices_monthly_summary.Models;
using Microsoft.EntityFrameworkCore;

namespace microservices_monthly_summary.Services
{

    public class MonthlySummaryService
    {
        private readonly DBContext _context;

        public MonthlySummaryService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlySummary>> GetAllAsync()
        {
            return await _context.MonthlySummary.Where(x => x.Status == true).ToListAsync();
        }

        public async Task<MonthlySummary> GetByCodeAsync(string codice)
        {
            return await _context.MonthlySummary.FirstOrDefaultAsync(order => order.SummaryCode == codice && order.Status == true);
        }

        public async Task<MonthlySummary> CreateAsync(MonthlySummary model)
        {
            model.Status = true;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            var createOrder = _context.MonthlySummary.Add(model);
            await _context.SaveChangesAsync();
            var id = createOrder.Entity.SummaryId;

            var result = await _context.MonthlySummary.FirstOrDefaultAsync(x => x.SummaryId == id);

            return result;
        }

        public async Task<MonthlySummary> UpdateAsync(string codice, MonthlySummary model)
        {
            var result = await _context.MonthlySummary.FirstOrDefaultAsync(x => x.SummaryCode == codice);

            if (result == null)
            {
                return null;
            }

            result.UpdatedAt = DateTime.Now;
            result.Month = model.Month;
            result.TotalCompletedOrders = model.TotalCompletedOrders;
            result.TotalSales = model.TotalSales;
            result.BestTeam = model.BestTeam;
            result.AveragePerformance = model.AveragePerformance;
            result.RecentIssues = model.RecentIssues;
            result.GeneratedAt = model.GeneratedAt;
            result.AdditionalNotes = model.AdditionalNotes;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<MonthlySummary> DeleteAsync(string codice)
        {
            var result = await _context.MonthlySummary.FirstOrDefaultAsync(x => x.SummaryCode == codice);

            if (result == null)
            {
                return null;
            }

            result.Status = false;
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<bool> CheckIfExistsAsync(string codice)
        {
            var exist = await _context.MonthlySummary.FirstOrDefaultAsync(x => x.SummaryCode == codice);
            if (exist == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> GenerateNextOrderCodeAsync()
        {
            var quantity = await _context.MonthlySummary.CountAsync();
            var nextCode = $"MONS_{quantity + 1}";
            return nextCode;
        }
    }


}
