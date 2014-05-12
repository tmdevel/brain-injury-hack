using System;
namespace TotalMobile.Infrastructure.Logging {
  /// <summary>
  /// Interface that loggers implement
  /// </summary>
  public interface ILog {
    /// <summary>
    /// Logs message as Info
    /// </summary>
    /// <param name="format">
    /// Format <see cref="System.String"/>
    /// </param>
    /// <param name="args">
    /// Arguments <see cref="System.Object[]"/>
    /// </param>
    void Info(string format, params object[] args);
    /// <summary>
    /// Logs message as a warning
    /// </summary>
    /// <param name="format">
    /// Format <see cref="System.String"/>
    /// </param>
    /// <param name="args">
    /// Arguments <see cref="System.Object[]"/>
    /// </param>
    void Warn(string format, params object[] args);
    /// <summary>
    /// Logs an Exception
    /// </summary>
    /// <param name="exception">
    /// The Exception <see cref="Exception"/>
    /// </param>
    void Error(Exception exception);
    /// <summary>
    /// Logs an Exception and a formatted message
    /// </summary>
    /// <param name="exception">
    /// The Exception <see cref="Exception"/>
    /// </param>
    /// <param name="format">
    /// Format <see cref="System.String"/>
    /// </param>
    /// <param name="args">
    /// Arguments <see cref="System.Object[]"/>
    /// </param>
    void Error(Exception exception, string format, params object[] args);
  }
}
