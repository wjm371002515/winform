using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JCodes.Framework.Common
{
    /// <summary>
    /// 提供的方法来操纵数据流的辅助方法
    /// </summary>
    public sealed class StreamUtils
    {
        private StreamUtils() { }

        /// <summary>
        /// 复制字节数据
        /// </summary>
        /// <param name="input">输入字节数组</param>
        /// <param name="output">输出字节数组</param>
        /// <param name="outputOffset">字节偏移位置</param>
        public static void Copy(byte[] input, byte[] output, long outputOffset)
        {
            if (input.Length == 0) return;

            for (int i = 0; i < input.Length; i++)
            {
                output[outputOffset + i] = input[i];
            }
        }

        /// <summary> Copy bytes from a <see cref="byte"/>[] to an Output <see cref="Stream"/>.</summary>
        /// <param name="input">the byte array to read from
        /// </param>
        /// <param name="output">the Output <see cref="Stream"/> to write to
        /// </param>
        /// <exception cref="IOException">if an I/O problem occurs</exception>		
        public static void Copy(byte[] input, Stream output)
        {
            if (input.Length == 0) return;

            output.Write(input, 0, input.Length);
        }

        /// <summary>
        /// Copy and convert bytes from a <see cref="byte"/>[] to chars on a
        /// <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="input">the byte array to read from</param>
        /// <param name="outputWriter">the <see cref="StreamWriter"/> to write to</param>
        /// <exception cref="IOException">in the case of an I/O problem</exception>
        public static void Copy(byte[] input, StreamWriter outputWriter)
        {
            MemoryStream inputStream = new MemoryStream(input);
            Copy(inputStream, outputWriter);
        }

        /// <summary>
        ///  Copy and convert bytes from a <see cref="byte"/>[] to chars on a
        /// <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="outputWriter"></param>
        /// <param name="encoding"></param>
        public static void Copy(byte[] input, StreamWriter outputWriter, string encoding)
        {
            MemoryStream inputStream = new MemoryStream(input);
            Copy(inputStream, outputWriter, encoding);
        }

        /// <summary> Copy the entire content from an Input <see cref="Stream"/> to an Output <see cref="Stream"/>.</summary>
        /// <param name="input">the Input <see cref="Stream"/>to read from the beginning of the stream
        /// </param>
        /// <param name="output">the Output <see cref="Stream"/> to write to
        /// </param>        
        /// <returns> the number of bytes copied        
        /// </returns>
        /// <exception cref="IOException">if an I/O problem occurs</exception>		
        public static int Copy(Stream input, Stream output)
        {
            return Copy(input, output, false);
        }

        /// <summary> Copy bytes from an Input <see cref="Stream"/> to an Output <see cref="Stream"/>.</summary>
        /// <param name="input">the Input <see cref="Stream"/>to read from
        /// </param>
        /// <param name="output">the Output <see cref="Stream"/> to write to
        /// </param>
        /// <param name="copyFromBeginning">Set true to copy from the beginning of the input stream, eg. input.Position=0,
        /// otherwise, it will start copying from whatever the current position in the input stream. 
        /// </param>
        /// <returns> the number of bytes copied        
        /// </returns>
        /// <exception cref="IOException">if an I/O problem occurs</exception>
        public static int Copy(Stream input, Stream output, bool copyFromBeginning)
        {
            byte[] buffer = new byte[Const.BUFFSIZE];
            int bytesRead = 0;
            int len = Const.BUFFSIZE;
            int offset = 0;

            if (copyFromBeginning)
                input.Seek(0, SeekOrigin.Begin);

            // set it to the beginning            
            while (len > 0)
            {
                len = input.Read(buffer, offset, Const.BUFFSIZE);
                output.Write(buffer, 0, len);
                bytesRead += len;
            }

            return bytesRead;
        }
        
        // ----------------------------------------------------------------
        // Reader -> Writer
        // ----------------------------------------------------------------

        /// <summary> Copy chars from a <see cref="StreamReader"/>  to a <see cref="StreamWriter"/>.</summary>
        /// <param name="inputStreamReader">the <see cref="StreamReader"/> to read from
        /// </param>
        /// <param name="outputStreamWriter">the <see cref="StreamWriter"/> to write to
        /// </param>
        /// <returns> the number of characters copied
        /// </returns>        
        /// <exception cref="IOException">if an I/O problem occurs</exception>
        public static int Copy(StreamReader inputStreamReader, StreamWriter outputStreamWriter)
        {
            char[] buffer = new char[Const.BUFFSIZE];
            int count = 0;
            int len = Const.BUFFSIZE;

            while (len > 0)
            {
                len = inputStreamReader.Read(buffer, 0, Const.BUFFSIZE);
                outputStreamWriter.Write(buffer, 0, len);
                count += len;
            }
            return count;
        }

        // ----------------------------------------------------------------
        // InputStream -> Writer
        // ----------------------------------------------------------------

        /// <summary> Copy and convert bytes from an Input <see cref="Stream"/> to chars on a
        /// <see cref="StreamWriter"/>.
        /// The platform's default encoding is used for the byte-to-char conversion.
        /// </summary>
        /// <param name="inputStream">the Input <see cref="Stream"/> to read from
        /// </param>
        /// <param name="outputStreamWriter">the <see cref="StreamWriter"/>to write to
        /// </param>
        /// <exception cref="IOException">if an I/O problem occurs</exception>
        public static int Copy(Stream inputStream, StreamWriter outputStreamWriter)
        {
            StreamReader inputStreamReader = new StreamReader(inputStream, System.Text.Encoding.Default);
            return Copy(inputStreamReader, outputStreamWriter);
        }

        /// <summary>
        /// Copy and convert bytes from an Input <see cref="Stream"/> to chars on a
        /// <see cref="StreamWriter"/>, using the specified encoding.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="outputWriter"></param>
        /// <param name="encoding">The name of a supported character encoding. See the
        /// <a href="http://www.iana.org/assignments/character-sets">IANA
        /// Charset Registry</a> and <a href="http://msdn2.microsoft.com/en-us/library/system.text.encoding.aspx">MSDN: Encoding class</a>
        /// for a list of valid encoding types.</param>
        /// <exception cref="IOException">an I/O problem occurs</exception>
        public static int Copy(Stream inputStream, StreamWriter outputWriter, String encoding)
        {
            Encoding encode = Encoding.Default;

            try
            {
                encode = Encoding.GetEncoding(encoding);
            }
            catch
            {
                encode = Encoding.Default;
            }
            StreamReader inputStreamReader = new StreamReader(inputStream, encode);
            return Copy(inputStreamReader, outputWriter);
        }


        // ----------------------------------------------------------------
        // Reader -> OutputStream
        // ----------------------------------------------------------------

        /// <summary>
        /// Serialize chars from a <see cref="StreamReader"/> to bytes on an 
        /// Output <see cref="Stream"/>, and flush the Output <see cref="Stream"/>.
        /// </summary>
        /// <param name="inputReader">the <see cref="StreamReader"/> to read from</param>
        /// <param name="output">the  <see cref="Stream"/> to write to</param>
        /// <exception cref="IOException">an I/O problem occurs</exception>
        public static void Copy(StreamReader inputReader, Stream output)
        {
            StreamWriter outputWriter = new StreamWriter(output, System.Text.Encoding.Default);
            Copy(inputReader, outputWriter);

            outputWriter.Flush();
        }

        /// <summary> Serialize chars from a <see cref="String"/> to bytes on an 
        /// Output <see cref="Stream"/>, and
        /// flush the Output <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">the <see cref="String"/> to read from
        /// </param>
        /// <param name="output">the Output <see cref="Stream"/> to write to
        /// </param>        
        /// <exception cref="IOException">an I/O problem occurs</exception>
        public static void Copy(String input, Stream output)
        {
            byte[] inputByteArray = new ASCIIEncoding().GetBytes(input);
            StreamWriter outWriter = new StreamWriter(output, System.Text.Encoding.Default);
            Copy(inputByteArray, outWriter);

            outWriter.Flush();
        }

        /// <summary> Copy chars from a <see cref="String"/> to a <see cref="StreamWriter"/>.</summary>
        /// <param name="input">the <see cref="String"/> to read from
        /// </param>
        /// <param name="output">the <see cref="StreamWriter"/> to write to
        /// </param>
        /// <exception cref="IOException">an I/O problem occurs</exception>      
        public static void Copy(String input, StreamWriter output)
        {
            output.Write(input);
        }

        /// <summary>
        /// Copy the exact number of bytes from the source <see cref="Stream"/> to a
        /// target <see cref="Stream"/>.
        /// </summary>
        /// <param name="source">Source <see cref="Stream"/> to copy from</param>
        /// <param name="target">Target <see cref="Stream"/> to copy to</param>
        /// <param name="len">number of bytes to copy</param>        
        /// <exception cref="IOException">if the source stream does not have enough data.</exception>
        public static void CopyExact(Stream source, Stream target, int len)
        {
            byte[] buffer = new byte[Const.BUFFSIZE];
            int bytesRead = 0;

            while (bytesRead < len)
            {
                int sizeNeeded = Math.Min(buffer.Length, len - bytesRead);
                int readSize = source.Read(buffer, 0, sizeNeeded);

                if (readSize <= 0)
                    throw new IOException(String.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", readSize, sizeNeeded));

                target.Write(buffer, 0, readSize);
                bytesRead += readSize;
            }
        }
        
        /// <summary>
        /// Reads data into a complete array, throwing an EndOfStreamException
        /// if the stream runs out of data first, or if an IOException
        /// naturally occurs.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="byteArray">The array to read bytes into. The array
        /// will be completely filled from the stream, so an appropriate
        /// size must be given.</param>
        public static void ReadIntoByteArray(Stream stream, byte[] byteArray)
        {
            int offset = 0;
            int remaining = byteArray.Length;
            stream.Position = 0;
            while (remaining > 0)
            {
                int read = stream.Read(byteArray, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException
                        (String.Format("End of stream reached with {0} bytes left to read", remaining));
                remaining -= read;
                offset += read;
            }
        }


        /// <summary>
        /// Reads data from the beginning of a stream until the end is reached. The
        /// data is returned as a byte array. 
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <exception cref="IOException">thrown if any of the underlying IO calls fail</exception>
        /// <remarks>Use this method if you don't know the length of the stream in advance 
        /// (for instance a network stream) and just want to read the whole lot into a buffer. 
        /// <para>
        /// <strong>Note:</strong><br/>
        /// This method of reading the stream is not terribly efficient.
        /// </para>
        ///  </remarks>
        public static byte[] GetBytes(Stream stream)
        {
            if (stream is MemoryStream)
                return ((MemoryStream)stream).ToArray();

            byte[] byteArray = new byte[Const.BUFFSIZE];
            using (MemoryStream ms = new MemoryStream())
            {
                stream.Position = 0;
                while (true)
                {
                    int readLen = stream.Read(byteArray, 0, byteArray.Length);
                    if (readLen <= 0)
                        return ms.ToArray();
                    ms.Write(byteArray, 0, readLen);
                }
            }
        }
        
        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. 
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length. If the length is &lt; 1,
        /// then the default value of <see cref="Int16.MaxValue"/> will be used.
        /// </param>
        /// <exception cref="IOException">thrown if any of the underlying IO calls fail</exception>
        /// <remarks>Use this method to get the data if you know the expected length of data to start with.</remarks>
        public static byte[] GetBytes(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
                initialLength = Int16.MaxValue;


            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        /// <summary>
        /// Return an ASCII string from a stream of data
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetAsciiString(Stream stream)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            return GetString(stream, encoding);
        }

        /// <summary>
        /// Return an UTF8 encoded string from a stream of data
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetString(Stream stream)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return GetString(stream, encoding);
        }

        /// <summary>
        /// Return a string from a stream. The string is returned with 
        /// the encoding provided.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(Stream stream, Encoding encoding)
        {
            if (stream == null)
                return string.Empty;

            byte[] bytes = new byte[stream.Length];

            if (stream is MemoryStream)
                bytes = ((MemoryStream)stream).GetBuffer();
            else
                ReadIntoByteArray(stream, bytes);

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Reads the specified number of bytes from any position in a source stream into a 
        /// specific byte array in a specific start index position. The byte
        /// array must have the necessary size to read the portion of the stream required.
        /// </summary>
        /// <param name="source">Source stream to read from</param>
        /// <param name="target">Target byte array to write to</param>
        /// <param name="targetOffset">offset index in the target</param>
        /// <param name="sourceOffset">offset position in the stream</param>
        /// <param name="bytesToRead">number of bytes to read in the stream</param>
        /// <exception cref="ArgumentException">thrown if the target byte array is too small to stored
        /// the required number of bytes read from the stream.</exception>
        public static void ReadExact(Stream source,
                                        byte[] target,
                                        int sourceOffset,
                                        int targetOffset,
                                        int bytesToRead)
        {

            if (targetOffset + bytesToRead > target.Length)
                throw new ArgumentException("target array to small");

            int bytesRead = 0;
            source.Seek(sourceOffset, SeekOrigin.Begin);
            while (bytesRead < bytesToRead)
            {   // need more data  
                int sizeNeeded = Math.Min(Const.BUFFSIZE, bytesToRead - bytesRead);

                // read either the whole buffer length or                   
                // the remaining # of bytes: bytesToRead - sizeNeeded 
                int readSize = source.Read(target, (targetOffset + bytesRead), sizeNeeded);

                if (readSize <= 0)
                    throw new IOException(String.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", readSize, sizeNeeded));

                bytesRead += readSize;
            }
        }

        /// <summary>
        /// Read a partial segment of a stream, starting from an offset position.
        /// </summary>
        /// <param name="source">Source stream to read from</param>
        /// <param name="sourceOffset">the starting offset position in the stream. Set to 0 if the stream is to be read from the beginning.</param>
        /// <param name="bytesToRead">number of bytes to read</param>
        /// <returns>return partial segment as an array of bytes.</returns>
        public static byte[] ReadPartial(Stream source,
                                         long sourceOffset,
                                         long bytesToRead)
        {
            long sizeDiff = source.Length - sourceOffset;
            if (bytesToRead > sizeDiff)
                throw new ArgumentException("Bytes required exceeds what is available in stream");

            byte[] target = new byte[bytesToRead];

            long bytesRead = 0;
            source.Seek(sourceOffset, SeekOrigin.Begin);

            while (bytesRead < bytesToRead)
            {   // need more data  
                int sizeNeeded = (int)Math.Min((long)Const.BUFFSIZE, bytesToRead - bytesRead);

                // read either the whole buffer length or                   
                // the remaining # of bytes: bytesToRead - sizeNeeded 

                int readSize = source.Read(target, 0, sizeNeeded);

                if (readSize <= 0)
                    throw new IOException(String.Format("Underlying stream does not have enough data. Read {0} bytes, but {1} needed", readSize, sizeNeeded));

                bytesRead += readSize;
            }
            return target;
        }

        /// <summary>
        /// Read a stream (like a file or HttpWebRequest) and write to another stream
        /// </summary>
        /// <param name="readStream">the stream to read</param>
        /// <param name="writeStream"> the stream to write to</param>
        [Obsolete("See StreamUtils.Copy")]
        public static void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            Byte[] buffer = new Byte[Const.BUFFSIZE];
            int bytesRead = readStream.Read(buffer, 0, Const.BUFFSIZE);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Const.BUFFSIZE);
            }
            readStream.Close();
            writeStream.Close();
        }
        
        /// <summary>
        /// Try to skip bytes in the input stream and return the actual number of bytes skipped.
        /// </summary>
        /// <param name="stream">Input stream that will be used to skip the bytes</param>
        /// <param name="skipBytes">Number of bytes to be skipped</param>
        /// <returns>Actual number of bytes skipped</returns>
        public static int Skip(Stream stream, int skipBytes)
        {
            long oldPosition = stream.Position;
            long result = stream.Seek(skipBytes, SeekOrigin.Current) - oldPosition;
            return (int)result;
        }

        /// <summary>
        /// Skips a given number of characters into a given Stream.
        /// </summary>
        /// <param name=" stream">The stream in which the skips are done.</param>
        /// <param name="number">The number of caracters to skip.</param>
        /// <returns>The number of characters skipped.</returns>
        public static long Skip(StreamReader stream, long number)
        {
            long skippedBytes = 0;
            for (long index = 0; index < number; index++)
            {
                stream.Read();
                skippedBytes++;
            }
            return skippedBytes;
        }

        /// <summary>
        /// Skips a given number of characters into a given StringReader.
        /// </summary>
        /// <param name="strReader">The StringReader in which the skips are done.</param>
        /// <param name="number">The number of caracters to skip.</param>
        /// <returns>The number of characters skipped.</returns>
        public static long Skip(StringReader strReader, long number)
        {
            long skippedBytes = 0;
            for (long index = 0; index < number; index++)
            {
                strReader.Read();
                skippedBytes++;
            }
            return skippedBytes;
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }

        /// <summary>
        /// Converts a array of object-type instances to a byte-type array.
        /// </summary>
        /// <param name="tempObjectArray">Array to convert.</param>
        /// <returns>An array of byte type elements.</returns>
        public static byte[] ToByteArray(Object[] tempObjectArray)
        {
            byte[] byteArray = null;
            if (tempObjectArray != null)
            {
                byteArray = new byte[tempObjectArray.Length];
                for (int index = 0; index < tempObjectArray.Length; index++)
                    byteArray[index] = (byte)tempObjectArray[index];
            }
            return byteArray;
        }
    }
}
