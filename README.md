# Postman2Restclient

This tool was created to allow you to move from Postman to the VS Code - RestClient plug-in. Both tools serve the same purpose, but the format in which they store data is different. To avoid long and tedious manual rewriting, Postman2Restclient has been created, which enables simple conversion of environments from the format used in Postman to a RestClient-compliant format.

# Usage

Postman2Restclient allows conversion in two simple steps:
1) enter the number of environments to convert
2) provide file paths for all environments to be converted

And that's it. As the output you will get settings.json file which can be placed in .vscode folder without any modifications.