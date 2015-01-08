using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer {

	public static class Utilities {
		public static string GetUserRoot () {
			return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
		}

		public static List<FileSystemInfo> GetFilesInDirectory (string directoryPath, bool includeHidden = false) {
			DirectoryInfo directory = new DirectoryInfo(directoryPath);

			FileSystemInfo[] array = directory.GetFileSystemInfos();
			List<FileSystemInfo> filesAndDirectories = new List<FileSystemInfo>();

			if (!includeHidden) {
				foreach (FileSystemInfo fileOrDirectory in array) {
					if ((fileOrDirectory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
						filesAndDirectories.Add(fileOrDirectory);
					}
				}
			}

			return filesAndDirectories;
		}
	}

}