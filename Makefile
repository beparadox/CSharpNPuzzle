.PHONY: NPuzzle.cs

all: run

NPuzzle.exe:
	@mcs NPuzzle.cs Problem.cs Node.cs NPuzzleUtils.cs Heuristics.cs Search.cs PriorityQueue.cs

clean:
	@rm -f *.exe

run: NPuzzle.exe
	@mono NPuzzle.exe

