using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer {

	public static class Utilities {
		public static string GetUserRoot () {
			System.Environment.SpecialFolder rootFolder;

			switch (Application.platform) {
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXWebPlayer:
				rootFolder = System.Environment.SpecialFolder.Personal;
				break;

			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.WindowsWebPlayer:
				rootFolder = System.Environment.SpecialFolder.MyComputer;
				break;

			case RuntimePlatform.LinuxPlayer:
				rootFolder = System.Environment.SpecialFolder.Personal;
				break;

			default:
				rootFolder = System.Environment.SpecialFolder.Personal;
				break;
			}

			return System.Environment.GetFolderPath(rootFolder);
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