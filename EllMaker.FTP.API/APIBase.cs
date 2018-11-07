using System;

namespace EllaMaker.FTP.API
{
    internal  abstract class APIBase
    {
        protected String _ServiceIP;
        protected int _ServicePort;

        internal APIBase(string Ip,int Port)
        {
            this._ServiceIP = Ip;
            this._ServicePort = Port;
        }
        
    }
}
