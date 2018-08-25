using WinSCP;

namespace nsync
{
    class Synchronizer
    {
        private Session session = null;
        private TransferOptions transferOptions = null;

        public void Connect(string host, int port, string user, string password, string fingerprint)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = host,
                PortNumber = port,
                UserName = user,
                Password = password,
                SshHostKeyFingerprint = fingerprint
            };

            session = new Session();
            session.Open(sessionOptions);
            transferOptions = new TransferOptions();
            transferOptions.TransferMode = TransferMode.Binary;

            Logger.InfoLine("Synchronization successfully initiated against {0}", host);
        }

        public void Sync(string source, string dest)
        {
            var transferResult = session.PutFiles(source, dest, false, transferOptions);
            transferResult.Check();
            foreach (TransferEventArgs transfer in transferResult.Transfers)
            {
                Logger.Info("T: {0} => {1}", transfer.FileName, transfer.Destination);
                Logger.Result(transfer.Error == null);
            }
        }

        public void Close()
        {
            session.Close();
            session.Dispose();
            session = null;
            transferOptions = null;
        }
    }
}
