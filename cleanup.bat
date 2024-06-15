dotnet clean

FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad obj') DO RMDIR /S /Q %%i
FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad bin') DO RMDIR /S /Q %%i
FOR /f "tokens=*" %%i in ('dir /s /b /o:n /ad .vs') DO RMDIR /S /Q %%i

RMDIR /S /Q obj
RMDIR /S /Q bin
RMDIR /S /Q .vs