using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pentagon.Models
{
    public class Chapter
    {
        public int ChapterID { get; set; }
        public int TutorialID { get; set; }
        public string ChapterName { get; set; }
        public int HierarchyLevel { get; set; }
        public string Discription { get; set; }
        public int TypeOfFile { get; set; }
        public string ContentOfFile { get; set; }
    }
}