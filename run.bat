@echo off
echo [33mCircle App compile[0m
echo [0m[0m
IF EXIST ["./CircleApp.exe"] (
	del "./CircleApp.exe"
)

csc -target:exe -out:CircleApp.exe -recurse:*.cs

echo [32mCircle App run[0m
start /d "." CircleApp.exe