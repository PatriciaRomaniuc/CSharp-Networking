using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]

    public class Entity<ID>
    {
        private ID id;

        public Entity(ID id) { this.id = id; }

        public ID Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
