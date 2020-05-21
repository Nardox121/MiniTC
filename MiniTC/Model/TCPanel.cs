using System;
using System.IO;
using System.Windows;

namespace MiniTC.Model
{
    class TCPanel
    {
        public string path { set; get; }
        public string[] files { set; get; }

        private string dirSymbol = "<D> ";
        private string backSymbol = "...";


        public TCPanel(string Path)
        {
            this.path = Path;
        }

        public static string[] getDisks()
        {
            return Directory.GetLogicalDrives();
        }

        public void update(string Path)
        {
            this.path = Path;
            int isNotDisk = 1;
            foreach(string disk in getDisks())
            {
                if (disk.Equals(Path))
                    isNotDisk=0;
            }
            string[] dirs = Directory.GetDirectories(Path);
            string[] fils = Directory.GetFiles(Path);
            int d = dirs.Length+isNotDisk;
            int f = fils.Length;
            string[] all = new string[d+f];
            if (isNotDisk == 1)
                all[0] = backSymbol;
            for (int i=isNotDisk;i<d;i++)
            {
                all[i] = dirSymbol + dirs[i-isNotDisk].Substring(path.Length,dirs[i-isNotDisk].Length-path.Length);
            }
            for (int i = d; i < d+f; i++)
            {
                all[i] = fils[i-d].Substring(path.Length, fils[i-d].Length - path.Length);
            }
            this.files = all;
        }

        public string clickPath(string fileName)
        {
            if (fileName.Equals(backSymbol))
                return Directory.GetParent(this.path.Substring(0,this.path.Length-2)).ToString()+"\\";
            return this.path + fileName.Substring(dirSymbol.Length, fileName.Length-dirSymbol.Length)+"\\";
        }

        public bool enable(string Path)
        {
            if (Path.Equals(backSymbol))
                return true;
            if (Path.Substring(0, dirSymbol.Length).Equals(dirSymbol))
                return true;
            return false;
        }

        public bool isCopyable(string Path)
        {
            if (Path.Equals(null))
                return false;
            if (Path.Equals(backSymbol))
                return false;
            if (Path.Substring(0, dirSymbol.Length).Equals(dirSymbol))
                return false;
            return true;
        }

        public void copy(string fileName, string destination)
        {
            string file = this.path + fileName;
            destination = destination + "\\" + fileName;
            try
            {
                File.Copy(file, destination);
            }
            catch(Exception e)
            {
                MessageBox.Show("Błąd: " + e.Message,"Uwaga!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
