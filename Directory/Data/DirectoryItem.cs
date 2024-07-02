using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WPFTreeView
{
    public class DirectoryItem
    {

        public DirectoryItemType Type { get; set; } 
        public required string FullPath { get; set; }

        /// <summary>
        /// The name of the File
        /// </summary>
        public string Name { get {  return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
