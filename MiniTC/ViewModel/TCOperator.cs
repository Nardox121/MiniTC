using System.IO;
using System.Windows.Input;
using System.Windows;
using System;

namespace MiniTC.ViewModel
{
    using Model;
    using Base;

    internal class TCOperator : ViewModelBase
    {
        TCPanel l = new TCPanel(Directory.GetLogicalDrives()[0]);
        TCPanel r = new TCPanel(Directory.GetLogicalDrives()[0]);

        public TCOperator()
        {
            this.Disks = TCPanel.getDisks();
            this.SelectedDiskL = l.path;
            this.SelectedDiskR = r.path;
        }

        string[] disks;
        string pathL;
        string pathR;
        string[] filesL;
        string[] filesR;
        string selectedFileL;
        string selectedFileR;
        string selectedDiskL;
        string selectedDiskR;

        public string PathL
        {
            get
            {
                return pathL;
            }
            set
            {
                pathL = value;
                updateL(value);
                onPropertyChanged(nameof(PathL));
            }
        }

        public string PathR
        {
            get
            {
                return pathR;
            }
            set
            {
                pathR = value;
                updateR(value);
                onPropertyChanged(nameof(PathR));
            }
        }

        public string[] Disks
        {
            get
            {
                return disks;
            }
            set
            {
                disks = value;
                onPropertyChanged(nameof(Disks));
            }
        }

        public string[] FilesL
        {
            get
            {
                return filesL;
            }
            set
            {
                filesL = value;
                onPropertyChanged(nameof(FilesL));
            }
        }


        public string SelectedFileL
        {
            get
            {
                return selectedFileL;
            }
            set
            {
                selectedFileL = value;
                onPropertyChanged(nameof(SelectedFileL));
            }
        }

        public string SelectedDiskL
        {
            get
            {
                return selectedDiskL;
            }
            set
            {
                try
                {
                    selectedDiskL = value;
                    PathL = value;
                    onPropertyChanged(nameof(SelectedDiskL));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Błąd: " + e.Message, "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public string[] FilesR
        {
            get
            {
                return filesR;
            }
            set
            {
                filesR = value;
                onPropertyChanged(nameof(FilesR));
            }
        }


        public string SelectedFileR
        {
            get
            {
                return selectedFileR;
            }
            set
            {
                selectedFileR = value;
                onPropertyChanged(nameof(SelectedFileR));
            }
        }

        public string SelectedDiskR
        {
            get
            {
                return selectedDiskR;
            }
            set
            {
                try
                {
                    selectedDiskR = value;
                    PathR = value;
                    onPropertyChanged(nameof(SelectedDiskR));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Błąd: " + e.Message, "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        ICommand copy = null;
        public ICommand Copy
        {
            get
            {
                if (copy == null)
                {
                    copy = new RelayCommand(
                        arg =>
                        {
                            l.copy(selectedFileL, PathR);
                            updateR(PathR);
                        },
                        arg =>
                        {
                            if (selectedFileL != null)
                            {
                                return l.isCopyable(selectedFileL);
                            }
                            return false;
                        }
                        );
                }
                return copy;
            }
        }

        ICommand doubleClickL = null;
        public ICommand DoubleClickL
        {
            get
            {
                if (doubleClickL == null)
                {
                    doubleClickL = new RelayCommand(
                        arg =>
                        {
                            try
                            {
                                PathL = l.clickPath(selectedFileL);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Błąd: " + e.Message, "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        },
                        arg => l.enable(selectedFileL)
                        );
                }
                return doubleClickL;
            }
        }

        ICommand doubleClickR = null;
        public ICommand DoubleClickR
        {
            get
            {
                if (doubleClickR == null)
                {
                    doubleClickR = new RelayCommand(
                        arg =>
                        {
                            try
                            {
                                PathR = r.clickPath(selectedFileR);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("Błąd: " + e.Message, "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        },
                        arg => r.enable(selectedFileR)
                        );
                }
                return doubleClickR;
            }
        }
        //przyznaje że podzielenie tego wszystkiego na R i L pewnie można było zrobić lepiej
        void updateL(string Path)
        {
            this.l.update(Path);
            this.FilesL = l.files;

        }
        void updateR(string Path)
        {
            this.r.update(Path);
            this.FilesR = r.files;
        }

    }
}
