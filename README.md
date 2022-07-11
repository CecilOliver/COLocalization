# CO Localization (Cecil Oliver's Localization)
### Introduction
This is an adaptation of [Game Dev Guide](https://www.youtube.com/c/GameDevGuide)'s [Building a Localization Tool in Unity](https://www.youtube.com/watch?v=c-dzg4M20wY) tutorials to allow the localization system to work with general C# projects.

---

### How to install:
1. Download `COLocalization.dll`
2. Reference `COLocalization.dll` to your project

---
### How to use:
##### `Localization.CURRENT_LANGUAGE` - Sets the default `Language`.

##### `Localization.CreateLocalizationFile(string path)` - Creates a CSV file at the give location. [NOTE] This function should not be in your code when shipping, this is just to setup localization.

##### `Localization.Init(string path, params Languages[] langs)` - Reads the `CSV` file that contains the localization data. `langs` is the list of languages that the localization file contains. This must be done before getting any localized values.

##### `Localization.GetValues(string key)` - Gets the localized value of `key` using the default language.

##### `Localization.GetValues(string key, Languages lang)` - Gets the localized value of `key` using language set as `lang`.

---
### Supported Languages:
You can find a complete list of the supported languages here: [LIST OF LANGUAGES](https://www.andiamo.co.uk/resources/iso-language-codes/)

---
### Credits
Adapted from [Game Dev Guide](https://www.youtube.com/c/GameDevGuide)'s [Building a Localization Tool in Unity](https://www.youtube.com/watch?v=c-dzg4M20wY) tutorials.
