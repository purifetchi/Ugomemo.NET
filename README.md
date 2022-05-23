# Ugomemo.NET
**A C# library for parsing Flipnote Studio .PPM files.**

## Features
* Parser implementation for Flipnote Studio's PPM file format, including (some of the) metadata, animation frames, thumbnails.
* Integration with ImageSharp for drawing the frames into images.

## Todo
* Reading and parsing of music and sound effects.
* Reading the rest of the flipnote's meta information.
* Ability to export flipnotes into GIFs and video files.
* Verification of flipnote signatures.
* Support for Flipnote Studio 3D's .KWZ files.
* Turning this into a NuGet package.
* Replacing BinaryBitLib.

## Contributing Guide
If you'd really want to contribute to this project (thank you!) please adhere to the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) commit format as much as you can. 

Also if it's possible, please write a test for any new parsing/functionality that you've added.

## Resources

* Rakujira's fantastic [flipnote.js](https://github.com/jaames/flipnote.js) library.
* [FlipnoteCollective's flipnote format documentation](https://github.com/Flipnote-Collective/flipnote-studio-3d-docs/).