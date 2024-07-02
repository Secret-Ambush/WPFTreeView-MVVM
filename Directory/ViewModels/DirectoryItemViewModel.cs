using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTreeView;

namespace WPFTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of an item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));

        /// <summary>
        /// The full path to an item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item is expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        public bool IsExpanded
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
                { 
                    if(this.Type == DirectoryItemType.OpenFolder)
                        this.Type = DirectoryItemType.Folder;
                    this.ClearChildren(); 
                }
            }
        }

        #endregion

        #region Public Commands
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        public DirectoryItemViewModel(string fullPath, DirectoryItemType Type)
        {
            //Create command
            this.ExpandCommand = new RelayCommand(Expand);

            //Set path and type
            this.FullPath = fullPath;
            this.Type = Type;

            //Setup the children as needed
            this.ClearChildren();
        }

        #endregion


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

        /// <summary>
        /// Expands this directory and finds all the children
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                            children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
            if (this.Type == DirectoryItemType.Folder)
                this.Type = DirectoryItemType.OpenFolder;

        }
    }
}
