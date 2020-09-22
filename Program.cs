using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection;

public class Program
{
	static void Main(string[] args)
	{
		//read input file
		string[] lines = System.IO.File.ReadAllLines("input.txt");
		
		var strK = lines[0].Split(" ");
		if(strK.Length != 5)
		{
			Console.WriteLine("KEY INPUT ERROR");
		}
		var strV = lines[1].Split(" ");
		if (strV.Length != 5)
		{
			Console.WriteLine("VAR INPUT ERROR");
		}
		var strN = lines[2].Split(" ");
		if (strN.Length != 2)
		{
			Console.WriteLine("NUM INPUT ERROR");
		}

		//Convert Args
		uint[] K = new uint[4];
		for (uint i = 0; i < K.Length; i++)
		{
			K[i] = Convert.ToUInt32(strK[i + 1], 16);
		}
		uint[] V = new uint[4];
		for (uint i = 0; i < V.Length; i++)
		{
			V[i] = Convert.ToUInt32(strV[i + 1], 16);
		}
		uint N = Convert.ToUInt32(strN[1]);

		Cipher cipher = new Cipher();
		cipher.Init(K, V);



		//initialization
		for (int i = 0; i < 32; i++)
		{
			var F = cipher.ClockFSM();
			cipher.InitLFSR(F);
		}

		//KeyStream PreProcess
		cipher.ClockFSM();
		cipher.KeyStreamLFSR();

		//Generation
		string[] res = new string[N];
		for (int i = 0; i < N; i++)
		{
			var F = cipher.ClockFSM();
			res[i] = (F ^ cipher.lfsr[0]).ToString("x8");
			cipher.KeyStreamLFSR();
		}

		//WriteFile
		System.IO.File.WriteAllLines("output.txt", res);
	}
}
