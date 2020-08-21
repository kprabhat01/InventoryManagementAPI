using IL.Service.Core.ItemService;
using IL.Service.Core.LoggerService;
using IM.Data.Core;
using System;
using System.Linq;
using System.Timers;

namespace IL.Service.Core.JobService
{
    public class JobService : IJobService
    {
        private Timer _jobTimer;
        private readonly IItemService _itemService;
        private readonly ILoggerService _logger;
        public JobService(IItemService itemService, ILoggerService logger)
        {
            this._itemService = itemService;
            this._logger = logger;
        }
        private void IntiateTimer()
        {
            _jobTimer = new Timer();
            _jobTimer.Interval = (1000 * 60) * 30;
            _jobTimer.Elapsed += ExecuteInterval;
            _jobTimer.Start();
        }
        public bool RunService()
        {
            IntiateTimer();
            return true;
        }
        public void ExecuteInterval(object source, ElapsedEventArgs e)
        {
            try
            {
                using (var entity = new db_InventoryEntities())
                {
                    if (!entity.logsDailyJobs.AsEnumerable().Any(p => p.LastRunDate.Date == DateTime.Now.Date))
                    {
                        this._logger.Log("Job has been started");
                        this._itemService.CaptureCurrentLogs();
                        var logs = entity.logsDailyJobs;
                        logs.RemoveRange(logs);
                        entity.SaveChanges();
                        entity.logsDailyJobs.Add(new logsDailyJob
                        {
                            LastRunDate = DateTime.Now
                        });
                        entity.SaveChanges();
                        this._logger.Log("Job has been completed successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.Log("Job has some error");
            }
        }
    }
}
