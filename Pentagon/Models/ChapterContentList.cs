using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pentagon.Models
{
    public class ChapterContentList
    {
        public string ChapterName { get; set; }
        public int HierarchyLevel { get; set; }
        public string Discription { get; set; }
        public int TypeOfFile { get; set; }
        public string ContentOfFile { get; set; }
    }
}