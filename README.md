# FileExplorer v0.5
<br>

## What is it?
FileExplorer is a customizable file explorer built for Unity3d.

#### Features
1. Provides file exploration window in different styles (only [OS X list style] (https://dl.dropboxusercontent.com/u/27907965/images/Screen%20Shot%202015-01-08%20at%20%E4%B8%8B%E5%8D%883.18.56.png) currently, more styles are planning to come).
2. Customizable for UI interaction.
  
<br><br>

## How to use?
#### 1. Open a list style File Explorer window:

```csharp
using FileExplorer.Ex;
//...
public void OpenFileExplorer () {
  WindowController controller = new WindowController();     // Basic window controller provided in the library
  FileExplorerEx.Open(controller, WindowStyle.List);
}
```
<br>

#### 2. Customize UI interaction

Create a class inherits from WindowController.

```csharp
// WindowController.cs

public class WindowController {
  //...
  
  // Override these functions to customize.
	public virtual void OnStart () {}
	public virtual void OnFileHighlighted (string path) {}
	protected virtual void OnCancelButtonPressed (Button cancelButton) {
		FileExplorerEx.Close();
	}
	protected virtual void OnOtherButtonPressed (Button otherButton) {}
}
```

