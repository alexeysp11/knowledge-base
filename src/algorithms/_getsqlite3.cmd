@echo off 

echo Downloading C/C++ external libraries 
if not exist packages mkdir "packages" 

echo sqlite3...
if not exist packages/sqlite3 mkdir "packages\sqlite3" 
if not exist packages/sqlite3/src mkdir "packages\sqlite3\src" 
if not exist packages/sqlite3/bin mkdir "packages\sqlite3\bin" 
if exist packages/sqlite3/src/shell.c (
    if exist packages/sqlite3/src/sqlite3.c (
        if exist packages/sqlite3/src/sqlite3.h (
            goto :sqlitedownloaded 
        ) else goto :downloadsqlite 
    ) else goto :downloadsqlite 
) else goto :downloadsqlite 
:downloadsqlite 
(
    curl -o packages\sqlite3\src\sqlite3.zip https://www.sqlite.org/2022/sqlite-amalgamation-3400100.zip
    tar -xf packages\sqlite3\src\sqlite3.zip 
    move "sqlite-amalgamation-3400100\shell.c" packages\sqlite3\src\shell.c
    move "sqlite-amalgamation-3400100\sqlite3.c" packages\sqlite3\src\sqlite3.c
    move "sqlite-amalgamation-3400100\sqlite3.h" packages\sqlite3\src\sqlite3.h
    del /S /Q "sqlite-amalgamation-3400100" && rmdir "sqlite-amalgamation-3400100"
    del /S /Q packages\sqlite3\src\sqlite3.zip 
)
:sqlitedownloaded
(
    echo sqlite3 package is downloaded 
)

echo Building C/C++ external libraries  
gcc packages\sqlite3\src\sqlite3.c packages\sqlite3\src\shell.c -lm -o packages\sqlite3\bin\sqlite3.exe 
echo C/C++ external libraries are built   

echo Creating SQLite database folder 
if not exist data mkdir "data" 
if not exist data/sqlite3 mkdir "data\sqlite3" 
echo SQLite database folder crreated 
