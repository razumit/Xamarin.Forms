using System;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using AView = Android.Views.View;

namespace Xamarin.Forms.Platform.Android
{
	internal static class KeyboardManager
	{
		internal static void HideKeyboard(this AView inputView, bool overrideValidation = false)
		{
			if (Forms.Context == null)
				throw new InvalidOperationException("Call Forms.Init() before HideKeyboard");

			if (inputView == null)
				throw new ArgumentNullException(nameof(inputView) + " must be set before the keyboard can be hidden.");

			using (var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService))
			{
				if (!overrideValidation && !(inputView is EditText || inputView is TextView || inputView is SearchView))
					throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");

				IBinder windowToken = inputView.WindowToken;
				if (windowToken != null)
					inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
			}
		}

		internal static void ShowKeyboard(this AView inputView)
		{
			if (Forms.Context == null)
				throw new InvalidOperationException("Call Forms.Init() before ShowKeyboard");

			if (inputView == null)
				throw new ArgumentNullException(nameof(inputView) + " must be set before the keyboard can be shown.");

			using (var inputMethodManager = (InputMethodManager)Forms.Context.GetSystemService(Context.InputMethodService))
			{
				if (inputView is EditText || inputView is TextView || inputView is SearchView)
				{
					inputMethodManager.ShowSoftInput(inputView, ShowFlags.Forced);
					inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);
				}
				else
					throw new ArgumentException("inputView should be of type EditText, SearchView, or TextView");
			}
		}
	}
}