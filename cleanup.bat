:: The script removes all obj, bin, .vs directories and all WG.*.xml, *.user files from template folder. 
:: Just go to template folder and run ../../ClearTemplateFolders.bat
FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad obj') DO RMDIR /S /Q %%i
FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad bin') DO RMDIR /S /Q %%i
FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad .vs') DO RMDIR /S /Q %%i
FOR /f "tokens=*" %%i in ('dir /s /b "WG.*.xml"') DO del /f %%i
FOR /f "tokens=*" %%i in ('dir /s /b "*.user"') DO del /f %%i
RMDIR /S /Q obj
RMDIR /S /Q bin
RMDIR /S /Q .vs