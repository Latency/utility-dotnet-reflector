# Redgate .NET Reflector License Generator
Allows you to generate and activate a copy of [Redgate .NET Reflector 11.1](https://www.red-gate.com/products/dotnet-development/reflector/) using offline manual activation.

<br />

---

<br />

# About
Developer holds no responsibility with what people decide to do with this app. It was developed strictly for demonstration purposes only.
Developed under the following conditions:
- Visual Studio 2022 (17.13.6)
- v9.0 .NET (Core)

<br />

---

<br />

# Usage
Download the project repo and open with Visual Studio.
Executable in `/bin/` folder.
If building your own version, you must ensure the following three files are in the same directory with each other:
- /bin/`ReflectorKG.exe`
- /bin/`ReflectorKG.exe.config`
- /bin/`readme.md`

<br />

---

<br />

# launchSettings.json
This file holds default values that the app uses when launching. You shouldn't need to modify these, but they're provided just in case:
```json
"environmentVariables": {
    "users_min_default": "1",
    "users_max_default": "30",
    "version_default": "3",
    "edition_default": "VSPro",
    "editions_list": "Standard,VS,VSPro",
    "ClientSettingsProvider.ServiceUri": ""
}
```
Don't modify these unless you know what you're doing, improperly configured, the Activation and Response will not generate into a valid serial key.

<br />

---

<br />

# Previews
![Main Interface](https://i.imgur.com/UAaaZEf.png)
![About](https://i.imgur.com/bHaMotZ.png)
![Activation 1](https://i.imgur.com/8geBMbu.png)
![Activation 2](https://i.imgur.com/Q2pwM75.png)
![.NET Reflector Registered](https://i.imgur.com/k3fQ7hJ.png)
