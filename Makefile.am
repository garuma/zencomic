
EXTRA_DIST =  expansions.m4

#Warning: This is an automatically generated file, do not edit!
if ENABLE_DEBUG
 SUBDIRS =  ZencomicLib DilbertComicAddin XkcdComicAddin GarfieldComicAddin GarfieldMinusGarfieldComicAddin GeekscottesComicAddin LolcatComicAddin TrollcatComicAddin Zencomic
endif
if ENABLE_RELEASE
 SUBDIRS =  ZencomicLib DilbertComicAddin XkcdComicAddin GarfieldComicAddin GarfieldMinusGarfieldComicAddin GeekscottesComicAddin LolcatComicAddin TrollcatComicAddin Zencomic
endif

run: all
	rm -rf bin
	mkdir bin
	find . -iname '*.dll' -or -iname '*.exe' -or -iname '*.mdb' | xargs -i -t cp ./{} bin
	mono bin/Zencomic.exe
