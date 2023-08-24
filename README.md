<h1>Tree-Search-Algorithms</h1>

<h2>Instructions:</h2>
<p>To run the program, navigate to the directory where the search.exe file is located – Robot Navigation/Obj/Debug. Then open a command prompt from that directory.</p>
<p>Input<br>
Type the command: "Robot Navigation.exe" &lt;path_to_file&gt; &lt;search_strategy&gt;<br>
Search strategies:<br>
• BFS<br>
• DFS<br>
• AS<br>
• GBFS<br>
• RS</p>

<p>Output:<br>
Example Output<br>
TestInput.txt<br>
BFS: 23<br>
down; right; right; right; right; up; up; right; right; right;</p>

<p>Once the program is run, a command-line UI will be generated for the map. The symbols will indicate the value of each cell: initial state (i), goals (g), and walls (w).</p>
<img width="674" alt="UI" src="User Interface.png">

<h2>Search Algorithms:</h2>
<ol>
<li>Depth-First Search (DFS) is an uninformed search algorithm that explores as far as possible along each branch before backtracking. DFS is easy to implement and memory-efficient, making it useful for large graphs. However, DFS does not guarantee the shortest path to the goal node, and it can get stuck in infinite loops if the graph contains cycles.</li>
<li>Breadth-First Search (BFS) is another uninformed search algorithm that explores all the nodes at the current depth before moving on to the next depth level. BFS guarantees the shortest path to the goal node and is more suitable for finding the optimal solution in unweighted graphs. However, BFS can be memory-intensive and may not work well for large graphs.</li>
<li>Greedy Best-First Search (GBFS) is an informed search algorithm that uses a heuristic to select the best node to explore next. GBFS is faster than BFS and more memory-efficient than A*, but it may not find the optimal solution to the problem.</li>
<li>A* Search, also known as A Star, is an algorithm that considers two factors when determining the optimal path. These factors are the G-Score, which is the path cost between nodes, and H-Score, which is an estimate of the distance to from the current location to the goal. Utilizing these factors, A* can search for the most direct path while minimizing the number of nodes that need to be explored. This results in a faster and more efficient system that uses less memory.</li>
<li>Random Search is a brute-force approach to an uninformed search algorithm that randomly selects a node from the available paths and explores it until it finds the goal node. Random search does not use any heuristics or search strategies, making it simple to implement but not very efficient or effective.</li>
</ol>