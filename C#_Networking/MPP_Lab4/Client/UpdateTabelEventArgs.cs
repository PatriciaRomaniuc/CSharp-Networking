using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{

    public enum UpdateTabelEvent
    {
        UpdateTabel
    };
    public class UpdateTabelEventArgs : EventArgs
    {
        private readonly UpdateTabelEvent uEvent;
        private readonly Object data;

        public UpdateTabelEventArgs(UpdateTabelEvent uEvent, object data)
        {
            this.uEvent = uEvent;
            this.data = data;
        }

        public UpdateTabelEvent eventType
        {
            get { return uEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
