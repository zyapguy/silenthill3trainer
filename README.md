# Silent Hill 3 Trainer
by zyapguy and bigwang74

## Technologies
- Well, uhh. C# and thats about it...
- There was also a weird **old** VB file that we converted to C#

## How to use?
Launch Silent Hill 3 first, then launch the trainer. It is preferable if the trainer is started when a game is in progress (i.e. the player is not in the main menu). After that everything will just *work*. NOTE : You must have SH3 Widescreen fix for this to work. Otherwise the memory addresses will be painfully offset.

## How?
We found an **old** Visual Basic file that allows writing values to certain memory addresses. Bigwang74 found the memory addresses of useful things in the game and created the original trainer using VB with this file in December 2021. Later, zyapguy painstakingly converted the memory editor file to C# and got it working (*somehow*). We later decided to port bigwang74's trainer into C#. With bigwang74's immense help, zyapguy was able to recreate the trainer in C#.

The project is currently maintained by zyapguy and bigwang74.

## FAQ

- Q: Do you touch grass?
- A: Definately no.


- Q: Will you add Silent Hill 2 support?
- A: We might do a SH2 trainer if this gets enough attention. ¯\_(ツ)_/¯


- Q: Will this receive updates?
- A: We will update it as long as we have ideas for it.

## Coming Soon
These are what we are planning on adding next, there is not a 100% guarantee that these will be added.
- Ability to change combinations of the puzzles. DONE!
- Changing FOV. DONE! (*we think*)
- Manually setting ammo counts

## Requirements
- You need to have Widescreen fix installed. Get it from [GitHub](https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/sh3/SilentHill3.WidescreenFix.zip)
- Silent Hill 3 on PC (duh.) Guide on downloading it for free [here](https://www.youtube.com/watch?v=j7gKwxRe7MQ)

## Contributing
We will most likely accept any pull requests as long as it's human readable code and accesses `ReadWritingMemory`.

Things we will **NOT** accept:
- Shitpost buttons such as "Russian Roulette"
- Small easter eggs
- Stuff that works 2% of the time
- Literal malware
- Uninterpretable Win32API Garbage. Such as legacy NT Kernel calls (such as `NtQueryInformationProcess`)
- Stuff that changes how the back-end works, such as changing memory with Win32 calls instead of the cursed VB file.

Other than that, you are good :)
