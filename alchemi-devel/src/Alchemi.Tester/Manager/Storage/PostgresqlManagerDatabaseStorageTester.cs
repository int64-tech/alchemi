#region Alchemi copyright and license notice

/*
* Alchemi [.NET Grid Computing Framework]
* http://www.alchemi.net
* Title         :  MySqlManagerDatabaseStorageTester.cs
* Project       :  Alchemi.Tester.Manager.Storage
* Created on    :  22 September 2005
* Copyright     :  Copyright � 2006 The University of Melbourne
*                    This technology has been developed with the support of
*                    the Australian Research Council and the University of Melbourne
*                    research grants as part of the Gridbus Project
*                    within GRIDS Laboratory at the University of Melbourne, Australia.
* Author        :  Tibor Biro (tb@tbiro.com)
* License       :  GPL
*                    This program is free software; you can redistribute it and/or
*                    modify it under the terms of the GNU General Public
*                    License as published by the Free Software Foundation;
*                    See the GNU General Public License
*                    (http://www.gnu.org/copyleft/gpl.html) for more 
details.
*
*/
#endregion

using System;
using System.Configuration;

using Alchemi.Core.Manager.Storage;
using Alchemi.Manager.Storage;

using NUnit.Framework;

namespace Alchemi.Tester.Manager.Storage
{
	/// <summary>
	/// Summary description for PostgresqlManagerDatabaseStorageTester.
	/// </summary>
	[TestFixture]
	public class PostgresqlManagerDatabaseStorageTester : ManagerStorageTester
	{
		private PostgresqlManagerDatabaseStorage _managerStorage;

		public override IManagerStorage ManagerStorage
		{
			get
			{
				return _managerStorage;
			}
		}

		/// <summary>
		/// Create the MySQL connection string.
		/// Look in the configuration file for the data and if not found then default to the hardcoded values.
		/// </summary>
		[SetUp]
		public void TestStartUp()
		{
			string connectionString = ConfigurationManager.AppSettings["PostgresqlTesterConnectionString"];

			if (connectionString == null)
			{
				connectionString = string.Format(
					"user id={1};password={2};database={3};data source={0};Connect Timeout=5; Max Pool Size=5; Min Pool Size=5;",
					"localhost",
					"alchemi",
					"alchemi",
					"AlchemiTester"
					);
			}

			_managerStorage = new PostgresqlManagerDatabaseStorage(connectionString);

			_managerStorage.SetUpStorage();
			_managerStorage.InitializeStorageData();
		}

		[TearDown]
		public void TestShutDown()
		{
			_managerStorage.TearDownStorage();
		}

        public PostgresqlManagerDatabaseStorageTester()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
