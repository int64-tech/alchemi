#region Alchemi copyright notice
/*
  Alchemi [.NET Grid Computing Framework]
  http://www.alchemi.net
  
  Copyright (c)  Akshay Luther (2002-2004) & Rajkumar Buyya (2003-to-date), 
  GRIDS Lab, The University of Melbourne, Australia.
  
  Maintained and Updated by: Krishna Nadiminti (2005-to-date)
---------------------------------------------------------------------------

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
#endregion

using System.Collections;
using System.Data;
using System.IO;
using Alchemi.Core.Owner;
using Alchemi.Core.Utility;
using Alchemi.Core.Manager.Storage;

namespace Alchemi.Core.Manager
{
	/// <summary>
	/// Represents an Application on the manager.
	/// </summary>
    public class MApplication
    {
		// Create a logger for use in this class
		private static readonly Logger logger = new	Logger();

        private string _Id;
        
		/// <summary>
		/// Creates an instance of MApplication
		/// </summary>
		/// <param name="id">id of the application</param>
        public MApplication(string id)
        {
            _Id = id;
        }
        
		/// <summary>
		/// Returns the MThread whose threadId is passed in
		/// </summary> //this is an indexer
        public MThread this[int threadId]
        {
            get
            {
                return new MThread(_Id, threadId);
            }
        }

		/// <summary>
		/// Gets or sets the application manifest which is a collection of fileDependencies
		/// </summary>
        public FileDependencyCollection Manifest
        {
            set 
            {
            	Utils.SerializeToFile(value, Path.Combine(DataDir, "manifest.dat"));
                this.State = ApplicationState.Ready;
            }

            get 
            {
                return (FileDependencyCollection) Utils.DeserializeFromFile(
                    Path.Combine(DataDir, "manifest.dat"));
            }
        }

        private string DataDir
        {
            get
            {
                return Path.Combine(InternalShared.Instance.DataRootDirectory, "application_" + _Id);
            }
        }
        
		/// <summary>
		/// Gets the finished threads as a byte array
		/// </summary>
        public byte[][] FinishedThreads
        {
            get
            {
            	ArrayList finishedThreads = new ArrayList();

            	DataTable dt = InternalShared.Instance.Database.ExecSql_DataTable(
                    string.Format("Threads_UpdateStateAndSelect '{0}', {1}, {2}", _Id, (int) ThreadState.Finished, (int) ThreadState.Dead)
                    );

                foreach (DataRow dr in dt.Rows)
                {
                    finishedThreads.Add(
                        this[int.Parse(dr["thread_id"].ToString())].Value
                        );
                }
                return (byte[][]) finishedThreads.ToArray(typeof(byte[]));
            }
        }

		/// <summary>
		/// Gets the count threads with the given thread-state.
		/// 
		/// Updates: 
		/// 
		///	27 October 2005 - Tibor Biro (tb@tbiro.com) - Replaced the direct database call with Manager Storage object calls
		///	
		/// </summary>
		/// <param name="ts"></param>
		/// <returns>Thread count</returns>
		public int ThreadCount(ThreadState ts)
		{
			return ManagerStorageFactory.ManagerStorage().GetThreadCount(_Id, ts);
		}

		/// <summary>
		/// Creates the data directory
		/// </summary>
        public void CreateDataDirectory()
        {
            Directory.CreateDirectory(DataDir);
        }

		/// <summary>
		/// Gets or sets the application state
		/// </summary>
        public ApplicationState State
        {
            get
            {
				ApplicationStorageView application = ManagerStorageFactory.ManagerStorage().GetApplication(_Id);

				return application.State;
            }
            set
            {
				ApplicationStorageView application = ManagerStorageFactory.ManagerStorage().GetApplication(_Id);

				application.State = value;

				ManagerStorageFactory.ManagerStorage().UpdateApplication(application);
            }
        }

		/// <summary>
		/// Gets the list of threads from the database, for this application.
		/// </summary>
        public DataSet ThreadList
        {
            get
            {
                return InternalShared.Instance.Database.ExecSql_DataSet(string.Format("select thread_id, state, time_started, time_finished, executor_id, priority, failed from thread where application_id = '{0}' order by thread_id", _Id));
            }
        }

		/// <summary>
		/// Gets the list of  threads with the given thread-state
		/// </summary>
		/// <param name="status"></param>
		/// <returns>Dataset with thread info.</returns>
		public DataSet GetThreadList(ThreadState status)
		{
			return InternalShared.Instance.Database.ExecSql_DataSet(string.Format("select thread_id, state, time_started, time_finished, executor_id, priority, failed from thread where application_id = '{0}' and state = {1} order by thread_id", _Id, (int)status));
		}

		/// <summary>
		/// Gets a value indicating whether the manager is at the top of the hierarchy.
		/// If true, this node is a primary manager.
		/// </summary>
        public bool IsPrimary
        {
            get 
            {
				ApplicationStorageView application = ManagerStorageFactory.ManagerStorage().GetApplication(_Id);

				return application.Primary;
            }
        }

		/// <summary>
		/// Stops an application.
		/// </summary>
        public void Stop()
        {
			ThreadStorageView[] threads = ManagerStorageFactory.ManagerStorage().GetThreads(_Id);

			foreach (ThreadStorageView thread in threads)
			{
			    GManager.AbortThread(new ThreadIdentifier(_Id, thread.ThreadId), thread.ExecutorId);
			}

			logger.Debug("Stopped the current application."+_Id);
        }
    }
}
