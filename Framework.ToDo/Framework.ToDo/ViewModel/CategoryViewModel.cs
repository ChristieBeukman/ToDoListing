using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Framework.ToDo.Services;
using Framework.ToDo.Model;
using System.Windows;
using Framework.ToDo.View;
using System.Collections.ObjectModel;

namespace Framework.ToDo.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        #region Properties
        #region private Properties
        IDataAccesCategory _ServiceProxy;
        private ObservableCollection<CategoryItem> _Categories;
        private CategoryItem _Category;
        private CategoryItem _SelectedCategory;
        private CategoryItem _Cat;
        private bool controlIsReadOnly = true;
        private bool hiddenControlEnabled = false;
        private bool visibleCOntrolEnabled = true;
        string catName;
        string _CatDescription;
        int _CatID;
        private CategoryItem _OldCategory;

        #endregion Private Properties

        #region Public Properties
        public ObservableCollection<CategoryItem> Categories
        {
            get
            {
                return _Categories;
            }

            set
            {
                _Categories = value;
                RaisePropertyChanged("Categories");
            }
        }

        public CategoryItem Category
        {
            get
            {
                return _Category;
            }

            set
            {
                _Category = value;
                RaisePropertyChanged("Category");
            }
        }

        public CategoryItem Cat
        {
            get
            {
                return _Cat;
            }

            set
            {
                _Cat = value;
                RaisePropertyChanged("Cat");
            }
        }

        public bool ControlIsReadOnly
        {
            get
            {
                return controlIsReadOnly;
            }

            set
            {
                controlIsReadOnly = value;
                RaisePropertyChanged("ControlIsReadOnly");
            }
        }

        public bool HiddenControlEnabled
        {
            get
            {
                return hiddenControlEnabled;
            }

            set
            {
                hiddenControlEnabled = value;
                RaisePropertyChanged("HiddenControlEnabled");
            }
        }

        public bool VisibleCOntrolEnabled
        {
            get
            {
                return visibleCOntrolEnabled;
            }

            set
            {
                visibleCOntrolEnabled = value;
                RaisePropertyChanged("VisibleCOntrolEnabled");
            }
        }

        public string CatName
        {
            get
            {
                return catName;
            }

            set
            {
                catName = value;
                RaisePropertyChanged("CatName");
            }
        }

        public string CatDescription
        {
            get
            {
                return _CatDescription;
            }

            set
            {
                _CatDescription = value;
                RaisePropertyChanged("CatDescription");
            }
        }

        public int CatID
        {
            get
            {
                return _CatID;
            }

            set
            {
                _CatID = value;
                RaisePropertyChanged("CatID");
            }
        }

        public CategoryItem OldCategory
        {
            get
            {
                return _OldCategory;
            }

            set
            {
                _OldCategory = value;
                RaisePropertyChanged("OldCategory");
            }
        }

        public CategoryItem SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }

            set
            {
                _SelectedCategory = value;
                RaisePropertyChanged("SelectedCategory");

            }
        }

        #endregion Public Properties

        #region Commands

        #endregion Commands


        #endregion Properties

        #region Constructor

        public CategoryViewModel(IDataAccesCategory prxy)
        {
            _ServiceProxy = prxy;
            Categories = new ObservableCollection<CategoryItem>();
            SelectedCategory = new CategoryItem();
            Category = new CategoryItem();
            Cat = new CategoryItem();
            OldCategory = new CategoryItem();

            GetCategories();
        }


        #endregion


        #region Methods

        /// <summary>
        /// disables text readonly and hides combobox
        /// </summary>
        void ToggleControl()
        {
            if (VisibleCOntrolEnabled == false)
            {
                ControlIsReadOnly = true;
                HiddenControlEnabled = false;
                VisibleCOntrolEnabled = true;
            }
            else if (VisibleCOntrolEnabled == true)
            {
                ControlIsReadOnly = false;
                HiddenControlEnabled = true;
                VisibleCOntrolEnabled = false;

                CatDescription = SelectedCategory.Description;
                catName = SelectedCategory.Name;
                CatID = SelectedCategory.CategoryItemId;

                RaisePropertyChanged("CatDescription");
                RaisePropertyChanged("catName");
                RaisePropertyChanged("CatID");
            }
            RaisePropertyChanged("ControlIsReadOnly");
            RaisePropertyChanged("HiddenControlEnabled");
            RaisePropertyChanged("VisibleCOntrolEnabled");

        }

        void GetCategories()
        {
            Categories.Clear();
            foreach (var item in _ServiceProxy.GetCategories())
            {
                Categories.Add(item);
            }
        }

        /// <summary>
        /// Add new record to the database
        /// </summary>
        void AddCategory()
        {
            Categories.Add(Cat);
            _ServiceProxy.CreateCategories(Cat);
            RaisePropertyChanged("Cat");

            MessageBox.Show(Cat + " has been succesfully added");

        }



        /// <summary>
        /// Opens the Add CategoryWindow
        /// </summary>
        void OpenAddCategoryWindow()
        {
            //var win = new AddSupplierWindowView();
            //win.ShowDialog();
        }

        /// <summary>
        /// delete selected record
        /// </summary>
        void DeleteCategory()
        {
            var Result = MessageBox.Show("Delete " + SelectedCategory.Name, "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (Result)
            {
                case MessageBoxResult.Yes:
                    _ServiceProxy.DeleteCategories(SelectedCategory);
                    Categories.Remove(SelectedCategory);
                    MessageBox.Show("Deleted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetCategories();
                    RaisePropertyChanged("SelectedCategory");
                    RaisePropertyChanged("Categories");
                    break;
                case MessageBoxResult.No:

                    break;
                default:
                    break;
            }

        }

        void UpdateSupplier()
        {
            if (SelectedCategory != null)
            {

                Categories.Add(SelectedCategory);
                OldCategory.Description = CatDescription;
                OldCategory.Name = CatName;
                OldCategory.CategoryItemId = CatID;
                Categories.Remove(OldCategory);
                _ServiceProxy.UpdateCategories(SelectedCategory, OldCategory);
                GetCategories();
                MessageBox.Show("Updated");
                RaisePropertyChanged("SelectedCategory");
                ToggleControl();
            }
        }

        #endregion Methods

    }
}
