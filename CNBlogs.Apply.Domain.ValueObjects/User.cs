using CNBlogs.Apply.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Domain.ValueObjects
{
    public class User
    {
        public string DisplayName { get; set; }

        public string Alias { get; set; }

        public DateTime? RegisterTime { get; set; }

        [JsonProperty("SpaceUserID")]
        public int Id { get; set; }
    }
}
