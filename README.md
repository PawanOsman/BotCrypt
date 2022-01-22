# BotCrypt
#### Simple way to Encrypt or Dycrypt String and Files!
#
[![GitHub issues](https://img.shields.io/github/issues/pawanosman/BotCrypt)](https://github.com/pawanosman/BotCrypt/issues)
[![GitHub forks](https://img.shields.io/github/forks/pawanosman/BotCrypt)](https://github.com/pawanosman/BotCrypt/network)
[![GitHub stars](https://img.shields.io/github/stars/pawanosman/BotCrypt)](https://github.com/pawanosman/BotCrypt/stargazers)
[![GitHub license](https://img.shields.io/github/license/pawanosman/BotCrypt)](https://github.com/pawanosman/BotCrypt)
<a href="https://www.nuget.org/packages/BotCrypt">
    <img alt="logo" src="https://badge.fury.io/nu/BotCrypt.svg">
</a>
#
### Installation Methodes
- Search for "BotCrypt" at Nuget Package Manager in Visual Studio.
- Run "Install-Package BotCrypt" at Package manager Console in Visual Studio.
- Run "dotnet add package BotCrypt" at your Command Line.
### Usage
- First add Package Refrence
```cs
...
using BotCrypt
...
```
- Encrypting String
```cs
...
string Text = "Hello BotCrypt!";
string Password = "EncryptionPassword";
string EncryptedText = Crypter.EncryptString(Password, Text); // EGhw8RQ35r1AGg4fBuuKAQ==
...
```
- Decrypting String
```cs
...
string EncryptedText = "EGhw8RQ35r1AGg4fBuuKAQ==";
string Password = "EncryptionPassword";
string Text = Crypter.DecryptString(Password, EncryptedText); // Hello BotCrypt!
...
```
- Encrypting File
```cs
...
string FilePath = "Image.png";
byte[] FileBytes = File.ReadAllBytes(FilePath);
string Password = "EncryptionPassword";
string EncryptedFile = Crypter.EncryptByte(Password, FileBytes);

//Updating File With Encryped File
File.WriteAllText(FilePath, EncryptedFile);
...
```
- Decrypting File
```cs
...
string FilePath = "Image.png";
string FileContent = File.ReadAllText(FilePath);
string Password = "EncryptionPassword";
byte[] DecryptedFile = Crypter.DecryptByte(Password, FileContent);

//Updating File With Decryped File
File.WriteAllBytes(FilePath, DecryptedFile);
...
```
#### ©2022, All rights reserved, BotCrypt
