﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Models.ViewModel
{
    public class ProjectViewModel
    {
            public int ProjectId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Skills { get; set; }
            public string Domain { get; set; }
            public string Tools { get; set; }
            public string StudentsAllowed { get; set; }
            public int SupervisorID { get; set; }
            public bool HasSentRequest { get; set; }
    }
}
