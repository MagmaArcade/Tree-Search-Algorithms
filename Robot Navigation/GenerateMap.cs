using System;
using System.Collections.Generic;
using System.IO;


namespace Robot_Navigation
{
    class GenerateMap
    {
        private string mapSize, initialState, goalState;
        private List<string> walls = new List<string>();
        private List<string> goalStates = new List<string>();


        // Define Readonly Properties
        public List<string> Walls { get { return walls; } }
        public string InitialState { get { return initialState; } }
        public List<string> GoalStates { get { return goalStates; } }
        public string Map { get { return mapSize; } }


        // Initializes variable from data file
        public GenerateMap(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            mapSize = reader.ReadLine();
            initialState = reader.ReadLine();
            goalState = reader.ReadLine();

            var goals = goalState.Split('|');

            foreach (string goal in goals)
            {
                goalStates.Add(goal.Trim());
            }


            while (reader.Peek() >= 0)
            {
               walls.Add(reader.ReadLine());
            }
            reader.Close();
        }


        //Print map info
        public void printInfo()
        {
            Console.WriteLine("Map size: " + mapSize);
            Console.WriteLine("Initial state: " + initialState);
            Console.Write("Goal state: ");
            goalStates.ForEach(goal => Console.Write("{0}, ", goal));
            Console.Write("\n Walls: ");
            walls.ForEach(wall => Console.Write("{0}, ", wall)) ;
        }
    }
}
