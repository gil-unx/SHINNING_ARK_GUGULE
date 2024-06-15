Kruptar 7
=========
Utility for text resources extraction, editing and insertion.


Features
========
Kruptar 7 allows extracting and editing text resources of almost any
format, including dictionary compression schemes (DTE, MTE). Text
data compressed by any other algorithm can be extracted into clear
and easy to edit plain text, but this process requires plug-in
creation, which supposes you can code Object Pascal dlls. For the
user's convenience multiple groups of text blocks can be created
during extraction process. Each block can have different text data
formats, encoding tables and pointers formats, which allows storing
all your text in one project file.
Kruptar 7 has convenient editing environment: tabs, text visualization,
and functional search, ability to work with different encodings:
SHIFT-JIS, UTF-16, etc. All these editing functions makes Kruptar a lot
better replacement for your notepad. But you can extract text into
simple *.txt file anytime: Kruptar 7 project file (*.kpx) is a simple
ZIP archive, which also contains encoding tables (*.tbl) and plain text
files (*.txt) inside. Kruptar 7 is notable for an excellent pointer
usage. Of course all pointers are recalculated automatically during
text insertion and text strings are inserted optimally into given space.
The Kruptar's flexible system which works with pointers also needs to
be pointed out: almost all possible pointer formats are implemented
here. Text insertion is a one click business and it is committed pretty
fast (the process is constantly optimizing from version to version).
That allows you to view the result instantly.
Unfortunately main disadvantage of Kruptar 7 is lack of full and
detailed documentation in English.


Documents and tutorials (in Russian)
====================================
"Kruptar for dummies" by Delex:
	http://www.magicteam.net/krupdoc.htm
"Plug-in coding for Kruptar 7" by Griever:
	http://www.magicteam.net/docs/kruptar/doc.htm


Updates
=======
The latest version can be found on our site.
http://www.magicteam.net


Special thanks
==============
NADVooDoo, MoonLight, Virtual_Killer, Delex, 
Griever, HoRRoR, Oraculum


Versions history
==============
31.12.2010, v7.1.1.14:	- Added SNES Lo-ROM pointer format
			  support (property ptSNESlorom).
			  Added ascending and descending
			  pointers' sort.
			  Changed behaviour of ptSplittedPtrs:
			  if pointer size is 2, low and high
			  bytes are separated;
			  if pointer size is 3, low word and
			  high byte are separated;
			  if pointer size is 4, low and hight
			  words are separated.
			  ptInterval value can be negative now.

05.12.2009, v7.1.1.10:	- The "\n" code now can be used
		 	  in TBL items as line break for
			  extracted text.			  

15.09.2009, v7.1.1.8	- Fixed text insertion when
			  pnFixed = True

06.08.2009, v7.1.1.7:	- Menu reworked, added
			  multilanguage support.
			- Project saving and loading
			  code rewritten.
			- Many of code optimized.
			- Added support of all
			  Windows codepages.
			- Now it's possible to save and
			  load from files or from Clipboard
			  most of all project elements.
			- In the 'GetData' plugin function
			  now is posible to use 'ROM' array.
			- Added pointers list property 
			  ptNotInSource. If it is TRUE
			  the text from source file
			  will not be extracted.
		  	- Renamed some fields in
		  	  project properties:
		  	  kpInputRom -> kpSourceROM;
		  	  kpOutputRom -> kpDestinationROM;
		  	  kpCoding -> kpCodepage;
		  	  ptMotorola -> ptBIG_ENDIAN;
		  	  ptPunType -> ptSplittedPtrs;
		  	  ptAutoStart -> ptAutoReference;
		  	  ptSigned -> ptAutoPtrStart;
		  	  etc.
		  	- Added two new pointer types:
		  	  1 signed & 2 signed.
		  	  Such pointer's value can be
		  	  negative.
		  	- Added save and text load option
		  	  from every tab - right click
		  	  pops up menu.
		  	- Fixed many bugs.
		  	
25.02.2009, v7.0.0.85:	- Added ROM launch in emulator.
			- Fixed ROM expansion function.
			- Fixed program behavior when
			  ptAlign is greater than 1.			  
			- Fixed some other issues.
			
19.06.2008, v7.0.0.78:	- Fixed some issues.

17.06.2008, v7.0.0.77:  - In pointer list properties added
			  ptSeekSame. If it is TRUE, during
			  pointer recalculation, the same
			  strings will be inserted in the same
			  place (earlier this option was always
			  switched on).
			- Fixed some issues.

06.06.2008, v7.0.0.75:	- Added command line support for
			  script insertion:
			  kruptar7.exe -i projectname.kpx
			- Fixed issue with insert error window
			  appearing after all groups insertion
			  (hotkey Ctrl+F9).
			- Fixed text insertion without pointers
			  while ptAutoStart is off.

09.04.2008, v7.0.0.62:	- Small fix.

09.04.2008, v7.0.0.61:	- Added "kpFlags" in project properties for different
			  flags and settings storage.
			- Added flag equal to 1 which sets first code in the 
			  line folding list as invisible.
			**After save in the current version project cannot  
			  be opened in previous Kruptar's versions.

19.03.2008, v7.0.0.59:	- Improved text insertion in multiple text blocks.
			- If text does not fit in specified space in the ROM, 
			  a list of specific groups and information regarding 
			  excessive size in bytes are shown.
			- Optimized recoding from SHIFT-JIS into
			  UTF-16 and vice versa.
			- Under the text window a size of recoded by
			  table string is shown.
			- If there is a number near the pointer, which 
			  is equal to string length in bytes, you can create 
			  a variable ("variable in a pointer list") and set 
			  a type "String Size". This variable will be 
			  automatically changed with string editing and it 
			  will be inserted with a pointer in the right 
			  place in the ROM.
			- Added "About".

18.03.2008, v7.0.0.32:	- Some fixes.

11.10.2006, v7.0.0.0:	- Program is rewritten from scratch.

* History of the previous versions is lost and the first one was 
  created for "Ranma 1/2: Treasure of The Red Cat Gang" translation project.


Readme translated from Russian by Griever.
© Magic Team, Djinn, 2004-2011
http://www.magicteam.net
magicdjinn@narod.ru