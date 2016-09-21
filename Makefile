.PHONY: NPuzzle.cs

all: run

NPuzzle.exe:
	@mcs NPuzzle.cs Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs -pkg:dotnet

clean:
	@rm -f *.exe tests/*.exe

test: tests/NPuzzleTests.exe
	#@mono tests/NPuzzleTests.exe
	@nunit-console tests/NPuzzleTests.exe

tests/NPuzzleTests.exe:
	@mcs tests/NPuzzleTests.cs tests/NPuzzleUtils_Test.cs Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs 

run: NPuzzle.exe
	@mono NPuzzle.exe

