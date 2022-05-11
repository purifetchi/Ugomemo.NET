using System;
using System.IO;
using BinaryBitLib;
using Ugomemo.NET.Exceptions;

namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        private static readonly char[] MAGIC_STRING = { 'P', 'A', 'R', 'A' };
        private const uint FORMAT_VERSION = 0x24;

        /// <summary>
        /// Constant epoch, always at the 1st of January 2000.
        /// </summary>
        private static readonly DateTimeOffset EPOCH = DateTimeOffset.FromUnixTimeSeconds(946684800);

        private void Parse(string filename)
        {
            using var fileStream = new FileStream(filename, FileMode.Open);
            using var bitReader = new BinaryBitReader(fileStream);
            bitReader.Encoding = System.Text.Encoding.ASCII;

            ParseHeader(filename, bitReader);
            ParseMetadata(bitReader);

            Thumbnail = new Thumbnail(bitReader);

            ParseAnimationHeader(bitReader);
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

        /// <summary>
        /// Parses the metadata of the flipnote.
        /// </summary>
        private void ParseMetadata(BinaryBitReader reader)
        {
            Locked = reader.ReadUInt(16) == 1;

            // TODO: Parse all of the skipped data.
            //       - Author name
            //       - Author ID
            //       - Filename
            reader.ReadBytes(136);

            var timestamp = reader.ReadUInt();
            CreatedOn = EPOCH.AddSeconds(timestamp);

            // NOTE: The last 2 bytes of the metadata are always null and ignored.
            reader.ReadUInt(16);
        }

        /// <summary>
        /// Parses the animation header of the flipnote.
        /// </summary>
        private void ParseAnimationHeader(BinaryBitReader reader)
        {
            var frameOffsetTableSize = reader.ReadUInt(16);
            reader.ReadUInt(32);

            var flags = reader.ReadUInt(16);
            AnimationInfo = new AnimationInfo
            {
                Looping = (flags & 0x2) != 0,
                HideLayer1 = (flags & 0x10) != 0,
                HideLayer2 = (flags & 0x20) != 0
            };

            ParseAnimationFrameOffsetTable(reader, frameOffsetTableSize);
        }

        /// <summary>
        /// Parses the animation frame offset table of the flipnote.
        /// </summary>
        private void ParseAnimationFrameOffsetTable(BinaryBitReader reader, uint size)
        {
            // TODO: Parse the animation frame offset table.
        }
    }
}
