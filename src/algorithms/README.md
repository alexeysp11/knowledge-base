# leetcode 

This repository contains solutions for **leetcode** problems in C#, Go, Python, JS and plain SQL. 

## Configuration 

In `cs` you can find `app.config` file. 
In this file you can easily specify, which algorithm will be executed, by setting parameter `ClassName`, for example: 
```XML
<?xml version="1.0" encoding="utf-8" ?>
<AppSettings>
    <ProgramName>leetcode</ProgramName>
    <ProgramDescription>Test program for solving leetcode problems</ProgramDescription>
    <ClassName>Studying.Leetcode.Graphs.RemovingIslands</ClassName>
</AppSettings>
```

## How to run 

If you want to run program in this repository, in most cases it's better to build the app and execute it as `exe` file (since you might use some libraries under the hood that need to compiled). So, go to the folder of a project (the folder name might be `cs`, `golang` etc) and execute the following command: 
```
run.cmd
```
This command creates `bin` folder, builds the app and executes it as `exe` file. 

## External libraries 

### SQLite

In order to work with SQLite databases, run the following command in CMD: 
```
_getsqlite3.cmd
```

This command allows you to download sqlite3 source files, compile them, and create `data/sqlite3` folder where SQLite databases will be stored. 

In order to run SQLite shell, type: 
```
_runsqlite3.cmd
```

Then you need use ".open FILENAME" to open on a persistent database. For example: 
```
.open data/sqlite3/EmployeeUniqueId.db
```

## Troubleshooting 

### Go 

If you're facing problem with gcc compliler: `runtime/cgo: C:\Program Files\Go\pkg\tool\windows_amd64\cgo.exe: exit status 2`, you need to take the following steps: 
1. Install `TDM-GCC` compiler from [GitHub repo](https://github.com/jmeubank/tdm-gcc) (go to releases and download exe installer) or from [TDM-GCC site](https://jmeubank.github.io/tdm-gcc/download/). 

2. Open command line and execute: 
```
go env GOENV
```
This command returns you a path to env file. 

3. Open env file in any text editor. 

4. Execute in command line:
```
where gcc
```
Copy the path of `TDM-GCC` compiler. 

5. Set `CC` parameter in env file to the path of `TDM-GCC` compiler. 

It's also possible to use [SWIG compiler](https://github.com/swig/swig) to compile C/C++ code in golang apps, but I haven't tested it yet. 
