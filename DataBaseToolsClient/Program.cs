using DatabaseTools;
using DatabaseTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;

namespace DataBaseToolsClient
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpChannel channel = new TcpChannel();
			ChannelServices.RegisterChannel(channel, false);
			IDataBase obj = (IDataBase)Activator.GetObject(typeof(DatabaseTools.IDataBase), "tcp://www.alibiaobiao.cn:8080/RemotingPersonService"); //tcp://www.alibiaobiao.cn:8080/RemotingPersonService
			if (obj == null)
			{
				Console.WriteLine("Couldn't crate Remoting Object 'Person'.");
			}

			//Console.WriteLine("Please enter your name：");
			//String name = Console.ReadLine();
			try
			{
				DataBaseSchema rdbs = obj.GetDataBaseSchema();
				DataBase db = new DataBase();
				DataBaseSchema ldbs = db.GetDataBaseSchema();
				var r = ldbs.CompareSchema(rdbs);
				if(r==null)
				   Console.WriteLine("数据库结构一致");
				else
					Console.WriteLine(r);

				
			}
			catch (System.Net.Sockets.SocketException e)
			{
				Console.WriteLine(e.ToString());
			}
			Console.ReadLine();

		}
	}
}
