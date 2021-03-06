﻿namespace Lands.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Translations
    {
        [JsonProperty(PropertyName = "de")]
        public string Germany { get; set; }

        [JsonProperty(PropertyName = "es")]
        public string Spanish { get; set; }

        [JsonProperty(PropertyName = "fr")]
        public string French { get; set; }

        [JsonProperty(PropertyName = "ja")]
        public string Japanese { get; set; }

        [JsonProperty(PropertyName = "it")]
        public string Italian { get; set; }

        [JsonProperty(PropertyName = "br")]
        public string Burmese { get; set; }

        [JsonProperty(PropertyName = "pt")]
        public string Portuguese { get; set; }

        [JsonProperty(PropertyName = "nl")]
        public string Detuch { get; set; }

        [JsonProperty(PropertyName = "hr")]
        public string Croatian { get; set; }

        [JsonProperty(PropertyName = "fa")]
        public string Persian { get; set; }
    }
}
