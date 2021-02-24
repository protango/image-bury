using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Image_Bury
{
    public class BitStream : IDisposable
    {
        private byte currentByte = 0;
        private int position = 0;
        private Stream sourceStream;

        public int Position { get => position; }

        public BitStream(Stream sourceStream)
        {
            this.sourceStream = sourceStream;
        }

        public bool? ReadBit() {
            int byteIndex = position % 8;
            if (byteIndex == 0) {
                int nextByte = sourceStream.ReadByte();
                if (nextByte == -1) return null;
                else currentByte = (byte)nextByte;
            }

            position++;
            return (currentByte & (1<<byteIndex)) == 1;
        }

        public bool[] ReadBits(int bitsToRead)
        {
            var result = new bool[bitsToRead];
            for (int i = 0; i < bitsToRead; i++) {
                var bit = ReadBit();
                if (bit == null) {
                    result = result.Take(i).ToArray();
                    break;
                }

                result[i] = bit.Value;
            }

            return result;
        }

        public void Dispose()
        {
            sourceStream.Dispose();
        }
    }
}
