using BlazorSignalR.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalR.Server
{
	public class Hab : Hub
	{
		public void Dostava(Proba p)
		{
			//Console.WriteLine($"{p.ID},{p.Nesto},{p.Bla}");
			DB ef = new DB();
			ef.Probas.Add(p);
			ef.SaveChanges();
			Clients.Caller.SendAsync("saServera", ef.Probas.ToList());
		}
	}
}
