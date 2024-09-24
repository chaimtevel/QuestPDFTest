Run this snippet to add lots of files:

```powershell
( 1 .. 500 ) | % { New-Item .\testfile$_.ttf  }
```
