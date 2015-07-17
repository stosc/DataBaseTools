using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseTools
{

	public interface IDataBase
	{
		List<String> GetTables();
	}



	public class DataBase : MarshalByRefObject, IDataBase  
	{
		public DataBase()  
        {
			Console.WriteLine("[DataBase]:Remoting Object 'Person' is activated.");  
        } 


		public List<string> GetTables()
		{
			return new List<string>() { "mysql","Sqlserver"};
		}
	}
}
