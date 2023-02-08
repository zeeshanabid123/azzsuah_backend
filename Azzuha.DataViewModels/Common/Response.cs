﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class Response<T>
    {
        public bool isError { get; set; }
        public string messages { get; set; }
        public T data { get; set; }
    }
}
