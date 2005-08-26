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

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using Alchemi.Core.Utility;

namespace Alchemi.Core.Owner
{
	/// <summary>
	/// The GConnection class represents a connection to a Alchemi manager.
	/// It is a container for the properties such as host, port, securityCredentials etc... used to connect to the manager.
	/// </summary>
    [Serializable]
    public class GConnection : System.ComponentModel.Component
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        private System.ComponentModel.Container components = null;
        private string _Host = "localhost";
        private int _Port = 9000;
        private string _Username = "";
        private string _Password = "";

		/// <summary>
		/// Gets or sets the host name of the Alchemi manager
		/// </summary>
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

		/// <summary>
		/// Gets or sets the port number of the Alchemi manager
		/// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

		/// <summary>
		/// Gets or sets the username used to connect to the manager
		/// </summary>
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

		/// <summary>
		/// Gets or sets the password used to connect to the manager
		/// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

		/// <summary>
		/// Creates a new instance of the GConnection
		/// </summary>
		/// <param name="container"></param>
        public GConnection(System.ComponentModel.IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

		/// <summary>
		/// Creates a new instance of the GConnection
		/// </summary>
        public GConnection()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Creates a new instance of the GConnection
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
        public GConnection(string host, int port, string username, string password)
        {
            _Host = host;
            _Port = port;
            _Username = username;
            _Password = password;
        }
        
		/// <summary>
		/// Disposes the GConnection object and performs clean up operations.
		/// </summary>
		/// <param name="disposing"></param>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

		/// <summary>
		/// Gets the "remoteEndPoint" object associated with the manager.
		/// </summary>
        public RemoteEndPoint RemoteEP
        {
            get 
            {
                return new RemoteEndPoint(_Host, _Port, RemotingMechanism.TcpBinary);
            }
        }

		/// <summary>
		/// Gets the SecurityCredentials object associated with this manager.
		/// </summary>
        public SecurityCredentials Credentials
        {
            get
            {
                return new SecurityCredentials(_Username, _Password);
            }
        }

		/// <summary>
		/// Gets an instance of the GConnection class, from values input through the console.
		/// </summary>
		/// <param name="defaultHost"></param>
		/// <param name="defaultPort"></param>
		/// <param name="defaultUsername"></param>
		/// <param name="defaultPassword"></param>
		/// <returns>GConnection</returns>
        public static GConnection FromConsole(string defaultHost, string defaultPort, string defaultUsername, string defaultPassword)
        {
            string host = Utils.ValueFromConsole("Host", defaultHost);
            string port = Utils.ValueFromConsole("Port", defaultPort);
            string username = Utils.ValueFromConsole("Username", defaultUsername);
            string password = Utils.ValueFromConsole("Password", defaultPassword);

            Console.WriteLine();

            return new GConnection(host, int.Parse(port), username, password);
        }
    }
}
