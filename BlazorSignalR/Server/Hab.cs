using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server
{
	public class Hab : Hub
	{
		public void Probna(string s)
		{
			Console.WriteLine("Uspeh :) " + s);
			Clients.Caller.SendAsync("zklj", 12);
		}
	}
}
