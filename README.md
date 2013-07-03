Crawl
=====
A free (as in freedom) and cross platform web crawler/spider written in Visual Basic. This program goes to a web page and returns all the links and or emails on said page. Depending on the given arguments, Crawl will either display the links on the screen or in a log file.

## Directories

	documents - Notes, manuals, or anything else that helps with using Crawl
	program   - The compiled version of the latest Crawl
	source	  - The uncompiled source files of the program

## Files

	documents
		notes.txt - ToDo's and other things I write down

	program
		linux
			crawl     - A script file to run Crawl
			Crawl     - The compiled Crawl program for *nix

		windows
			Crawl.exe - The compiled Crawl program for Windows

	source
		compile   - A script that compiles Crawl on GNU/Linux
		Crawl.vb  - The uncompile Visual Basic file of the program
## Installation

Note: Installation isn't required but is recommended

	Windows*  - Copy Crawl.exe to C:\Windows\System32 or equivlent (may require you to be admin)
	GNU/Linux - Copy crawl and Crawl to your /bin folder or equivlent (deppending on your distro), this may require root

## Executing (If installed)

	Windows*  - Open up cmd.exe and type crawl <args>
	GNU/Linux - Open up any terminal and type crawl <args>

## Executing (If not installed)

	Windows*  - Open cmd.exe and use the 'cd' command until you are in the same directory as Crawl, then type Crawl.exe <args>
	GNU/Linux - Open up a terminal window, cd to the programs directory, type mono Crawl <args>

## Compiling

	Windows*  - Open Visual Studios and click 'build' (you may have to create a project first)
	GNU/Linux - You can either run compile OR use vbnc2 (vbnc2 -c Crawl.vb)

## Dependencies

	Windows*  - .NET Framework 2.0 or higher (never been tested on 1.1)
	GNU/Linux - Mono 2.5 or higher (never been tested on older versions)

## Notes

Windows* = Any version of Windows as long as it can handle Visual Studios version that supports .NET Framework 2.0 or higher

GNU/Linux also means any Unix based operating system that can run Mono ( http://mono-project.com )
