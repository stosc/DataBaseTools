using DatabaseTools;
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
			IDataBase obj = (IDataBase)Activator.GetObject(typeof(DatabaseTools.IDataBase), "tcp://localhost:8080/RemotingPersonService");
			if (obj == null)
			{
				Console.WriteLine("Couldn't crate Remoting Object 'Person'.");
			}

			Console.WriteLine("Please enter your name：");
			String name = Console.ReadLine();
			try
			{
				Console.WriteLine(obj.GetTables());
			}
			catch (System.Net.Sockets.SocketException e)
			{
				Console.WriteLine(e.ToString());
			}
			Console.ReadLine();

		}
	}
}
