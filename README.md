StyleCopMagic
=============

Experimental StyleCop fixer built using Roslyn.

Compiling StyleCopMagic
-----------------------

As it uses the latest Roslyn CTP, then you'll need Visual Studio 2012. The desktop express version will do.

Running StyleCopMagic
---------------------

Currently the only way to run StyleCopMagic is through the commandline StyleCopMagicCmd:

    StyleCopMagicCmd.exe ProjectFile [Filename1.cs] ... [FilenameN.cs]

ProjectFile may be a .csproj or .sln file.

If no filenames are specified then all files in the project/solution will be processed.

Supported Rules
---------------

- SA1005
- SA1101
- SA1200
- SA1201
- SA1202
- SA1203
- SA1400
- SA1503
- SA1633
- SA1642

Project Status
--------------

The project, like Roslyn itself is a moving target, very much a work in progress. Don't run it on anything you can't revert!