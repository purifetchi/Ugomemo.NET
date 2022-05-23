using System.IO;
using System.Reflection;
using System.Text;
using BinaryBitLib;

namespace Ugomemo.NET.IO
{
    /// <summary>
    /// This exists only due to a limitation with BinaryBitLib's implementation of a bit reader
    /// which doesn't allow us to properly seek. This implements a *very* crude seeking mechanism
    /// using reflection.
    /// 
    /// TODO: This really shouldn't stay here for long, this is only a crude fix to a dumb problem
    ///       that will stay here until I figure out a better solution.
    /// </summary>
    internal class BinaryBitReaderEx : BinaryBitReader
    {
        private FieldInfo bufferPositionFieldInfo;

        public BinaryBitReaderEx(Stream baseStream, Encoding encoding) 
            : base(baseStream, encoding)
        {
            GetReflectedFields();
        }

        /// <summary>
        /// Gets the fields required for reflected seeking.
        /// </summary>
        private void GetReflectedFields()
        {
            bufferPositionFieldInfo = GetType().BaseType.GetField("_bufferCurrentPosition", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Seeks to the specified position from the start of the buffer.
        /// </summary>
        /// <param name="bytes">The amount of bytes to seek by.</param>
        public void Seek(long bytes)
        {
            // Force refresh the bit reader's internal buffer.
            BufferSize -= 1;
            BufferSize += 1;

            // Seek within the stream and set the buffer position to be 0.
            BaseStream.Position = bytes;
            bufferPositionFieldInfo.SetValue(this, 0);
        }
    }
}
