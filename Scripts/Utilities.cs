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
				// This results the function returns empty string, 
				// as MyComputer is not a physical concept.
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
			List<FileSystemInfo> filesAndDirectories = new List<FileSystemInfo>();

			// IMPORTANT: A null or empty string path results in the root directories of all logical drives be returned.
			if (directoryPath == null || directoryPath.Equals("")) {
				string[] drives = Directory.GetLogicalDrives ();
				foreach (string drive in drives) {
					DirectoryInfo info = new DirectoryInfo (drive);
					if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
						filesAndDirectories.Add (info);
					}
				}
			} 
			else {
				DirectoryInfo directory = new DirectoryInfo (directoryPath);
				
				FileSystemInfo[] array = directory.GetFileSystemInfos ();
				
				if (!includeHidden) {
					foreach (FileSystemInfo fileOrDirectory in array) {
						if ((fileOrDirectory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
							filesAndDirectories.Add (fileOrDirectory);
						}
					}
				}
			}
			
			return filesAndDirectories;
		}
	}
	
}