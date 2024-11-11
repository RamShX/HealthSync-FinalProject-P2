using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Dtos
{
    public abstract class BaseDto
    {
        public DateTime? ChangeDate { get; set; }
    }
}
