# This is a Wrapped Version of ToastWallet to make it Portable.
TWP is a VeraCrypt Encrypted Portable version of ToastWallet.
It will keep the AppData\Roaming\ToastWallet files localy in the Executable directrory, like on a usb flash drive or external hardrive, these files will be stored in a Veracrypt Encrypted Container, for now using a SHA-512 AES Serpent Algorithm. If ppl like the project i will consider building in more Algorithms.

# Usage
Either Download the Source code and follow the instructions bellow. Or download the Executable from [here](https://drive.google.com/open?id=1QQM_9QhXm3_MQ3Zg4qfa-h1DRwCIrIip) and extract it \
Copy TWP.exe to a location of choice, and execute it, first time run, TWP will ask you to create a VeraCrypt container.
after that every time TWP is started you will be asked this password to unlock TWP.
this is a extra layer of security, as the Toastwallet database files are already encrypted with the standard pin you use to unlock your Toastwallet.


# Build Instructions
Compiling from source is pritty easy\
(1) Clone the repo;\
(2) Download the Toastwallet.exe from the link below;\
(3) Rename Toastwallet-xxx.exe to ToastWallet.exe;\
(4) Download the VeraCrypt Portable version from link below and extract files;\
(5) Copy dependancies "Toastwallet.exe", "veracrypt-x64.exe", "veracrypt format-x64.exe", "verarypt-x64.sys" to Resources folder;\
(6) Import the project into Visual studio 2017 and compile the binary;

Or Download a PreCompiled version [here](https://drive.google.com/open?id=1QQM_9QhXm3_MQ3Zg4qfa-h1DRwCIrIip)

# 3e party SourceCode and Binaries (Dependencies)
ToastWallet:

ToastWallet SourceCode; https://github.com/ToastWallet/core \
ToastWallet Binary that is used; https://toastwallet.com/download/windows/ToastWallet%202.3.10.exe 

VeraCrypt: 

Veracrypt SourceCode; https://github.com/veracrypt/VeraCrypt \
Veracrypt Binaries that are used; https://launchpad.net/veracrypt/trunk/1.22/+download/VeraCrypt%20Portable%201.22.exe 

# Support the Project
If you like this project, and want to support it, consider a donation,\
XRP : rUaESERZHHjE7duW8ZhWo4yjXsjiDGQ1Ws
