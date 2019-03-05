# Serialize - by Stoic Dreams

```
Pre-release - Expect possible breaking changes throughout development until 1.0 release.
```
Simple abstract helper methods to convert between C# classes, string data formats, and binary storage.

## Use Cases
This project was created to standardize and simplify specific use cases we have run into in our projects over the years.
* Loading configuration data into projects.
* Serializing and deserializing data that needs to be stored to remember states.
* Serializing and deserializing data that needs to be synced to other devices.
* Loading configuration data from multiple files where the config file in the parent folder is overwritten by config files from the child folders.

## Current Features
* JSON Serializer - Convert between JSON formatted text and class objects.
* Load object from File - Allows scanning folders for and loading 

## Planned Features
* XML Serializer - Convert between XML formatted text and class objects.
* Binary Serializer - Convert between binary formats and class objects.


## Getting Started

Install StoicDreams.Serialize library using [Nuget Package](https://www.nuget.org/packages/StoicDreamsSerialize).

## Contributing

We are not currently accepting contributions to this project. But if you'd like to provide feedback or ask questions, please visit [StoicDreams.com](https://www.stoicdreams.com/home) and drop us a comment through our Feedback module.

## Author

* **[Erik Gassler](https://www.erikgassler.com/home)** - Just a simpleton who likes making stuff with bits and bytes. Visit [my Patreon page](https://www.patreon.com/erikgassler) if you would like to provide support.

## License

This project is [public domain](LICENSE.md)

## Acknowledgments

* [Newtonsoft](https://www.newtonsoft.com/json) - Great, and robust library for JSON serialization and deserialization.
