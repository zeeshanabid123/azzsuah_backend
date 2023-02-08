using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class PhoneNumber
    {
        [JsonProperty(PropertyName = "dialCode")]
        public string DialCode { get; set; } = "";

        private string number;

        public string Number
        {
            get { return number; }
            set { number = value.Replace(" ", ""); }
        }
    }
}
