using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;

namespace DataBaseToolsServer
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpChannel channel = new TcpChannel(8080);
			ChannelServices.RegisterChannel(channel, false);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(DatabaseTools.DataBase), "RemotingPersonService", WellKnownObjectMode.SingleCall);

			System.Console.WriteLine("Server:Press Enter key to exit");
			System.Console.ReadLine();

		}
	}
}
