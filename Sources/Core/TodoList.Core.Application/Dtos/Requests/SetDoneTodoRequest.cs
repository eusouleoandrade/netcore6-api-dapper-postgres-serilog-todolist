using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Core.Application.Dtos.Requests
{
    public class SetDoneTodoRequest
    {
        public bool Done { get; set; }
    }
}