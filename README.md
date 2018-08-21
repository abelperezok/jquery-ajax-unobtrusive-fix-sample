# jquery-ajax-unobtrusive-fix-sample
This repository contains the source code of a working example of the fix to jquery-ajax-unobtrusive to support input type files.

This is a result of the pull request https://github.com/aspnet/jquery-ajax-unobtrusive/pull/1 being reactivated and followed up recently.

## Set up

```shell
dotnet build
dotnet run
```

* Browse to https://localhost:5001/TestUpload/AjaxPost 
* Upload a sample file
* Find it under ~/wwwroot/_temp_upload
* Test download the file after being uploaded

Just for debugging purposes and to compare, there is another page https://localhost:5001/TestUpload/NormalPost which doesn't use AJAX at all.

