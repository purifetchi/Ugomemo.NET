﻿using System.IO;
using Ugomemo.NET.Exceptions;
using BinaryBitLib;

namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        private static readonly char[] MAGIC_STRING = { 'P', 'A', 'R', 'A' };
        private const uint FORMAT_VERSION = 0x24;

        private void Parse(string filename)
        {
            using var fileStream = new FileStream(filename, FileMode.Open);
            using var bitReader = new BinaryBitReader(fileStream);
            bitReader.Encoding = System.Text.Encoding.ASCII;

            ParseHeader(filename, bitReader);
        }

        /// <summary>
        /// Parses the header of the flipnote.
        /// </summary>
        private void ParseHeader(string filename, BinaryBitReader reader)
        {
            if (reader.ReadByte() != MAGIC_STRING[0] ||
                reader.ReadByte() != MAGIC_STRING[1] ||
                reader.ReadByte() != MAGIC_STRING[2] ||
                reader.ReadByte() != MAGIC_STRING[3])
            {
                throw new NotAFlipnoteException($"{filename} is not a flipnote - invalid magic!");
            }

            AnimationDataSize = reader.ReadUInt();
            SoundDataSize = reader.ReadUInt();
            FrameCount = reader.ReadUInt(16);

            var formatVersion = reader.ReadUInt(16);
            if (formatVersion != FORMAT_VERSION)
                throw new NotAFlipnoteException($"{filename} is not a flipnote - invalid format version!");
        }
    }
}