# Decks

This is an implementation of generic behavior for a "deck" when we say deck, we mean the sort of component that you often find in 
board and card games.  While this is often cards, it may not be a Bicycle deck of playing cards or cards at all.  Often board games
have decks of tiles or chits that represent various game elements.  This tries to represent the behavior common for all of them.

## Getting Started

Decks is typically installed as a [NuGet Package](https://www.nuget.org/packages/Decks/).

### Prerequisites

The library is a .NET Standard 2.0 project.  It has very few dependencies.  Really it only has one project dependency and that's on
[Caliburn.Micro.Core](https://www.nuget.org/packages/Caliburn.Micro.Core/).  This project provides abstractions for a lot of 
application concepts.  In the contents of this project it supports the core precepts of a project change semantics in such a way that it
accounts for threading issues in the that the library is used in a UWP, WPF or Xamarin app.

## Running the tests

If you clone or fork the repository, you can run the projects contained in the *Decks.Tests* project.  


## Built With

* [Caliburn.Micro.Core](https://www.nuget.org/packages/Caliburn.Micro.Core/) - Property changed implementation

## Documentation

For a full description of this library and it's features please see the [wiki](https://github.com/MikeKenyon/Decks/wiki).

## Contributing

Please read [CodingStandards.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, 
and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/MikeKenyon/Decks/tags). 

## Authors

* **[Mike Kenyon](https://github.com/MikeKenyon)** - *Initial work* - 

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the GNU Verion 3 License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* The Caliburn Micro team for handling the heavy lifting in bizarre threading conditions when property changed handlers start interacting 
with display loops.
* All those whose games inspired features.
