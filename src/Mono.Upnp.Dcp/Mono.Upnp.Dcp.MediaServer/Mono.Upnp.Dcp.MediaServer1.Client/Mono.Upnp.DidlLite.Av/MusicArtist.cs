// 
// MusicArtist.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Peterson
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Mono.Upnp.DidlLite.Av
{
	public class MusicArtist : Person
	{
		readonly List<string> genre_list = new List<string> ();
		readonly ReadOnlyCollection<string> genres;
		
		protected MusicArtist ()
		{
			genres = genre_list.AsReadOnly ();
		}
		
		public ReadOnlyCollection<string> Genres { get { return genres; } }
		public Uri ArtistDiscographyUri { get; private set; }
		
		IEnumerable<MusicAlbum> Albums { get; set; }
		IEnumerable<MusicTrack> Tracks { get; set; }
		IEnumerable<MusicVideoClip> Videos { get; set; }
		
		protected override void DeserializePropertyElement (XmlReader reader)
		{
			if (reader == null) throw new ArgumentNullException ("reader");
			
			if (reader.NamespaceURI == Protocol.UpnpSchema) {
				if (reader.Name == "genre") {
					genre_list.Add (reader.ReadString ());
				} else if (reader.Name == "artistDiscographyURI") {
					ArtistDiscographyUri = new Uri (reader.ReadString ());
				} else {
					base.DeserializePropertyElement (reader);
				}
			} else {
				base.DeserializePropertyElement (reader);
			}
		}
	}
}
