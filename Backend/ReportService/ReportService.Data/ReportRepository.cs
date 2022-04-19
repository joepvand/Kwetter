using Microsoft.EntityFrameworkCore;
using ReportService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using ReportService.DomainModels;
using Report = ReportService.Data.Models.Report;

namespace ReportService.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportContext _context;

        public ReportRepository(ReportContext context)
        {
            _context = context;
        }

        public IQueryable<Report> GetAll()
        {
            return this._context.Reports.AsQueryable();
        }

        public Task<Report> GetById(Guid id)
        {
            return this._context.Reports.SingleAsync(x => x.Id == id);
        }

        public async Task DeleteById(Guid id)
        {
            var example = this._context.Reports.SingleAsync(x => x.Id == id);
            this._context.Reports.Remove(await example);
            await this._context.SaveChangesAsync();
        }

        public async Task<Report> Add(DomainModels.Report report)
        {
            var dataModel = report.Adapt<Report>();

            var response = await _context.Reports.AddAsync(dataModel);
            return response.Entity;
        }


        public async Task Update(Guid id, ReportStatus status, string closureMessage = "")
        {
            var report = await this.GetById(id);
            report.Status = status;
            if (!string.IsNullOrWhiteSpace(closureMessage))
                report.ClosureMessage = closureMessage;

            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}
