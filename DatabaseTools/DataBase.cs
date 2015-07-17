using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DatabaseTools.Model;

namespace DatabaseTools
{

	public interface IDataBase
	{
		DataBaseSchema GetDataBaseSchema();
	}



	public class DataBase : MarshalByRefObject, IDataBase  
	{
		public DataBase()  
        {
			Console.WriteLine("[DataBase]:Remoting Object 'DataBase' is activated.");  
        }


		public DataBaseSchema GetDataBaseSchema()
		{
			DataBaseSchema dbs = new DataBaseSchema();
			try
			{
				using (var db = DbEntry.GetDb())
				{
					dbs.Name = db.Query<string>("select database();").First();
					var tables = db.Query<string>("show tables;").ToList<string>();
					dbs.Tables = new List<TableSchema>();
					foreach (var t in tables)
					{
						TableSchema tc = new TableSchema();
						tc.Name = t;
						string sql = string.Format("describe {0};", t);
						tc.Cloumes = db.Query<FieldSchema>(sql).ToList<FieldSchema>();
						dbs.Tables.Add(tc);
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message); 
			}
			
			
			return dbs;
		}
	}
}
