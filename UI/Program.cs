using UI;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.File(@"..\DL\logFile.txt").CreateLogger();
MenuFactory.GetMenu("main").Start();