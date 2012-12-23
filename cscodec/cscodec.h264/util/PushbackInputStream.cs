namespace cscodec.h264.util
{
	#if false
	public class PushbackInputStream : FilterInputStream {

		protected sbyte[] buf;
		protected int pos;

		public PushbackInputStream(InputStream is) {
			super(is);
			buf = (is == null)?null:new sbyte[1];
			pos = 1;
		}
	
		public PushbackInputStream(InputStream is, int size) {
			super(is);
			buf = (is == null)?null:new sbyte[size];
			pos = size;
		}
	
		public int available() throws IOException {
			if(buf == null)
				throw new IOException();
			return buf.Length - pos + inputStream.available();		
		}
	
		public void close() throws IOException {
			if(inputStream != null) {
				inputStream.close();
				inputStream = null;
				buf = null;
			} // if	
		}
	
		public boolean markSupported() { return false; }
	
		public int read() throws IOException {
			if(buf==null)
				throw new IOException();
			if(pos < buf.Length)
				return (buf[pos++] & 0xff);
			return inputStream.read();
		}
	
		public int read(sbyte[] buffer, int offset, int len) throws IOException {
			if(buf==null)
				throw new IOException();
			int copiedBytes = 0;
			int copyLength = 0;
			int newOffset = offset;
			if(pos < buf.Length) {
				copyLength = (buf.Length - pos >= len)?len:buf.Length - pos;
				Array.Copy(buf, pos, buffer, newOffset, copyLength);
				newOffset += copyLength;
				copiedBytes += copyLength;
				pos += copyLength;
			} // if
			if(copyLength == len) {
				return len;
			} // if
			int inCopied = inputStream.read(buffer,newOffset,len - copiedBytes);
			if(inCopied > 0)
				return inCopied + copiedBytes;
			if(copiedBytes == 0)
				return inCopied;
			return copiedBytes;
		}
	
		public long skip(long count) throws IOException {
			if(inputStream==null)
				throw new IOException();
			if(count <= 0) return 0;
			int numSkipped = 0;
			if(pos < buf.Length) {
				numSkipped += (count < buf.Length - pos)?count:buf.Length - pos;
				pos += numSkipped;
			} // if
			if(numSkipped < count)
				numSkipped += inputStream.skip(count - numSkipped);
			return numSkipped;	
		}
	
		public void unread(sbyte[] buffer) throws IOException {
			unread(buffer,0,buffer.Length);
		}
	
		public void unread(sbyte[] buffer, int offset, int length) throws IOException {
			if(length > pos)
				throw new IOException();
			Array.Copy(buffer, offset, buf, pos - length, length);
			pos =  pos - length;
		}
	
		public void unread(int oneByte) throws IOException {
			if(buf == null)
				throw new IOException();
			buf[--pos] = (sbyte)oneByte;
		}
	
		public void mark(int limit) { return; } // Not Support.
		public void reset() throws IOException { // Not Support.
			throw new IOException();
		}
	
		
	}
#endif
}