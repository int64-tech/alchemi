#region Alchemi copyright notice
/*
  Alchemi [.NET Grid Computing Framework]
  Copyright (c) 2002-2004 Akshay Luther
  http://www.alchemi.net
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
using System.IO;
using System.Xml.Serialization;
using Alchemi.Core;

namespace Alchemi.Core.Executor
{
    public class Configuration
    {
        [NonSerialized] private string ConfigFile = "";
        private const string ConfigFileName = "Alchemi.Executor.config.xml";
    
        public string Id = "";
        public string ManagerHost = "localhost";
        public int ManagerPort = 9000;
        public bool Dedicated = true;
        public int OwnPort = 9001;
        public bool ConnectVerified = false;
        public string Username = "executor";
        public string Password = "executor";
    
        //-----------------------------------------------------------------------------------------------
    
        public static Configuration GetConfiguration()
        {
            return DeSlz(ConfigFileName);
        }

        public static Configuration GetConfiguration(string location)
        {
            return DeSlz(Path.Combine(location, ConfigFileName));
        }
    
        public Configuration()
        {
            ConfigFile = ConfigFileName;
        }

        //-----------------------------------------------------------------------------------------------
    
        // serialises configuration
        public void Slz()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Configuration));
            StreamWriter sw = new StreamWriter(ConfigFile);
            xs.Serialize(sw, this);
            sw.Close();
        }

        //-----------------------------------------------------------------------------------------------

        // deserialises configuration
        private static Configuration DeSlz(string file)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Configuration));
            FileStream fs = new FileStream(file, FileMode.Open);
            Configuration temp = (Configuration) xs.Deserialize(fs);
            fs.Close();
            temp.ConfigFile = file;
            return temp;
        }

    }
}
