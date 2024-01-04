# Application Development in C#
## C# in General (Goals)
- Development of safe programs (less likely to crash)
  - Type safety (strong type checking)
  - Array bounds checking 
  - Detection of the use of unintialized variables 
- "Easy" to use 
  - Convenient 
  - Gather a high grade of readability within the source code 
  - Standard approach on Documentation  
- Internationalization
- Garbage Collection (avoidance of memory leaks)
- Protability (Just in time compling)
- Object orientated 
- High development performance 
  
## C/C++ vs. C# Strong type checking 

![Strong type Checking](../images/StrongTypeChecking.png)

## Basic types in C# vs C/C++
- In C/C##: A double is a aligned space in memory of 8 bytes to represent a floating point number
- In C#: A double is already a struct with static methods
- Internationalization: CulturInfo! (Problem: Abspeichern und wieder laden z.B. in DE speichern, in USA Starten)
- double d4 = double.Parse("3.54")
- f = 3.6 (so ist es ein double), therefore in C#: f = 3.6 f
- d = 1 / 3 : integer divided by integer = 0 (fällt bei großem Zähler nicht direkt auf, daher gefährlich!)
 
<div style="page-break-after: always;"></div> 

## Arrays
  - Arrays werden mit new eingeleitet, hier 2 x new (1 x für Array, 1 x für Klasse)

    int [] test = new int[100]

    MyClass[] myTest = new MyClass[100]

    for (int i = 0; i < 100; i++){

        myTest[i] = new myClass();
    }

## Unicode and Encoding 
    - ASCII -> 7-Bit Representation, 8-bits per char to represent a String in C, can not interpret special characters
    - UTF8 muss nicht immer kürzer sein z.B. Sonderzeichen (Herz), da ist es größer als Unicode, kann Sonderzeichen aber darstellen im Gegensatz zu ASCII
    - C# uses two bytes per char, so called Unicode
    - Unicode in UTF16, UTF8 en- /decoded
   ![ascii_utf8_unicode](../images/Unicode.png)

## Definitions 

### Reference types: 
    - refer to objects (Instances of classes)
    - "new"
  
<div style="page-break-after: always;"></div>

### Value Type: 
    - based on structs
    - have normal and static methods
    - are instantiated implicity
  
### Structs: 
    - can not have an explicit parameter less constructor (e.g. new Vector3D v = new Vector() works, but in class is no param less constructor defined)
    - they can have explicit constructors with paramters, but these must define every field
    - will not loose their paramter less constructor, if a constructor with params is defined
    - not be inherited, but all are inherited once from "Value Type"
    - can implement interfaces 
    - can be generic 
### Static: 
    - Static Fields:
      - "One per class"
      - can be accessed by static and non static methods 
      -  then a single copy of the variable is created and shared among all objects at the class level

    - Static Properties:
      - "One per class"
      - can be accessed by static and non static methods 
      - Mostly used for specific instance creation:
 ![static_property](../images/static_property.png)

<div style="page-break-after: always;"></div>

    - Staitc methods:
      - they are not associated with a certain object, but with the class itself
      - they can be called without an creating an instance of the class (an object)
      - they have no access to non-static fields (how could they?)
      - Useful Application: Generation of specific objects
  ![static_method](../images/static_methods.png)

    - Static classes: 
      - Can not be instantiated 
      - Can only contain static fields or constants
      - Useful Application: Group matching functions e.g. a collection of math. functions
 
  ![static_class](../images/static_class.png)

<div style="page-break-after: always;"></div>

## Example of a struct 
![Vector_example](../images/struct_example.png)

- Structs are not references: it is a Value Type 
- Vector 3D a = new Vector(1,2,3);
- Vector 3D b = a; (whole copy)
- a.10 = 1000; // a is changing, b not, but would it be a class it would change bcs than it would be a pointer there
- Array.sort uses the CompareTo from the Interface
<div style="page-break-after: always;"></div>

## Enums 
![enums_example](../images/enum.png)

## Inline if else
![if_else_example](../images/if_else.png)

## Access
    - private: access only within the class
    - protected: within the class and all derived classes
    - public: full access

<div style="page-break-after: always;"></div>

## Precision with 0.1
    - 0.1f + 0.1f+0.1f + 0.1f+0.1f + 0.1f+0.1f + 0.1f+0.1f + 0.1f = 1,000001
      - not precise enough 
    - 0,1+0,1+0,1+0,1+0,1+0,1+0,1+0,1+0,1+0,1 = 0,999999
      - not precise enough
    - 0,1 ist nicht binary-conform, ist nicht zur Basis 2
    - you can use decimal for 10 ^: 
      - 0,1m+0,1m+0,1m+0,1m+0,1m+0,1m+0,1m+0,1m+0,1m+0,1m = 1,0
    - technical stuff: double 
    - waste amount of data: flout 
## WriteLine
writeLine has an overload of 17 therefore you don’t need to convert an int to a string

## Constructors
 - if a constructor (with params) is definied, the parameterless (default) constructor will stop existing, otherwise you have to define it. If you have a struct the parameter less constructor will still be there
  
![constructor](../images/main_person.png)
<div style="page-break-after: always;"></div>

![constructor](../images/L2_main.png)

## Inheritance 
- The class is supposed to a specialized version of the other class e.g. student is still a person
- derived class can call public methods of the base class, but the field is not visible outside the object ('protected')
- access public field of the base class as if they were their own, additional fields can be defined
![inheritance](../images/InheritanceI.png)
![inheritance](../images/InheritanceII.png)
![inheritance](../images/car.png)
![inheritance](../images/vw.png)
![inheritance](../images/carvwmain.png)

<div style="page-break-after: always;"></div>

## Polymorphism 
- have many forms e.g. base class can be the derived classes 
- Objects can be identified by more than one type e.g. derived class can be called by his class or base class 
  - e.g. you have Base Class: Vehicle and derived classes: car, bycycle, boat 
  - you want to have an array with all three derived classes so you have to make an array of the base class like vehicle
  - Vehicle [] vehicles = new Vehicle []{car, bycyle, boat};
- Base class method: 'virtual' 
    - e.g. Person:
      -  idCard() has to be virtual
      -  toString() has to be overriden bcs it is from object-class 
![polymor](../images/polymor.png)

- Every Reference type in C# is derived from the class 'object', which has some predefined methods like public virtual string ToString(), public virtual bool Equals(Object obj): 
- e.g.:
    public override bool Equals(object obj)
        {
            var person = obj as Person;
            return person != null &&
                   GivenName == person.GivenName &&
                   Surname == person.Surname &&
                   PlaceOfBirth == person.PlaceOfBirth &&
                   Height == person.Height;
        }
## Abstract classes
-  für gemeinsame Vereinbarungen die die abgeleiteten Klassen haben müssen, in der abstrakte klasse darf man auch Methoden definieren, abstrakte Klasse kann nicht initialisiert werden, daher lohnt es sich
-  eine abstrakte klasse für die klasse zu setzen die man nicht initialisieren möchte 
- can not be instantiated but derived from 
- contains fields, methods with and without implementation
- all derived classes must iplement (override) of the abstract methods
### Example Abstract Classes, Interface, Properties, events
![Abstract](../images/abstrat_person.png)
![Abstract](../images/abstract_student.png)
![Abstract](../images/abstract_interface.png)
![Abstract](../images/abstract_prof.png)
![Abstract](../images/abstract_mainI.png)
![Abstract](../images/abstract_mainII.png)
![Abstract](../images/abstract_output.png)
## For Each
foreach (var item in people){
    Console.Writeline(item);
}

## Is-Keyword 
- If people i is student s -> wird direkt gecasted, ansonsten müsste man nochmal casten 
- As Keyword -> gefährlicher!, da nicht abgefragt wird => keine Bedingung 
![Is-Keyword](../images/IsI.png)
![Is-Keyword](../images/IsII.png)
<div style="page-break-after: always;"></div>

## Properties 
- Properties: Backingfield, ohne Backingfield ganz normales Public Feld , get auch bei Properties e.g. idCard()
- Get and set methods should be used to check incoming data if
necessary
- Example: Height is not supposed to be 0 or negative
  - ![Properties](../images/properties.png)
- Missing “set” realizes a read-only condition
- A write-only condition can be realized by only implementing
“set”
- Properties do not need to have a background-fields, sometime you have to create one background-field!
## ToString()
- toString normalerweise zurück wenn ich nichts überlade: Namespacenmae + Klasse 
- GetHashCode für Dictionaries zum bessere Sortieren 
  
## Delegates 

- Have similarities with function pointers in C/C++, but they are Object-oriented, type-safe, Secure
- An instance of a delegate can hold: 
    - No method at all (Exception!)
    - One or Multiple Methods( can be added (+=) and substracted (-=), called in their order they were assigned (useful for multiple callbacks in GUI-Programs) 
    - Instances of delegates can be passed as arguments into methods()

- ![Delegate](../images/Delegate.png)

- Delegate types: 
  - ![Delegate Types](../images/DelegateTypes.png)
  
## Anonymous methods 
- The mechanism to inject a functionality by assigning a method using a delegate is extremely powerful
- It can look more complicated that it needs to be, because the method is defined “elsewhere”
- Anonymous methods provide an easy way of defining a method “on the fly”
- ![Anonymous methods](../images/AnonymousMethods.png)
  <div style="page-break-after: always;"></div>

## Lambda Functions - Statement Lambda 
- Even simpler syntax to define a target for a delegate type 
- often used in LINQ expressions 
- ![Lambda funtion](../images/LambdaFunction.png)
  
## Expression Lambda 
- consist of one instruction only 
- the return value of that instruction will be the return value of the lambda function 
- this example modified without the console output 
- ![Lambda Expression](../images/LambdaExpression.png)
  
## Delegate Example 
- If(anOperation != null){anOperation(2,3)}; //this makes sure that anOperation has a function
- Static and non static method can be assigned to an Instance of a delegate
- ![Delegate Example](../images/DelegateExampleI.png)
- ![Delegate Example](../images/DelegateExampleII.png)

<div style="page-break-after: always;"></div>

### Delegate Example with void and int 
- DelegateClass:
  - ![DelegateClass](../images/DelegateClass.png)
  
  <div style="page-break-after: always;"></div>

- DelegateDefinitions: 
  - ![DelegateDefinitions](../images/DelegateDefinitions.png) 


- DelegateVoid:
  - ![DelegateVoid](../images/DelegateVoid.png) 
- DelegateInt:
  - ![DelegateInt](../images/DelegateInt.png) 
  


- CallDelegates:
  - ![CallDelegates](../images/CallDelegates.png) 
  
## Delegate Events 
- An Event can be used by an object to inform other (objects) that something happened
1. Delegates can only be invoked (raised) from WITHIN the object, object calls the method that are associated with the event
2. Methods (Event handlers) can only be added and substracted (sub. += and unsub. -=) but not assigned
3. They can be used in Interfaces
- ![Delegate Event Example](../images/DelegateEventExampleI.png)
- ![Delegate Event Example](../images/DelegateEventExampleII.png)

- ![Delegate Event Example](../images/DelegateEventExampleIII.png)
### Delegate Example Lab2 Chessgame
- Delegate Type and Event Definition:
  - ![Delegate Type and Event Definition](../images/Chess_dele.png)
- Subscribe to Event and execute Method:
  - ![SubEventandCallMethod](../imagea/../images/propertyBoard.png)
- Call Event
  - ![Call Event](../imagea/../images/Chess_Call.png)
### Delegate Example Oscilloscope 
- Delegate Type/ Events Defintion: 
  - ![Dele Subscribe](../images/Lab3DeleInit.png)
- Subscribe to the event:
  - ![Dele Subscribe](../images/Lab3DeleSub.png)
-  Call Event: 
    - ![Dele Call](../images/Lab3DeleCall.png)
    - Method NewDataHandler:
      - ![Data Handler](../images/newDataHandler.png)

<div style="page-break-after: always;"></div>

## External Hardware Timer vs. DispatcherTimer
- Timer: 
  - is using a thread and the gui has his thread: Application.Current.      Dispatcher.Invoke(()=>{
		Slider.Value++
});
 - System.Timers: no GUI sync 
 - better for calling to a server for accessing data, dispatcher time could block the messageloop bcs of its priority


- Dispatcher Timer: 
  - führt den Delegaten synchron auf dem Thread aus
  - System.DispatcherTimer: GUI sync, Priority is an additional property to be aware of
  - Dispatcher Timer:  synchronized with GUI, the call with be in the messageloop, disadavantage it´s not that predicise , has specific priority, good for events

### Example
![Timer Example](../images/TimerExample.png)
-  timer.Tick += myTimer_Tick;
- timer.Start();
- timer.Interval = TimeSpan.FromSeconds(1);
- timeLeft = 10;
<div style="page-break-after: always;"></div>

## Interfaces 
- A class can have multiple Interfaces therefore you use it either than an abstract class 
- Not allowed to implement methods, to contain fields
- Everything is public by default
- Can have events, method-definitions
- ![Interface Example](../images/InterfaceExampleI.png)
- ![Interface Example](../images/InterfaceExampleII.png)
- Var item is typ object: to String überladen 
- foreach (var item in people){    
If(OnValueCHanged != null){
    
                	if(item is IUniversityMember m) //nach Interfaces fragen
                	{
                   	 m.getIdentificationNumber();
                	}
}
<div style="page-break-after: always;"></div>

## Overview 
- ![Interface Example](../images/Overview.png)

## Generics 
- effecient + more flexibile
- ![Generics](../images/Generics.png)
- ![Generics](../images/GenericII.png)

### Generic Enumerator
- ![Generics](../images/squares.png)
- ![Generics](../images/genericenumerator.png)
-  ![Generics](../images/genenusquare.png)
  <div style="page-break-after: always;"></div>

## Inheritance and Generic Classes
### Closing 
- Decrease the number of generic types:
- ![Closing](../images/closing.png)

### Opening 
- Simply pass on the type to the base class:
- ![Opening](../images/opening.png)
  
## Collections: List <T>
- Add: add an item to the list
- AddRange(IEnumerable<T> collection): Many items
- Contains(T item): checks if the list contain “item”
- Find(Predicate<T> match) returns the first item that the predicate returns true on
- List teuerer als Arrays

- ![ListExample](../images/ListExampleI.png)
- ![ListExample](../images/ListExampleII.png)
- ![Collection](../images/Collection.png)
  
## Lamda Expressions/ Predicate
- words.RemoveAll(s=> s.length ==3)
- ForEach short version:
	allWordsWithE.ForEach(s=>Console.WriteLine(s))
- List of type string:
	length, contains methods are available
- Find:
	Is FindFirst()
- Convert:
	Is select

## Types of List
- Queue<T> (FIFO)
- Stack <T> (LIFO)

## IEnumerable and IEnumerator 

### IEnumerable 
- GetEnumerator() 
- ![IEnumerator](../images/IEnumerable.png)
- “yield return” will return the object and remain the position
for the “next call”, if it would be just return the method would be finished 
- Practically an object with the Interface IEnumenrator will be
generated
- Yield: Rückgabewerte enuberable ist, yield gibt immer ein neues Element zurück, um eine Collection zu füllen 
### IEnumerator 
![IEnumerator](../images/IEnumerator.png)

### IEnumerator Example Chessgame 
- public class Board : IEnumerable<Piece>
- ![BoardEnumerator](../images/chess_IEnumerator.png)
  
<div style="page-break-after: always;"></div>

## Example Array and Enumerator
![Arr](../images/Arr_enum.png)

<div style="page-break-after: always;"></div>

## Extension Methods 
![Extension](../images/ExtensionMethods.png)


![Extension](../images/ExtensionMethodsII.png)

## Extension Methods Example Split
![Split](../images/Extension_SplitName.png)
![Split](../images/Extension_SplitNameII.png)

## Example Collection + Extension Method

![Example](../images/Car_class.png)
![Example](../images/Car_collection.png)
![Example](../images/Car_extension.png)

# String.Join

![Join](../images/join.png) 

## Example OwnCollection

![OwnCollection](../images/ownCollection.png)
![OwnCollection](../images/mainOwnCollection.png)

<div style="page-break-after: always;"></div>

## Example own Collection IEnumerable, IEnumerator 
![IEnumerator](../images/CarCollectonIEnumerable.png)
![IEnumerator](../imagea/../images/CarEnumeratorI.png)
![IEnumerator](../images/CarEnumeratorII.png)
![IEnumerator](../images/CallIEnumerableCollection.png)

<div style="page-break-after: always;"></div>



## LINQ (Language Integrated Query)

![ListExample](../images/powerful%20techniques.png)
- LINQ nutzt extension methods, die auf ein enumerable enden 
- Query any Collection implementing IEnumerable<T>
- Use of extension methods
- Main applications: 
  - Filter (Where) => only the objects you want!
  (Takes a Predicate)

  - Sorter (OrderBy) => return something comparable (e.g. number)

  - Projector (Select) => projects onto another type

  - Simply return the type you want in the lambda 

- All the above are implemented "lazy", the will "only" return an IEnumerable<T>
- Convert toArray() or toList() if you want the job to be done 
- Cascadieren: Filter -> sorter -> Projector 
- Mydata.Where(....).OrderBy(.....).Select(......)
  <div style="page-break-after: always;"></div>

## Example Funfair 

![Exercie](../images/ex1.png)
![Exercie](../images/ex2.png)
![Exercie](../images/ex3.png)

<div style="page-break-after: always;"></div>

## Solution 

![Exercie](../images/FunfairI.png)
![Exercie](../images/FunfairII.png)
- Select ändert die Anzahl nicht, aber z.B. Where oder Sum !


![Solution](../images/solution.png)
![Solution](../images/solutionII.png)
- Where statement:
![Where](../images/Where.png)
 
 <div style="page-break-after: always;"></div>  

## AsyncAwait 
![AsyncAwait](../images/AsAwI.png)
![AsyncAwait](../images/AsAwII.png)
![AsyncAwait](../images/AsAwIII.png)
![AsyncAwait](../images/AsAwIIII.png)

### AsyncAwait Task<Int> Example 
![Task<Int>](../images/Task%3Cint%3E.png)
![Task<Int>](../images/Task%3Cint%3EII.png)

 <div style="page-break-after: always;"></div>  

### AsyncAwait Task<Int> Example without Static
![Task<Int>](../imagea/../images/AsynCAwaitExI.png)
![Task<Int>](../images/AsynCAwaitExII.png)

  <div style="page-break-after: always;"></div>  

### AsyncAwait Lectureexample 
![AsyncAwait](../images/AsyncVLI.png)
![AsyncAwait](../images/AsyncVLII.png)

  <div style="page-break-after: always;"></div>

### AsyncAwait Example Lab3
![AsyncAwait](../images/AsycnWaitExampleLab.png)

## Dictionary Example
![Dict](../images/Dict.png)

<div style="page-break-after: always;"></div>

## Lab1 Bits
![Bits](../images/Bits.png)


### Example 
![Bits](../images/BinarI.png)
![Bits](../images/BinaryII.png)
![Bits](../images/BinaryIV.png)
<div style="page-break-after: always;"></div>

# Delegate/ Events and XAML
public delegate void ChangeValue(Byte newData); 

public event ChangeValue BitChange;

![X](../images/registerEventInXAML.png)
![X](../images/CallEvent.png)
![X](../images/ExecuteEvent.png)

<div style="page-break-after: always;"></div>

# Delegate/ Event and XAML 
## UserControl
![X](../images/LAB1_UserControle.png)
## XAML
![X](../images/LAB1_XAML.png)

## MainWindow
![X](../images/LAB1_MainWindow.png)

<div style="page-break-after: always;"></div>

# Delegate/ Events and Interface 
![I](../images/I_I.png)
![I](../images/I_II.png)
![I](../images/I_III.png)

<div style="page-break-after: always;"></div>

## LINQ further examples
![LINQ](../images/FELINQI.png)
![LINQ](../images/FELINQII.png)
<div style="page-break-after: always;"></div>
## MVVM - Pattern

### Content Control 
Controls with only one single Content
- Content can be assigned to the „Content“ property or the
- Inherit from ContentControl
- Examples:
  - Button
  - Label
  - ViewBox
  - Window (surprised?)
Examples:
- Canvas (Free Positioning with coordinates)
- StackPanel (Horizontzal and vertical Stacks)
- DockPanel(Everything is docked, left right etc)
- UniformGrid(Grid style with automatic Rows and Columns)
- Grid (Free positioning, resizing, use of row and columns, most common)

#### Content Controls and Panel Controls
The Controls have only one single content! Therefore we need to add a panel control to the window, bcs Panel Controls have the ability to display multiple controls
<window>
  <StackPanel>
      <StackPanel>
      </StackPanel>
      <StackPanel>
      <StackPanel>
      <Grid>
      </Grid>
      <Button>
      </Button>
  </StackPanel>
</window>

### XAML and C#

![XAMLC#](../images/XAMLC%23.png)

### Databinding 
#### App:
![Databinding](../images/MVVMEx0.png)
#### Model
![Databinding](../images/MVVMExI.png)
![Databinding](../images/MVVMEXII.png)
#### View 
![Databinding](../images/MVVMEXIII.png)

#### Extensions
![Databinding](../images/MVVM_III.png)
#### ModelView
![Databinding](../images/MVVMEXIV.png)
#### App after changing data
![Databinding](../images/MVVMEXV.png)

### Mode One Way

![OneWay](../images/OneWay.png)