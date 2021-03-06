﻿using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.ModelHelpers
{
    public class ContactParams:BaseSearchParams
    {

        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameTitle { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
