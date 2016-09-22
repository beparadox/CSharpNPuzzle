.PHONY: NPuzzle.cs

all: run

NPuzzle.exe:
	@mcs NPuzzle.cs Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs -pkg:dotnet

clean:
	@rm -f *.exe tests/*.exe

test: tests/NPuzzle_Tests.exe
	#@mono tests/NPuzzle_Tests.exe
	@nunit-console tests/NPuzzle_Tests.exe

tests/NPuzzle_Tests.exe:
	@mcs tests/NPuzzle_Tests.cs tests/NPuzzleUtils_Tests.cs tests/Search_Tests.cs tests/Node_Tests.cs tests/Problem_Tests.cs Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs -pkg:nunit

run: NPuzzle.exe
	@mono NPuzzle.exe

