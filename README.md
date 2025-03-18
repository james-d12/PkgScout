# PkgScout

PkgScout collects package information from a variety of sources. It is in prototyping stage. So nothing really of use
just yet. It uses a separate package called [OsScout](https://github.com/james-d12/OsScout) to get specific Operating
System information.

# Getting Started

You can run PkgScout by first building it then running the executable. For example below searches the downloads folder
for the user called 'User'. It will go through all files recursively and try to extract packages from various known
sources.

```./PkgScout.exe search C:\Users\User\Downloads```