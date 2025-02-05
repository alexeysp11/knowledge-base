# simple-config-reader-cs

[English](README.md) | [Русский](README.ru.md)

A program that can import data from different sources into objects of the `Configuration` class.
The class should look like this:
```C#
public class Configuration
{
     public string Name { get; set; }
     public string Description { get; set; }
}
```
At a minimum, the program should accept files of two formats as input - let them be xml and csv. Each file contains one Configuration object. The specific file structure is not important. The important thing is that after processing the file, the program is able to obtain the Configuration object.

For example, here is the file structure:
```XML
<?xml version="1.0" encoding="utf-8"?>
<config>
     <name>Configuration 1</name>
     <description>Description of Configuration 1</description>
</config>
```

```CSV
Configuration 2;Description of Configuration 2
```

After processing each new file, the program displays a list of all available Configurations. As part of a test task, this list can simply be stored in memory (for example, a public static variable).

The program itself can be implemented as a console application. The selection of files can be done without user intervention, but simply sorting through files in a previously known folder.
