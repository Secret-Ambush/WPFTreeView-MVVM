using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The full path to an item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path to an item
        /// </summary>
        public string FullPath { get; set; }

        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        #endregion

        public ObservableCollection <DirectoryItemViewModel> Children { get; set; }
        public bool CanExpland { get { return this.Type != DirectoryItemType.File; } }

        public bool isExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }

            set
            {
                if (value == true)
                    Expand();

                else
                    this.ClearChildren();
            }
        }

        #region Helper Method
        private void ClearChildren()
        {
            //Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show expand arrow if not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// Expands the directory
        private void Expand()
        {

        }
    }
}
