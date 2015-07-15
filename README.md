# EmbeddedPython

This is a .NET wrapper for Python 2 and Python 3 written in C#.

It somehow similar to Python .NET project with exception that it enforces more type safety and abstracts Python even further, so that more CLR native objects used instead.

It also makes heavy use of interfaces, which allows it to work identivally for both Python 2 and Python 3. On Windows it's even possible to run both Python 2 and Python 3 within the same application.

It provides automatic type conversion and handles discrepancies between Python 2 and Python 3 internally, so you can enjoy coding, not muddling around. This means, Unicode Strings and Ints are handled properly, which is a big drawback for Python version upgrade.

Project is fully covered with Unit Test, including stress tests to ensure no memory leaks on Python side are introduced.
