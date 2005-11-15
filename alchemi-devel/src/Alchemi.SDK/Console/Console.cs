#region Alchemi copyright and license notice

/*
* Alchemi [.NET Grid Computing Framework]
* http://www.alchemi.net
* Title			:	Console.cs
* Project		:	Alchemi Console
* Created on	:	Sep 2005
* Copyright		:	Copyright � 2005 The University of Melbourne
*					This technology has been developed with the support of 
*					the Australian Research Council and the University of Melbourne
*					research grants as part of the Gridbus Project
*					within GRIDS Laboratory at the University of Melbourne, Australia.
* Author         :  Krishna Nadiminti (kna@cs.mu.oz.au) and Rajkumar Buyya (raj@cs.mu.oz.au)
* License        :  GPL
*					This program is free software; you can redistribute it and/or 
*					modify it under the terms of the GNU General Public
*					License as published by the Free Software Foundation;
*					See the GNU General Public License 
*					(http://www.gnu.org/copyleft/gpl.html) for more details.
*
*/ 
#endregion

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch=true)]
namespace Alchemi.Console
{
	/// <summary>
	///This is the entry point for the Alchemi Console.
	/// </summary>
	public class Console
	{

		// Create a logger for use in this class
		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		[STAThread]
		static void Main() 
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			if (!System.IO.Directory.Exists("dat"))
			{
				System.IO.Directory.CreateDirectory("dat");
				log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Assembly.GetExecutingAssembly().GetName()+".config"));
			}
			
			logger.Debug(Application.ExecutablePath+".config");

			//Application.EnableVisualStyles();
			Application.Run(new ConsoleParentForm());
		}

		public Console()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = (Exception) e.ExceptionObject;
			HandleAllUnknownErrors(sender.ToString(),ex);
		}

		private static void HandleAllUnknownErrors(string sender, Exception ex)
		{
			logger.Error("Unknown Error from: " + sender,ex);
			MessageBox.Show("Error occured: (continuing after error...)\n"+ex.ToString(), "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
