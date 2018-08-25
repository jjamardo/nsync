using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace nsync
{
    class Config
    {
        public string Source
        {
            get
            {
                return ConfigurationManager.AppSettings["sourcePath"];
            }
        }

        public string Target
        {
            get
            {
                return ConfigurationManager.AppSettings["targetPath"];
            }
        }

        public string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["hostName"];
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["portNumber"]);
            }
        }

        public string User
        {
            get
            {
                return ConfigurationManager.AppSettings["userName"];
            }
        }

        public string Pass
        {
            get
            {
                return ConfigurationManager.AppSettings["password"];
            }
        }

        public string Fingerprint
        {
            get
            {
                return ConfigurationManager.AppSettings["sshHostKeyFingerprint"];
            }
        }

        public bool InitialSync
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["initialSync"]);
            }
        }
    }
}
