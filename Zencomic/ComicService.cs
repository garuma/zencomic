// 
// ComicService.cs
//  
// Author:
//       Jérémie "Garuma" Laval <jeremie.laval@gmail.com>
// 
// Copyright (c) 2009 Jérémie "Garuma" Laval
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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Gdk;
using Mono.Addins;

using ZencomicLib;

namespace Zencomic
{
	public static class ComicService
	{
		static IComicAddin[] comics;
		static List<IComicAddin> shuffled = new List<IComicAddin> ();
		static IEnumerator<IComicAddin> enumerator;
		static Random rand = new Random ();
		
		static ComicService ()
		{
			InitComics ();
			AddinManager.ExtensionChanged += delegate { InitComics (); };
		}
		
		static void InitComics ()
		{
			comics = AddinManager.GetExtensionObjects ("/Zencomic/ComicAddins").Cast<IComicAddin> ().ToArray ();
			FillShuffleBuffer ();
			
			Console.Write ("Enabled addin : ");
			foreach (var comic in comics) {
				Console.Write (comic.ComicName);
				Console.Write (';');
			}
			Console.WriteLine ();
		}
		
		static void FillShuffleBuffer ()
		{
			shuffled.Clear ();
			foreach (IComicAddin addin in comics)
				shuffled.Insert (rand.Next (0, shuffled.Count), addin);
			
			enumerator = shuffled.GetEnumerator ();
		}
		
		// The parameter are : comic image, comic name, comic author
		public static void GetNextComic (Action <Pixbuf, string, string> callback)
		{
			// No comic addin loaded
			if (comics.Length == 0)
				return;
			
			if (!enumerator.MoveNext ()) {
				FillShuffleBuffer ();
				GetNextComic (callback);
				
				return;
			}
			
			IComicAddin addin = enumerator.Current;
			
			ThreadPool.QueueUserWorkItem (delegate {
				Pixbuf pixbuf = null;
				try {
					for (int i = 0; i < 5 && pixbuf == null; i++)
						pixbuf = addin.GetNextComic ();
				} catch {
					return;
				}
				
				callback (pixbuf, addin.ComicName, addin.ComicAuthor);
			});
		}		
	}
}
