using System;
using System.IO;
using RisksApp.Core;

namespace RisksApp {
  public class Database : SQLite.SQLiteConnection {
    internal Database(string file) : base (file) {
    }
            
    static Database() {
      EnsureDB();
	  string RootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..");
      var db = Path.Combine (RootDirectory, "Documents/Risks.db");
      Instance = new Database (db);
    }
            
    public static Database Instance { get; private set; }
        
    public static void EnsureDB() {
      string dbname = "Risks.db";
      string documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // This goes to the documents directory for your app
      string db = Path.Combine (documents, dbname);
            
      string rootPath = Environment.CurrentDirectory;
      string rootDbPath = Path.Combine (rootPath, dbname);
            
      Logger.DebugLog ("Root Db Path: " + rootDbPath);
      Logger.DebugLog ("Final Db Path: " + db);
            
      if (File.Exists (db) == false) {
        Logger.DebugLog  ("Copying DB!");
        File.Copy (rootDbPath, db);
      }
      else {
        Logger.DebugLog ("DB Exists, not copying.");
      }       
    }
  }
}