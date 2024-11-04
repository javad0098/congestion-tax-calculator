using Serilog;

namespace CongestionTaxApp.Common
{
    public interface ILoggingService
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception ex);
    }

    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] args) => _logger.LogInformation(message, args);
        public void LogWarning(string message, params object[] args) => _logger.LogWarning(message, args);
        public void LogError(string message, Exception ex) => _logger.LogError(ex, message);

    }
}
