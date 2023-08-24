# Tree-Search-Algorithms

Instructions: 
To run the program, navigate to the directory where search.exe file is location – Robot Navigation/Obj/Debug. Then open command prompt from that directory.
Input
Type the command: “Robot Navagation.exe” <path_to_file> <search_strategy> 
Search strategies:
•	BFS
•	DFS
•	AS
•	GBFS
•	RS

Output:
Example Output
TestInput.txt 
BFS: 23 
down; right; right; right; right; up; up; right; right; right;


Once the program is run a command-line UI will be generated for the map. The symbols will indicate the value of each cell: initial state (i), goals (g) and walls (w).



# Search Algorithms: 
1.	Depth-First Search (DFS) is an uninformed search algorithm that explores as far as possible along each branch before backtracking. DFS is easy to implement and memory-efficient, making it useful for large graphs. However, DFS does not guarantee the shortest path to the goal node, and it can get stuck in infinite loops if the graph contains cycles.
2.	Breadth-First Search (BFS) is another uninformed search algorithm that explores all the nodes at the current depth before moving on to the next depth level. BFS guarantees the shortest path to the goal node and is more suitable for finding the optimal solution in unweighted graphs. However, BFS can be memory-intensive and may not work well for large graphs.
3.	Greedy Best-First Search (GBFS) is an informed search algorithm that uses a heuristic to select the best node to explore next. GBFS is faster than BFS and more memory-efficient than A*, but it may not find the optimal solution to the problem.
4.	A* Search, also known as A Star, is an algorithm that considers two factors when determining the optimal path. These factors are the G-Score, which is the path cost between nodes, and H-Score, which is an estimate of the distance to from the current location to the goal. Utilizing these factors, A* can search for the most direct path while minimising the number of nodes that need to be explored. This results in a faster and more efficient system that uses less memory. 
5.	Random Search is a brute-force approach to an uninformed search algorithm that randomly selects a node from the available paths and explores it until it finds the goal node. Random search does not use any heuristics or search strategies, making it simple to implement but not very efficient or effective.

