SRC_FILES = Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs Frontier.cs 
MAIN_SRC = NPuzzle.cs
TESTS = NPuzzle_Tests.cs Heuristics_Tests.cs NPuzzleUtils_Tests.cs Search_Tests.cs Node_Tests.cs Problem_Tests.cs Frontier_Tests.cs

TEST_FILES = $(addprefix "tests/", $(TESTS))

.PHONY: NPuzzle.cs

all: run

NPuzzle.exe:
	@mcs $(MAIN_SRC) $(SRC_FILES) -pkg:dotnet

clean:
	@rm -f *.exe tests/*.exe

test: tests/NPuzzle_Tests.exe
	@nunit-console tests/NPuzzle_Tests.exe

tests/NPuzzle_Tests.exe:
	@mcs $(TEST_FILES) $(SRC_FILES) -pkg:nunit

test-problem: tests/NPuzzle_TestProblem.exe
	@nunit-console tests/NPuzzle_TestProblem.exe

tests/NPuzzle_TestProblem.exe:
	@mcs NPuzzle_Tests.cs Problem_Tests.cs Problem.cs



run: NPuzzle.exe
	@mono NPuzzle.exe

