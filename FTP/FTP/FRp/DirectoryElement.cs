using System.Collections.Generic;


namespace FRp
{
    class DirectoryElement
    {
        public List<DirectoryElement> subdirectories;
        public string Name { get;}
        public bool isFolder { get;}

        public DirectoryElement(string name,bool is_folder)
        {
            Name = name;
            isFolder = is_folder;
            subdirectories = new List<DirectoryElement>();
        }

        public void addElem(DirectoryElement elem)
        {
            subdirectories.Add(elem);
        }
    }
}
