# cs-screenshot-cli

Clone and build or use binary in `bin/release` .

```
screenshot_cli.exe pid=1234 output=path/to/file.png
```

Exit codes: 
```
Success = 0,
MissingPid = 1,
ProcessNotFound = 2,
MissingOutputPath = 8,
BadPidInput = 9,
UnknownError = 10
```
