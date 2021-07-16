# Comet ☄️ 

[![dev-build](https://github.com/dotnet/Comet/actions/workflows/dev.yml/badge.svg)](https://github.com/dotnet/Comet/actions/workflows/dev.yml)  [![Clancey.Comet on fuget.org](https://www.fuget.org/packages/Clancey.Comet/badge.svg)](https://www.fuget.org/packages/Clancey.Comet)
[Chat on Discord](https://discord.gg/7Ms7ptM)


What is Comet? Comet is a prototype for a new UI Framework/Pattern to write app UI.  It follows the Model View Update (MVU) pattern. It magically databinds for you!  

Video Preview:

[![Video Demo](http://img.youtube.com/vi/-Ieg9UadN8s/0.jpg)](http://www.youtube.com/watch?v=-Ieg9UadN8s)

## Getting Started

When you're ready to take a ride on the comet, head over to the wiki and follow the [Getting Started](https://github.com/Clancey/Comet/wiki/Getting-Started) guide.

## Key Concepts

Comet is an MVU style pattern:

![MVU pattern](art/mvu-pattern.png)

`View` is a screen. Views have a `Body` method that you can assign either by an attribute `[Body]`:

``` cs
public class MyPage : View {
	[Body]
	View body() => new Text("Hello World");
}
```

or:

``` cs
public class MyPage : View {
	public MyPage() {
		Body = body;
	}
	View body() => new Text("Hello World");
}
```

## Hot Reload

Hot Reload is included by default! The setup is very easy: a Visual Studio extension and a NuGet. Download both from [Releases](https://github.com/Clancey/Comet/releases) here on GitHub.

Download and install the VS extension from the [Releases](https://github.com/Clancey/Comet/releases/)

Then add to your `AppDelegate.cs` and/or `MainActivity.cs`, or similar. See the sample projects here for examples.

``` cs
 #if DEBUG
            Comet.Reload.Init();
 #endif
```


## State

As of right now there are two supported ways to add state.

### 1. Simple data types like int, bool?

Just add a `State<T>` field to your View

``` cs
class MyPage : View {
	readonly State<int> clickCount = 1;
}
```

`View` is state aware. When the state changes, databinding will automatically update, or rebuild the view as needed.

### 2. Do you want to use more complex data types?

You can either implement [INotifyPropertyRead](https://github.com/Clancey/Comet/blob/master/src/Comet/BindingObject.cs#L13) or you can use [BindingObject](https://github.com/Clancey/Comet/blob/master/src/Comet/BindingObject.cs) to make it even simpler.

Add it as a Field/Property, and add the `[State]` attribute!


``` cs
public class MainPage : View {
		class MyBindingObject : BindingObject {
			public bool CanEdit {
				get => GetProperty<bool> ();
				set => SetProperty (value);
			}
			public string Text {
				get => GetProperty<string> ();
				set => SetProperty (value);
			}
		}

		[State]
		readonly MyBindingObject state;
}

```

`INotifyPropertyRead` is just like `INotifyPropertyChanged`. Just call `PropertyRead` whenever a property Getter is called. And `PropertyChanged` whenever a property Value changes.

### How do I use the State?

Simply update the stateful value and the framework handles the rest. 

``` cs
public class MyPage : View {

	readonly State<int> clickCount = 1;
	readonly State<string> text = "Hello World";

	public MyPage() {
		Body = () => new VStack {
			new Text (text),			
			new Button("Update Text", () => state.Text = $"Click Count: {clickCount.Value++}")
		};

	}
}
```

That is all!, now when the Text Changes everything updates. 

### What if I want to format my value without an extra state property?

While `new Button("Update Text", () => state.Text = $"Click Count: {clickCount.Value++}" )` works, it isn't efficient.

Instead, use `new Text(()=> $"Click Count: {clickCount}")`.

``` cs
public class MyPage : View {

	readonly State<int> clickCount = new State<int> (1);

	public MyPage() {
		Body = () => new VStack {
			new Text (() => $"Click Count: {clickCount}"),
			new Button("Update Text", () => {
				clickCount.Value++;
			}
		};
	}
}

```


## What platforms are being targeted?

* iOS
* Android
* UWP
* WPF
* Mac OS
* Xamarin.Forms - all Forms targets: Linux, macOS, Tizen, WPF, and of course Android, iOS, UWP.
* Blazor

# Disclaimer

Comet is a **proof of concept**. There is **no** official support. Use at your own Risk.
