
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/Zencomic.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../ZencomicLib/bin/Debug/ZencomicLib.dll
BUILD_DIR = bin/Debug

if ENABLE_ENABLE_GLIB_TIMEOUT_SECOND
ASSEMBLY_COMPILER_FLAGS += -define:USE_GLIB_TIMEOUT_SECONDS
endif
ZENCOMICLIB_DLL_SOURCE=../ZencomicLib/bin/Debug/ZencomicLib.dll
ZENCOMICLIB_DLL=$(BUILD_DIR)/ZencomicLib.dll
ZENCOMICLIB_DLL_MDB_SOURCE=../ZencomicLib/bin/Debug/ZencomicLib.dll.mdb
ZENCOMICLIB_DLL_MDB=$(BUILD_DIR)/ZencomicLib.dll.mdb
ZENCOMIC_EXE_MDB_SOURCE=bin/Debug/Zencomic.exe.mdb
ZENCOMIC_EXE_MDB=$(BUILD_DIR)/Zencomic.exe.mdb
COMICADDINLIB_DLL=

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/Zencomic.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../ZencomicLib/bin/Release/ComicAddinLib.dll
BUILD_DIR = bin/Release

if ENABLE_ENABLE_GLIB_TIMEOUT_SECOND
ASSEMBLY_COMPILER_FLAGS += -define:USE_GLIB_TIMEOUT_SECONDS
endif
ZENCOMICLIB_DLL=
ZENCOMICLIB_DLL_MDB=
ZENCOMIC_EXE_MDB=
COMICADDINLIB_DLL_SOURCE=../ZencomicLib/bin/Release/ComicAddinLib.dll
COMICADDINLIB_DLL=$(BUILD_DIR)/ComicAddinLib.dll

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(ZENCOMICLIB_DLL) \
	$(ZENCOMICLIB_DLL_MDB) \
	$(ZENCOMIC_EXE_MDB) \
	$(COMICADDINLIB_DLL)  

BINARIES = \
	$(ZENCOMIC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	gtk-gui/generated.cs \
	Main.cs \
	AssemblyInfo.cs \
	StatusIcon.cs \
	PreferencesDialog.cs \
	Notifications.cs \
	gtk-gui/Zencomic.PreferencesDialog.cs \
	Config.cs \
	Screensaver.cs \
	ComicService.cs \
	LinkLabel.cs \
	PlatformUtils.cs 

DATA_FILES = 

RESOURCES = \
	gtk-gui/gui.stetic \
	data/zencomic.png,Zencomic.data.zencomic.png 

EXTRAS = \
	app.desktop \
	zencomic.in 

REFERENCES =  \
	System \
	Mono.Posix \
	$(MONO_ADDINS_GUI_LIBS) \
	$(MONO_ADDINS_LIBS) \
	System.Core \
	$(NDESK_DBUS_10_LIBS) \
	$(NDESK_DBUS_GLIB_10_LIBS) \
	System.Xml \
	$(GLIB_SHARP_20_LIBS) \
	$(GTK_SHARP_20_LIBS)

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

ZENCOMIC = $(BUILD_DIR)/zencomic

$(eval $(call emit-deploy-target,ZENCOMICLIB_DLL))
$(eval $(call emit-deploy-target,ZENCOMICLIB_DLL_MDB))
$(eval $(call emit-deploy-wrapper,ZENCOMIC,zencomic,x))
$(eval $(call emit-deploy-target,COMICADDINLIB_DLL))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
