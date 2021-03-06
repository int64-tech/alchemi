#region Alchemi copyright and license notice

/*
* Alchemi [.NET Grid Computing Framework]
* http://www.alchemi.net
*
* Title			:	ManagerMainForm.cs
* Project		:	Alchemi Manager Application
* Created on	:	2003
* Copyright		:	Copyright � 2006 The University of Melbourne
*					This technology has been developed with the support of 
*					the Australian Research Council and the University of Melbourne
*					research grants as part of the Gridbus Project
*					within GRIDS Laboratory at the University of Melbourne, Australia.
* Author         :  Akshay Luther (akshayl@csse.unimelb.edu.au), Rajkumar Buyya (raj@csse.unimelb.edu.au), and Krishna Nadiminti (kna@csse.unimelb.edu.au)
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
using System.ComponentModel;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using Alchemi.Core;
using Alchemi.Core.Manager;
using Alchemi.Manager;
using log4net;
using System.Diagnostics;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch=true)]

namespace Alchemi.ManagerService
{
	public class ManagerMainForm : ManagerTemplateForm
	{
        private const string serviceName = "Alchemi Manager Service";

		public ManagerMainForm():base()
		{
			InitializeComponent();
			this.Text = "Alchemi Manager Service Controller";
			Logger.LogHandler += new LogEventHandler(LogHandler);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.uiSetupConnectionTabPage.SuspendLayout();
            //this.uiTabControl.SuspendLayout();
            this.uiNodeConfigurationGroupBox.SuspendLayout();
            this.uiActionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbIntermediate
            // 
            this.uiIntermediateComboBox.CheckedChanged += new System.EventHandler(this.uiIntermediateComboBox_CheckedChanged);
            // 
            // statusBar
            // 
            this.uiProgressBar.Location = new System.Drawing.Point(0, 556);
            // 
            // lnkViewLog
            // 
            this.uiViewFullLogLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uiViewFullLogLinkLabel_LinkClicked);
            // 
            // ManagerMainForm
            // 
            this.ClientSize = new System.Drawing.Size(458, 578);
            this.Name = "ManagerMainForm";
            this.Load += new System.EventHandler(this.ManagerMainForm_Load);
            this.uiSetupConnectionTabPage.ResumeLayout(false);
            //this.uiTabControl.ResumeLayout(false);
            this.uiNodeConfigurationGroupBox.ResumeLayout(false);
            this.uiNodeConfigurationGroupBox.PerformLayout();
            this.uiActionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void uiViewFullLogLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //show the log .
            string logFile = null;
            try
            {
                logFile = GetLogFilePath();
                Process p = new Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = logFile;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not show log file {0}! Error : {1}", logFile, ex.Message), "Alchemi Manager", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
		#endregion

		//-----------------------------------------------------------------------------------------------    

		private void LogHandler(object sender, LogEventArgs e)
		{
			// Create a logger for use in this class
			ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
			switch (e.Level)
			{
				case LogLevel.Debug:
					string message = e.Source  + ":" + e.Member + " - " + e.Message;
					logger.Debug(message,e.Exception);
					break;
				case LogLevel.Info:
					logger.Info(e.Message);
					break;
				case LogLevel.Error:
					logger.Error(e.Message,e.Exception);
					break;
				case LogLevel.Warn:
					logger.Warn(e.Message, e.Exception);
					break;
			}
		}
    
		//-----------------------------------------------------------------------------------------------    

		private void ManagerMainForm_Load(object sender, EventArgs e)
		{

			//this is a service. just read the config.			
			RefreshUIControls(ReadManagerConfig(false));
			uiStartButton.Focus();
		}

		private Configuration ReadManagerConfig(bool useDefault)
		{
			//in case it is a service, the container would be null since we dont need it really.
			//but we still need to get the config from it, so create a new one and read the config.
			ManagerContainer mc = new ManagerContainer();
			mc.ReadConfig();
            return mc.Config;
		}

		//-----------------------------------------------------------------------------------------------    

        private void uiIntermediateComboBox_CheckedChanged(object sender, EventArgs e)
		{
            Configuration config = ReadManagerConfig(true);
            config.Intermediate = uiIntermediateComboBox.Checked;
			//_container.Config = Config;
            RefreshUIControls(config);
		}

		#region Implementation of methods from ManagerTemplateForm
		protected override bool Started
		{
			get
			{
				bool started = false;
				try
				{
					ServiceController sc = new ServiceController(serviceName);
					if (sc.Status == ServiceControllerStatus.Running || sc.Status == ServiceControllerStatus.StartPending)
					{
						started = true;
					}
					sc = null;
				}
				catch (Exception ex)
				{
					logger.Error("Error trying to determine service status",ex);
				}
				return started;
			}
		}
		protected override void Exit()
		{
			//for the service controller, we dont stop the manager. we simply end the service-controller.
			this.Close();
			Application.Exit();
		}

		protected override void ResetManager()
		{
			//only reset the GUI.
			RefreshUIControls(ReadManagerConfig(true));
		}
     
		protected override void StopManager()
		{
			if (!Started)
			{
				Log("The Manager Service is already stopped.");
                RefreshUIControls(ReadManagerConfig(true));
				return;
			}

			try
			{
				uiProgressBar.Text = "Stopping Manager Service...";
				Log("Stopping Manager Service...");

				uiStopButton.Enabled = false; //to avoid clicking on this again.
				ServiceController sc = new ServiceController(serviceName);
				if (sc.CanStop)
				{
					sc.Stop();
					sc.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,28));
					Log("Manager Service stopped.");
				}
				else
				{
					logger.Debug("Could not stop service: CanStop = false");	
				}
			}
            catch (System.ServiceProcess.TimeoutException)
			{
				Log("Timeout expired trying to stop Manager Service.");
			}
			catch (Exception ex)
			{
				Log("Error stopping ManagerService");
				logger.Error(ex.Message, ex);
			}
            RefreshUIControls(ReadManagerConfig(true));
		}
    
		protected override void StartManager()
		{
			if (Started)
			{
				Log("Manager Service is already started.");
                RefreshUIControls(ReadManagerConfig(true));
				return;
			}

			try
			{
				//to avoid people from clicking this again during the start process!
                uiStartButton.Enabled = false;
                uiResetButton.Enabled = false;
                uiStopButton.Enabled = false;

                uiProgressBar.Text = "Starting Manager Service...";

				Log("Attempting to start Manager Service...");

				ServiceController sc = new ServiceController(serviceName);
				if (sc.Status != ServiceControllerStatus.Running && sc.Status != ServiceControllerStatus.StartPending)
				{
					//get latest config from UI and serialize the  object, so that the service uses the latest config.
					Configuration Config = GetConfigFromUI();
					if (Config!=null)
					{					
						Config.Serialize();
					}
					sc.Start();
					sc.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,28));
					Log("Manager Service started.");
				}
			}
            catch (System.ServiceProcess.TimeoutException)
			{
				Log("Timeout expired trying to start Manager Service.");
			}
			catch (Exception ex)
			{
				Log("Error starting ManagerService");
				logger.Error("Error starting ManagerService",ex);
				StopManager();
			}
            RefreshUIControls(ReadManagerConfig(true));
		}
		#endregion

	}
}
