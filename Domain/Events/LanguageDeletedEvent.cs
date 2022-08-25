using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class LanguageDeletedEvent:BaseEvent
    {
        public LanguageDeletedEvent(int Id)
        {
            this.Id = Id;
        }

        public int Id { get; }
    }
}
