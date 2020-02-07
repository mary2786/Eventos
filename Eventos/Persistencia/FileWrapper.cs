﻿using System.IO;

namespace Eventos.Persistencia
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
