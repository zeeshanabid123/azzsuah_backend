﻿using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class Counter
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
