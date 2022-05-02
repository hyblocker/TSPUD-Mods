# ModThatLetsYouMod 

> This page is intended for developers. If you are not a developer, [please go here](https://github.com/hyblocker/TSPUD-Mods)).

This mod is designed as a **CoreMod** for TSPUD. It's designed to facilitate interfacing with the game by abstracting commonly used APIs into handy, optimised functions.

>  ⚠ **THIS MOD IS CURRENTLY IN EARLY DEVELOPMENT. BREAKING API CHANGES WILL OCCUR. JOIN [THE MODDING DISCORD](https://discord.gg/5pwNxfcxnU) TO HELP GUIDE THIS MOD'S DIRECTION** 

## Setup

To setup your development environment, [clone the TSPUD Mod template](https://github.com/hyblocker/TSPUD-Mods-Template), and follow the setup instructions from there. Then download the [latest release]() and add it to your Libraries folder.

Edit the `Directory.Build.Props` file found in the project root, and add this section:

```xml
<Reference Include="ModThatLetsYouMod, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null">
	<SpecificVersion>false</SpecificVersion>
	<Private>false</Private>
	<HintPath>$(MsBuildThisFileDirectory)Libraries\ModThatLetsYouMod.dll</HintPath>
</Reference>
```

at the bottom before the closing `</ItemGroup` tag.

## Usage

### Settings API

Settings are an object which can be saved and loaded for persistent variables. These don't necessarily have to be settings, and can be used for story variables too. Settings get stored as JSON files in the `UserData` directory.

- Create a new object, with publicly exposed properties:

```cs
public class MySettings {
    public bool AnAwesomeSetting = true; // anything with an equals is the default value
}
```

- In your main file:

```cs

public static MySettings modSettings;

public override void OnApplicationStart()
{
	Settings.Initialize(out modSettings);
}
```

## Mods Menu

Currently WIP. Any bug reports will be ignored.